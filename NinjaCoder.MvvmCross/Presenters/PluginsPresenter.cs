// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Presenters
{
    using System.Collections.Generic;
    using Entities;

    using NinjaCoder.MvvmCross.Services.Interfaces;

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
            foreach (string viewModelName in viewModelNames)
            {
                this.view.AddViewModel(viewModelName);
            }

            if (plugins != null)
            {
                foreach (Plugin plugin in plugins)
                {
                    this.view.AddPlugin(plugin);
                }
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
    }
}
