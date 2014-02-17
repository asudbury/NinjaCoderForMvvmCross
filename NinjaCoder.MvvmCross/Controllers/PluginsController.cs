// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Constants;

    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.ViewModels;
    using NinjaCoder.MvvmCross.Views;

    using Scorchio.Infrastructure.Services;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;

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
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="nugetService">The nuget service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="resolverService">The resolver service.</param>
        public PluginsController(
            IConfigurationService configurationService,
            IPluginsService pluginsService,
            INugetService nugetService,
            IVisualStudioService visualStudioService,
            IReadMeService readMeService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IResolverService resolverService)
            : base(
            configurationService,
            visualStudioService, 
            readMeService, 
            settingsService, 
            messageBoxService,
            resolverService)
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
            this.nugetService.OpenNugetWindow(this.VisualStudioService);

            if (this.VisualStudioService.IsMvvmCrossSolution)
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
                this.ShowNotMvvmCrossSolutionMessage();
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
                if (this.SettingsService.SuspendReSharperDuringBuild)
                {
                    this.VisualStudioService.DTEService.ExecuteCommand(Settings.SuspendReSharperCommand);
                }

                IEnumerable<string> messages = this.pluginsService.AddPlugins(
                    this.VisualStudioService,
                    plugins,
                    implementInViewModel,
                    includeUnitTests,
                    this.SettingsService.UseNugetForPlugins);
                
                this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

                this.VisualStudioService.DTEService.SaveAll();

                IEnumerable<string> commands = plugins.SelectMany(x => x.NugetCommands).ToList();

                if (commands.Any())
                {
                    if (SettingsService.ProcessNugetCommands)
                    {
                        this.nugetService.Execute(
                            this.VisualStudioService,
                            this.GetReadMePath(),
                            commands,
                            this.SettingsService.SuspendReSharperDuringBuild);
                    }

                    this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NugetDownload);
                }
                else
                {
                    this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.PluginsCompleted);

                    if (this.SettingsService.SuspendReSharperDuringBuild)
                    {
                        this.VisualStudioService.DTEService.ExecuteCommand(Settings.ResumeReSharperCommand);
                    }
                }

                //// show the readme.
                this.ShowReadMe("Add Plugins", messages, this.SettingsService.UseNugetForPlugins);
            }
            catch (Exception exception)
            {
                TraceService.WriteError("Cannot create plugins exception=" + exception.Message);
            }
        }
    }
}
