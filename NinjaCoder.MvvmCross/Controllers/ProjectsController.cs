// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using Constants;
    using Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.ViewModels.AddNugetPackages;
    using NinjaCoder.MvvmCross.ViewModels.AddViews;
    using NinjaCoder.MvvmCross.ViewModels.Wizard;
    using NinjaCoder.MvvmCross.Views.Wizard;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Services;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels.AddProjects;

    using PluginsViewModel = NinjaCoder.MvvmCross.ViewModels.AddProjects.PluginsViewModel;

    /// <summary>
    /// Defines the ProjectsController type.
    /// </summary>
    internal class ProjectsController : BaseController
    {
        /// <summary>
        /// The projects service.
        /// </summary>
        private readonly IProjectsService projectsService;

        /// <summary>
        /// The nuget service.
        /// </summary>
        private readonly INugetService nugetService;

        /// <summary>
        /// The view model views service.
        /// </summary>
        private readonly IViewModelViewsService viewModelViewsService;
        
        /// <summary>
        /// The project factory.
        /// </summary>
        private readonly IProjectFactory projectFactory;

        /// <summary>
        /// The application service.
        /// </summary>
        private readonly IApplicationService applicationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController" /> class.
        /// </summary>
        /// <param name="projectsService">The projects service.</param>
        /// <param name="nugetService">The nuget service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="viewModelViewsService">The view model views service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="projectFactory">The project factory.</param>
        /// <param name="applicationService">The application service.</param>
        public ProjectsController(
            IProjectsService projectsService,
            INugetService nugetService,
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IResolverService resolverService,
            IViewModelViewsService viewModelViewsService,
            IReadMeService readMeService,
            IProjectFactory projectFactory,
            IApplicationService applicationService)
            : base(visualStudioService, settingsService, messageBoxService, resolverService, readMeService)
        {
            TraceService.WriteLine("ProjectsController::Constructor");

            this.projectsService = projectsService;
            this.nugetService = nugetService;
            this.viewModelViewsService = viewModelViewsService;
            this.projectFactory = projectFactory;
            this.applicationService = applicationService;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteHeader("ProjectsController::Run");
          
            //// we open the nuget package manager console so we don't have
            //// a wait condition later!
            
            this.nugetService.OpenNugetWindow();

            this.projectFactory.RegisterWizardData();

            WizardFrameViewModel viewModel = this.ShowDialog<WizardFrameViewModel>(new WizardView());

            if (viewModel.Continue)
            {
                ProjectsViewModel projectsViewModel = (ProjectsViewModel)viewModel.GetWizardStepViewModel("ProjectsViewModel").ViewModel;
                ViewsViewModel viewsViewModel = (ViewsViewModel)viewModel.GetWizardStepViewModel("ViewsViewModel").ViewModel;
                PluginsViewModel pluginsViewModel = (PluginsViewModel)viewModel.GetWizardStepViewModel("PluginsViewModel").ViewModel;
                NugetPackagesViewModel nugetPackagesViewModel = (NugetPackagesViewModel)viewModel.GetWizardStepViewModel("NugetPackagesViewModel").ViewModel;
                XamarinFormsLabsViewModel xamarinFormsLabsViewModel = (XamarinFormsLabsViewModel)viewModel.GetWizardStepViewModel("XamarinFormsLabsViewModel").ViewModel;

                this.Process(
                    projectsViewModel, 
                    viewsViewModel, 
                    pluginsViewModel, 
                    nugetPackagesViewModel,
                    xamarinFormsLabsViewModel);
            }
        }

        /// <summary>
        /// Processes the specified solution path.
        /// </summary>
        /// <param name="projectsViewModel">The projects view model.</param>
        /// <param name="viewsViewModel">The views view model.</param>
        /// <param name="pluginsViewModel">The plugins view model.</param>
        /// <param name="nugetPackagesViewModel">The nuget packages view model.</param>
        /// <param name="xamarinFormsLabsViewModel">The xamarin forms labs view model.</param>
        internal void Process(
            ProjectsViewModel projectsViewModel,
            ViewsViewModel viewsViewModel,
            PluginsViewModel pluginsViewModel,
            NugetPackagesViewModel nugetPackagesViewModel,
            XamarinFormsLabsViewModel xamarinFormsLabsViewModel)
        {
            TraceService.WriteLine("ProjectsController::Process");

            string commands = string.Empty;

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            this.applicationService.SuspendResharperIfRequested();

            //// create the solution if we don't have one!
            if (this.VisualStudioService.SolutionAlreadyCreated == false)
            {
                this.VisualStudioService.SolutionService.CreateEmptySolution(projectsViewModel.GetSolutionPath(), projectsViewModel.Project);
            }

            List<string> messages = this.projectsService.AddProjects(
                    this.VisualStudioService,
                    projectsViewModel.GetSolutionPath(),
                    projectsViewModel.GetFormattedRequiredTemplates())
                    .ToList();
            
            //// there is a bug in the xamarin iOS code that means it doesnt apply a couple of xml elements
            //// in the info.plist - here we fix that issue.

            if (this.SettingsService.FixInfoPlist)
            {
                this.applicationService.FixInfoPList(projectsViewModel.GetFormattedRequiredTemplates()
                        .FirstOrDefault(x => x.ProjectSuffix == ProjectSuffix.iOS.GetDescription()));
            }

            if (this.SettingsService.ProcessViewModelAndViews && 
                this.SettingsService.FrameworkType != FrameworkType.NoFramework &&
                viewsViewModel != null)
            {
                IEnumerable<string> viewModelMessages = this.viewModelViewsService.AddViewModelsAndViews(viewsViewModel.Views);
            
                messages.AddRange(viewModelMessages);
            }

            if (pluginsViewModel != null &&
                (this.SettingsService.FrameworkType == FrameworkType.MvvmCross ||
                 this.SettingsService.FrameworkType == FrameworkType.MvvmCrossAndXamarinForms))
            {
                commands += pluginsViewModel.GetNugetCommands();
                messages.AddRange(pluginsViewModel.GetNugetMessages());
            }

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

            this.VisualStudioService.CodeTidyUp(
                this.SettingsService.RemoveDefaultFileHeaders,
                this.SettingsService.RemoveDefaultComments);

            commands += this.nugetService.GetNugetCommands(projectsViewModel.GetFormattedRequiredTemplates());

            if (nugetPackagesViewModel != null)
            {
                commands += nugetPackagesViewModel.GetNugetCommands();
                messages.AddRange(nugetPackagesViewModel.GetNugetMessages());
            }

            if (xamarinFormsLabsViewModel != null)
            {
                commands += xamarinFormsLabsViewModel.GetNugetCommands();
                messages.AddRange(xamarinFormsLabsViewModel.GetNugetMessages());
            }

            //// a bit of (unnecessary) tidying up - replace double new lines!
            commands = commands.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine); 

            if (this.SettingsService.OutputNugetCommandsToReadMe)
            {
                messages.Add(string.Empty);
                messages.Add(this.ReadMeService.GetSeperatorLine());
                messages.Add("Nuget Commands");
                messages.Add(this.ReadMeService.GetSeperatorLine());
                messages.Add(string.Empty);
                messages.Add(commands);
            }

            this.ReadMeService.AddLines(
                this.GetReadMePath(),
                "Add Projects",
                messages);

            TraceService.WriteHeader("RequestedNugetCommands=" + commands);

            if (this.SettingsService.ProcessNugetCommands)
            {
                this.nugetService.Execute(
                    this.GetReadMePath(), 
                    commands,
                    this.SettingsService.SuspendReSharperDuringBuild);
            }

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NugetDownload);
        }
    }
}
