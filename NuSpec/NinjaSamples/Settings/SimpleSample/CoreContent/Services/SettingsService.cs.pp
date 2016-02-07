// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SettingsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Services
{
    using Refractored.Xam.Settings;
    using Refractored.Xam.Settings.Abstractions;

    /// <summary>
    ///      Defines the SettingService type.
    /// </summary>
    public class SettingsService : ISettingsService
    {
        /// <summary>
        /// The greetings key.
        /// </summary>
        private const string GreetingsKey = "GreetingsKey";

        /// <summary>
        /// The settings.
        /// </summary>
        private static ISettings Settings
        {
            get { return CrossSettings.Current; }
        }

        /// <summary>
        /// Gets or sets the greeting.
        /// </summary>
        public string Greeting
        {
            get { return Settings.GetValueOrDefault(GreetingsKey, "Hello from the Ninja Coder"); }
            set { Settings.AddOrUpdateValue(GreetingsKey, value); }
        }
    }
}