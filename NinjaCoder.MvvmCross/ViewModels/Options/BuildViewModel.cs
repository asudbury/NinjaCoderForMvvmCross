// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BuildViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.Options
{
    using System.Windows;

    using NinjaCoder.MvvmCross.Infrastructure.Services;

    /// <summary>
    ///  Defines the BuildViewModel type.
    /// </summary>
    public class BuildViewModel : BaseViewModel
    {
        /// <summary>
        /// The suspend re sharper during build.
        /// </summary>
        private bool suspendReSharperDuringBuild;

        /// <summary>
        /// The copy assemblies to lib folder.
        /// </summary>
        private bool copyAssembliesToLibFolder;

        /// <summary>
        /// The include lib folder in projects.
        /// </summary>
        private bool includeLibFolderInProjects;

        /// <summary>
        /// The check for updates.
        /// </summary>
        private bool checkForUpdates;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public BuildViewModel(ISettingsService settingsService)
            : base(settingsService)
        {
            this.Init();
        }

        /// <summary>
        /// Gets or sets the language dictionary.
        /// </summary>
        public ResourceDictionary LanguageDictionary { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [suspend re sharper during build].
        /// </summary>
        public bool SuspendReSharperDuringBuild
        {
            get { return this.suspendReSharperDuringBuild; }
            set { this.SetProperty(ref this.suspendReSharperDuringBuild, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [copy assemblies to lib folder].
        /// </summary>
        public bool CopyAssembliesToLibFolder
        {
            get { return this.copyAssembliesToLibFolder; }
            set { this.SetProperty(ref this.copyAssembliesToLibFolder, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [include lib folder in projects].
        /// </summary>
        public bool IncludeLibFolderInProjects
        {
            get { return this.includeLibFolderInProjects; }
            set { this.SetProperty(ref this.includeLibFolderInProjects, value); }
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
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            this.SettingsService.CopyAssembliesToLibFolder = this.CopyAssembliesToLibFolder;
            this.SettingsService.IncludeLibFolderInProjects = this.IncludeLibFolderInProjects;
            this.SettingsService.SuspendReSharperDuringBuild = this.SuspendReSharperDuringBuild;
            this.SettingsService.CheckForUpdates = this.checkForUpdates;
        }

        /// <summary>
        /// Inits this instance.
        /// </summary>
        internal void Init()
        {
            this.CopyAssembliesToLibFolder = this.SettingsService.CopyAssembliesToLibFolder;
            this.IncludeLibFolderInProjects = this.SettingsService.IncludeLibFolderInProjects;
            this.SuspendReSharperDuringBuild = this.SettingsService.SuspendReSharperDuringBuild;
            this.CheckForUpdates = this.SettingsService.CheckForUpdates;
        }
    }
}
