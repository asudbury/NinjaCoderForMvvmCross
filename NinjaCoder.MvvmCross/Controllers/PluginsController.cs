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
    public class PluginsController : BaseController
    {
        /// <summary>
        /// The nuget service.
        /// </summary>
        private readonly INugetService nugetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsController" /> class.
        /// </summary>
        /// <param name="nugetService">The nuget service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="readMeService">The read me service.</param>
        public PluginsController(
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

            if (frameworkType.IsMvvmCrossSolutionType())
            {
                PluginsViewModel viewModel = this.ShowDialog<PluginsViewModel>(new PluginsView());

                if (viewModel.Continue)
                {
                    IEnumerable<Plugin> plugins = viewModel.GetRequiredPlugins();

                    if (plugins.Any())
                    {
                        this.Process(plugins);
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
        internal void Process(IEnumerable<Plugin> plugins)
        {
            TraceService.WriteLine("PluginsController::Process");

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            try
            {
                List<string> messages = new List<string>();
                
                this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

                this.VisualStudioService.DTEService.SaveAll();

                List<string> commands = plugins.Select(plugin => plugin.GetNugetCommandStrings(
                    this.VisualStudioService, 
                    this.SettingsService.UsePreReleaseMvvmCrossNugetPackages)).ToList();

                if (commands.Any())
                {
                    if (this.SettingsService.ProcessNugetCommands)
                    {
                        this.nugetService.Execute(
                            this.GetReadMePath(),
                            commands,
                            this.SettingsService.SuspendReSharperDuringBuild);
                    }
                    
                    string message = NinjaMessages.NugetDownload;

                    if (this.SettingsService.UseLocalNuget)
                    {
                        message += " (using local " + this.SettingsService.LocalNugetName + ")";
                    }

                    this.VisualStudioService.WriteStatusBarMessage(message);
                }

                if (this.SettingsService.OutputNugetCommandsToReadMe)
                {
                    messages.Add(string.Join(Environment.NewLine, commands));
                }

                this.ReadMeService.AddLines(
                        this.GetReadMePath(),
                        "Add MvvmCross Plugins",
                        messages);
            }
            catch (Exception exception)
            {
                TraceService.WriteError("Cannot create plugins exception=" + exception.Message);
            }
        }
    }
}
