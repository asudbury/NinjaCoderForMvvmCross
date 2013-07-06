 // --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the OptionsPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Presenters
{
    using System;

    using Services;
    using Services.Interfaces;
    using Views.Interfaces;

    /// <summary>
    ///    Defines the OptionsPresenter type.
    /// </summary>
    public class OptionsPresenter : BasePresenter
    {
        /// <summary>
        /// The view.
        /// </summary>
        private readonly IOptionsView view;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public OptionsPresenter(IOptionsView view)
            : this(view, new SettingsService())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="settingsService">The settings service.</param>
        public OptionsPresenter(
            IOptionsView view,
            ISettingsService settingsService)
        {
            this.view = view;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        public void LoadSettings()
        {
            this.view.LogToFile = this.settingsService.LogToFile;

            this.view.LogFilePath = this.settingsService.LogFilePath;
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            this.settingsService.LogToFile = this.view.LogToFile;
            this.settingsService.LogFilePath = this.view.LogFilePath;
        }
    }
}
