// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddProjects
{
    using Entities;
    using Factories.Interfaces;
    using Scorchio.Infrastructure.Wpf;
    using Scorchio.Infrastructure.Wpf.ViewModels;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Input;

    /// <summary>
    ///  Defines the PluginsViewModel type.
    /// </summary>
    public class PluginsViewModel : NugetPackagesBaseViewModel
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The core plugins.
        /// </summary>
        private ObservableCollection<SelectableItemViewModel<Plugin>> corePlugins;

        /// <summary>
        /// The community plugins.
        /// </summary>
        private ObservableCollection<SelectableItemViewModel<Plugin>> communityPlugins;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="projectFactory">The project factory.</param>
        /// <param name="pluginFactory">The plugin factory.</param>
        public PluginsViewModel(
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
        /// Gets or sets a value indicating whether [core plugins selected].
        /// </summary>
        public bool CorePluginsSelected { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [community plugins selected].
        /// </summary>
        public bool CommunityPluginsSelected { get; set; }

        /// <summary>
        /// Gets or sets the core plugins.
        /// </summary>
        public ObservableCollection<SelectableItemViewModel<Plugin>> CorePlugins
        {
            get { return this.corePlugins; }
            set { this.SetProperty(ref this.corePlugins, value); }
        }
        
        /// <summary>
        /// Gets or sets the community plugins.
        /// </summary>
        public ObservableCollection<SelectableItemViewModel<Plugin>> CommunityPlugins
        {
            get { return this.communityPlugins; }
            set { this.SetProperty(ref this.communityPlugins, value); }
        }

        /// <summary>
        /// Gets the wiki page command.
        /// </summary>
        public ICommand WikiPageCommand
        {
            get { return new RelayCommand(this.DisplayWikiPage); }
        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        public override void OnInitialize()
        {
            if (this.CorePlugins == null)
            {
                Plugins allPlugins = this.GetPlugins();

                this.CorePlugins = this.GetPlugins(allPlugins, false);
                this.CommunityPlugins = this.GetPlugins(allPlugins, true);

                if (this.corePlugins.Any() == false && this.communityPlugins.Any())
                {
                    this.CommunityPluginsSelected = true;
                }
                else
                {
                    this.CorePluginsSelected = true;
                }
            }
        }

        /// <summary>
        /// Gets the nuget packages URI.
        /// </summary>
        protected override string NugetPackagesUri
        {
            get { return this.settingsService.MvvmCrossPluginsUri; }
        }

        /// <summary>
        /// Gets the get nuget packages.
        /// </summary>
        protected override IEnumerable<SelectableItemViewModel<Plugin>> NugetPackages
        {
            get { return this.CorePlugins != null ? this.CorePlugins.Union(this.CommunityPlugins) : new List<SelectableItemViewModel<Plugin>>(); }
        }

        /// <summary>
        /// Displays the wiki page.
        /// </summary>
        internal void DisplayWikiPage()
        {
            Process.Start(this.settingsService.MvvmCrossPluginsWikiPage);
        }

        /// <summary>
        /// Gets the plugins.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <param name="includeCommunityPlugins">if set to <c>true</c> [community plugins].</param>
        /// <returns>
        /// The view Models.
        /// </returns>
        internal ObservableCollection<SelectableItemViewModel<Plugin>> GetPlugins(
            Plugins plugins,
            bool includeCommunityPlugins)
        {
            ObservableCollection<SelectableItemViewModel<Plugin>> viewModels = new ObservableCollection<SelectableItemViewModel<Plugin>>();

            foreach (SelectableItemViewModel<Plugin> viewModel in from plugin in plugins.Items
                                                                  where plugin.IsCommunityPlugin == includeCommunityPlugins &&
                                                                        plugin.Frameworks.Contains(this.settingsService.FrameworkType)
                                                                  select new SelectableItemViewModel<Plugin>(plugin))
            {
                viewModels.Add(viewModel);
            }

            return viewModels;
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get { return "MvvmCross Plugins"; }
        }
    }
}