// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.Options
{
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;

    using Scorchio.Infrastructure.Wpf;

    /// <summary>
    ///  Defines the ProjectsViewModel type.
    /// </summary>
    public class ProjectsViewModel : NinjaBaseViewModel
    {
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
        /// The view types
        /// </summary>
        private IEnumerable<string> viewTypes;

        /// <summary>
        /// The selected phone version.
        /// </summary>
        private string selectedWindowsPhoneVersion;

        /// <summary>
        /// The selected view type.
        /// </summary>
        private string selectedViewType;

        /// <summary>
        /// The MVVM crossi os sample data view types.
        /// </summary>
        private IEnumerable<string> mvvmCrossiOSSampleDataViewTypes;

        /// <summary>
        /// The selected MVVM crossi os sample data view type
        /// </summary>
        private string selectedMvvmCrossiOSSampleDataViewType;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjaBaseViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="viewModelAndViewsFactory">The view model and views factory.</param>
        public ProjectsViewModel(
            ISettingsService settingsService,
            IViewModelAndViewsFactory viewModelAndViewsFactory)
            : base(settingsService)
        {
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
        /// Gets or sets the MVVM crossi os sample data view types.
        /// </summary>
        public IEnumerable<string> MvvmCrossiOSSampleDataViewTypes
        {
            get { return this.mvvmCrossiOSSampleDataViewTypes; }
            set { this.SetProperty(ref this.mvvmCrossiOSSampleDataViewTypes, value); }
        }

        /// <summary>
        /// Gets or sets the type of the selected view.
        /// </summary>
        public string SelectedMvvmCrossiOSSampleDataViewType
        {
            get { return this.selectedMvvmCrossiOSSampleDataViewType; }
            set { this.SetProperty(ref this.selectedMvvmCrossiOSSampleDataViewType, value); }
        }

        /// <summary>
        /// Gets the UI help page command.
        /// </summary>
        public ICommand UIHelpPageCommand
        {
            get { return new RelayCommand(this.DisplayUIHelpPage); }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            this.SettingsService.SelectedMvvmCrossiOSSampleDataViewType = this.selectedMvvmCrossiOSSampleDataViewType;
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

            this.ViewTypes = this.viewModelAndViewsFactory.GetAvailableViewTypes();
            this.MvvmCrossiOSSampleDataViewTypes = this.viewModelAndViewsFactory.GetAvailableMvvmCrossiOSSampleDataViewTypes();

            this.SelectedMvvmCrossiOSSampleDataViewType = this.SettingsService.SelectedMvvmCrossiOSSampleDataViewType;
        }

        /// <summary>
        /// Displays the UI help page.
        /// </summary>
        internal void DisplayUIHelpPage()
        {
            Process.Start(this.SettingsService.MvvmCrossiOSSampleDataWebPage);

        }
    }
}
