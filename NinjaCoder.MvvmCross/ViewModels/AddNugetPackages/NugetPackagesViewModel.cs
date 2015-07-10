// ------------------------------------- -------------------------------------------------------------------------------
// <summary>
//    Defines the NugetPackagesViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddNugetPackages
{
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.UserControls.AddProjects;
    using Scorchio.Infrastructure.Wpf;
    using Scorchio.Infrastructure.Wpf.ViewModels;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Services;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Windows.Input;

    /// <summary>
    ///  Defines the NugetPackagesViewModel type.
    /// </summary>
    public class NugetPackagesViewModel : BaseWizardStepViewModel
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
        /// The plugins service.
        /// </summary>
        private readonly IPluginsService pluginsService;

        /// <summary>
        /// The plugin factory.
        /// </summary>
        private readonly IPluginFactory pluginFactory;

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
        /// <param name="pluginFactory">The plugin factory.</param>
        public NugetPackagesViewModel(
            IApplicationService applicationService,
            ISettingsService settingsService,
            IPluginsService pluginsService,
            IPluginFactory pluginFactory)
        {
            TraceService.WriteLine("NugetServicesViewModel::Constructor Start");

            this.applicationService = applicationService;
            this.settingsService = settingsService;
            this.pluginsService = pluginsService;
            this.pluginFactory = pluginFactory;

            TraceService.WriteLine("NugetServicesViewModel::Constructor End");
        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        public override void OnInitialize()
        {
            Plugins corePlugins = this.pluginFactory.GetPlugins(this.settingsService.NugetPackagesUri);
            this.CoreNugetPackages = this.GetPackages(corePlugins);

            Plugins formsPlugins = this.pluginFactory.GetPlugins(this.settingsService.XamarinFormsNugetPackagesUri);
            this.FormsNugetPackages = this.GetPackages(formsPlugins);

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
            IEnumerable<Plugin> plugins = this.GetRequiredPackages();

            if (plugins.FirstOrDefault(x => x.FriendlyName.Contains("Xamarin Forms Labs")) == null)
            {
                return new RouteModifier
                {
                    ExcludeViewTypes = new List<Type>
                                           {
                                               typeof(XamarinFormsLabsControl), 
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
        /// Gets the required packages.
        /// </summary>
        /// <returns>The required packages.</returns>
        public IEnumerable<Plugin> GetRequiredPackages()
        {
            TraceService.WriteLine("NugetPackagesViewModel::GetRequiredPackages");

            IEnumerable<SelectableItemViewModel<Plugin>> viewModels = this.CoreNugetPackages.Concat(this.FormsNugetPackages);
                                   
            return viewModels.ToList()
                .Where(x => x.IsSelected)
                .Select(x => x.Item).ToList();
        }

        /// <summary>
        /// Gets the nuget web site command.
        /// </summary>
        public ICommand NugetWebSiteCommand
        {
            get { return new RelayCommand(this.DisplayWebPage); }
        }


        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        /// <returns></returns>
        public string GetNugetCommands()
        {
            List<Plugin> requiredPlugins = this.GetRequiredPackages().ToList();

            string commands = string.Empty;

            if (requiredPlugins.Any())
            {
                commands += string.Join(
                    Environment.NewLine,
                    this.pluginsService.GetNugetCommands(requiredPlugins,
                    this.settingsService.UsePreReleaseXamarinFormsNugetPackages));
            }

            return commands;
        }

        /// <summary>
        /// Gets the nuget messages.
        /// </summary>
        /// <returns></returns>
        public List<string> GetNugetMessages()
        {
            List<Plugin> requiredPlugins = this.GetRequiredPackages().ToList();

            List<string> messages = new List<string>();

            if (requiredPlugins.Any())
            {
                messages.Add(string.Empty);

                messages.AddRange(this.pluginsService.GetNugetMessages(requiredPlugins));
            }

            return messages;
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

            foreach (SelectableItemViewModel<Plugin> viewModel in from plugin in plugins.Items
                                                                  where plugin.Frameworks.Contains(this.settingsService.FrameworkType)
                                                                  select new SelectableItemViewModel<Plugin>(plugin))
            {
                viewModels.Add(viewModel);
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
