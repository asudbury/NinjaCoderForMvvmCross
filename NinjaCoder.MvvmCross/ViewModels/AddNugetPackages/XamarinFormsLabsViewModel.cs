// ------------------------------------- -------------------------------------------------------------------------------
// <summary>
//    Defines the XamarinFormsLabsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddNugetPackages
{
    using Entities;
    using Factories.Interfaces;
    using Scorchio.Infrastructure.Wpf;
    using Scorchio.Infrastructure.Wpf.ViewModels;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Windows.Input;

    /// <summary>
    ///  Defines the XamarinFormsLabsViewModel type.
    /// </summary>
    public class XamarinFormsLabsViewModel : NugetPackagesBaseViewModel
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The plugins.
        /// </summary>
        private ObservableCollection<SelectableItemViewModel<Plugin>> plugins;

        /// <summary>
        /// Initializes a new instance of the <see cref="XamarinFormsLabsViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="projectFactory">The project factory.</param>
        /// <param name="pluginFactory">The plugin factory.</param>
        public XamarinFormsLabsViewModel(
            ISettingsService settingsService,
            IPluginsService pluginsService,
            IProjectFactory projectFactory,
            IPluginFactory pluginFactory)
            : base(
                settingsService,
                pluginsService,
                projectFactory,
                pluginFactory)
        {
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Gets or sets the plugins.
        /// </summary>
        public ObservableCollection<SelectableItemViewModel<Plugin>> Plugins
        {
            get { return this.plugins; }
            set { this.SetProperty(ref this.plugins, value); }
        }

        /// <summary>
        /// Gets the git hub page command.
        /// </summary>
        public ICommand GitHubPageCommand
        {
            get { return new RelayCommand(this.DisplayGitHubPage); }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get { return "Xamarin Forms Labs Plugins"; }
        }

        /// <summary>
        /// Gets the nuget packages URI.
        /// </summary>
        protected override string NugetPackagesUri
        {
            get { return this.settingsService.XamarinFormsLabsPluginsUri; }
        }

        /// <summary>
        /// Gets the get nuget packages.
        /// </summary>
        protected override IEnumerable<SelectableItemViewModel<Plugin>> NugetPackages
        {
            get { return this.Plugins; }
        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        public override void OnInitialize()
        {
            if (this.Plugins == null)
            {
                Plugins allPackages = this.GetPlugins();
                this.Plugins = this.GetCategoryNugetPackages(allPackages, string.Empty);
            }
        }

        /// <summary>
        /// Displays the git hub page.
        /// </summary>
        internal void DisplayGitHubPage()
        {
            Process.Start(this.settingsService.XamarinFormsLabsNugetPackagesGitHubPage);
        }
    }
}
