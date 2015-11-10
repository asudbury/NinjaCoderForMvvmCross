// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the SettingsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.ViewModels
{
    using $rootnamespace$.Services;

    /// <summary>
    ///  Defines the SettingsViewModel type.
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The greeting.
        /// </summary>
        private string greeting;

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsViewModel"/> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public SettingsViewModel(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
            this.greeting = this.settingsService.Greeting;
        }

        /// <summary>
        /// Gets or sets the greeting.
        /// </summary>
        public string Greeting
        {
            get
            {
                return this.greeting;
            }
            set
            {
                this.SetProperty(ref this.greeting, value);
                this.settingsService.Greeting = value;
            }
        }
    }
}