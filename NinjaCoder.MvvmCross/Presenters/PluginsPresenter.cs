// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Presenters
{
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;

    using Entities;

    using NinjaCoder.MvvmCross.Infrastructure.Services;

    using Views.Interfaces;

    /// <summary>
    ///  Defines the PluginsPresenter type.
    /// </summary>
    public class PluginsPresenter : BasePresenter
    {
        /// <summary>
        /// The view.
        /// </summary>
        private readonly IPluginsView view;

        /// <summary>
        /// The settings service
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="settingsService">The settings service.</param>
        public PluginsPresenter(
            IPluginsView view,
            ISettingsService settingsService)
        {
            this.view = view;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <param name="viewModelNames">The view model names.</param>
        /// <param name="plugins">The plugins.</param>
        public void Load(
            IEnumerable<string> viewModelNames,
            IEnumerable<Plugin> plugins)
        {
            viewModelNames
                .ToList()
                .ForEach(x => this.view.AddViewModel(x));

            if (plugins != null)
            {
                plugins
                    .ToList()
                    .ForEach(this.AddPlugin);
            }

            this.view.DisplayLogo = this.settingsService.DisplayLogo;
            this.view.UseNuget = this.settingsService.UseNugetForPlugins;
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            this.settingsService.UseNugetForPlugins = this.view.UseNuget;
        }

        /// <summary>
        /// Shows the MVVM cross plugins wiki page.
        /// </summary>
        public void ShowMvvmCrossPluginsWikiPage()
        {
            Process.Start(@"IEXPLORE.EXE", this.settingsService.MvvmCrossPluginsWikiPage);
        }

        /// <summary>
        /// Opens the user plugins folder.
        /// </summary>
        public void OpenUserPluginsFolder()
        {
            Process.Start(this.settingsService.MvvmCrossAssembliesOverrideDirectory);
        }

        /// <summary>
        /// Adds the plugin.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        internal void AddPlugin(Plugin plugin)
        {
            if (plugin.IsUserPlugin)
            {
                this.view.AddUserPlugin(plugin);
            }
            else if (plugin.IsCommunityPlugin)
            {
                this.view.AddCommunityPlugin(plugin);
            }
            else
            {
                this.view.AddCorePlugin(plugin);
            }
        }
    }
}
