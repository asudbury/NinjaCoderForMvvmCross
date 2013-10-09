// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ServicesPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Presenters
{
    using System.Collections.Generic;
    using Scorchio.VisualStudio.Entities;
    using Services.Interfaces;
    using Views.Interfaces;

    /// <summary>
    ///  Defines the ServicesPresenter type.
    /// </summary>
    public class ServicesPresenter : BasePresenter
    {
        /// <summary>
        /// The view.
        /// </summary>
        private readonly IServicesView view;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="settingsService">The settings service.</param>
        public ServicesPresenter(
            IServicesView view,
            ISettingsService settingsService)
        {
            this.view = view;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <param name="viewModelNames">The view model names.</param>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        public void Load(
            IEnumerable<string> viewModelNames,
            IEnumerable<ItemTemplateInfo> itemTemplateInfos)
        {
            this.view.DisplayLogo = this.settingsService.DisplayLogo;
            this.view.UseNuget = this.settingsService.UseNugetForServices;

            foreach (ItemTemplateInfo itemTemplateInfo in itemTemplateInfos)
            {
                this.view.AddTemplate(itemTemplateInfo);
            }

            foreach (string viewModelName in viewModelNames)
            {
                this.view.AddViewModel(viewModelName);
            }
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            this.settingsService.UseNugetForServices = this.view.UseNuget;
        }
    }
}
