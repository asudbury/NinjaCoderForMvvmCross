// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Entities;

    using Interfaces;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System.Linq;

    /// <summary>
    /// Defines the PluginService type.
    /// </summary>
    public class PluginService : BaseService, IPluginService
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginService" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public PluginService(ISettingsService settingsService)
        {
            TraceService.WriteLine("PluginService::Constructor");

            this.settingsService = settingsService;
        }

        /// <summary>
        /// Adds the project plugin.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="plugin">The plugin.</param>
        public void AddProjectPlugin(
            IProjectService projectService,
            Plugin plugin)
        {
            TraceService.WriteLine("PluginService::AddProjectPlugin plugin=" + plugin.FriendlyName);

            string suffix = projectService.Name.Split('.')[1];

            if (plugin.Platforms.Contains(suffix))
            {
                this.RequestBootstrapFile(projectService, plugin);
            }
        }
        
        /// <summary>
        /// Requests the bootstrap file.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="plugin">The plugin.</param>
        internal void RequestBootstrapFile(
            IProjectService projectService,
            Plugin plugin)
        {
            TraceService.WriteLine("PluginService::RequestBootstrapFile plugin=" + plugin.FriendlyName);

            string bootstrapFileName = plugin.FriendlyName + "PluginBootstrap.cs";

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
    }
}
