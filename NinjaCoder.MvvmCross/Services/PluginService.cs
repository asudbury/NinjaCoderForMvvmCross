// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System.IO.Abstractions;
    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    /// Defines the PluginService type.
    /// </summary>
    public class PluginService : BaseService, IPluginService
    {
        /// <summary>
        /// The file system.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// The settings service,
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The code config service.
        /// </summary>
        private readonly ICodeConfigService codeConfigService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginService" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="codeConfigService">The code config service.</param>
        public PluginService(
            IFileSystem fileSystem,
            ISettingsService settingsService,
            ICodeConfigService codeConfigService)
        {
            TraceService.WriteLine("PluginService::Constructor");

            this.fileSystem = fileSystem;
            this.settingsService = settingsService;
            this.codeConfigService = codeConfigService;
        }

        /// <summary>
        /// Adds the plugin.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="plugin">The plugin.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="extensionName">Name of the extension.</param>
        /// <returns>True or false.</returns>
        public bool AddPlugin(
            IProjectService projectService,
            Plugin plugin,
            string folderName,
            string extensionName)
        {
            TraceService.WriteLine("PluginService::AddPlugin " + plugin.FriendlyName);

            bool added = false;

            string projectPath = projectService.GetProjectPath();
            string source = plugin.Source;
            string destination = string.Format(@"{0}\Lib\{1}", projectPath, plugin.FileName);

            CodeConfig codeConfig = this.codeConfigService.GetCodeConfig(projectService, plugin.FriendlyName, false);

            //// we need to work out if the user has requested that the plugin is added from nuget
            //// and also if the plugin can be added from nuget - currently not all can!
            bool addPluginFromLocalDisk = this.codeConfigService.UseLocalDiskCopy(codeConfig);

            //// at this moment we only want ot do the core as this plugin might not be
            //// supported in the ui project.
            if (extensionName == Settings.Core ||
                extensionName == Settings.CoreTests)
            {
                added = this.AddCorePlugin(
                    projectService, 
                    plugin, 
                    codeConfig, 
                    addPluginFromLocalDisk, 
                    destination, 
                    source);
            }
            else
            {
                //// now if we are not the core project we need to add the platform specific assemblies
                //// and the bootstrap item templates!

                string extensionSource = this.GetPluginPath(extensionName, source);

                string extensionDestination = this.GetPluginPath(extensionName, destination);

                if (this.fileSystem.File.Exists(extensionSource))
                {
                    //// if the plugin is supported add in the core library.

                    added = this.AddUIPlugin(
                        projectService,
                        plugin.FriendlyName,
                        source,
                        destination,
                        extensionSource,
                        extensionDestination,
                        addPluginFromLocalDisk);
                }
            }

            return added;
        }

        /// <summary>
        /// Adds the project plugin.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="extensionName">Name of the extension.</param>
        /// <param name="plugin">The plugin.</param>
        /// <returns>Returns true if plugin added.</returns>
        public bool AddProjectPlugin(
            IProjectService projectService,
            string folderName,
            string extensionName,
            Plugin plugin)
        {
            TraceService.WriteLine("PluginService::AddProjectPlugin folder=" + folderName);

            this.Messages.Clear();

            string message = string.Format("Ninja Coder is adding {0} plugin to {1} project.", plugin.FriendlyName, projectService.Name);
            projectService.WriteStatusBarMessage(message);

            bool added = this.AddPlugin(projectService, plugin, folderName, extensionName);

            if (added)
            {
                //// if we want to add via nuget then generate the command.
                if (this.settingsService.UseNugetForPlugins)
                {
                    string command = this.GetProjectNugetCommand(projectService, plugin.FriendlyName);

                    if (string.IsNullOrEmpty(command) == false)
                    {
                        plugin.NugetCommands.Add(command);
                    }
                }

                this.Messages.Add(plugin.FriendlyName + " plugin added to project " + projectService.Name + ".");
            }

            return added;
        }

        /// <summary>
        /// Adds the non core plugin.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="extensionSource">The extension source.</param>
        /// <param name="extensionDestination">The extension destination.</param>
        /// <param name="addPluginFromLocalDisk">if set to <c>true</c> [add plugin from local disk].</param>
        /// <returns>True or false.</returns>
        public bool AddUIPlugin(
            IProjectService projectService,
            string friendlyName,
            string source,
            string destination,
            string extensionSource,
            string extensionDestination,
            bool addPluginFromLocalDisk)
        {
            TraceService.WriteLine("PluginService::AddUIPlugin " + friendlyName);

            bool added = false;

            this.codeConfigService.ProcessCodeConfig(
                projectService, 
                friendlyName, 
                extensionSource, 
                extensionDestination);

            //// only do if destination file doesn't exist
            if (this.fileSystem.File.Exists(destination) == false)
            {
                if (addPluginFromLocalDisk)
                {
                    projectService.AddReference(
                        "Lib",
                        destination,
                        source,
                        this.settingsService.IncludeLibFolderInProjects,
                        this.settingsService.CopyAssembliesToLibFolder);
                }
            }

            //// only do if extensionDestination file doesn't exist
            if (this.fileSystem.File.Exists(extensionDestination) == false)
            {
                if (addPluginFromLocalDisk)
                {
                    projectService.AddReference(
                        "Lib",
                        extensionDestination,
                        extensionSource,
                        this.settingsService.IncludeLibFolderInProjects,
                        this.settingsService.CopyAssembliesToLibFolder);
                }

                added = true;

                this.RequestBootstrapFile(
                    projectService, 
                    friendlyName);
            }

            return added;
        }

        /// <summary>
        /// Adds the core.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="plugin">The plugin.</param>
        /// <param name="codeConfig">The code config.</param>
        /// <param name="addPluginFromLocalDisk">if set to <c>true</c> [add plugin from local disk].</param>
        /// <param name="destination">The destination.</param>
        /// <param name="source">The source.</param>
        /// <returns>True or false.</returns>
        internal bool AddCorePlugin(
            IProjectService projectService,
            Plugin plugin,
            CodeConfig codeConfig,
            bool addPluginFromLocalDisk,
            string destination,
            string source)
        {
            //// inform the user that we cant install from nuget.
            if (this.codeConfigService.NugetRequestedAndNotSupported(codeConfig))
            {
                this.Messages.Add(
                    plugin.FriendlyName
                    + " plugin does not support being installed via Nuget and has been installed from the local machine.");
            }

            //// don't need to add reference if we are going to use nuget.
            if (addPluginFromLocalDisk)
            {
                //// only do if destination file doesn't exist
                if (this.fileSystem.File.Exists(destination) == false)
                {
                    projectService.AddReference(
                        "Lib",
                        destination,
                        source,
                        this.settingsService.IncludeLibFolderInProjects,
                        this.settingsService.CopyAssembliesToLibFolder);
                }
            }

            return true;
        }

        /// <summary>
        /// Updates the plugin via nuget.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <returns>True if updated via nuget.</returns>
        internal string GetProjectNugetCommand(
            IProjectService projectService,
            string friendlyName)
        {
            TraceService.WriteLine("PluginService::UpdateViaNuget friendlyName=" + friendlyName);

            CodeConfig codeConfig = this.codeConfigService.GetCodeConfig(projectService, friendlyName, false);

            return this.codeConfigService.GetProjectNugetCommand(codeConfig, projectService);
        }

        /// <summary>
        /// Requests the bootstrap file.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        internal void RequestBootstrapFile(
            IProjectService projectService,
            string friendlyName)
        {
            TraceService.WriteLine("PluginService::RequestBootstrapFile name=" + friendlyName);

            CodeConfig codeConfig = this.codeConfigService.GetCodeConfig(projectService, friendlyName, false);

            string bootstrapFileName = this.codeConfigService.GetBootstrapFileName(codeConfig, friendlyName);

            //// check if the file already exists

            IProjectItemService projectItemService = projectService.GetProjectItem(bootstrapFileName);

            if (projectItemService == null)
            {
                //// get the currently requested plugins.
                string currentPlugins = this.settingsService.PluginsToAdd;

                string newPlugins = currentPlugins + "+" + bootstrapFileName;

                //// and now save the new requested plugins.
                this.settingsService.PluginsToAdd = newPlugins;
                
                TraceService.WriteLine("***plugins for" + projectService.Name + " " + newPlugins);
            }
        }

        /// <summary>
        /// Gets the plugin path.
        /// </summary>
        /// <param name="extensionName">Name of the extension.</param>
        /// <param name="path">The path.</param>
        /// <returns>The plugin path.</returns>
        internal string GetPluginPath(
            string extensionName,
            string path)
        {
            return path.Replace(".dll", string.Format(".{0}.dll", extensionName));
        }
    }
}
