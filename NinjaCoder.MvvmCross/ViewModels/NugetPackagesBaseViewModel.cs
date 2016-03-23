// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the NugetPackagesBaseViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels
{
    using Entities;
    using Factories.Interfaces;
    using Scorchio.Infrastructure.Wpf.ViewModels;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    ///  Defines the NugetPackagesBaseViewModel type.
    /// </summary>
    public abstract class NugetPackagesBaseViewModel : BaseWizardStepViewModel
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
        /// The project factory.
        /// </summary>
        private readonly IProjectFactory projectFactory;

        /// <summary>
        /// The plugin factory.
        /// </summary>
        private readonly IPluginFactory pluginFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetPackagesBaseViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="projectFactory">The project factory.</param>
        /// <param name="pluginFactory">The plugin factory.</param>
        protected NugetPackagesBaseViewModel(
            ISettingsService settingsService,
            IPluginsService pluginsService,
            IProjectFactory projectFactory,
            IPluginFactory pluginFactory)
        {
            this.settingsService = settingsService;
            this.pluginsService = pluginsService;
            this.projectFactory = projectFactory;
            this.pluginFactory = pluginFactory;
        }

        /// <summary>
        /// Gets the nuget actions.
        /// </summary>
        /// <returns></returns>
        public NugetActions GetNugetActions()
        {
            TraceService.WriteLine("NugetPackagesBaseViewModel::GetNugetActions");

            NugetActions nugetActions = new NugetActions
            {
                NugetCommands = this.GetNugetCommands(),
                PostNugetCommands = this.pluginsService.GetPostNugetCommands(this.GetRequiredNugetPackages()),
                PostNugetFileOperations = this.pluginsService.GetPostNugetFileOperations(this.GetRequiredNugetPackages()),
            };

            return nugetActions;
        }

        /// <summary>
        /// Gets the route modifier.
        /// </summary>
        /// <returns></returns>
        protected RouteModifier GetRouteModifier()
        {
            return this.projectFactory.GetRouteModifier(this.settingsService.FrameworkType);
        }

        /// <summary>
        /// Gets the nuget packages URI.
        /// </summary>
        protected abstract string NugetPackagesUri { get; }

        /// <summary>
        /// Gets the get nuget packages.
        /// </summary>
        protected abstract IEnumerable<SelectableItemViewModel<Plugin>> NugetPackages { get; }

        /// <summary>
        /// Gets the required nuget packages.
        /// </summary>
        /// <returns></returns>
        protected IEnumerable<Plugin> GetRequiredNugetPackages()
        {
            TraceService.WriteLine("NugetPackagesBaseViewModel::GetRequiredNugetPackages");

            if (this.NugetPackages != null)
            {
                return this.NugetPackages.ToList().Where(x => x.IsSelected).Select(x => x.Item).ToList();
            }

            return new List<Plugin>();
        }
        
        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        /// <returns></returns>
        protected string GetNugetCommands()
        {
            TraceService.WriteLine("NugetPackagesBaseViewModel::GetNugetCommands");

            string commands = string.Empty;

            IEnumerable<Plugin> requiredPlugins = this.GetRequiredNugetPackages();

            if (requiredPlugins != null)
            {
                List<Plugin> plugins = requiredPlugins.ToList();

                if (plugins.Any())
                {
                    commands += string.Join(
                        Environment.NewLine,
                        this.pluginsService.GetNugetCommands(
                        plugins,
                        this.settingsService.UsePreReleaseXamarinFormsNugetPackages));
                }
            }

            return commands;
        }

        /// <summary>
        /// Gets the plugins.
        /// </summary>
        protected Plugins GetPlugins()
        {
            return this.pluginFactory.GetPlugins(this.NugetPackagesUri);
        }

        /// <summary>
        /// Gets the plugins.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns></returns>
        protected Plugins GetPlugins(string uri)
        {
            return this.pluginFactory.GetPlugins(uri);
        }

        /// <summary>
        /// Gets the category nuget packages.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <param name="category">The category.</param>
        /// <returns></returns>
        protected ObservableCollection<SelectableItemViewModel<Plugin>> GetCategoryNugetPackages(
            Plugins plugins,
            string category)
        {
            if (plugins == null)
            {
                return new ObservableCollection<SelectableItemViewModel<Plugin>>();
            }

            ObservableCollection<SelectableItemViewModel<Plugin>> viewModels = new ObservableCollection<SelectableItemViewModel<Plugin>>();

            foreach (SelectableItemViewModel<Plugin> viewModel in from plugin in plugins.Items
                                                                  where plugin.Frameworks.Contains(this.settingsService.FrameworkType) &&
                                                                  plugin.Category == category
                                                                  select new SelectableItemViewModel<Plugin>(plugin))
            {
                viewModels.Add(viewModel);
            }

            return new ObservableCollection<SelectableItemViewModel<Plugin>>(viewModels.OrderBy(x => x.Item.FriendlyName));
        }
    }
}