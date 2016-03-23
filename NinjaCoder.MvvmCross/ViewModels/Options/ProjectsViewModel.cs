// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.Options
{
    using Factories.Interfaces;
    using Scorchio.Infrastructure.Wpf;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Windows;
    using System.Windows.Input;

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
        /// The view types
        /// </summary>
        private IEnumerable<string> viewTypes;

        /// <summary>
        /// The selected view type.
        /// </summary>
        private string selectedViewType;

        /// <summary>
        /// The MVVM cross ios view types.
        /// </summary>
        private IEnumerable<string> mvvmCrossiOSViewTypes;

        /// <summary>
        /// The selected MVVM cross ios view type
        /// </summary>
        private string selectedMvvmCrossiOSViewType;
        
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
        /// Initializes a new instance of the <see cref="ProjectsViewModel" /> class.
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
        /// Gets or sets the MVVM cross ios view types.
        /// </summary>
        public IEnumerable<string> MvvmCrossiOSViewTypes
        {
            get { return this.mvvmCrossiOSViewTypes; }
            set { this.SetProperty(ref this.mvvmCrossiOSViewTypes, value); }
        }

        /// <summary>
        /// Gets or sets the type of the selected view.
        /// </summary>
        public string SelectedMvvmCrossiOSViewType
        {
            get { return this.selectedMvvmCrossiOSViewType; }
            set { this.SetProperty(ref this.selectedMvvmCrossiOSViewType, value); }
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
            this.SettingsService.DefaultViewType = this.selectedViewType;
            this.SettingsService.SelectedMvvmCrossiOSViewType = this.selectedMvvmCrossiOSViewType;
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
            this.ViewTypes = this.viewModelAndViewsFactory.GetAvailableViewTypes();
            this.MvvmCrossiOSViewTypes = this.viewModelAndViewsFactory.GetAvailableMvvmCrossiOSViewTypes();

            this.SelectedViewType = this.SettingsService.DefaultViewType;
            this.SelectedMvvmCrossiOSViewType = this.SettingsService.SelectedMvvmCrossiOSViewType;

            this.addProjectsSkipViewsOptions = this.SettingsService.AddProjectsSkipViewOptions;
            this.addProjectsSkipNinjaCoderOptions = this.SettingsService.AddProjectsSkipNinjaCoderOptions;
            this.addProjectsSkipApplicationOptions = this.SettingsService.AddProjectsSkipApplicationOptions;
            this.addProjectsSkipMvvmCrossPluginOptions = this.SettingsService.AddProjectsSkipMvvmCrossPluginOptions;
            this.addProjectsSkipNugetPackageOptions = this.SettingsService.AddProjectsSkipNugetPackageOptions;
        }

        /// <summary>
        /// Displays the UI help page.
        /// </summary>
        internal void DisplayUIHelpPage()
        {
            Process.Start(this.SettingsService.MvvmCrossiOSViewWebPage);
        }
    }
}
