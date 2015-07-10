// ------------------------------------- -------------------------------------------------------------------------------
// <summary>
//    Defines the XamarinFormsLabsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddNugetPackages
{
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
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
    ///  Defines the XamarinFormsLabsViewModel type.
    /// </summary>
    public class XamarinFormsLabsViewModel : BaseWizardStepViewModel
    {
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
        /// The plugins.
        /// </summary>
        private ObservableCollection<SelectableItemViewModel<Plugin>> plugins;

        /// <summary>
        /// Initializes a new instance of the <see cref="XamarinFormsLabsViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="pluginFactory">The plugin factory.</param>
        public XamarinFormsLabsViewModel(
            ISettingsService settingsService,
            IPluginsService pluginsService,
            IPluginFactory pluginFactory)
        {
            TraceService.WriteLine("XamarinFormsLabsViewModel::Constructor Start");

            this.settingsService = settingsService;
            this.pluginsService = pluginsService;
            this.pluginFactory = pluginFactory;

            TraceService.WriteLine("XamarinFormsLabsViewModel::Constructor End");
        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        public override void OnInitialize()
        {
            Plugins allPackages = this.pluginFactory.GetPlugins(this.settingsService.XamarinFormsLabsPluginsUri);
            this.Plugins = this.GetPlugins(allPackages);
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
        /// Gets the required plugins.
        /// </summary>
        /// <returns>The required plugins.</returns>
        public IEnumerable<Plugin> GetRequiredPlugins()
        {
            TraceService.WriteLine("XamarinFormsLabsViewModel::GetRequiredPlugins");

            IEnumerable<SelectableItemViewModel<Plugin>> viewModels = this.plugins;
            
            if (this.plugins != null)
            {                       
                return viewModels.ToList()
                    .Where(x => x.IsSelected)
                    .Select(x => x.Item).ToList();
            }

            return new List<Plugin>();
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
        /// Gets the nuget commands.
        /// </summary>
        /// <returns></returns>
        public string GetNugetCommands()
        {
            List<Plugin> requiredPlugins = this.GetRequiredPlugins().ToList();

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
            List<Plugin> requiredPlugins = this.GetRequiredPlugins().ToList();

            List<string> messages = new List<string>();

            if (requiredPlugins.Any())
            {
                messages.Add(string.Empty);

                messages.AddRange(this.pluginsService.GetNugetMessages(requiredPlugins));
            }

            return messages;
        }

        /// <summary>
        /// Displays the git hub page.
        /// </summary>
        internal void DisplayGitHubPage()
        {
            Process.Start(this.settingsService.XamarinFormsLabsNugetPackagesGitHubPage);
        }

        /// <summary>
        /// Gets the plugins.
        /// </summary>
        /// <param name="selectedPlugins">The selected plugins.</param>
        /// <returns></returns>
        internal ObservableCollection<SelectableItemViewModel<Plugin>> GetPlugins(Plugins selectedPlugins)
        {
            ObservableCollection<SelectableItemViewModel<Plugin>> viewModels = new ObservableCollection<SelectableItemViewModel<Plugin>>();

            foreach (SelectableItemViewModel<Plugin> viewModel in from plugin in selectedPlugins.Items
                                                                  where plugin.Frameworks.Contains(this.settingsService.FrameworkType)
                                                                  select new SelectableItemViewModel<Plugin>(plugin))
            {
                viewModels.Add(viewModel);
            }

            return new ObservableCollection<SelectableItemViewModel<Plugin>>(viewModels.OrderBy(x => x.Item.FriendlyName));
        }
    }
}
