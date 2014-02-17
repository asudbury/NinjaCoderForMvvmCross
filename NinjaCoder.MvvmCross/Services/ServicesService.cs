// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ServicesService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Constants;
    using Entities;
    using EnvDTE;
    using Interfaces;

    using MahApps.Metro;

    using NinjaCoder.MvvmCross.Exceptions;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Infrastructure.Services;

    using Scorchio.Infrastructure.Services;
    using Scorchio.VisualStudio.Entities;

    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the ServicesService type.
    /// </summary>
    internal class ServicesService : BaseService, IServicesService
    {
        /// <summary>
        /// The code config factory.
        /// </summary>
        private readonly ICodeConfigFactory codeConfigFactory;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The message box service.
        /// </summary>
        private readonly IMessageBoxService messageBoxService;

        /// <summary>
        /// The nuget service.
        /// </summary>
        private readonly INugetService nugetService;

        /// <summary>
        /// The plugin factory.
        /// </summary>
        private readonly IPluginFactory pluginFactory;

        /// <summary>
        /// The code snippet factory.
        /// </summary>
        private readonly ICodeSnippetFactory codeSnippetFactory;

        /// <summary>
        /// The code snippet service.
        /// </summary>
        private ICodeSnippetService codeSnippetService;

        /// <summary>
        /// The plugins service.
        /// </summary>
        private IPluginsService pluginsService;

        /// <summary>
        /// The code config service.
        /// </summary>
        private ICodeConfigService codeConfigService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesService" /> class.
        /// </summary>
        /// <param name="codeConfigFactory">The code config factory.</param>
        /// <param name="codeSnippetFactory">The code snippet factory.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="nugetService">The nuget service.</param>
        /// <param name="pluginFactory">The plugin factory.</param>
        public ServicesService(
            ICodeConfigFactory codeConfigFactory,
            ICodeSnippetFactory codeSnippetFactory,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            INugetService nugetService,
            IPluginFactory pluginFactory)
        {
            TraceService.WriteLine("ServicesService::Constructor");

            this.codeConfigFactory = codeConfigFactory;
            this.settingsService = settingsService;
            this.messageBoxService = messageBoxService;
            this.nugetService = nugetService;
            this.pluginFactory = pluginFactory;
            this.codeSnippetFactory = codeSnippetFactory;

            this.Init();
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        public void Init()
        {
            TraceService.WriteLine("ServicesService::Init");

            this.pluginsService = pluginFactory.GetPluginsService();
            this.codeSnippetService = codeSnippetFactory.GetCodeSnippetService();
            this.codeConfigService = codeConfigFactory.GetCodeConfigService();
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

            if (coreProjectService != null)
            {
                ProjectItemsEvents cSharpProjectItemsEvents = visualStudioService.DTEService.GetCSharpProjectItemsEvents();

                if (cSharpProjectItemsEvents != null)
                {
                    cSharpProjectItemsEvents.ItemAdded += this.ProjectItemsEventsItemAdded;
                }

                foreach (ItemTemplateInfo itemTemplateInfo in itemTemplateInfos)
                {
                    try
                    {
                        this.AddService(
                            visualStudioService,
                            coreProjectService,
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
                }

                //// remove event handler.

                if (cSharpProjectItemsEvents != null)
                {
                    cSharpProjectItemsEvents.ItemAdded -= this.ProjectItemsEventsItemAdded;
                }
            }

            return this.Messages;
        }

        /// <summary>
        /// Adds the service.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="projectService">The project service.</param>
        /// <param name="templateInfo">The template info.</param>
        /// <returns>True or false.</returns>
        internal bool AddService(
            IVisualStudioService visualStudioService,
            IProjectService projectService,
            ItemTemplateInfo templateInfo)
        {
            TraceService.WriteLine("ServicesService::AddServices");

            //// put at the top of the stack!
            if (this.settingsService.UseNugetForPlugins)
            {
                IEnumerable<string> messages = this.nugetService.GetInitNugetMessages(NinjaMessages.ServicesViaNuget);
                this.Messages.AddRange(messages);
            }

            string fileName = templateInfo.FriendlyName + ".cs";

            try
            {
                projectService.AddToFolderFromTemplate(templateInfo.FileName, fileName);

                CodeConfig codeConfig = this.codeConfigFactory.GetServiceConfig(templateInfo.FriendlyName);

                if (codeConfig != null)
                {
                    this.ProcessConfig(
                        visualStudioService,
                        projectService,
                        codeConfig);
                }

                return true;
            }
            catch (Exception exception)
            {
                string message = "AddToFolderFromTemplate error:-" + exception.Message;

                TraceService.WriteError(message);
                this.messageBoxService.Show(
                    message,
                    "Ninja Code for MvvmCross - Add Service",
                    this.settingsService.BetaTesting,
                    Theme.Light,
                    this.settingsService.ThemeColor);

                throw new NinjaCoderServiceException
                {
                    NinjaMessage = "Ninja exception :-" + message,
                    FileName = templateInfo.FileName,
                };
            }
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

            IProjectItemService projectItemService = coreProjectService.GetProjectItem(viewModelName);

            if (projectItemService != null)
            {
                itemTemplateInfos.ToList()
                    .ForEach(x => this.ImplementCodeSnippet(
                        visualStudioService,
                        viewModelName,
                        coreProjectService,
                        projectItemService,
                        x));

                if (createUnitTests)
                {
                    itemTemplateInfos.ToList()
                        .ForEach(x => this.ImplementTestCodeSnippet(
                            visualStudioService, 
                            viewModelName, 
                            x));
                }

                projectItemService.FixUsingStatements();
            }
        }

        /// <summary>
        /// Processes the config.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="projectService">The project service.</param>
        /// <param name="codeConfig">The code config.</param>
        internal void ProcessConfig(
            IVisualStudioService visualStudioService,
            IProjectService projectService, 
            CodeConfig codeConfig)
        {
            TraceService.WriteLine("ServicesService::ProcessConfig");

            if (codeConfig != null)
            {
                //// do we have any dependent plugins?
                codeConfig.DependentPlugins.ForEach(x => this.AddDependantPlugin(visualStudioService, x));

                string sourceDirectory = this.settingsService.MvvmCrossAssembliesPath;

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
        /// Adds the dependant plugin.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="dependentPlugin">The dependent plugin.</param>
        internal void AddDependantPlugin(
            IVisualStudioService visualStudioService, 
            string dependentPlugin)
        {
            List<Plugin> plugins = new List<Plugin>();

            Plugin plugin = this.pluginFactory.GetPluginByName(dependentPlugin);

            plugins.Add(plugin);

            this.pluginsService.AddPlugins(
                visualStudioService,
                plugins,
                string.Empty,
                false,
                this.settingsService.UseNugetForServices);
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

            ProjectItemService projectItemService = new ProjectItemService(projectItem);

            this.OnFileAddedToProject(projectItemService);
        }

        /// <summary>
        /// Called when [file added to project].
        /// </summary>
        /// <param name="projectItemService">The project item service.</param>
        internal void OnFileAddedToProject(IProjectItemService projectItemService)
        {
            if (projectItemService.IsCSharpFile())
            {
                this.Messages.Add(projectItemService.GetFolder() + @"\" + projectItemService.Name + " added to project " + projectItemService.ContainingProjectService.Name + ".");

                if (this.settingsService.RemoveDefaultComments)
                {
                    projectItemService.RemoveComments();
                }

                if (this.settingsService.RemoveDefaultFileHeaders)
                {
                    projectItemService.RemoveHeader();
                }
            }
        }

        /// <summary>
        /// Implements the code snippet.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="coreProjectService">The core project service.</param>
        /// <param name="projectItemService">The project item service.</param>
        /// <param name="itemTemplateInfo">The item template info.</param>
        /// <returns>The project Item service.</returns>
        internal void ImplementCodeSnippet(
            IVisualStudioService visualStudioService,
            string viewModelName,
            IProjectService coreProjectService,
            IProjectItemService projectItemService,
            ItemTemplateInfo itemTemplateInfo)
        {
            TraceService.WriteLine("ServicesService::CreateSnippet service=" + itemTemplateInfo.FriendlyName + " viewModelName=" + viewModelName);

            CodeSnippet codeSnippet = this.codeSnippetFactory.GetServiceSnippet(itemTemplateInfo.FriendlyName);

            if (codeSnippet != null)
            {
                projectItemService.ImplementCodeSnippet(
                    codeSnippet, 
                    this.settingsService.FormatFunctionParameters);

                this.Messages.Add(itemTemplateInfo.FriendlyName + " service code added to " + viewModelName + ".cs in project " + coreProjectService.Name + ".");
            }
        }

        /// <summary>
        /// Implements the test code snippet.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="itemTemplateInfo">The item template info.</param>
        internal void ImplementTestCodeSnippet(
            IVisualStudioService visualStudioService,
            string viewModelName,
            ItemTemplateInfo itemTemplateInfo)
        {
            CodeSnippet testCodeSnippet = this.codeSnippetFactory.GetServiceTestSnippet(itemTemplateInfo.FriendlyName);

            IProjectItemService projectItemService =  this.codeSnippetService.CreateUnitTests(
                visualStudioService,
                visualStudioService.CoreTestsProjectService,
                testCodeSnippet,
                viewModelName,
                itemTemplateInfo.FriendlyName,
                string.Empty);

            projectItemService.FixUsingStatements();
        }
    }
}
