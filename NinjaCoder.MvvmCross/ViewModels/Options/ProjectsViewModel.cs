// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.Options
{
    using System.Collections.Generic;
    using System.Windows;

    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    /// <summary>
    ///  Defines the ProjectsViewModel type.
    /// </summary>
    public class ProjectsViewModel : NinjaBaseViewModel
    {
        /// <summary>
        /// The testing service factory
        /// </summary>
        private readonly ITestingServiceFactory testingServiceFactory;

        /// <summary>
        /// The mocking service factory
        /// </summary>
        private readonly IMockingServiceFactory mockingServiceFactory;

        /// <summary>
        /// The view model and views factory.
        /// </summary>
        private readonly IViewModelAndViewsFactory viewModelAndViewsFactory;

        /// <summary>
        /// The PCL profiles.
        /// </summary>
        private IEnumerable<string> pclProfiles;

        /// <summary>
        /// The selected PCL profile.
        /// </summary>
        private string selectedPCLProfile;

        /// <summary>
        /// The windows phone versions.
        /// </summary>
        private IEnumerable<string> windowsPhoneVersions;

        /// <summary>
        /// The testing frameworks.
        /// </summary>
        private IEnumerable<string> testingFrameworks;

        /// <summary>
        /// The mocking frameworks.
        /// </summary>
        private IEnumerable<string> mockingFrameworks;

        /// <summary>
        /// The view types
        /// </summary>
        private IEnumerable<string> viewTypes;

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
        /// The selected view type.
        /// </summary>
        private string selectedViewType;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjaBaseViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="testingServiceFactory">The testing service factory.</param>
        /// <param name="mockingServiceFactory">The mocking service factory.</param>
        /// <param name="viewModelAndViewsFactory">The view model and views factory.</param>
        public ProjectsViewModel(
            ISettingsService settingsService,
            ITestingServiceFactory testingServiceFactory,
            IMockingServiceFactory mockingServiceFactory,
            IViewModelAndViewsFactory viewModelAndViewsFactory)
            : base(settingsService)
        {
            this.testingServiceFactory = testingServiceFactory;
            this.mockingServiceFactory = mockingServiceFactory;
            this.viewModelAndViewsFactory = viewModelAndViewsFactory;
            this.Init();
        }
        
        /// <summary>
        /// Gets or sets the language dictionary.
        /// </summary>
        public ResourceDictionary LanguageDictionary { get; set; }

        /// <summary>
        /// Gets or sets the PCL profiles.
        /// </summary>
        public IEnumerable<string> PCLProfiles
        {
            get { return this.pclProfiles; }
            set { this.SetProperty(ref this.pclProfiles, value); }
        }

        /// <summary>
        /// Gets or sets the selected PCL profile.
        /// </summary>
        public string SelectedPCLProfile
        {
            get { return this.selectedPCLProfile; }
            set { this.SetProperty(ref this.selectedPCLProfile, value); }
        }

        /// <summary>
        /// Gets or sets the windows phone versions.
        /// </summary>
        public IEnumerable<string> WindowsPhoneVersions
        {
            get { return this.windowsPhoneVersions; }
            set { this.SetProperty(ref this.windowsPhoneVersions, value); }
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
        /// Gets or sets the view types.
        /// </summary>
        public IEnumerable<string> ViewTypes
        {
            get { return this.viewTypes; }
            set { this.SetProperty(ref this.viewTypes, value); }
        }

        /// <summary>
        /// Gets or sets the type of the selected view.
        /// </summary>
        public string SelectedViewType
        {
            get { return this.selectedViewType; }
            set { this.SetProperty(ref this.selectedViewType, value); }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            this.testingServiceFactory.CurrentFrameWork = this.SelectedTestingFramework;
            this.mockingServiceFactory.CurrentFrameWork = this.SelectedMockingFramework;
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        internal void Init()
        {
            this.SelectedPCLProfile = this.SettingsService.PCLProfile;
            this.SelectedWindowsPhoneVersion = this.SettingsService.WindowsPhoneBuildVersion;
            
            //// for now there are no real options for these - maybe change in the future
            this.PCLProfiles = new List<string> { this.selectedPCLProfile };
            this.WindowsPhoneVersions = new List<string> { this.selectedWindowsPhoneVersion };

            this.TestingFrameworks = this.testingServiceFactory.FrameWorks;
            this.MockingFrameworks = this.mockingServiceFactory.FrameWorks;

            this.SelectedTestingFramework = this.testingServiceFactory.CurrentFrameWork;
            this.SelectedMockingFramework = this.mockingServiceFactory.CurrentFrameWork;

            this.ViewTypes = this.viewModelAndViewsFactory.GetAvailableViewTypes();
        }
    }
}
