// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Constants;
    using Entities;
    using Interfaces;

    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Infrastructure.Services;

    using Scorchio.Infrastructure.Services.Testing.Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    /// Defines the PluginsService type.
    /// </summary>
    internal class PluginsService : BaseService, IPluginsService
    {
        /// <summary>
        /// The plugin service.
        /// </summary>
        private readonly IPluginService pluginService;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The code snippet service.
        /// </summary>
        private readonly ICodeSnippetService codeSnippetService;

        /// <summary>
        /// The nuget service.
        /// </summary>
        private readonly INugetService nugetService;

        /// <summary>
        /// The code snippet factory.
        /// </summary>
        private readonly ICodeSnippetFactory codeSnippetFactory;

        /// <summary>
        /// The testing service.
        /// </summary>
        private readonly ITestingService testingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsService" /> class.
        /// </summary>
        /// <param name="pluginService">The plugin service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="nugetService">The nuget service.</param>
        /// <param name="codeSnippetFactory">The code snippet factory.</param>
        /// <param name="testingServiceFactory">The testing service factory.</param>
        public PluginsService(
            IPluginService pluginService,
            ISettingsService settingsService,
            INugetService nugetService,
            ICodeSnippetFactory codeSnippetFactory,
            ITestingServiceFactory testingServiceFactory)
        {
            TraceService.WriteLine("PluginsService::Constructor");

            this.pluginService = pluginService;
            this.settingsService = settingsService;
            this.nugetService = nugetService;
            this.codeSnippetFactory = codeSnippetFactory;

            this.testingService = testingServiceFactory.GetTestingService();
            this.codeSnippetService = codeSnippetFactory.GetCodeSnippetService();
        }

        /// <summary>
        /// Adds the plugins.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="plugins">The plugins.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="createUnitTests">if set to <c>true</c> [create unit tests].</param>
        /// <param name="useNuget">if set to <c>true</c> [use nuget].</param>
        /// <returns>The messages.</returns>
        public IEnumerable<string> AddPlugins(
            IVisualStudioService visualStudioService,
            IEnumerable<Plugin> plugins,
            string viewModelName,
            bool createUnitTests,
            bool useNuget)
        {
            TraceService.WriteLine("PluginsService::AddPlugins viewModelName=" + viewModelName);

            this.Messages.Clear();

            //// put at the top of the stack!
            if (useNuget)
            {
                IEnumerable<string> messages = this.nugetService.GetInitNugetMessages(NinjaMessages.PluginsViaNuget);
                this.Messages.AddRange(messages);
            }

            IProjectService coreProjectService = visualStudioService.CoreProjectService;

            Plugin[] enumerablePlugins = plugins as Plugin[] ?? plugins.ToArray();
            
            this.AddProjectPlugins(
                coreProjectService,
                enumerablePlugins,
                Settings.Core, 
                Settings.Core,
                false);

            this.AddProjectPlugins(
                 visualStudioService.CoreTestsProjectService,
                enumerablePlugins,
                Settings.CoreTests,
                Settings.CoreTests,
                false);

            this.AddProjectPlugins(
                visualStudioService.DroidProjectService, 
                enumerablePlugins,
                Settings.Droid,
                Settings.Droid,
                true);
            
            this.AddProjectPlugins(
                visualStudioService.iOSProjectService, 
                enumerablePlugins,
                Settings.iOS,
                Settings.Touch,
                true);
            
            this.AddProjectPlugins(
                visualStudioService.WindowsPhoneProjectService, 
                enumerablePlugins, 
                Settings.WindowsPhone, 
                Settings.WindowsPhone,
                true);
            
            this.AddProjectPlugins(
                visualStudioService.WindowsStoreProjectService, 
                enumerablePlugins, 
                Settings.WindowsStore, 
                Settings.WindowsStore,
                true);
            
            this.AddProjectPlugins(
                visualStudioService.WpfProjectService, 
                enumerablePlugins, 
                Settings.Wpf, 
                Settings.Wpf,
                true);

            //// reset the active project!
            this.settingsService.ActiveProject = string.Empty;

            if (string.IsNullOrEmpty(viewModelName) == false)
            {
                this.AddToViewModel(visualStudioService, viewModelName, createUnitTests, coreProjectService, enumerablePlugins);
            }

            return this.Messages;
        }

        /// <summary>
        /// Adds the project plugins.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="plugins">The plugins.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="extensionName">Name of the extension.</param>
        /// <param name="addTemplates">if set to <c>true</c> [add templates].</param>
        /// <returns>The added plugins.</returns>
        public IEnumerable<Plugin> AddProjectPlugins(
            IProjectService projectService,
            IEnumerable<Plugin> plugins,
            string folderName,
            string extensionName,
            bool addTemplates)
        {
            TraceService.WriteLine("PluginsService::AddProjectPlugins folder=" + folderName);

            List<Plugin> addedPlugins = new List<Plugin>();

            if (projectService != null)
            {
                this.settingsService.PluginsToAdd = string.Empty;

                //// set the project to be the active project!
                this.settingsService.ActiveProject = projectService.Name;

                foreach (Plugin plugin in plugins)
                {
                    bool added = this.pluginService.AddProjectPlugin(
                                                            projectService, 
                                                            folderName, 
                                                            extensionName, 
                                                            plugin);

                    //// we keep a list of plugins that really got added 
                    //// assume if we have a nuget command they will be added via nuget!
                    if (added || plugin.NugetCommands.Any())
                    {
                        addedPlugins.Add(plugin);

                        this.Messages.AddRange(this.pluginService.Messages);
                    }
                }

                if (addTemplates)
                {
                    projectService.AddToFolderFromTemplate("MvvmCross.Plugin.zip", "Ninja");
                }
            }

            return addedPlugins;
        }

        /// <summary>
        /// Adds to view model.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="createUnitTests">if set to <c>true</c> [create unit tests].</param>
        /// <param name="coreProjectService">The core project service.</param>
        /// <param name="enumerablePlugins">The enumerable plugins.</param>
        internal void AddToViewModel(
            IVisualStudioService visualStudioService, 
            string viewModelName, 
            bool createUnitTests, 
            IProjectService coreProjectService, 
            IEnumerable<Plugin> enumerablePlugins)
        {
            TraceService.WriteLine("PluginsService::AddToViewModel viewModelName=" + viewModelName);
            
            IProjectItemService testProjectItemService = null;
            IProjectItemService projectItemService = coreProjectService.GetProjectItem(viewModelName);

            if (projectItemService.ProjectItem != null)
            {
                foreach (Plugin plugin in enumerablePlugins)
                {
                    testProjectItemService = this.CreateSnippet(
                        visualStudioService,
                        viewModelName,
                        createUnitTests,
                        coreProjectService.Name,
                        projectItemService,
                        plugin);
                }

                if (this.settingsService.UseNugetForPlugins == false)
                {
                    projectItemService.FixUsingStatements();
                }
                else
                {
                    projectItemService.MoveUsingStatements();
                }

                projectItemService.RemoveDoubleBlankLines();

                //// also only do once for the unit test file.
                if (createUnitTests && testProjectItemService != null)
                {
                    if (this.settingsService.UseNugetForPlugins == false)
                    {
                        testProjectItemService.FixUsingStatements();
                    }
                    else
                    {
                        testProjectItemService.MoveUsingStatements();
                    }

                    //// we may need to update the testing method attribute.
                    this.testingService.UpdateTestMethodAttribute(testProjectItemService);
                }
            }
        }

        /// <summary>
        /// Creates the snippet.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="createUnitTests">if set to <c>true</c> [create unit tests].</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="projectItemService">The project item service.</param>
        /// <param name="plugin">The plugin.</param>
        /// <returns>
        /// The project item service interface..
        /// </returns>
        internal IProjectItemService CreateSnippet(
            IVisualStudioService visualStudioService, 
            string viewModelName, 
            bool createUnitTests, 
            string projectName, 
            IProjectItemService projectItemService, 
            Plugin plugin)
        {
            TraceService.WriteLine("PluginsService::CreateSnippet plugin=" + plugin.FriendlyName + " viewModelName=" + viewModelName);

            IProjectItemService testProjectItemService = null;

            CodeSnippet codeSnippet = this.codeSnippetFactory.GetPluginSnippet(plugin);

            if (codeSnippet != null)
            {
                //// add in the reference to the plugin - doing this way means we don't need it in the xml files
                codeSnippet.UsingStatements.Add(Path.GetFileNameWithoutExtension(plugin.FileName));

                projectItemService.ImplementCodeSnippet(
                    codeSnippet,
                    this.settingsService.FormatFunctionParameters);

                this.Messages.Add(plugin.FriendlyName + " plugin code added to " + viewModelName + ".cs in project " + projectName + ".");

                //// do we need to implement any unit tests?
                if (createUnitTests)
                {
                    IProjectItemService itemService = this.CreateUnitTests(
                        visualStudioService,
                        visualStudioService.CoreTestsProjectService,
                        plugin,
                        viewModelName);

                    this.Messages.Add(plugin.FriendlyName + " test plugin code added to Test" + viewModelName + ".cs in project " + visualStudioService.CoreTestsProjectService.Name + ".");

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
        /// Creates the unit tests.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="projectService">The project service.</param>
        /// <param name="plugin">The plugin.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <returns>The project item service.</returns>
        internal IProjectItemService CreateUnitTests(
            IVisualStudioService visualStudioService,
            IProjectService projectService,
            Plugin plugin,
            string viewModelName)
        {
            TraceService.WriteLine("PluginsService::CreateUnitTests viewModelName=" + viewModelName);

            this.pluginService.AddPlugin(projectService, plugin, Settings.CoreTests);

            CodeSnippet codeSnippet = this.codeSnippetFactory.GetPluginTestSnippet(plugin);

            return this.codeSnippetService.CreateUnitTests(
                visualStudioService,
                projectService,
                codeSnippet,
                viewModelName,
                plugin.FriendlyName,
                Path.GetFileNameWithoutExtension(plugin.FileName));
        }
    }
}
