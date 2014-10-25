// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BuildViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.Options
{
    using System.Windows;

    using NinjaCoder.MvvmCross.Services.Interfaces;

    /// <summary>
    ///  Defines the BuildViewModel type.
    /// </summary>
    public class BuildViewModel : NinjaBaseViewModel
    {
        /// <summary>
        /// The check for updates.
        /// </summary>
        private bool checkForUpdates;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjaBaseViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public BuildViewModel(ISettingsService settingsService)
            : base(settingsService)
        {
            this.Init();
        }

        /// <summary>
        /// Gets or sets the language dictionary.
        /// </summary>
        public ResourceDictionary LanguageDictionary { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [check for updates].
        /// </summary>
        public bool CheckForUpdates
        {
            get { return this.checkForUpdates; }
            set { this.SetProperty(ref this.checkForUpdates, value); }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            this.SettingsService.CheckForUpdates = this.checkForUpdates;
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        internal void Init()
        {
            this.CheckForUpdates = this.SettingsService.CheckForUpdates;
        }
    }
}
