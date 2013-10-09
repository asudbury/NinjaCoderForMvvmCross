// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ServicesService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using Constants;
    using Entities;
    using EnvDTE;
    using Interfaces;

    using NinjaCoder.MvvmCross.Exceptions;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Translators;

    /// <summary>
    ///  Defines the ServicesService type.
    /// </summary>
    public class ServicesService : BaseCodeService, IServicesService
    {
        /// <summary>
        /// The plugin translator.
        /// </summary>
        private readonly ITranslator<FileInfoBase, Plugin> pluginTranslator;

        /// <summary>
        /// The plugins service.
        /// </summary>
        private readonly IPluginsService pluginsService;

        /// <summary>
        /// The message box service.
        /// </summary>
        private readonly IMessageBoxService messageBoxService;

        /// <summary>
        /// The template being added.
        /// </summary>
        private string templateName = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesService" /> class.
        /// </summary>
        /// <param name="pluginTranslator">The plugin translator.</param>
        /// <param name="codeConfigTranslator">The code config translator.</param>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="snippetService">The snippet service.</param>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        public ServicesService(
            ITranslator<FileInfoBase, Plugin> pluginTranslator,
            ITranslator<string, CodeConfig> codeConfigTranslator,
            IFileSystem fileSystem, 
            ISettingsService settingsService,
            ISnippetService snippetService,
            IPluginsService pluginsService,
            IMessageBoxService messageBoxService)
            : base(codeConfigTranslator, fileSystem, settingsService, snippetService)
        {
            TraceService.WriteLine("ServicesService::Constructor");

            this.pluginTranslator = pluginTranslator;
            this.pluginsService = pluginsService;
            this.messageBoxService = messageBoxService;
        }

        /// <summary>
        /// Adds the services.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="createUnitTests">if set to <c>true</c> [create unit tests].</param>
        /// <returns> The messages.</returns>
        public List<string> AddServices(
            IVisualStudioService visualStudioService,
            IEnumerable<ItemTemplateInfo> itemTemplateInfos,
            string viewModelName,
            bool createUnitTests)
        {
            TraceService.WriteLine("ServicesService::AddServices");

            //// reset the messages.
            this.Messages = new List<string>();

            IProjectService coreProjectService = visualStudioService.CoreProjectService;

            ProjectItemsEvents cSharpProjectItemsEvents = visualStudioService.DTEService.GetCSharpProjectItemsEvents();
            cSharpProjectItemsEvents.ItemAdded += this.ProjectItemsEventsItemAdded;

            foreach (ItemTemplateInfo itemTemplateInfo in itemTemplateInfos)
            {
                this.templateName = itemTemplateInfo.FriendlyName;

                try
                {
                    this.AddService(
                        visualStudioService,
                        coreProjectService, 
                        this.SettingsService.ConfigPath, 
                        itemTemplateInfo);
                }
                catch (NinjaCoderServiceException exception)
                {
                   this.Messages.Add("Unable to add Services.");

                   string message = string.Format(
                        "{0} {1} {2}", 
                        exception.NinjaMessage, 
                        exception.FolderName, 
                        exception.FileName);
                   
                   this.Messages.Add(message);

                   TraceService.WriteError("ServicesService::AddServices " + message);
                   return this.Messages;
                }
            }
            
            //// do we want to implement in a view model?
            if (string.IsNullOrEmpty(viewModelName) == false)
            {
                this.AddServicesToViewModel(
                    visualStudioService, 
                    itemTemplateInfos, 
                    viewModelName, 
                    createUnitTests, 
                    coreProjectService);

                //// remove event handler.
                cSharpProjectItemsEvents.ItemAdded -= this.ProjectItemsEventsItemAdded;
            }

            //// remove the globals.
            this.RemoveGlobals(visualStudioService);

            return this.Messages;
        }

        /// <summary>
        /// Adds the services to view model.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="createUnitTests">if set to <c>true</c> [create unit tests].</param>
        /// <param name="coreProjectService">The core project service.</param>
        internal void AddServicesToViewModel(
            IVisualStudioService visualStudioService,
            IEnumerable<ItemTemplateInfo> itemTemplateInfos,
            string viewModelName,
            bool createUnitTests,
            IProjectService coreProjectService)
        {
            IProjectItemService testProjectItemService = null;
            IProjectItemService projectItemService = coreProjectService.GetProjectItem(viewModelName);

            if (projectItemService.ProjectItem != null)
            {
                foreach (ItemTemplateInfo itemTemplateInfo in itemTemplateInfos)
                {
                    testProjectItemService = this.CreateSnippet(
                        visualStudioService,
                        viewModelName,
                        this.SettingsService.CodeSnippetsPath + @"\Services",
                        createUnitTests,
                        coreProjectService,
                        projectItemService,
                        itemTemplateInfo);
                }

                projectItemService.FixUsingStatements();

                //// also only do once for the unit test file.
                if (createUnitTests && testProjectItemService != null)
                {
                    testProjectItemService.FixUsingStatements();
                }
            }
        }

        /// <summary>
        /// Adds the service.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="projectService">The project service.</param>
        /// <param name="templatesPath">The templates path.</param>
        /// <param name="templateInfo">The template info.</param>
        /// <returns>True or false.</returns>
        internal bool AddService(
            IVisualStudioService visualStudioService,
            IProjectService projectService,
            string templatesPath,
            ItemTemplateInfo templateInfo)
        {
            TraceService.WriteLine("ServicesService::AddServices adding from template path " + templatesPath + " template=" + templateInfo.FileName);

            this.InitNuget(NinjaMessages.ServicesViaNuget);

            string fileName = templateInfo.FriendlyName + ".cs";

            try
            {
                projectService.AddToFolderFromTemplate("Services", templateInfo.FileName, fileName, false);

                string configFile = string.Format(@"{0}\Services\Config.{1}.xml", this.SettingsService.ConfigPath, templateInfo.FriendlyName);

                if (this.FileSystem.File.Exists(configFile))
                {
                    this.ProcessConfig(
                        visualStudioService,
                        projectService,
                        configFile);
                }

                return true;
            }
            catch (Exception exception)
            {
                string message = "AddToFolderFromTemplate error:-" + exception.Message;

                TraceService.WriteError(message);
                this.messageBoxService.Show(message, "Ninja Code for MvvmCross - Add Service");

                throw new NinjaCoderServiceException
                {
                    NinjaMessage = "Ninja exception :-" + message,
                    FileName = templateInfo.FileName,
                    FolderName = templateInfo.FolderName
                };
            }
        }

        /// <summary>
        /// Processes the config.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="projectService">The project service.</param>
        /// <param name="configFile">The config file.</param>
        internal void ProcessConfig(
            IVisualStudioService visualStudioService,
            IProjectService projectService, 
            string configFile)
        {
            TraceService.WriteLine("ServicesService::ProcessConfig");

            CodeConfig codeConfig = this.Translator.Translate(configFile);

            if (codeConfig != null)
            {
                //// do we have any dependent plugins?
                if (codeConfig.DependentPlugins != null)
                {
                    foreach (string dependentPlugin in codeConfig.DependentPlugins)
                    {
                        List<Plugin> plugins = new List<Plugin>();

                        string source = this.SettingsService.CorePluginsPath + dependentPlugin;

                        //// append dll if its missing.
                        if (source.ToLower().EndsWith(".dll") == false)
                        {
                            source += ".dll";
                        }
                        
                        FileInfo fileInfo = new FileInfo(source);
                        
                        Plugin plugin = this.pluginTranslator.Translate(fileInfo);
                        
                        plugins.Add(plugin);

                        this.pluginsService.AddPlugins(
                            visualStudioService, 
                            plugins, 
                            string.Empty, 
                            false);
                    }
                }

                string sourceDirectory = this.SettingsService.CorePluginsPath;

                //// don't need to add reference if we are going to use nuget.
                if (this.SettingsService.UseNugetForServices == false)
                {
                    foreach (string reference in codeConfig.References)
                    {
                        projectService.AddReference(
                            "Lib",
                            projectService.GetProjectPath() + @"\Lib\" + reference,
                            sourceDirectory + @"\" + reference,
                            this.SettingsService.IncludeLibFolderInProjects);
                    }
                }

                //// if we want to add via nuget then generate the command.
                if (this.SettingsService.UseNugetForServices || 
                    codeConfig.NugetInstallationMandatory == "Y")
                {
                    this.UpdateServiceViaNuget(projectService, codeConfig);
                }

                //// apply any code dependencies
                this.ApplyCodeDependencies(visualStudioService, codeConfig);
            }
        }

        /// <summary>
        /// Applies the code dependencies.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="codeConfig">The code config.</param>
        internal void ApplyCodeDependencies(
            IVisualStudioService visualStudioService,
            CodeConfig codeConfig)
        {
            TraceService.WriteLine("ServicesService::ApplyCodeDependencies");

            //// apply any code dependencies
            if (codeConfig.CodeDependencies != null)
            {
                foreach (CodeSnippet codeSnippet in codeConfig.CodeDependencies)
                {
                    //// find the project
                    IProjectService projectService = visualStudioService.GetProjectServiceBySuffix(codeSnippet.Project);

                    if (projectService != null)
                    {
                        //// find the class
                        IProjectItemService projectItemService = projectService.GetProjectItem(codeSnippet.Class + ".cs");

                        if (projectItemService != null)
                        {
                            //// find the method.
                            CodeFunction codeFunction = projectItemService.GetFirstClass().GetFunction(codeSnippet.Method);

                            if (codeFunction != null)
                            {
                                string code  = codeFunction.GetCode();

                                if (code.Contains(codeSnippet.Code.Trim()) == false)
                                {
                                    codeFunction.InsertCode(codeSnippet.Code, true);

                                    string message = string.Format(
                                        "Code added to project {0} class {1} method {2}.",
                                        projectService.Name,
                                        codeSnippet.Class,
                                        codeSnippet.Method);

                                    this.Messages.Add(message);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Project Item added event handler.
        /// </summary>
        /// <param name="projectItem">The project item.</param>
        internal void ProjectItemsEventsItemAdded(ProjectItem projectItem)
        {
            string message = string.Format(
                "ServicesService::ProjectItemsEventsItemAdded file={0}", 
                projectItem.Name);

            TraceService.WriteLine(message);

            if (projectItem.IsCSharpFile())
            {
                this.Messages.Add(projectItem.GetFolder() + @"\" + projectItem.Name + " added to project " + projectItem.ContainingProject.Name + ".");
            
                //// now we want to amend some of the namespaces!
                //// TODO: this should really be done in the template!
                projectItem.ReplaceText(
                    "MvvmCross." + this.templateName, 
                    projectItem.ContainingProject.Name);

                if (this.SettingsService.RemoveDefaultComments)
                {
                    projectItem.RemoveComments();
                }

                if (this.SettingsService.RemoveDefaultFileHeaders)
                {
                    projectItem.RemoveHeader();
                }
            }
        }

        /// <summary>
        /// Creates the snippet.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="codeSnippetsPath">The code snippets path.</param>
        /// <param name="createUnitTests">if set to <c>true</c> [create unit tests].</param>
        /// <param name="coreProjectService">The core project service.</param>
        /// <param name="projectItemService">The project item service.</param>
        /// <param name="itemTemplateInfo">The item template info.</param>
        /// <returns>The project Item service.</returns>
        internal IProjectItemService CreateSnippet(
            IVisualStudioService visualStudioService,
            string viewModelName,
            string codeSnippetsPath,
            bool createUnitTests,
            IProjectService coreProjectService,
            IProjectItemService projectItemService,
            ItemTemplateInfo itemTemplateInfo)
        {
            TraceService.WriteLine("ServicesService::CreateSnippet service=" + itemTemplateInfo.FriendlyName + " viewModelName=" + viewModelName);

            string snippetPath = string.Format(@"{0}\Services.{1}.xml", codeSnippetsPath, itemTemplateInfo.FriendlyName);
            IProjectItemService testProjectItemService = null;

            CodeSnippet codeSnippet = this.SnippetService.GetSnippet(snippetPath);

            if (codeSnippet != null)
            {
                //// Do some variable substitution!!!!

                if (this.SettingsService.ReplaceVariablesInSnippets)
                {
                    this.ApplyGlobals(visualStudioService, codeSnippet);
                }

                projectItemService.ImplementCodeSnippet(codeSnippet, this.SettingsService.FormatFunctionParameters);

                this.Messages.Add(itemTemplateInfo.FriendlyName + " service code added to " + viewModelName + ".cs in project " + coreProjectService.Name + ".");

                //// do we need to implement any unit tests?
                if (createUnitTests)
                {
                    string testSnippetPath = string.Format(@"{0}\Services.{1}.Tests.xml", codeSnippetsPath, itemTemplateInfo.FriendlyName);

                    IProjectItemService itemService = this.CreateUnitTests(
                            visualStudioService,
                            visualStudioService.CoreTestsProjectService,
                            testSnippetPath,
                            viewModelName,
                            itemTemplateInfo.FriendlyName,
                            string.Empty);

                    //// if we actually create some units tests save the pointer to do
                    //// the sort and remove of the using statements later!
                    if (itemService != null)
                    {
                        testProjectItemService = itemService;
                    }
                }
            }

            return testProjectItemService;
        }

        /// <summary>
        /// Updates the service via nuget.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="codeConfig">The code config.</param>
        /// <returns>True if updated via nuget.</returns>
        internal bool UpdateServiceViaNuget(
            IProjectService projectService,
            CodeConfig codeConfig)
        {
            TraceService.WriteLine("ServicesService::UpdateViaNuget");

            return this.UpdateViaNuget(projectService, codeConfig);
        }
    }
}
