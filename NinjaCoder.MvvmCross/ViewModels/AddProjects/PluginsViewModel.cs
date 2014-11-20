// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddProjects
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Input;

    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.Infrastructure.Wpf;
    using Scorchio.Infrastructure.Wpf.ViewModels;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the PluginsViewModel type.
    /// </summary>
    public class PluginsViewModel : BaseWizardStepViewModel
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The plugin factory.
        /// </summary>
        private readonly IPluginFactory pluginFactory;

        /// <summary>
        /// The core plugins.
        /// </summary>
        private ObservableCollection<SelectableItemViewModel<Plugin>> corePlugins;

        /// <summary>
        /// The community plugins.
        /// </summary>
        private ObservableCollection<SelectableItemViewModel<Plugin>> communityPlugins;

        /// <summary>
        /// The core plugins selected
        /// </summary>
        private bool corePluginsSelected;

        /// <summary>
        /// The community plugins selected
        /// </summary>
        public bool communityPluginsSelected;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="pluginFactory">The plugin factory.</param>
        public PluginsViewModel(
            ISettingsService settingsService,
            IPluginFactory pluginFactory)
        {
            TraceService.WriteLine("PluginsViewModel::Constructor Start");

            this.settingsService = settingsService;
            this.pluginFactory = pluginFactory;

            TraceService.WriteLine("PluginsViewModel::Constructor End");
        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        public override void OnInitialize()
        {
            if (this.settingsService.FrameworkType != FrameworkType.NoFramework)
            {
                Plugins allPlugins = this.pluginFactory.GetPlugins(this.settingsService.PluginsUri);

                this.CorePlugins = this.GetPlugins(allPlugins, false);
                this.CommunityPlugins = this.GetPlugins(allPlugins, true);

                if (this.corePlugins.Any() == false && 
                    this.communityPlugins.Any())
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
        /// Gets or sets a value indicating whether [core plugins selected].
        /// </summary>
        public bool CorePluginsSelected
        {
            get { return this.corePluginsSelected; }
            set { this.corePluginsSelected = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [community plugins selected].
        /// </summary>
        public bool CommunityPluginsSelected
        {
            get { return this.communityPluginsSelected; }
            set { this.communityPluginsSelected = value; }
        }

        /// <summary>
        /// Gets the core plugins.
        /// </summary>
        public ObservableCollection<SelectableItemViewModel<Plugin>> CorePlugins
        {
            get { return this.corePlugins; }
            set { this.SetProperty(ref this.corePlugins, value); }
        }

        /// <summary>
        /// Gets the community plugins.
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
        /// Gets the required plugins.
        /// </summary>
        /// <returns>The required plugins.</returns>
        public IEnumerable<Plugin> GetRequiredPlugins()
        {
            TraceService.WriteLine("PluginsViewModel::GetRequiredPlugins");

            IEnumerable<SelectableItemViewModel<Plugin>> viewModels = this.CorePlugins
                                                                .Union(this.CommunityPlugins);

            return viewModels.ToList()
                .Where(x => x.IsSelected)
                .Select(x => x.Item).ToList();
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
