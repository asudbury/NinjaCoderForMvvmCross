// ---------------------------------- ----------------------------------------------------------------------------------
// <summary>
//    Defines the NinjaCoderOptionsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddProjects
{
    using Entities;
    using Factories.Interfaces;
    using Scorchio.Infrastructure.Wpf.ViewModels;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    ///  Defines the NinjaCoderOptionsViewModel type.
    /// </summary>
    public class NinjaCoderOptionsViewModel : NugetPackagesBaseViewModel
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The caching service.
        /// </summary>
        private readonly ICachingService cachingService;

        /// <summary>
        /// The ninja commumity plugins.
        /// </summary>
        private ObservableCollection<SelectableItemViewModel<Plugin>> ninjaCommumityPlugins;

        /// <summary>
        /// The ninja plugins.
        /// </summary>
        private ObservableCollection<SelectableItemViewModel<Plugin>> ninjaPlugins;

        /// <summary>
        /// The local plugins.
        /// </summary>
        private ObservableCollection<SelectableItemViewModel<Plugin>> localPlugins;

        /// <summary>
        /// The display ninja plugins.
        /// </summary>
        private bool displayNinjaPlugins;

        /// <summary>
        /// The display community plugins.
        /// </summary>
        private bool displayCommunityPlugins;

        /// <summary>
        /// The display local plugins.
        /// </summary>
        private bool displayLocalPlugins;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjaCoderOptionsViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="pluginFactory">The plugin factory.</param>
        /// <param name="cachingService">The caching service.</param>
        /// <param name="projectFactory">The project factory.</param>
        public NinjaCoderOptionsViewModel(
            ISettingsService settingsService,
            IPluginsService pluginsService,
            IPluginFactory pluginFactory,
            ICachingService cachingService,
            IProjectFactory projectFactory)
            : base(
                settingsService,
                pluginsService,
                projectFactory,
                pluginFactory)
        {
            this.settingsService = settingsService;
            this.cachingService = cachingService;
        }

        /// <summary>
        /// Gets or sets the ninja commumity plugins.
        /// </summary>
        public ObservableCollection<SelectableItemViewModel<Plugin>> NinjaCommumityPlugins
        {
            get { return this.ninjaCommumityPlugins; }
            set { this.SetProperty(ref this.ninjaCommumityPlugins, value); }
        }

        /// <summary>
        /// Gets or sets the ninja plugins.
        /// </summary>
        public ObservableCollection<SelectableItemViewModel<Plugin>> NinjaPlugins
        {
            get { return this.ninjaPlugins; }
            set { this.SetProperty(ref this.ninjaPlugins, value); }
        }

        /// <summary>
        /// Gets or sets the local plugins.
        /// </summary>
        /// <value>
        /// The local plugins.
        /// </value>
        public ObservableCollection<SelectableItemViewModel<Plugin>> LocalPlugins
        {
            get { return this.localPlugins; }
            set { this.SetProperty(ref this.localPlugins, value); }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get { return "Ninja Coder Options"; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [display ninja plugins].
        /// </summary>
        public bool DisplayNinjaPlugins
        {
            get { return this.displayNinjaPlugins; }
            set { this.SetProperty(ref this.displayNinjaPlugins, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [display community plugins].
        /// </summary>
        public bool DisplayCommunityPlugins
        {
            get { return this.displayCommunityPlugins; }
            set { this.SetProperty(ref this.displayCommunityPlugins, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [display local plugins].
        /// </summary>
        public bool DisplayLocalPlugins
        {
            get { return this.displayLocalPlugins; }
            set { this.SetProperty(ref this.displayLocalPlugins, value); }
        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        public override void OnInitialize()
        {
            if (this.NinjaPlugins == null)
            {
                //// TODO : part of this code is repeated in the ApplicationOptionsViewModel - refactor!

                Plugins plugins = this.GetPlugins(this.settingsService.NinjaNugetPackagesUri);
                this.NinjaPlugins = this.GetCategoryNugetPackages(plugins, string.Empty);

                this.DisplayNinjaPlugins = this.NinjaPlugins.Count > 0;

                plugins = this.GetPlugins(this.settingsService.NinjaCommunityNugetPackagesUri);
                this.NinjaCommumityPlugins = this.GetCategoryNugetPackages(plugins, string.Empty);

                this.DisplayCommunityPlugins = this.NinjaCommumityPlugins.Count > 0;
                
                plugins = this.GetPlugins(this.settingsService.LocalNugetPackagesUri);
                this.LocalPlugins = this.GetCategoryNugetPackages(plugins, string.Empty);

                this.DisplayLocalPlugins = this.LocalPlugins.Count > 0;
            }
        }

        /// <summary>
        /// For when yous need to save some values that can't be directly bound to UI elements.
        /// Not called when moving previous (see WizardViewModel.MoveToNextStep).
        /// </summary>
        /// <returns>
        /// An object that may modify the route
        /// </returns>
        public override RouteModifier OnNext()
        {
            List<Plugin> samplePlugins = new List<Plugin>();

            IEnumerable<Plugin> plugins = this.GetRequiredNugetPackages();

            foreach (Plugin plugin in plugins.Where(plugin => plugin.NinjaSamples.Any()))
            {
                samplePlugins.AddRange(plugin.NinjaSamples);
            }

            this.cachingService.ApplicationSamplePlugIns.ToList().AddRange(samplePlugins); 

            return this.GetRouteModifier();
        }

        /// <summary>
        /// Gets the nuget packages URI.
        /// </summary>
        protected override string NugetPackagesUri
        {
            get { return this.settingsService.NugetPackagesUri; }
        }

        /// <summary>
        /// Gets the get nuget packages.
        /// </summary>
        protected override IEnumerable<SelectableItemViewModel<Plugin>> NugetPackages
        {
            get 
            { 
                return this.NinjaPlugins == null ? new List<SelectableItemViewModel<Plugin>>() : 
                    this.NinjaCommumityPlugins.Union(this.NinjaPlugins).Union(this.LocalPlugins); 
            }
        }
    }
}
