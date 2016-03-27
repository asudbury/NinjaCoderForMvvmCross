// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the AboutViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels
{
    using Services.Interfaces;

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
        /// Initializes a new instance of the <see cref="AboutViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public AboutViewModel(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
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
    }
}
