// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NugetViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.Options
{
    using System.Windows;

    using NinjaCoder.MvvmCross.Infrastructure.Services;

    /// <summary>
    ///  Defines the NugetViewModel type.
    /// </summary>
    public class NugetViewModel : BaseViewModel
    {
        /// <summary>
        /// The use nuget for project templates.
        /// </summary>
        private bool useNugetForProjectTemplates;

        /// <summary>
        /// The use nuget for plugins.
        /// </summary>
        private bool useNugetForPlugins;

        /// <summary>
        /// The use nuget for services.
        /// </summary>
        private bool useNugetForServices;

        /// <summary>
        /// The use pre release nuget packages.
        /// </summary>
        private bool usePreReleaseNugetPackages;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public NugetViewModel(ISettingsService settingsService)
            : base(settingsService)
        {
            this.Init();
        }
        
        /// <summary>
        /// Gets or sets the language dictionary.
        /// </summary>
        public ResourceDictionary LanguageDictionary { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget for project templates].
        /// </summary>
        public bool UseNugetForProjectTemplates
        {
            get { return this.useNugetForProjectTemplates; }
            set { this.SetProperty(ref this.useNugetForProjectTemplates, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget for plugins].
        /// </summary>
        public bool UseNugetForPlugins
        {
            get { return this.useNugetForPlugins; }
            set { this.SetProperty(ref this.useNugetForPlugins, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget for services].
        /// </summary>
        public bool UseNugetForServices
        {
            get { return this.useNugetForServices; }
            set { this.SetProperty(ref this.useNugetForServices, value); }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether [use pre release nuget packages].
        /// </summary>
        public bool UsePreReleaseNugetPackages
        {
            get { return this.usePreReleaseNugetPackages; }
            set { this.SetProperty(ref this.usePreReleaseNugetPackages, value); }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            this.SettingsService.UseNugetForProjectTemplates = this.UseNugetForProjectTemplates;
            this.SettingsService.UseNugetForPlugins = this.UseNugetForPlugins;
            this.SettingsService.UseNugetForServices = this.UseNugetForServices;
            this.SettingsService.UsePreReleaseNugetPackages = this.usePreReleaseNugetPackages;
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        internal void Init()
        {
            this.UseNugetForProjectTemplates = this.SettingsService.UseNugetForProjectTemplates;
            this.UseNugetForPlugins = this.SettingsService.UseNugetForPlugins;
            this.UseNugetForServices = this.SettingsService.UseNugetForServices;
            this.UsePreReleaseNugetPackages = this.SettingsService.UsePreReleaseNugetPackages;

        }
    }
}
