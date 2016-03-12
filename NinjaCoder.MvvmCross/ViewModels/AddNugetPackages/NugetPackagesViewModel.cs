// ------------------------------------- -------------------------------------------------------------------------------
// <summary>
//    Defines the NugetPackagesViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddNugetPackages
{
    using Entities;
    using Factories.Interfaces;
    using Scorchio.Infrastructure.Wpf;
    using Scorchio.Infrastructure.Wpf.ViewModels;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Input;
    using UserControls.AddProjects;

    /// <summary>
    ///  Defines the NugetPackagesViewModel type.
    /// </summary>
    public class NugetPackagesViewModel : NugetPackagesBaseViewModel
    {
        /// <summary>
        /// The application service.
        /// </summary>
        private readonly IApplicationService applicationService;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The plugin factory.
        /// </summary>
        private readonly IPluginFactory pluginFactory;

        /// <summary>
        /// The caching service.
        /// </summary>
        private readonly ICachingService cachingService;

        /// <summary>
        /// The core nuget packages.
        /// </summary>
        private ObservableCollection<SelectableItemViewModel<Plugin>> coreNugetPackages;

        /// <summary>
        /// The forms nuget packages.
        /// </summary>
        private ObservableCollection<SelectableItemViewModel<Plugin>> formsNugetPackages;

        /// <summary>
        /// The display forms nuget packages.
        /// </summary>
        private bool displayFormsNugetPackages;

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetPackagesViewModel" /> class.
        /// </summary>
        /// <param name="applicationService">The application service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="projectFactory">The project factory.</param>
        /// <param name="pluginFactory">The plugin factory.</param>
        /// <param name="cachingService">The caching service.</param>
        public NugetPackagesViewModel(
            IApplicationService applicationService,
            ISettingsService settingsService,
            IPluginsService pluginsService,
            IProjectFactory projectFactory,
            IPluginFactory pluginFactory,
            ICachingService cachingService)
            : base(
                settingsService,
                pluginsService,
                projectFactory,
                pluginFactory)
        {
            this.applicationService = applicationService;
            this.settingsService = settingsService;
            this.pluginFactory = pluginFactory;
            this.cachingService = cachingService;
        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        public override void OnInitialize()
        {
            if (this.CoreNugetPackages == null)
            {
                Plugins corePlugins = this.pluginFactory.GetPlugins(this.settingsService.NugetPackagesUri);
                this.CoreNugetPackages = this.GetPackages(corePlugins);
            }

            if (this.FormsNugetPackages == null)
            {
                Plugins formsPlugins = this.pluginFactory.GetPlugins(this.settingsService.XamarinFormsNugetPackagesUri);
                this.FormsNugetPackages = this.GetPackages(formsPlugins);
            }

            FrameworkType frameworkType = this.applicationService.GetApplicationFramework();

            if (frameworkType == FrameworkType.XamarinForms ||
                frameworkType == FrameworkType.MvvmCrossAndXamarinForms)
            {
                this.DisplayFormsNugetPackages = true;
            }
            else
            {
                this.DisplayFormsNugetPackages = false;
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
            this.cachingService.XamarinFormsLabsNugetPackageRequested = true;

            IEnumerable<Plugin> plugins = this.GetRequiredNugetPackages();

            if (plugins.FirstOrDefault(x => x.FriendlyName.Contains("Xamarin Forms Labs")) == null)
            {
                this.cachingService.XamarinFormsLabsNugetPackageRequested = false;
                return new RouteModifier
                {
                    ExcludeViewTypes = new List<Type>
                                           {
                                               typeof(XamarinFormsLabsControl)
                                           }
                };
            }

            return new RouteModifier();
        }

        /// <summary>
        /// Gets or sets the core nuget packages.
        /// </summary>
        public ObservableCollection<SelectableItemViewModel<Plugin>> CoreNugetPackages
        {
            get { return this.coreNugetPackages; }
            set { this.SetProperty(ref this.coreNugetPackages, value); }
        }

        /// <summary>
        /// Gets or sets the forms nuget packages.
        /// </summary>
        public ObservableCollection<SelectableItemViewModel<Plugin>> FormsNugetPackages
        {
            get { return this.formsNugetPackages; }
            set { this.SetProperty(ref this.formsNugetPackages, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to display forms nuget packages.
        /// </summary>
        public bool DisplayFormsNugetPackages
        {
            get { return this.displayFormsNugetPackages; }
            set { this.SetProperty(ref this.displayFormsNugetPackages, value); }
        }

        /// <summary>
        /// Gets the nuget web site command.
        /// </summary>
        public ICommand NugetWebSiteCommand
        {
            get { return new RelayCommand(this.DisplayWebPage); }
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
            get { return this.CoreNugetPackages.Concat(this.FormsNugetPackages); }
        }

        /// <summary>
        /// Displays the web page.
        /// </summary>
        internal void DisplayWebPage()
        {
            Process.Start(this.settingsService.NugetWebsitePage);
        }
        
        /// <summary>
        /// Gets the packages.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <returns></returns>
        internal ObservableCollection<SelectableItemViewModel<Plugin>> GetPackages(Plugins plugins)
        {
            ObservableCollection<SelectableItemViewModel<Plugin>> viewModels = new ObservableCollection<SelectableItemViewModel<Plugin>>();

            if (plugins != null)
            { 
                foreach (SelectableItemViewModel<Plugin> viewModel in from plugin in plugins.Items
                                                                      where plugin.Frameworks.Contains(this.settingsService.FrameworkType) &&
                                                                      plugin.Category == string.Empty
                                                                      select new SelectableItemViewModel<Plugin>(plugin))
                {
                    viewModels.Add(viewModel);
                }
            }

            return new ObservableCollection<SelectableItemViewModel<Plugin>>(viewModels.OrderBy(x => x.Item.FriendlyName));
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get { return "Nuget Packages"; }
        }
    }
}
