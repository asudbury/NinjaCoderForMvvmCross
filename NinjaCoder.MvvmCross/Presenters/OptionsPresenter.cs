// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the OptionsPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Presenters
{
    using System;

    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.Views.Interfaces;

    /// <summary>
    ///    Defines the OptionsPresenter type.
    /// </summary>
    public class OptionsPresenter
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

            this.view.LogFilePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\ninja-coder-for-mvvmcross.log";
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            this.settingsService.LogToFile = this.view.LogToFile;
        }
    }
}
