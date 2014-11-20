// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using Constants;
    using Entities;
    using Extensions;

    using Scorchio.Infrastructure.Services;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels;
    using Views;

    /// <summary>
    /// Defines the PluginsController type.
    /// </summary>
    internal class PluginsController : BaseController
    {
        /// <summary>
        /// The plugins service.
        /// </summary>
        private readonly IPluginsService pluginsService;

        /// <summary>
        /// The nuget service.
        /// </summary>
        private readonly INugetService nugetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsController" /> class.
        /// </summary>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="nugetService">The nuget service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="readMeService">The read me service.</param>
        public PluginsController(
            IPluginsService pluginsService,
            INugetService nugetService,
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IResolverService resolverService,
            IReadMeService readMeService)
            : base(
            visualStudioService, 
            settingsService, 
            messageBoxService,
            resolverService,
            readMeService)
        {
            TraceService.WriteLine("PluginsController::Constructor");

            this.pluginsService = pluginsService;
            this.nugetService = nugetService;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteHeader("PluginsController::Run");

            //// we open the nuget package manager console so we don't have
            //// a wait condition later!
            this.nugetService.OpenNugetWindow();

            FrameworkType frameworkType = this.VisualStudioService.GetFrameworkType();

            if (frameworkType == FrameworkType.MvvmCross || 
                frameworkType == FrameworkType.XamarinForms ||
                frameworkType == FrameworkType.MvvmCrossAndXamarinForms)
            {
                PluginsViewModel viewModel = this.ShowDialog<PluginsViewModel>(new PluginsView());

                if (viewModel.Continue)
                {
                    IEnumerable<Plugin> plugins = viewModel.GetRequiredPlugins();

                    if (plugins.Any())
                    {
                        this.Process(
                            plugins, 
                            viewModel.ImplementInViewModel, 
                            viewModel.IncludeUnitTests);
                    }
                }
            }
            else
            {
                this.ShowNotMvvmCrossOrXamarinFormsSolutionMessage();
            }
        }

        /// <summary>
        /// Processes the specified form.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <param name="implementInViewModel">if set to <c>true</c> [implement in view model].</param>
        /// <param name="includeUnitTests">if set to <c>true</c> [include unit tests].</param>
        internal void Process(
            IEnumerable<Plugin> plugins,
            string implementInViewModel,
            bool includeUnitTests)
        {
            TraceService.WriteLine("PluginsController::Process");

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            try
            {
                IEnumerable<string> messages = this.pluginsService.AddPlugins(
                    this.VisualStudioService,
                    plugins,
                    implementInViewModel,
                    includeUnitTests);
                
                this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

                this.VisualStudioService.DTEService.SaveAll();

                List<string> commands = plugins.Select(plugin => plugin.GetNugetCommandStrings(this.VisualStudioService)).ToList();

                if (commands.Any())
                {
                    if (SettingsService.ProcessNugetCommands)
                    {
                        this.nugetService.Execute(
                            this.GetReadMePath(),
                            commands);
                    }

                    this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NugetDownload);
                }

                //// show the readme.
                this.ShowReadMe("Add Plugins", messages);
            }
            catch (Exception exception)
            {
                TraceService.WriteError("Cannot create plugins exception=" + exception.Message);
            }
        }
    }
}
