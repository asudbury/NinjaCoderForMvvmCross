// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NugetPackagesController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using Constants;
    using Entities;
    using Factories.Interfaces;
    using Scorchio.Infrastructure.Services;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System.Collections.Generic;
    using ViewModels;
    using ViewModels.AddNugetPackages;
    using ViewModels.AddProjects;
    using ViewModels.Wizard;
    using Views.Wizard;

    /// <summary>
    /// Defines the NugetPackagesController type.
    /// </summary>
    internal class NugetPackagesController : BaseController
    {
        /// <summary>
        /// The nuget service.
        /// </summary>
        private readonly INugetService nugetService;

        /// <summary>
        /// The nuget packages factory.
        /// </summary>
        private readonly INugetPackagesFactory nugetPackagesFactory;

        /// <summary>
        /// The post nuget commands.
        /// </summary>
        private readonly List<StudioCommand> postNugetCommands;

        /// <summary>
        /// The post nuget file operations.
        /// </summary>
        private readonly List<FileOperation> postNugetFileOperations;

        /// <summary>
        /// The commands.
        /// </summary>
        private string commands;

        /// <summary>
        /// The messages.
        /// </summary>
        private List<string> messages;

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetPackagesController" /> class.
        /// </summary>
        /// <param name="nugetService">The nuget service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="nugetPackagesFactory">The nuget packages factory.</param>
        public NugetPackagesController(
            INugetService nugetService,
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IResolverService resolverService,
            IReadMeService readMeService,
            INugetPackagesFactory nugetPackagesFactory)
            : base(
            visualStudioService, 
            settingsService, 
            messageBoxService,
            resolverService,
            readMeService)
        {
            TraceService.WriteLine("NugetPackagesController::Constructor");

            this.nugetService = nugetService;
            this.nugetPackagesFactory = nugetPackagesFactory;
            
            this.commands = string.Empty;
            this.postNugetCommands = new List<StudioCommand>();
            this.postNugetFileOperations = new List<FileOperation>();
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteHeader("NugetPackagesController::Run");

            //// we open the nuget package manager console so we don't have
            //// a wait condition later!
            this.nugetService.OpenNugetWindow();

            this.nugetPackagesFactory.RegisterWizardData();

            WizardFrameViewModel viewModel = this.ShowDialog<WizardFrameViewModel>(new WizardView());

            if (viewModel.Continue)
            {
                ApplicationOptionsViewModel applicationOptionsViewModel = (ApplicationOptionsViewModel)viewModel.GetWizardStepViewModel("ApplicationOptionsViewModel").ViewModel;
                NinjaCoderOptionsViewModel ninjaCoderOptionsViewModel = (NinjaCoderOptionsViewModel)viewModel.GetWizardStepViewModel("NinjaCoderOptionsViewModel").ViewModel;
                ApplicationSamplesOptionsViewModel applicationSamplesOptionsViewModel = (ApplicationSamplesOptionsViewModel)viewModel.GetWizardStepViewModel("ApplicationSamplesOptionsViewModel").ViewModel;
                NugetPackagesViewModel nugetPackagesViewModel = (NugetPackagesViewModel)viewModel.GetWizardStepViewModel("NugetPackagesViewModel").ViewModel;
                XamarinFormsLabsViewModel xamarinFormsLabsViewModel = (XamarinFormsLabsViewModel)viewModel.GetWizardStepViewModel("XamarinFormsLabsViewModel").ViewModel;

                this.Process(
                    applicationOptionsViewModel,
                    ninjaCoderOptionsViewModel,
                    applicationSamplesOptionsViewModel,
                    nugetPackagesViewModel,
                    xamarinFormsLabsViewModel);
            }
        }

        /// <summary>
        /// Processes the specified form.
        /// </summary>
        /// <param name="applicationOptionsViewModel">The application options view model.</param>
        /// <param name="ninjaCoderOptionsViewModel">The ninja coder options view model.</param>
        /// <param name="applicationSamplesOptionsViewModel">The application samples options view model.</param>
        /// <param name="nugetPackagesViewModel">The nuget packages view model.</param>
        /// <param name="xamarinFormsLabsViewModel">The xamarin forms labs view model.</param>
        internal void Process(
             ApplicationOptionsViewModel applicationOptionsViewModel,
             NinjaCoderOptionsViewModel ninjaCoderOptionsViewModel,
             ApplicationSamplesOptionsViewModel applicationSamplesOptionsViewModel,
             NugetPackagesViewModel nugetPackagesViewModel,
             XamarinFormsLabsViewModel xamarinFormsLabsViewModel)
        {
            TraceService.WriteLine("NugetPackagesController::Process");

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            this.messages = new List<string>();
            
            this.PopulateNugetActions(applicationOptionsViewModel);
            this.PopulateNugetActions(ninjaCoderOptionsViewModel);
            this.PopulateNugetActions(applicationSamplesOptionsViewModel);
            this.PopulateNugetActions(nugetPackagesViewModel);
            this.PopulateNugetActions(xamarinFormsLabsViewModel);

            if (this.SettingsService.OutputNugetCommandsToReadMe)
            {
                this.messages.Add(this.commands);
            }

            this.ReadMeService.AddLines(
                this.GetReadMePath(),
                "Add Nuget Packages",
                this.messages);

            TraceService.WriteHeader("RequestedNugetCommands=" + this.commands);

            if (this.SettingsService.ProcessNugetCommands)
            {
                this.nugetService.Execute(
                    this.GetReadMePath(),
                    this.commands,
                    this.SettingsService.SuspendReSharperDuringBuild);
            }

            string message = NinjaMessages.NugetDownload;

            if (this.SettingsService.UseLocalNuget)
            {
                message += " (using local " + this.SettingsService.LocalNugetName + ")";
            }

            this.VisualStudioService.WriteStatusBarMessage(message);
        }

        /// <summary>
        /// Populates the nuget actions.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        internal void PopulateNugetActions(NugetPackagesBaseViewModel viewModel)
        {
            if (viewModel != null)
            {
                NugetActions nugetActions = viewModel.GetNugetActions();

                this.commands += nugetActions.NugetCommands;
                this.postNugetCommands.AddRange(nugetActions.PostNugetCommands);
                this.postNugetFileOperations.AddRange(nugetActions.PostNugetFileOperations);
            }
        }
    }
}
