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
        /// The add projects skip views options.
        /// </summary>
        private bool addProjectsSkipViewsOptions;

        /// <summary>
        /// The add projects skip application options.
        /// </summary>
        private bool addProjectsSkipApplicationOptions;

        /// <summary>
        /// The add projects skip ninja coder options.
        /// </summary>
        private bool addProjectsSkipNinjaCoderOptions;
        
        /// <summary>
        /// The add projects skip MVVM cross plugin options
        /// </summary>
        private bool addProjectsSkipMvvmCrossPluginOptions;

        /// <summary>
        /// The add projects skip nuget package options.
        /// </summary>
        private bool addProjectsSkipNugetPackageOptions;
        
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
        /// Gets or sets a value indicating whether [add projects skip views options].
        /// </summary>
        public bool AddProjectsSkipViewsOptions
        {
            get { return this.addProjectsSkipViewsOptions; }
            set { this.SetProperty(ref this.addProjectsSkipViewsOptions, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [add projects skip application options].
        /// </summary>
        public bool AddProjectsSkipApplicationOptions
        {
            get { return this.addProjectsSkipApplicationOptions; }
            set { this.SetProperty(ref this.addProjectsSkipApplicationOptions, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [add projects skip ninja coder options].
        /// </summary>
        public bool AddProjectsSkipNinjaCoderOptions
        {
            get { return this.addProjectsSkipNinjaCoderOptions; }
            set { this.SetProperty(ref this.addProjectsSkipNinjaCoderOptions, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [add projects skip MVVM cross plugin options].
        /// </summary>
        public bool AddProjectsSkipMvvmCrossPluginOptions
        {
            get { return this.addProjectsSkipMvvmCrossPluginOptions; }
            set { this.SetProperty(ref this.addProjectsSkipMvvmCrossPluginOptions, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [add projects skip nuget package options].
        /// </summary>
        public bool AddProjectsSkipNugetPackageOptions
        {
            get { return this.addProjectsSkipNugetPackageOptions; }
            set { this.SetProperty(ref this.addProjectsSkipNugetPackageOptions, value); }
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
            this.SettingsService.AddProjectsSkipViewOptions = this.addProjectsSkipViewsOptions;
            this.SettingsService.AddProjectsSkipNinjaCoderOptions = this.addProjectsSkipNinjaCoderOptions;
            this.SettingsService.AddProjectsSkipApplicationOptions = this.addProjectsSkipApplicationOptions;
            this.SettingsService.AddProjectsSkipMvvmCrossPluginOptions = this.addProjectsSkipMvvmCrossPluginOptions;
            this.SettingsService.AddProjectsSkipNugetPackageOptions = this.addProjectsSkipNugetPackageOptions;
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
            this.addProjectsSkipViewsOptions = this.SettingsService.AddProjectsSkipViewOptions;
            this.addProjectsSkipNinjaCoderOptions  = this.SettingsService.AddProjectsSkipNinjaCoderOptions;
            this.addProjectsSkipApplicationOptions = this.SettingsService.AddProjectsSkipApplicationOptions;
            this.addProjectsSkipMvvmCrossPluginOptions = this.SettingsService.AddProjectsSkipMvvmCrossPluginOptions;
            this.addProjectsSkipNugetPackageOptions = this.SettingsService.AddProjectsSkipNugetPackageOptions;
        }
    }
}
