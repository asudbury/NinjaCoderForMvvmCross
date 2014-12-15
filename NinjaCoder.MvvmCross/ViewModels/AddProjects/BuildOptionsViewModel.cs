// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BuildOptionsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddProjects
{
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the BuildOptionsViewModel type.
    /// </summary>
    public class BuildOptionsViewModel : BaseWizardStepViewModel
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The testing service factory
        /// </summary>
        private readonly ITestingServiceFactory testingServiceFactory;

        /// <summary>
        /// The mocking service factory
        /// </summary>
        private readonly IMockingServiceFactory mockingServiceFactory;

        /// <summary>
        /// The ios versions.
        /// </summary>
        private IEnumerable<string> iosVersions;

        /// <summary>
        /// The testing frameworks.
        /// </summary>
        private IEnumerable<string> testingFrameworks;

        /// <summary>
        /// The mocking frameworks.
        /// </summary>
        private IEnumerable<string> mockingFrameworks;

        /// <summary>
        /// The selected phone version.
        /// </summary>
        private string selectedWindowsPhoneVersion;

        /// <summary>
        /// The selectedi ios versionn.
        /// </summary>
        private string selectediOSVersion;

        /// <summary>
        /// The selected testing framework.
        /// </summary>
        private string selectedTestingFramework;

        /// <summary>
        /// The selected mocking framework.
        /// </summary>
        private string selectedMockingFramework;

        /// <summary>
        /// The bind context in xaml for xamarin forms.
        /// </summary>
        private bool bindContextInXamlForXamarinForms;

        /// <summary>
        /// The use pre release MVVM cross nuget packages
        /// </summary>
        private bool usePreReleaseMvvmCrossNugetPackages;

        /// <summary>
        /// The use pre release xamarin forms nuget packages.
        /// </summary>
        private bool usePreReleaseXamarinFormsNugetPackages;

        /// <summary>
        /// The create platform test projects.
        /// </summary>
        private bool createPlatformTestProjects;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjaBaseViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="testingServiceFactory">The testing service factory.</param>
        /// <param name="mockingServiceFactory">The mocking service factory.</param>
        public BuildOptionsViewModel(
            ISettingsService settingsService,
            ITestingServiceFactory testingServiceFactory,
            IMockingServiceFactory mockingServiceFactory)
        {
            this.settingsService = settingsService;
            this.testingServiceFactory = testingServiceFactory;
            this.mockingServiceFactory = mockingServiceFactory;
            this.Init();
        }

        /// <summary>
        /// Gets or sets the ios versions.
        /// </summary>
        public IEnumerable<string> iOSVersions
        {
            get { return this.iosVersions; }
            set { this.SetProperty(ref this.iosVersions, value); }
        }

        /// <summary>
        /// Gets or sets the selected ios version.
        /// </summary>
        public string SelectediOSVersion
        {
            get { return this.selectediOSVersion; }
            set { this.SetProperty(ref this.selectediOSVersion, value); }
        }

        /// <summary>
        /// Gets or sets the selected windows phone version.
        /// </summary>
        public string SelectedWindowsPhoneVersion
        {
            get { return this.selectedWindowsPhoneVersion; }
            set { this.SetProperty(ref this.selectedWindowsPhoneVersion, value); }
        }

        /// <summary>
        /// Gets or sets the testing frameworks.
        /// </summary>
        public IEnumerable<string> TestingFrameworks
        {
            get { return this.testingFrameworks; }
            set { this.SetProperty(ref this.testingFrameworks, value); }
        }

        /// <summary>
        /// Gets or sets the selected testing framework.
        /// </summary>
        public string SelectedTestingFramework
        {
            get { return this.selectedTestingFramework; }
            set { this.SetProperty(ref this.selectedTestingFramework, value); }
        }

        /// <summary>
        /// Gets or sets the mocking frameworks.
        /// </summary>
        public IEnumerable<string> MockingFrameworks
        {
            get { return this.mockingFrameworks; }
            set { this.SetProperty(ref this.mockingFrameworks, value); }
        }

        /// <summary>
        /// Gets or sets the selected mocking framework.
        /// </summary>
        public string SelectedMockingFramework
        {
            get { return this.selectedMockingFramework; }
            set { this.SetProperty(ref this.selectedMockingFramework, value); }
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
        /// Gets or sets a value indicating whether [bind context in xaml for xamarin forms].
        /// </summary>
        public bool BindContextInXamlForXamarinForms
        {
            get { return this.bindContextInXamlForXamarinForms; }
            set { this.SetProperty(ref this.bindContextInXamlForXamarinForms, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [create platform test projects].
        /// </summary>
        public bool CreatePlatformTestProjects
        {
            get { return this.createPlatformTestProjects; }
            set { this.SetProperty(ref this.createPlatformTestProjects, value); }
        }
        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            this.testingServiceFactory.CurrentFrameWork = this.SelectedTestingFramework;
            this.mockingServiceFactory.CurrentFrameWork = this.SelectedMockingFramework;

            //// ReSharper disable once RedundantCheckBeforeAssignment
            if (this.settingsService.iOSApiVersion != this.SelectediOSVersion)
            {
                this.settingsService.iOSApiVersion = this.SelectediOSVersion;
            }

            this.settingsService.BindContextInXamlForXamarinForms = this.bindContextInXamlForXamarinForms;

            this.settingsService.UsePreReleaseMvvmCrossNugetPackages = this.usePreReleaseMvvmCrossNugetPackages;
            this.settingsService.UsePreReleaseXamarinFormsNugetPackages = this.usePreReleaseXamarinFormsNugetPackages;

            this.settingsService.CreatePlatformTestProjects = this.createPlatformTestProjects;
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        internal void Init()
        {
            this.SelectediOSVersion = this.settingsService.iOSApiVersion;
            this.SelectedWindowsPhoneVersion = this.settingsService.WindowsPhoneBuildVersion;
            this.iOSVersions = new List<string> { "Classic", "Unified" };

            this.TestingFrameworks = this.testingServiceFactory.FrameWorks;
            this.MockingFrameworks = this.mockingServiceFactory.FrameWorks;

            this.SelectedTestingFramework = this.testingServiceFactory.CurrentFrameWork;
            this.SelectedMockingFramework = this.mockingServiceFactory.CurrentFrameWork;

            this.BindContextInXamlForXamarinForms = this.settingsService.BindContextInXamlForXamarinForms;

            this.UsePreReleaseMvvmCrossNugetPackages = this.settingsService.UsePreReleaseMvvmCrossNugetPackages;
            this.UsePreReleaseXamarinFormsNugetPackages = this.settingsService.UsePreReleaseXamarinFormsNugetPackages;

            this.CreatePlatformTestProjects = this.settingsService.CreatePlatformTestProjects;
        }

        public override bool CanMoveToNextPage()
        {
            this.Save();
            return base.CanMoveToNextPage();
        }

        public override string DisplayName
        { 
            get { return "Build Options"; }
        }
    }
}
