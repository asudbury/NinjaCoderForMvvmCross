// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BuildViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.Options
{
    using Services.Interfaces;

    /// <summary>
    ///  Defines the BuildViewModel type.
    /// </summary>
    public class BuildViewModel : NinjaBaseViewModel
    {
        /// <summary>
        /// The user local uris.
        /// </summary>
        private bool userLocalUris;

        /// <summary>
        /// The use local text templates.
        /// </summary>
        private bool useLocalTextTemplates;

        /// <summary>
        /// The output nuget commands to read me
        /// </summary>
        private bool outputNugetCommandsToReadMe;

        /// <summary>
        /// The output errors to read me.
        /// </summary>
        private bool outputErrorsToReadMe;

        /// <summary>
        /// The use xamarin forms xaml compilation.
        /// </summary>
        private bool useXamarinFormsXamlCompilation;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildViewModel" /> class.
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
        /// Gets or sets a value indicating whether [use local text templates].
        /// </summary>
        public bool UseLocalTextTemplates
        {
            get { return this.useLocalTextTemplates; }
            set { this.SetProperty(ref this.useLocalTextTemplates, value); }
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
        /// Gets or sets a value indicating whether [use xamarin forms xaml compilation].
        /// </summary>
        public bool UseXamarinFormsXamlCompilation
        {
            get { return this.useXamarinFormsXamlCompilation; }
            set { this.SetProperty(ref this.useXamarinFormsXamlCompilation, value); }
        }
        
        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            this.SettingsService.UseLocalUris = this.UseLocalUris;
            this.SettingsService.OutputNugetCommandsToReadMe = this.OutputNugetCommandsToReadMe;
            this.SettingsService.OutputErrorsToReadMe = this.OutputErrorsToReadMe;
            this.SettingsService.UseLocalTextTemplates = this.useLocalTextTemplates;
            this.SettingsService.UseXamarinFormsXamlCompilation = this.useXamarinFormsXamlCompilation;
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        internal void Init()
        {
            this.UseLocalUris = this.SettingsService.UseLocalUris;
            this.OutputNugetCommandsToReadMe = this.SettingsService.OutputNugetCommandsToReadMe;
            this.OutputErrorsToReadMe = this.SettingsService.OutputErrorsToReadMe;
            this.useLocalTextTemplates = this.SettingsService.UseLocalTextTemplates;
            this.useXamarinFormsXamlCompilation = this.SettingsService.UseXamarinFormsXamlCompilation;
        }
    }
}
