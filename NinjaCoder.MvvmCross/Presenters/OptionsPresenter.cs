 // --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the OptionsPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Presenters
{
    using System.Diagnostics;
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
        /// <param name="settingsService">The settings service.</param>
        public OptionsPresenter(
            IOptionsView view,
            ISettingsService settingsService)
        {
            this.settingsService = settingsService;
            this.view = view;
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        public void LoadSettings()
        {
            this.view.TraceOutputEnabled = this.settingsService.LogToTrace;
            this.view.LogToFile = this.settingsService.LogToFile;
            this.view.LogFilePath = this.settingsService.LogFilePath;
            this.view.IncludeLibFolderInProjects = this.settingsService.IncludeLibFolderInProjects;
            this.view.DisplayErrors = this.settingsService.DisplayErrors;
            this.view.RemoveDefaultComments = this.settingsService.RemoveDefaultComments;
            this.view.RemoveDefaultFileHeaders = this.settingsService.RemoveDefaultFileHeaders;
            this.view.UseNugetForProjectTemplates = this.settingsService.UseNugetForProjectTemplates;
            this.view.UseNugetForPlugins = this.settingsService.UseNugetForPlugins;
            this.view.SuspendReSharperDuringBuild = this.settingsService.SuspendReSharperDuringBuild;
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
            this.settingsService.RemoveDefaultComments = this.view.RemoveDefaultComments;
            this.settingsService.RemoveDefaultFileHeaders = this.view.RemoveDefaultFileHeaders;
            this.settingsService.UseNugetForProjectTemplates = this.view.UseNugetForProjectTemplates;
            this.settingsService.UseNugetForPlugins = this.view.UseNugetForPlugins;
            this.settingsService.SuspendReSharperDuringBuild = this.view.SuspendReSharperDuringBuild;
        }

        /// <summary>
        /// Shows the download nuget page.
        /// </summary>
        public void ShowDownloadNugetPage()
        {
           Process.Start(@"IEXPLORE.EXE", this.settingsService.DownloadNugetPage);
        }
    }
}
