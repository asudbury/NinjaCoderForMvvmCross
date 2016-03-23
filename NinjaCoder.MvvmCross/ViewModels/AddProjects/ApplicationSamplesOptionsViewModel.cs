// ---------------------------------- ----------------------------------------------------------------------------------
// <summary>
//    Defines the ApplicationSamplesOptionsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddProjects
{
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.Infrastructure.Wpf.ViewModels;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    ///  Defines the ApplicationSamplesOptionsViewModel type.
    /// </summary>
    public class ApplicationSamplesOptionsViewModel : NugetPackagesBaseViewModel
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
        /// The samples plugins.
        /// </summary>
        private ObservableCollection<SelectableItemViewModel<Plugin>> samplesPlugins;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationSamplesOptionsViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="cachingService">The caching service.</param>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="projectFactory">The project factory.</param>
        /// <param name="pluginFactory">The plugin factory.</param>
        public ApplicationSamplesOptionsViewModel(
            ISettingsService settingsService,
            ICachingService cachingService,
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
            this.cachingService = cachingService;
        }

        /// <summary>
        /// Gets or sets the samples plugins.
        /// </summary>
        public ObservableCollection<SelectableItemViewModel<Plugin>> SamplesPlugins
        {
            get { return this.samplesPlugins; }
            set { this.SetProperty(ref this.samplesPlugins, value); }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get { return "Application Sample Options"; }
        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        public override void OnInitialize()
        {
            if (this.SamplesPlugins == null)
            {
                this.SamplesPlugins = this.GetPlugins(this.cachingService.ApplicationSamplePlugIns);
            }
        }

        /// <summary>
        /// Gets the nuget packages URI.
        /// </summary>
        /// <exception cref="System.NotImplementedException">Exception</exception>
        protected override string NugetPackagesUri
        {
            get { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Gets the get nuget packages.
        /// </summary>
        protected override IEnumerable<SelectableItemViewModel<Plugin>> NugetPackages
        {
            get { return this.SamplesPlugins ?? (IEnumerable<SelectableItemViewModel<Plugin>>)new List<SelectableItemViewModel<Plugin>>(); }
        }

        /// <summary>
        /// Gets the plugins.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <returns>
        /// The view Models.
        /// </returns>
        internal ObservableCollection<SelectableItemViewModel<Plugin>> GetPlugins(IEnumerable<Plugin> plugins)
        {
            ObservableCollection<SelectableItemViewModel<Plugin>> viewModels = new ObservableCollection<SelectableItemViewModel<Plugin>>();

            if (plugins == null)
            {
                return viewModels;
            }

            foreach (SelectableItemViewModel<Plugin> viewModel in from plugin in plugins
                                                                  where plugin.Frameworks.Contains(this.settingsService.FrameworkType) 
                                                                  select new SelectableItemViewModel<Plugin>(plugin))
            {
                viewModels.Add(viewModel);
            }

            return viewModels;
        }
    }
}
