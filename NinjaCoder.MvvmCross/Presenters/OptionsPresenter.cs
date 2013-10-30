 // --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the OptionsPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Presenters
{
    using System.Diagnostics;
    using System.IO;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using Views.Interfaces;

    /// <summary>
    ///    Defines the OptionsPresenter type.
    /// </summary>
    internal class OptionsPresenter : BasePresenter
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
            this.view.CopyAssembliesToLibFolder = this.settingsService.CopyAssembliesToLibFolder;
            this.view.IncludeLibFolderInProjects = this.settingsService.IncludeLibFolderInProjects;
            this.view.DisplayErrors = this.settingsService.DisplayErrors;
            this.view.RemoveDefaultComments = this.settingsService.RemoveDefaultComments;
            this.view.RemoveDefaultFileHeaders = this.settingsService.RemoveDefaultFileHeaders;
            this.view.FormatFunctionParameters = this.settingsService.FormatFunctionParameters;
            this.view.UseNugetForProjectTemplates = this.settingsService.UseNugetForProjectTemplates;
            this.view.UseNugetForPlugins = this.settingsService.UseNugetForPlugins;
            this.view.UseNugetForServices = this.settingsService.UseNugetForServices;
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
            this.settingsService.CopyAssembliesToLibFolder = this.view.CopyAssembliesToLibFolder;
            this.settingsService.IncludeLibFolderInProjects = this.view.IncludeLibFolderInProjects;
            this.settingsService.DisplayErrors = this.view.DisplayErrors;
            this.settingsService.RemoveDefaultComments = this.view.RemoveDefaultComments;
            this.settingsService.RemoveDefaultFileHeaders = this.view.RemoveDefaultFileHeaders;
            this.settingsService.FormatFunctionParameters = this.view.FormatFunctionParameters;
            this.settingsService.UseNugetForProjectTemplates = this.view.UseNugetForProjectTemplates;
            this.settingsService.UseNugetForPlugins = this.view.UseNugetForPlugins;
            this.settingsService.UseNugetForServices = this.view.UseNugetForServices;
            this.settingsService.SuspendReSharperDuringBuild = this.view.SuspendReSharperDuringBuild;
        }

        /// <summary>
        /// Shows the download nuget page.
        /// </summary>
        public void ShowDownloadNugetPage()
        {
           Process.Start(this.settingsService.DownloadNugetPage);
        }

        /// <summary>
        /// Clears the log.
        /// </summary>
        public void ClearLog()
        {
            if (File.Exists(this.view.LogFilePath))
            {
                File.Delete(this.view.LogFilePath);
            }
        }

        /// <summary>
        /// Views the log.
        /// </summary>
        public void ViewLog()
        {
            if (File.Exists(this.view.LogFilePath) == false)
            {
                File.Create(this.view.LogFilePath);
            }

            Process.Start(this.view.LogFilePath);    
         }
    }
}
