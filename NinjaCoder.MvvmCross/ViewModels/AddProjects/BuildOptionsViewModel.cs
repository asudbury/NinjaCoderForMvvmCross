// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BuildOptionsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddProjects
{
    using MahApps.Metro;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.Infrastructure.Services;
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
        /// The message box service.
        /// </summary>
        private readonly IMessageBoxService messageBoxService;

        /// <summary>
        /// The testing service factory
        /// </summary>
        private readonly ITestingServiceFactory testingServiceFactory;

        /// <summary>
        /// The mocking service factory
        /// </summary>
        private readonly IMockingServiceFactory mockingServiceFactory;

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
        /// The selected testing framework.
        /// </summary>
        private string selectedTestingFramework;

        /// <summary>
        /// The selected mocking framework.
        /// </summary>
        private string selectedMockingFramework;

        /// <summary>
        /// The use pre release MVVM cross nuget packages
        /// </summary>
        private bool usePreReleaseMvvmCrossNugetPackages;

        /// <summary>
        /// The use pre release xamarin forms nuget packages.
        /// </summary>
        private bool usePreReleaseXamarinFormsNugetPackages;

        /// <summary>
        /// The use pre release ninja coder nuget packages.
        /// </summary>
        private bool usePreReleaseNinjaCoderNugetPackages;

        /// <summary>
        /// The create platform test projects.
        /// </summary>
        private bool createPlatformTestProjects;

        /// <summary>
        /// The use xamarin test cloud.
        /// </summary>
        private bool useXamarinTestCloud;

        /// <summary>
        /// The use xamarin insights.
        /// </summary>
        private bool useXamarinInsights;

        /// <summary>
        /// The use style cop.
        /// </summary>
        private bool useStyleCop;

        /// <summary>
        /// Initializes a new instance of the <see cref="BuildOptionsViewModel"/> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="testingServiceFactory">The testing service factory.</param>
        /// <param name="mockingServiceFactory">The mocking service factory.</param>
        public BuildOptionsViewModel(
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            ITestingServiceFactory testingServiceFactory,
            IMockingServiceFactory mockingServiceFactory)
        {
            this.settingsService = settingsService;
            this.messageBoxService = messageBoxService;
            this.testingServiceFactory = testingServiceFactory;
            this.mockingServiceFactory = mockingServiceFactory;
            this.Init();
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
        /// Gets or sets a value indicating whether [use pre release ninja coder nuget packages].
        /// </summary>
        public bool UsePreReleaseNinjaCoderNugetPackages
        {
            get { return this.usePreReleaseNinjaCoderNugetPackages; }
            set { this.SetProperty(ref this.usePreReleaseNinjaCoderNugetPackages, value); }
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
        /// Gets or sets a value indicating whether [use xamarin test cloud].
        /// </summary>
        public bool UseXamarinTestCloud
        {
            get { return this.useXamarinTestCloud; }
            set { this.SetProperty(ref this.useXamarinTestCloud, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use xamarin insights].
        /// </summary>
        public bool UseXamarinInsights
        {
            get { return this.useXamarinInsights; }
            set { this.SetProperty(ref this.useXamarinInsights, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use style cop].
        /// </summary>
        public bool UseStyleCop
        {
            get { return this.useStyleCop; }
            set { this.SetProperty(ref this.useStyleCop, value); }
        }
        
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get { return "Build Options"; }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            this.testingServiceFactory.CurrentFrameWork = this.SelectedTestingFramework;
            this.mockingServiceFactory.CurrentFrameWork = this.SelectedMockingFramework;

            this.settingsService.UsePreReleaseMvvmCrossNugetPackages = this.usePreReleaseMvvmCrossNugetPackages;
            this.settingsService.UsePreReleaseXamarinFormsNugetPackages = this.usePreReleaseXamarinFormsNugetPackages;
            this.settingsService.UsePreReleaseNinjaNugetPackages = this.usePreReleaseNinjaCoderNugetPackages;

            this.settingsService.CreatePlatformTestProjects = this.createPlatformTestProjects;
            this.settingsService.UseXamarinTestCloud = this.useXamarinTestCloud;
            this.settingsService.UseXamarinInsights = this.useXamarinInsights;
            this.settingsService.UseStyleCop = this.useStyleCop;
        }

        /// <summary>
        /// Determines whether this instance [can move to next page].
        /// </summary>
        /// <returns></returns>
        public override bool CanMoveToNextPage()
        {
            if (this.UseXamarinTestCloud && 
                this.selectedTestingFramework != "NUnit")
            {
                this.messageBoxService.Show(
                    Constants.Settings.XamarinTestCloudAndNUnit,
                    Constants.Settings.ApplicationName,
                    this.settingsService.BetaTesting,
                    Theme.Light,
                    this.settingsService.ThemeColor);

                return false;
            }

            this.Save();
            return base.CanMoveToNextPage();
        }
       
        /// <summary>
        /// Inits this instance.
        /// </summary>
        internal void Init()
        {
            this.SelectedWindowsPhoneVersion = this.settingsService.WindowsPhoneBuildVersion;

            this.TestingFrameworks = this.testingServiceFactory.FrameWorks;
            this.MockingFrameworks = this.mockingServiceFactory.FrameWorks;

            this.SelectedTestingFramework = this.testingServiceFactory.CurrentFrameWork;
            this.SelectedMockingFramework = this.mockingServiceFactory.CurrentFrameWork;

            this.UsePreReleaseMvvmCrossNugetPackages = this.settingsService.UsePreReleaseMvvmCrossNugetPackages;
            this.UsePreReleaseXamarinFormsNugetPackages = this.settingsService.UsePreReleaseXamarinFormsNugetPackages;
            this.UsePreReleaseNinjaCoderNugetPackages = this.settingsService.UsePreReleaseNinjaNugetPackages;

            this.CreatePlatformTestProjects = this.settingsService.CreatePlatformTestProjects;
            this.UseXamarinTestCloud = this.settingsService.UseXamarinTestCloud;
            this.UseXamarinInsights = this.settingsService.UseXamarinInsights;
            this.UseStyleCop = this.settingsService.UseStyleCop;
        }
    }
}
