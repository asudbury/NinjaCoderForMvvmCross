// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NugetPackagesController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using Constants;
    using Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.ViewModels.AddNugetPackages;
    using NinjaCoder.MvvmCross.ViewModels.Wizard;
    using NinjaCoder.MvvmCross.Views.Wizard;
    using Scorchio.Infrastructure.Services;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using NinjaCoder.MvvmCross.ViewModels;
    using NinjaCoder.MvvmCross.ViewModels.AddProjects;

    /// <summary>
    /// Defines the NugetPackagesController type.
    /// </summary>
    internal class NugetPackagesController : BaseController
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
        /// The nuget packages factory.
        /// </summary>
        private readonly INugetPackagesFactory nugetPackagesFactory;

        /// <summary>
        /// The commands.
        /// </summary>
        private string commands;

        /// <summary>
        /// The messages.
        /// </summary>
        private List<string> messages;

        /// <summary>
        /// The post nuget commands.
        /// </summary>
        private readonly List<Command> postNugetCommands;

        /// <summary>
        /// The post nuget file operations.
        /// </summary>
        private readonly List<FileOperation> postNugetFileOperations;

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetPackagesController" /> class.
        /// </summary>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="nugetService">The nuget service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="nugetPackagesFactory">The nuget packages factory.</param>
        public NugetPackagesController(
            IPluginsService pluginsService,
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

            this.pluginsService = pluginsService;
            this.nugetService = nugetService;
            this.nugetPackagesFactory = nugetPackagesFactory;
            
            this.commands = string.Empty;
            this.postNugetCommands = new List<Command>();
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

            TraceService.WriteLine("ProjectsController::Process");

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            this.messages = new List<string>();
            
            this.PopulateNugetActions(applicationOptionsViewModel);
            this.PopulateNugetActions(ninjaCoderOptionsViewModel);
            this.PopulateNugetActions(applicationSamplesOptionsViewModel);
            this.PopulateNugetActions(nugetPackagesViewModel);
            this.PopulateNugetActions(xamarinFormsLabsViewModel);

            if (this.SettingsService.OutputNugetCommandsToReadMe)
            {
                this.messages.Add(Environment.NewLine);
                this.messages.Add(this.ReadMeService.GetSeperatorLine());
                this.messages.Add(this.commands);
                this.messages.Add(this.ReadMeService.GetSeperatorLine());
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

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NugetDownload);
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
                this.messages.AddRange(nugetActions.NugetMessages);
                this.postNugetCommands.AddRange(nugetActions.PostNugetCommands);
                this.postNugetFileOperations.AddRange(nugetActions.PostNugetFileOperations);
            }
        }
    }
}
