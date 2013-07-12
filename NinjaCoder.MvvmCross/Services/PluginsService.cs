// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using System.Linq;
    using Constants;
    using Entities;
    using Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    /// Defines the PluginsService type.
    /// </summary>
    public class PluginsService : BaseService, IPluginsService
    {
        /// <summary>
        /// The file system.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The snippet service.
        /// </summary>
        private readonly ISnippetService snippetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsService" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="snippetService">The snippet service.</param>
        public PluginsService(
            IFileSystem fileSystem,
            ISettingsService settingsService,
            ISnippetService snippetService)
        {
            TraceService.WriteLine("PluginsService::Constructor");

            this.fileSystem = fileSystem;
            this.settingsService = settingsService;
            this.snippetService = snippetService;
        }

        /// <summary>
        /// Adds the plugins.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="plugins">The plugins.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="createUnitTests">if set to <c>true</c> [create unit tests].</param>
        /// <returns>
        /// The messages.
        /// </returns>
        public IEnumerable<string> AddPlugins(
            IVisualStudioService visualStudioService,
            IEnumerable<Plugin> plugins,
            string viewModelName,
            bool createUnitTests)
        {
            TraceService.WriteLine("PluginsService::AddPlugins viewModelName=" + viewModelName);

            IProjectService coreProjectService = visualStudioService.CoreProjectService;

            Plugin[] enumerablePlugins = plugins as Plugin[] ?? plugins.ToArray();

            this.AddProjectPlugins(coreProjectService, enumerablePlugins, Settings.Core, Settings.Core);
            this.AddProjectPlugins(visualStudioService.DroidProjectService, enumerablePlugins, Settings.Droid, Settings.Droid);
            this.AddProjectPlugins(visualStudioService.iOSProjectService, enumerablePlugins, Settings.iOS, "Touch");
            this.AddProjectPlugins(visualStudioService.WindowsPhoneProjectService, enumerablePlugins, Settings.WindowsPhone, Settings.WindowsPhone);
            this.AddProjectPlugins(visualStudioService.WindowsStoreProjectService, enumerablePlugins, Settings.WindowsStore, Settings.WindowsStore);
            this.AddProjectPlugins(visualStudioService.WpfProjectService, enumerablePlugins, Settings.Wpf, Settings.Wpf);

            if (string.IsNullOrEmpty(viewModelName) == false)
            {
                IProjectItemService testProjectItemService = null;
                IProjectItemService projectItemService = coreProjectService.GetProjectItem(viewModelName);

                if (projectItemService.ProjectItem != null)
                {
                    foreach (Plugin plugin in enumerablePlugins)
                    {
                        testProjectItemService = this.CreateSnippet(
                            visualStudioService,
                            viewModelName,
                            this.settingsService.CodeSnippetsPath + @"\Plugins",
                            createUnitTests,
                            coreProjectService,
                            projectItemService,
                            plugin);
                    }
                    
                    projectItemService.ProjectItem.FixUsingStatements();

                    //// also only do once for the unit test file.
                    if (createUnitTests && testProjectItemService != null)
                    {
                        testProjectItemService.ProjectItem.FixUsingStatements();
                    }
                }
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
        public void AddProjectPlugins(
            IProjectService projectService,
            IEnumerable<Plugin> plugins,
            string folderName,
            string extensionName)
        {
            TraceService.WriteLine("PluginsService::AddProjectPlugins folder=" + folderName);

            if (projectService.Project != null)
            {
                foreach (Plugin plugin in plugins)
                {
                    string message = string.Format("Ninja Coder is adding {0} plugin to {1} project.", plugin.FriendlyName, projectService.Name);
                    projectService.WriteStatusBarMessage(message);

                    this.AddPlugin(projectService, plugin, folderName, extensionName);
                    this.Messages.Add(plugin.FriendlyName + " plugin added to project " + projectService.Name + ".");
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
        /// <param name="plugin">The plugin.</param>
        internal IProjectItemService CreateSnippet(
            IVisualStudioService visualStudioService, 
            string viewModelName, 
            string codeSnippetsPath, 
            bool createUnitTests, 
            IProjectService coreProjectService, 
            IProjectItemService projectItemService, 
            Plugin plugin)
        {
            TraceService.WriteLine("PluginsService::CreateSnippet plugin=" + plugin.FriendlyName + " viewModelName=" + viewModelName);

            string snippetPath = string.Format(@"{0}\Plugins.{1}.xml", codeSnippetsPath, plugin.FriendlyName);
            IProjectItemService testProjectItemService = null;

            CodeSnippet codeSnippet = this.snippetService.GetSnippet(snippetPath);

            if (codeSnippet != null)
            {
                //// add in the reference to the plugin - doing this way means we don't need it in the xml files
                codeSnippet.UsingStatements.Add(Path.GetFileNameWithoutExtension(plugin.FileName));

                projectItemService.ImplementCodeSnippet(codeSnippet);

                this.Messages.Add(plugin.FriendlyName + " plugin code added to " + viewModelName + ".cs in project " + coreProjectService.Name + ".");

                //// do we need to implement any unit tests?
                if (createUnitTests)
                {
                    IProjectItemService itemService = this.CreateUnitTests(
                        visualStudioService.CoreTestsProjectService,
                        plugin,
                        codeSnippetsPath,
                        viewModelName);

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
        /// <param name="projectService">The project service.</param>
        /// <param name="plugin">The plugin.</param>
        /// <param name="codeSnippetsPath">The code snippets path.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <returns>The project item service.</returns>
        internal IProjectItemService CreateUnitTests(
            IProjectService projectService,
            Plugin plugin,
            string codeSnippetsPath, 
            string viewModelName)
        {
            TraceService.WriteLine("PluginsService::CreateUnitTests viewModelName=" + viewModelName);

            string testSnippetPath = string.Format(@"{0}\Plugins.{1}.Tests.xml", codeSnippetsPath, plugin.FriendlyName);

            this.AddPlugin(projectService, plugin, string.Empty, Settings.CoreTests);

            return base.CreateUnitTests(
                this.snippetService, 
                projectService, 
                testSnippetPath, 
                viewModelName, 
                plugin.FriendlyName,
                Path.GetFileNameWithoutExtension(plugin.FileName));
        }

        /// <summary>
        /// Adds the plugin.
        /// </summary>)
        /// <param name="projectService">The project service.</param>
        /// <param name="plugin">The plugin.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="extensionName">Name of the extension.</param>
        internal void AddPlugin(
            IProjectService projectService,
            Plugin plugin,
            string folderName,
            string extensionName)
        {
            TraceService.WriteLine("PluginsService::AddPlugin " + plugin.FriendlyName);

            string projectPath = projectService.GetProjectPath();
            string source = plugin.Source;
            string destination = string.Format(@"{0}\Lib\{1}", projectPath, plugin.FileName);

            //// at this moment we only want ot do the core as this plugin might not be
            //// supported in the ui project.
            if (extensionName == Settings.Core)
            {
                projectService.AddReference(
                    "Lib", 
                    destination, 
                    source,
                    this.settingsService.IncludeLibFolderInProjects);
            }
            else if (extensionName == Settings.CoreTests)
            {
                projectService.AddReference(
                    "Lib", 
                    destination, 
                    source, 
                    this.settingsService.IncludeLibFolderInProjects);
            }
            else
            {
                //// now if we are not the core project we need to add the platform specific assemblies
                //// and the bootstrap item templates!

                string extensionSource = this.GetExtensionSource(folderName, extensionName, source);

                string extensionDestination = this.GetExtensionDestination(folderName, extensionName, destination);

                if (this.fileSystem.File.Exists(extensionSource))
                {
                    //// if the plugin is supported add in the core library.

                    this.AddNonCorePlugin(
                        projectService,
                        plugin,
                        source,
                        destination,
                        extensionSource,
                        extensionDestination);
                }
            }
        }

        /// <summary>
        /// Adds the non core plugin.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="plugin">The plugin.</param>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="extensionSource">The extension source.</param>
        /// <param name="extensionDestination">The extension destination.</param>
        internal void AddNonCorePlugin(
            IProjectService projectService,
            Plugin plugin,
            string source,
            string destination,
            string extensionSource,
            string extensionDestination)
        {
            TraceService.WriteLine("PluginsService::AddNonCorePlugin " + plugin.FriendlyName);

            //// only do if destination file doesn't exist
            if (this.fileSystem.File.Exists(destination) == false)
            {
                projectService.AddReference(
                    "Lib", 
                    destination, 
                    source, 
                    this.settingsService.IncludeLibFolderInProjects);
            }

            //// only do if extensionDestination file doesn't exist
            if (this.fileSystem.File.Exists(extensionDestination) == false)
            {
                projectService.AddReference(
                    "Lib", 
                    extensionDestination, 
                    extensionSource,
                    this.settingsService.IncludeLibFolderInProjects);

                this.BuildSourceFile(projectService, extensionSource, extensionDestination, plugin.FriendlyName);
            }
        }

        /// <summary>
        /// Gets the extension destination.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="extensionName">Name of the extension.</param>
        /// <param name="destination">The destination.</param>
        /// <returns>The extension destination.</returns>
        internal string GetExtensionDestination(
            string folderName,
            string extensionName,
            string destination)
        {
            string extensionDestination = destination.Replace(@"\Core\", string.Format(@"\{0}\", folderName));
            extensionDestination = extensionDestination.Replace(".dll", string.Format(".{0}.dll", extensionName));
            return extensionDestination;
        }

        /// <summary>
        /// Gets the extension source.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="extensionName">Name of the extension.</param>
        /// <param name="source">The source.</param>
        /// <returns>The extension source.</returns>
        internal string GetExtensionSource(
            string folderName,
            string extensionName,
            string source)
        {
            string extensionSource = source.Replace(@"\Core\", string.Format(@"\{0}\", folderName));
            extensionSource = extensionSource.Replace(".dll", string.Format(".{0}.dll", extensionName));
            return extensionSource;
        }

        /// <summary>
        /// Builds the source file.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="extensionSource">The extensionSource.</param>
        /// <param name="extensionDestination">The extension destination.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        internal void BuildSourceFile(
            IProjectService projectService,
            string extensionSource,
            string extensionDestination,
            string friendlyName)
        {
            TraceService.WriteLine("PluginsService::BuildSourceFile " + friendlyName);

            try
            {
                string message = string.Format("BuildSourceFile Project Name={0} friendlyName={1}", projectService.Name, friendlyName);

                TraceService.WriteLine(message);

                string sourceFile = friendlyName + "PluginBootstrap.cs";

                //// now we need to sort out the item template!
                projectService.AddToFolderFromTemplate("Bootstrap", "MvvmCross.Plugin.zip", sourceFile, false);

                this.Messages.Add(string.Format(@"Bootstrap\{0} added to {1} project.", sourceFile, projectService.Name));

                IProjectItemService projectItemService = projectService.GetProjectItem(sourceFile);

                //// if we find the project item replace the text in it.
                
                if (projectItemService.ProjectItem != null)
                {
                    //// fix ups!

                    projectItemService.ReplaceText("All", friendlyName);
                    projectItemService.ReplaceText("CoreTemplates.Bootstrap", projectService.Name + ".Bootstrap");
                    projectItemService.ReplaceText("class Plugin", "class " + friendlyName + "PluginBootstrap");
                }
            }
            catch (Exception exception)
            {
                TraceService.WriteError("BuildSourceFile " + exception.Message);
            }
        }
    }
}
