// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BuildViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.Options
{
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
        /// The user local uris.
        /// </summary>
        private bool userLocalUris;

        /// <summary>
        /// The output nuget commands to read me
        /// </summary>
        private bool outputNugetCommandsToReadMe;

        /// <summary>
        /// The output errors to read me.
        /// </summary>
        private bool outputErrorsToReadMe;

        /// <summary>
        /// The use pre release MVVM cross nuget packages
        /// </summary>
        private bool usePreReleaseMvvmCrossNugetPackages;

        /// <summary>
        /// The use pre release xamarin forms nuget packages.
        /// </summary>
        private bool usePreReleaseXamarinFormsNugetPackages;

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
        /// Gets or sets a value indicating whether [check for updates].
        /// </summary>
        public bool UseLocalUris
        {
            get { return this.userLocalUris; }
            set { this.SetProperty(ref this.userLocalUris, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [check for updates].
        /// </summary>
        public bool CheckForUpdates
        {
            get { return this.checkForUpdates; }
            set { this.SetProperty(ref this.checkForUpdates, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [output nuget commands to read me].
        /// </summary>
        public bool OutputNugetCommandsToReadMe
        {
            get { return this.outputNugetCommandsToReadMe; }
            set { this.SetProperty(ref this.outputNugetCommandsToReadMe, value); }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether [output errors to read me].
        /// </summary>
        public bool OutputErrorsToReadMe
        {
            get { return this.outputErrorsToReadMe; }
            set { this.SetProperty(ref this.outputErrorsToReadMe, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use pre release MVVM cross nuget packages].
        /// </summary>
        public bool UsePreReleaseMvvmCrossNugetPackages 
        {
            get { return this.usePreReleaseMvvmCrossNugetPackages; }
            set { this.SetProperty(ref this.usePreReleaseMvvmCrossNugetPackages, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use pre release xamarin forms nuget packages].
        /// </summary>
        public bool UsePreReleaseXamarinFormsNugetPackages
        {
            get { return this.usePreReleaseXamarinFormsNugetPackages; }
            set { this.SetProperty(ref this.usePreReleaseXamarinFormsNugetPackages, value); }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            this.SettingsService.UseLocalUris = this.UseLocalUris;
            this.SettingsService.CheckForUpdates = this.checkForUpdates;
            this.SettingsService.OutputNugetCommandsToReadMe = this.OutputNugetCommandsToReadMe;
            this.SettingsService.OutputErrorsToReadMe = this.OutputErrorsToReadMe;
            this.SettingsService.UsePreReleaseMvvmCrossNugetPackages = this.UsePreReleaseMvvmCrossNugetPackages;
            this.SettingsService.UsePreReleaseXamarinFormsNugetPackages = this.UsePreReleaseXamarinFormsNugetPackages;
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        internal void Init()
        {
            this.UseLocalUris = this.SettingsService.UseLocalUris;
            this.CheckForUpdates = this.SettingsService.CheckForUpdates;
            this.OutputNugetCommandsToReadMe = this.SettingsService.OutputNugetCommandsToReadMe;
            this.OutputErrorsToReadMe = this.SettingsService.OutputErrorsToReadMe;
            this.UsePreReleaseMvvmCrossNugetPackages = this.SettingsService.UsePreReleaseMvvmCrossNugetPackages;
            this.UsePreReleaseXamarinFormsNugetPackages = this.SettingsService.UsePreReleaseXamarinFormsNugetPackages;
        }
    }
}
