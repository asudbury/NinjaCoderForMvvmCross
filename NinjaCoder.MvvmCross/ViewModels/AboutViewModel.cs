// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the AboutViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels
{
    using Scorchio.Infrastructure.Wpf;
    using Services.Interfaces;
    using System.Windows.Input;

    /// <summary>
    ///  Defines the AboutViewModel type.
    /// </summary>
    public class AboutViewModel 
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The application service.
        /// </summary>
        private readonly IApplicationService applicationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AboutViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="applicationService">The application service.</param>
        public AboutViewModel(
            ISettingsService settingsService,
            IApplicationService applicationService)
        {
            this.settingsService = settingsService;
            this.applicationService = applicationService;
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        public string Version
        {
            get { return this.settingsService.ApplicationVersion; }
        }

        /// <summary>
        /// Gets the build date time.
        /// </summary>
        public string BuildDateTime
        {
            get { return this.settingsService.BuildDateTime; }
        }

        /// <summary>
        /// Gets the open installation directory command.
        /// </summary>
        public ICommand OpenInstallationDirectoryCommand
        {
            get { return new RelayCommand(this.OpenInstallationDirectory); }
        }

        /// <summary>
        /// Opens the installation directory.
        /// </summary>
        private void OpenInstallationDirectory()
        {
            this.applicationService.OpenInstallationDirectory();
        }
    }
}
