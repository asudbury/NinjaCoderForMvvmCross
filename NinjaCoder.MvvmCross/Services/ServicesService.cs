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
    using NinjaCoder.MvvmCross.Infrastructure.Services;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Translators;

    /// <summary>
    ///  Defines the ServicesService type.
    /// </summary>
    public class ServicesService : BaseService, IServicesService
    {
        /// <summary>
        /// The plugin translator.
        /// </summary>
        private readonly ITranslator<FileInfoBase, Plugin> pluginTranslator;

        /// <summary>
        /// The file system.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// The code config service.
        /// </summary>
        private readonly ICodeConfigService codeConfigService;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The snippet service.
        /// </summary>
        private readonly ISnippetService snippetService;

        /// <summary>
        /// The plugins service.
        /// </summary>
        private readonly IPluginsService pluginsService;

        /// <summary>
        /// The message box service.
        /// </summary>
        private readonly IMessageBoxService messageBoxService;

        /// <summary>
        /// The nuget service.
        /// </summary>
        private readonly INugetService nugetService;

        /// <summary>
        /// The template being added.
        /// </summary>
        private string templateName = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesService" /> class.
        /// </summary>
        /// <param name="pluginTranslator">The plugin translator.</param>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="codeConfigService">The code config service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="snippetService">The snippet service.</param>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="nugetService">The nuget service.</param>
        public ServicesService(
            ITranslator<FileInfoBase, Plugin> pluginTranslator,
            IFileSystem fileSystem, 
            ICodeConfigService codeConfigService,
            ISettingsService settingsService,
            ISnippetService snippetService,
            IPluginsService pluginsService,
            IMessageBoxService messageBoxService,
            INugetService nugetService)
        {
            TraceService.WriteLine("ServicesService::Constructor");

            this.pluginTranslator = pluginTranslator;
            this.fileSystem = fileSystem;
            this.codeConfigService = codeConfigService;
            this.settingsService = settingsService;
            this.snippetService = snippetService;
            this.pluginsService = pluginsService;
            this.messageBoxService = messageBoxService;
            this.nugetService = nugetService;
        }

        /// <summary>
        /// Adds the services.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="createUnitTests">if set to <c>true</c> [create unit tests].</param>
        /// <returns> The messages.</returns>
        public IList<string> AddServices(
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
                        this.settingsService.ConfigPath, 
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
            visualStudioService.DTEService.SolutionService.RemoveGlobalVariables();

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
            TraceService.WriteLine("ServicesService::AddServicesToViewModel");

            IProjectItemService testProjectItemService = null;
            IProjectItemService projectItemService = coreProjectService.GetProjectItem(viewModelName);

            if (projectItemService.ProjectItem != null)
            {
                foreach (ItemTemplateInfo itemTemplateInfo in itemTemplateInfos)
                {
                    testProjectItemService = this.CreateSnippet(
                        visualStudioService,
                        viewModelName,
                        this.settingsService.CodeSnippetsPath + @"\Services",
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

            //// put at the top of the stack!
            if (this.settingsService.UseNugetForPlugins)
            {
                IEnumerable<string> messages = this.nugetService.GetInitNugetMessages(NinjaMessages.ServicesViaNuget);
                this.Messages.AddRange(messages);
            }

            string fileName = templateInfo.FriendlyName + ".cs";

            try
            {
                projectService.AddToFolderFromTemplate("Services", templateInfo.FileName, fileName, false);

                string configFile = string.Format(@"{0}\Services\Config.{1}.xml", this.settingsService.ConfigPath, templateInfo.FriendlyName);

                if (this.fileSystem.File.Exists(configFile))
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

            CodeConfig codeConfig = this.codeConfigService.GetCodeConfigFromPath(configFile);

            if (codeConfig != null)
            {
                //// do we have any dependent plugins?
                if (codeConfig.DependentPlugins != null)
                {
                    foreach (string dependentPlugin in codeConfig.DependentPlugins)
                    {
                        List<Plugin> plugins = new List<Plugin>();

                        string source = this.settingsService.InstalledDirectory + @"\Plugins\" + dependentPlugin;

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

                string sourceDirectory = this.settingsService.InstalledDirectory + @"\Plugins\Core";

                //// don't need to add reference if we are going to use nuget.
                if (this.settingsService.UseNugetForServices == false)
                {
                    foreach (string reference in codeConfig.References)
                    {
                        projectService.AddReference(
                            "Lib",
                            projectService.GetProjectPath() + @"\Lib\" + reference,
                            sourceDirectory + @"\" + reference,
                            this.settingsService.IncludeLibFolderInProjects,
                            this.settingsService.CopyAssembliesToLibFolder);
                    }
                }

                //// if we want to add via nuget then generate the command.
                if (this.settingsService.UseNugetForServices || 
                    codeConfig.NugetInstallationMandatory == "Y")
                {
                    ////this.nugetService.UpdateViaNuget(projectService, codeConfig);
                }

                //// apply any code dependencies
                IEnumerable<string> messages = this.codeConfigService.ApplyCodeDependencies(
                    visualStudioService, 
                    codeConfig);

                this.Messages.AddRange(messages);
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

                if (this.settingsService.RemoveDefaultComments)
                {
                    projectItem.RemoveComments();
                }

                if (this.settingsService.RemoveDefaultFileHeaders)
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

            CodeSnippet codeSnippet = this.snippetService.GetSnippet(snippetPath);

            if (codeSnippet != null)
            {
                //// Do some variable substitution!!!!

                if (this.settingsService.ReplaceVariablesInSnippets)
                {
                    this.snippetService.ApplyGlobals(visualStudioService, codeSnippet);
                }

                projectItemService.ImplementCodeSnippet(codeSnippet, this.settingsService.FormatFunctionParameters);

                this.Messages.Add(itemTemplateInfo.FriendlyName + " service code added to " + viewModelName + ".cs in project " + coreProjectService.Name + ".");

                //// do we need to implement any unit tests?
                if (createUnitTests)
                {
                    string testSnippetPath = string.Format(@"{0}\Services.{1}.Tests.xml", codeSnippetsPath, itemTemplateInfo.FriendlyName);

                    IProjectItemService itemService = this.snippetService.CreateUnitTests(
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
    }
}
