 // --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the OptionsPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Presenters
{
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
        /// The display logo.
        /// </summary>
        private readonly bool displayLogo;

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="displayLogo">if set to <c>true</c> [display logo].</param>
        public OptionsPresenter(IOptionsView view, bool displayLogo)
            : this(view, new SettingsService(), displayLogo)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OptionsPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="displayLogo">if set to <c>true</c> [display logo].</param>
        public OptionsPresenter(
            IOptionsView view,
            ISettingsService settingsService,
            bool displayLogo)
        {
            this.view = view;
            this.settingsService = settingsService;
            this.displayLogo = displayLogo;
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        public void LoadSettings()
        {
            this.view.DisplayLogo = this.displayLogo;

            this.view.TraceOutputEnabled = this.settingsService.LogToTrace;
            this.view.LogToFile = this.settingsService.LogToFile;
            this.view.LogFilePath = this.settingsService.LogFilePath;
            this.view.IncludeLibFolderInProjects = this.settingsService.IncludeLibFolderInProjects;
            this.view.DisplayErrors = this.settingsService.DisplayErrors;
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            this.settingsService.LogToTrace = this.view.TraceOutputEnabled;
            this.settingsService.LogToFile = this.view.LogToFile;
            this.settingsService.LogFilePath = this.view.LogFilePath;
            this.settingsService.IncludeLibFolderInProjects = this.view.IncludeLibFolderInProjects;
            this.settingsService.DisplayErrors = this.view.DisplayErrors;
        }
    }
}
