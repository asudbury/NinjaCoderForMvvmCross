// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ServicesController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System.Collections.Generic;
    using System.Linq;

    using Constants;

    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.ViewModels;
    using NinjaCoder.MvvmCross.Views;

    using Scorchio.Infrastructure.Services;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Services.Interfaces;

    /// <summary>
    /// Defines the ServicesController type.
    /// </summary>
    internal class ServicesController : BaseController
    {
        /// <summary>
        /// The services service.
        /// </summary>
        private readonly IServicesService servicesService;

        /// <summary>
        /// The nuget service.
        /// </summary>
        private readonly INugetService nugetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesController" /> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="servicesService">The services service.</param>
        /// <param name="nugetService">The nuget service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="resolverService">The resolver service.</param>
        public ServicesController(
            IConfigurationService configurationService,
            IServicesService servicesService,
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
            TraceService.WriteLine("ServicesController::Constructor");

            this.servicesService = servicesService;
            this.nugetService = nugetService;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteHeader("ServicesController::Run");

            //// we open the nuget package manager console so we don't have
            //// a wait condition later!
            this.nugetService.OpenNugetWindow(this.VisualStudioService);

            if (this.VisualStudioService.IsMvvmCrossSolution)
            {
                ServicesViewModel viewModel = this.ShowDialog<ServicesViewModel>(new ServicesView());

                if (viewModel.Continue)
                {
                    this.Process(
                        viewModel.GetRequiredServices(), 
                        viewModel.ImplementInViewModel, 
                        viewModel.IncludeUnitTests);
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
        /// <param name="templateInfos">The template infos.</param>
        /// <param name="implementInViewModel">The implement in view model.</param>
        /// <param name="includeUnitTests">if set to <c>true</c> [include unit tests].</param>
        internal void Process(
            IEnumerable<ItemTemplateInfo> templateInfos,
            string implementInViewModel,
            bool includeUnitTests)
        {
            TraceService.WriteLine("ServicesController::Process");

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            IProjectService projectService = this.VisualStudioService.CoreProjectService;

            if (projectService != null)
            {
                if (this.SettingsService.SuspendReSharperDuringBuild)
                {
                    this.VisualStudioService.DTEService.ExecuteCommand(Settings.SuspendReSharperCommand);
                }

                IList<string> messages = this.servicesService.AddServices(
                    this.VisualStudioService,
                    templateInfos,
                    implementInViewModel,
                    includeUnitTests);

                this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

                this.VisualStudioService.DTEService.SaveAll();

                //// currently we arent supporting services via nuget!
                IEnumerable<string> commands = new List<string>();

                if (commands.Any())
                {
                    this.nugetService.Execute(
                        this.VisualStudioService,
                        this.GetReadMePath(),
                        commands,
                        this.SettingsService.SuspendReSharperDuringBuild);

                    this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NugetDownload);
                }
                else
                {
                    this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.ServicesCompleted);

                    if (this.SettingsService.SuspendReSharperDuringBuild)
                    {
                        this.VisualStudioService.DTEService.ExecuteCommand(Settings.ResumeReSharperCommand);
                    }
                }

                //// show the readme.
                this.ShowReadMe("Add Services", messages, this.SettingsService.UseNugetForServices);
            }
            else
            {
                TraceService.WriteError("ServicesController::AddServices Cannot find Core project");
            }
        }
    }
}
