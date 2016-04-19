// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsSuffixesViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.Options
{
    using Messages;
    using Services.Interfaces;
    using TinyMessenger;

    /// <summary>
    ///  Defines the ProjectsSuffixesViewModel type.
    /// </summary>
    public class ProjectsSuffixesViewModel : NinjaBaseViewModel
    {
        /// <summary>
        /// The tiny messenger hub.
        /// </summary>
        private readonly ITinyMessengerHub tinyMessengerHub;

        /// <summary>
        /// The core project suffix.
        /// </summary>
        private string coreProjectSuffix;

        /// <summary>
        /// The xamarin forms project suffix.
        /// </summary>
        private string xamarinFormsProjectSuffix;
        
        /// <summary>
        /// The ios project suffix.
        /// </summary>
        private string iOSProjectSuffix;

        /// <summary>
        /// The droid project suffix.
        /// </summary>
        private string droidProjectSuffix;

        /// <summary>
        /// The windows phone project suffix.
        /// </summary>
        private string windowsPhoneProjectSuffix;

        /// <summary>
        /// The WPF project suffix.
        /// </summary>
        private string wpfProjectSuffix;

        /// <summary>
        /// The windows universal project suffix.
        /// </summary>
        private string windowsUniversalProjectSuffix;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsSuffixesViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="tinyMessengerHub">The tiny messenger hub.</param>
        public ProjectsSuffixesViewModel(
            ISettingsService settingsService,
            ITinyMessengerHub tinyMessengerHub)
            : base(settingsService)
        {
            this.tinyMessengerHub = tinyMessengerHub;
            this.Init();
        }

        /// <summary>
        /// Gets or sets the core project suffix.
        /// </summary>
        public string CoreProjectSuffix
        {
            get { return this.coreProjectSuffix; }
            set { this.SetProperty(ref this.coreProjectSuffix, value); }
        }

        /// <summary>
        /// Gets or sets the XamarinFormsProjectSuffix.
        /// </summary>
        public string XamarinFormsProjectSuffix
        {
            get { return this.xamarinFormsProjectSuffix; }
            set { this.SetProperty(ref this.xamarinFormsProjectSuffix, value); }
        }

        /// <summary>
        /// Gets or sets the ios project suffix.
        /// </summary>
        public string IOSProjectSuffix
        {
            get { return this.iOSProjectSuffix; }
            set { this.SetProperty(ref this.iOSProjectSuffix, value); }
        }

        /// <summary>
        /// Gets or sets the droid project suffix.
        /// </summary>
        public string DroidProjectSuffix
        {
            get { return this.droidProjectSuffix; }
            set { this.SetProperty(ref this.droidProjectSuffix, value); }
        }

        /// <summary>
        /// Gets or sets the windows phone project suffix.
        /// </summary>
        public string WindowsPhoneProjectSuffix
        {
            get { return this.windowsPhoneProjectSuffix; }
            set { this.SetProperty(ref this.windowsPhoneProjectSuffix, value); }
        }

        /// <summary>
        /// Gets or sets the WPF project suffix.
        /// </summary>
        public string WpfProjectSuffix
        {
            get { return this.wpfProjectSuffix; }
            set { this.SetProperty(ref this.wpfProjectSuffix, value); }
        }

        /// <summary>
        /// Gets or sets the windows universal project suffix.
        /// </summary>
        public string WindowsUniversalProjectSuffix
        {
            get { return this.windowsUniversalProjectSuffix; }
            set { this.SetProperty(ref this.windowsUniversalProjectSuffix, value); }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            this.SettingsService.CoreProjectSuffix = this.GetSuffix(this.CoreProjectSuffix);
            this.SettingsService.CoreTestsProjectSuffix = this.GetTestsSuffix(this.CoreProjectSuffix);

            this.SettingsService.XamarinFormsProjectSuffix = this.GetSuffix(this.XamarinFormsProjectSuffix);
            this.SettingsService.XamarinFormsTestsProjectSuffix = this.GetTestsSuffix(this.XamarinFormsProjectSuffix);

            this.SettingsService.iOSProjectSuffix = this.GetSuffix(this.iOSProjectSuffix);
            this.SettingsService.iOSTestsProjectSuffix = this.GetTestsSuffix(this.iOSProjectSuffix);

            this.SettingsService.DroidProjectSuffix = this.GetSuffix(this.DroidProjectSuffix);
            this.SettingsService.DroidTestsProjectSuffix = this.GetTestsSuffix(this.DroidProjectSuffix);

            this.SettingsService.WindowsPhoneProjectSuffix = this.GetSuffix(this.WindowsPhoneProjectSuffix);
            this.SettingsService.WindowsPhoneTestsProjectSuffix = this.GetTestsSuffix(this.WindowsPhoneProjectSuffix);

            this.SettingsService.WpfProjectSuffix = this.GetSuffix(this.WpfProjectSuffix);
            this.SettingsService.WpfTestsProjectSuffix = this.GetTestsSuffix(this.WpfProjectSuffix);

            this.SettingsService.WindowsUniversalProjectSuffix = this.GetSuffix(this.WindowsUniversalProjectSuffix);
            this.SettingsService.WindowsUniversalTestsProjectSuffix = this.GetTestsSuffix(this.WindowsUniversalProjectSuffix);

            this.tinyMessengerHub.Publish(new ProjectSuffixesUpdatedMessage());
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        internal void Init()
        {
            this.CoreProjectSuffix = this.SettingsService.CoreProjectSuffix.Substring(1);
            this.XamarinFormsProjectSuffix = this.SettingsService.XamarinFormsProjectSuffix.Substring(1);
            this.IOSProjectSuffix = this.SettingsService.iOSProjectSuffix.Substring(1);
            this.DroidProjectSuffix = this.SettingsService.DroidProjectSuffix.Substring(1);
            this.WindowsPhoneProjectSuffix = this.SettingsService.WindowsPhoneProjectSuffix.Substring(1);
            this.WpfProjectSuffix = this.SettingsService.WpfProjectSuffix.Substring(1);
            this.WindowsUniversalProjectSuffix = this.SettingsService.WindowsUniversalProjectSuffix.Substring(1);
        }

        /// <summary>
        /// Gets the suffix.
        /// </summary>
        /// <param name="suffix">The suffix.</param>
        /// <returns>The suffix.</returns>
        internal string GetSuffix(string suffix)
        {
            if (suffix.StartsWith("."))
            {
                return suffix;
            }

            return "." + suffix;
        }

        /// <summary>
        /// Gets the tests suffix.
        /// </summary>
        /// <param name="suffix">The suffix.</param>
        /// <returns>The tests suffix.</returns>
        internal string GetTestsSuffix(string suffix)
        {
            string testsSuffix = this.GetSuffix(suffix);

            return testsSuffix + ".Tests";
        }
    }
}
