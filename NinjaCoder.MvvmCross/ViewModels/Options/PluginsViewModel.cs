// --------------------------------------------------------------------------------------------------------------------
//    Defines the PluginsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.ViewModels.Options
{
    using System.Windows;

    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.Infrastructure.Services;
    using Scorchio.Infrastructure.Wpf.ViewModels;

    using BaseViewModel = NinjaCoder.MvvmCross.ViewModels.NinjaBaseViewModel;

    /// <summary>
    ///  Defines the PluginsViewModel type.
    /// </summary>
    public class PluginsViewModel : BaseViewModel
    {
        /// <summary>
        /// The folder browser dialog service.
        /// </summary>
        private readonly IFolderBrowserDialogService folderBrowserDialogService;

        /// <summary>
        /// The user plugins directory view model.
        /// </summary>
        private DirectoryPickerViewModel userPluginsDirectoryViewModel;

        /// <summary>
        /// The user plugins snippets directory view model.
        /// </summary>
        private DirectoryPickerViewModel userPluginsSnippetsDirectoryViewModel;

        /// <summary>
        /// The user plugins config directory view model.
        /// </summary>
        private DirectoryPickerViewModel userPluginsConfigDirectoryViewModel;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="folderBrowserDialogService">The folder browser dialog service.</param>
        public PluginsViewModel(
            ISettingsService settingsService,
            IFolderBrowserDialogService folderBrowserDialogService)
            : base(settingsService)
        {
            this.folderBrowserDialogService = folderBrowserDialogService;
            this.Init();
        }
        
        /// <summary>
        /// Gets or sets the language dictionary.
        /// </summary>
        public ResourceDictionary LanguageDictionary { get; set; }

        /// <summary>
        /// Gets or sets the user plugins directory.
        /// </summary>
        public DirectoryPickerViewModel UserPluginsDirectoryViewModel
        {
            get { return this.userPluginsDirectoryViewModel; }
            set { this.SetProperty(ref this.userPluginsDirectoryViewModel, value); }
        }

        /// <summary>
        /// Gets or sets the user plugins snippets directory.
        /// </summary>
        public DirectoryPickerViewModel UserPluginsSnippetsDirectoryViewModel
        {
            get { return this.userPluginsSnippetsDirectoryViewModel; }
            set { this.SetProperty(ref this.userPluginsSnippetsDirectoryViewModel, value); }
        }

        /// <summary>
        /// Gets or sets the user plugins config directory view model.
        /// </summary>
        public DirectoryPickerViewModel UserPluginsConfigDirectoryViewModel
        {
            get { return this.userPluginsConfigDirectoryViewModel; }
            set { this.SetProperty(ref this.userPluginsConfigDirectoryViewModel, value); }
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            this.SettingsService.UserPluginsPath = this.UserPluginsDirectoryViewModel.Directory;
            this.SettingsService.UserCodeSnippetsPluginsPath = this.UserPluginsSnippetsDirectoryViewModel.Directory;
            this.SettingsService.UserCodeConfigPluginsPath = this.UserPluginsConfigDirectoryViewModel.Directory;
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        internal void Init()
        {
            this.UserPluginsDirectoryViewModel = new DirectoryPickerViewModel(folderBrowserDialogService)
            {
                LabelBindingKey = "UserPluginsDirectory",
                OpenBindingKey = "Open",
                Directory = this.SettingsService.UserPluginsPath
            };

            this.UserPluginsSnippetsDirectoryViewModel = new DirectoryPickerViewModel(folderBrowserDialogService)
            {
                LabelBindingKey = "UserPluginsSnippetsDirectory",
                OpenBindingKey = "Open",
                Directory = this.SettingsService.UserCodeSnippetsPluginsPath
            };

            this.UserPluginsConfigDirectoryViewModel = new DirectoryPickerViewModel(folderBrowserDialogService)
            {
                LabelBindingKey = "UserPluginsConfigDirectory",
                OpenBindingKey = "Open",
                Directory = this.SettingsService.UserCodeConfigPluginsPath
            };
        }
    }
}
