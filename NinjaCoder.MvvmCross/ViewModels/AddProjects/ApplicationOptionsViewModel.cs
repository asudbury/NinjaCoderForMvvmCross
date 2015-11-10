// ---------------------------------- ----------------------------------------------------------------------------------
// <summary>
//    Defines the ApplicationOptionsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddProjects
{
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.Infrastructure.Wpf.ViewModels;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    ///  Defines the ApplicationOptionsViewModel type.
    /// </summary>
    public class ApplicationOptionsViewModel : NugetPackagesBaseViewModel
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
        /// The authentication plugins.
        /// </summary>
        private ObservableCollection<SelectableItemViewModel<Plugin>> authenticationPlugins;

        /// <summary>
        /// The local storage plugins.
        /// </summary>
        private ObservableCollection<SelectableItemViewModel<Plugin>> localStoragePlugins;

        /// <summary>
        /// The cloud services plugins.
        /// </summary>
        private ObservableCollection<SelectableItemViewModel<Plugin>> cloudServicesPlugins;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationOptionsViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="pluginFactory">The plugin factory.</param>
        /// <param name="cachingService">The caching service.</param>
        /// <param name="projectFactory">The project factory.</param>
        public ApplicationOptionsViewModel(
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
        /// Gets or sets the authentication plugins.
        /// </summary>
        public ObservableCollection<SelectableItemViewModel<Plugin>> AuthenticationPlugins
        {
            get { return this.authenticationPlugins; }
            set { this.SetProperty(ref this.authenticationPlugins, value); }
        }

        /// <summary>
        /// Gets or sets the local storage plugins.
        /// </summary>
        public ObservableCollection<SelectableItemViewModel<Plugin>> LocalStoragePlugins
        {
            get { return this.localStoragePlugins; }
            set { this.SetProperty(ref this.localStoragePlugins, value); }
        }

        /// <summary>
        /// Gets or sets the cloud services plugins.
        /// </summary>
        public ObservableCollection<SelectableItemViewModel<Plugin>> CloudServicesPlugins
        {
            get { return this.cloudServicesPlugins; }
            set { this.SetProperty(ref this.cloudServicesPlugins, value); }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get { return "Application Options"; }
        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        public override void OnInitialize()
        {
            if (this.AuthenticationPlugins == null)
            {
                this.cachingService.ApplicationSamplePlugIns = new List<Plugin>();

                Plugins allPlugins = this.GetPlugins();

                this.AuthenticationPlugins = this.GetCategoryNugetPackages(allPlugins, "Authentication");
                this.LocalStoragePlugins = this.GetCategoryNugetPackages(allPlugins, "LocalStorage");
                this.CloudServicesPlugins = this.GetCategoryNugetPackages(allPlugins, "CloudServices");

                this.cachingService.HasNinjaNugetPackages = this.GetCategoryNugetPackages(allPlugins, "NinjaCoder").Any();
                this.cachingService.HasNinjaCommunityNugetPackages = this.GetCategoryNugetPackages(allPlugins, "NinjaCoderCommunity").Any();

                //// TODO : this code is repeated in the NinjaCoderOptionsViewModel - refactor!
                Plugins plugins = this.GetPlugins(this.settingsService.NinjaNugetPackagesUri);

                if (plugins != null)
                {
                    this.cachingService.HasNinjaNugetPackages = this.GetCategoryNugetPackages(plugins, string.Empty).Any();
                }

                plugins = this.GetPlugins(this.settingsService.NinjaCommunityNugetPackagesUri);

                if (plugins != null)
                {
                    this.cachingService.HasNinjaCommunityNugetPackages = this.GetCategoryNugetPackages(plugins, string.Empty).Any();
                }

                plugins = this.GetPlugins(this.settingsService.LocalNugetPackagesUri);

                if (plugins != null)
                {
                    this.cachingService.HasLocalNugetPackages = this.GetCategoryNugetPackages(plugins, string.Empty).Any();
                }
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

            this.cachingService.ApplicationSamplePlugIns = samplePlugins;

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
                    return this.LocalStoragePlugins == null ? new List<SelectableItemViewModel<Plugin>>() : 
                    this.LocalStoragePlugins.Union(this.CloudServicesPlugins).Union(this.AuthenticationPlugins); 
            }
        }
    }
}
