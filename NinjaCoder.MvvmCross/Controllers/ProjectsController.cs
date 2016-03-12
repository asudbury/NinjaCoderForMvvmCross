// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using Constants;
    using Entities;
    using Factories.Interfaces;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Services;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ViewModels;
    using ViewModels.AddNugetPackages;
    using ViewModels.AddProjects;
    using ViewModels.AddViews;
    using ViewModels.Wizard;
    using Views.Wizard;
    using PluginsViewModel = ViewModels.AddProjects.PluginsViewModel;

    /// <summary>
    /// Defines the ProjectsController type.
    /// </summary>
    public class ProjectsController : BaseController
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
        /// The caching service.
        /// </summary>
        private readonly ICachingService cachingService;

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
        private readonly List<StudioCommand> postNugetCommands;

        /// <summary>
        /// The post nuget file operations.
        /// </summary>
        private readonly List<FileOperation> postNugetFileOperations;

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
        /// <param name="cachingService">The caching service.</param>
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
            IApplicationService applicationService,
            ICachingService cachingService)
            : base(visualStudioService, settingsService, messageBoxService, resolverService, readMeService)
        {
            TraceService.WriteLine("ProjectsController::Constructor");

            this.projectsService = projectsService;
            this.nugetService = nugetService;
            this.viewModelViewsService = viewModelViewsService;
            this.projectFactory = projectFactory;
            this.applicationService = applicationService;
            this.cachingService = cachingService;

            this.commands = string.Empty;
            this.postNugetCommands = new List<StudioCommand>();
            this.postNugetFileOperations = new List<FileOperation>();
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteHeader("ProjectsController::Run");
          
            //// we open the nuget package manager console so we don't have a wait condition later!
            
            this.nugetService.OpenNugetWindow();

            this.projectFactory.RegisterWizardData();

            WizardFrameViewModel viewModel = this.ShowDialog<WizardFrameViewModel>(new WizardView());

            try
            {
                if (viewModel.Continue)
                {
                    ProjectsViewModel projectsViewModel = (ProjectsViewModel)viewModel.GetWizardStepViewModel("ProjectsViewModel").ViewModel;
                    ApplicationOptionsViewModel applicationOptionsViewModel = (ApplicationOptionsViewModel)viewModel.GetWizardStepViewModel("ApplicationOptionsViewModel").ViewModel;
                    NinjaCoderOptionsViewModel ninjaCoderOptionsViewModel = (NinjaCoderOptionsViewModel)viewModel.GetWizardStepViewModel("NinjaCoderOptionsViewModel").ViewModel;
                    ApplicationSamplesOptionsViewModel applicationSamplesOptionsViewModel = (ApplicationSamplesOptionsViewModel)viewModel.GetWizardStepViewModel("ApplicationSamplesOptionsViewModel").ViewModel;
                    ViewsViewModel viewsViewModel = (ViewsViewModel)viewModel.GetWizardStepViewModel("ViewsViewModel").ViewModel;
                    PluginsViewModel pluginsViewModel = (PluginsViewModel)viewModel.GetWizardStepViewModel("PluginsViewModel").ViewModel;
                    NugetPackagesViewModel nugetPackagesViewModel = (NugetPackagesViewModel)viewModel.GetWizardStepViewModel("NugetPackagesViewModel").ViewModel;
                    XamarinFormsLabsViewModel xamarinFormsLabsViewModel = (XamarinFormsLabsViewModel)viewModel.GetWizardStepViewModel("XamarinFormsLabsViewModel").ViewModel;
                    
                    this.Process(
                        projectsViewModel,
                        applicationOptionsViewModel,
                        ninjaCoderOptionsViewModel,
                        applicationSamplesOptionsViewModel,
                        viewsViewModel,
                        pluginsViewModel,
                        nugetPackagesViewModel,
                        xamarinFormsLabsViewModel);
                }
            }
            catch (Exception exception)
            {
                TraceService.WriteError(exception);
            }
        }

        /// <summary>
        /// Processes the specified solution path.
        /// </summary>
        /// <param name="projectsViewModel">The projects view model.</param>
        /// <param name="applicationOptionsViewModel">The application options view model.</param>
        /// <param name="ninjaCoderOptionsViewModel">The ninja coder options view model.</param>
        /// <param name="applicationSamplesOptionsViewModel">The application samples options view model.</param>
        /// <param name="viewsViewModel">The views view model.</param>
        /// <param name="pluginsViewModel">The plugins view model.</param>
        /// <param name="nugetPackagesViewModel">The nuget packages view model.</param>
        /// <param name="xamarinFormsLabsViewModel">The xamarin forms labs view model.</param>
        internal void Process(
            ProjectsViewModel projectsViewModel,
            ApplicationOptionsViewModel applicationOptionsViewModel,
            NinjaCoderOptionsViewModel ninjaCoderOptionsViewModel,
            ApplicationSamplesOptionsViewModel applicationSamplesOptionsViewModel,
            ViewsViewModel viewsViewModel,
            PluginsViewModel pluginsViewModel,
            NugetPackagesViewModel nugetPackagesViewModel,
            XamarinFormsLabsViewModel xamarinFormsLabsViewModel)
        {
            TraceService.WriteLine("ProjectsController::Process");
            
            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            this.applicationService.SuspendResharperIfRequested();

            //// create the solution if we don't have one!
            if (this.VisualStudioService.SolutionAlreadyCreated == false)
            {
                this.VisualStudioService.SolutionService.CreateEmptySolution(projectsViewModel.GetSolutionPath(), projectsViewModel.Project);
            }

            if (this.SettingsService.CreateTestProjectsSolutionFolder)
            {
                this.VisualStudioService.SolutionService.AddSolutionFolder(this.SettingsService.TestProjectsSolutionFolderName);
                this.VisualStudioService.DTEService.SaveAll();
            }

            TraceService.WriteLine("ProjectsController::Process AddProjects");

           this.messages = this.projectsService.AddProjects(
                    this.VisualStudioService,
                    projectsViewModel.GetSolutionPath(),
                    projectsViewModel.GetFormattedRequiredTemplates())
                    .ToList();

            string startUpProject = this.SettingsService.StartUpProject;

            if (startUpProject != string.Empty)
            {
                if (this.VisualStudioService.GetProjectServiceBySuffix(startUpProject) != null)
                {
                    this.VisualStudioService.SolutionService.SetStartUpProject(projectsViewModel.Project + "." + startUpProject);
                }
            }

            //// there is a bug in the xamarin iOS code that means it doesnt apply a couple of xml elements
            //// in the info.plist - here we fix that issue.

            if (this.SettingsService.FixInfoPlist)
            {
                this.applicationService.FixInfoPList(projectsViewModel.GetFormattedRequiredTemplates()
                        .FirstOrDefault(x => x.ProjectSuffix == ProjectSuffix.iOS.GetDescription()));
            }

            IEnumerable<string> viewNugetCommands = new List<string>();

            if (this.SettingsService.FrameworkType != FrameworkType.NoFramework &&
                viewsViewModel != null)
            {
                //// if we dont have a viewmodel and view in memory - add one
                //// user will have dont show views and viewmodel options selected.
                if (!viewsViewModel.Views.Any())
                {
                    viewsViewModel.Add();
                }

                IEnumerable<string> viewModelMessages = this.viewModelViewsService.AddViewModelsAndViews(viewsViewModel.Views);
            
                this.messages.AddRange(viewModelMessages);

                viewNugetCommands = this.viewModelViewsService.GetNugetCommands();
            }
            
            TraceService.WriteLine("ProjectsController::Process GetApplication Commands");

            //// we need to get the post nuget commands that are now hosted in xml file that used to be in code
            CommandsList commandsList = this.applicationService.GetCommandsList();

            if (commandsList != null)
            {
                this.postNugetCommands.AddRange(commandsList.Commands);
                this.postNugetFileOperations.AddRange(commandsList.FileOperations);
            }

            IEnumerable<ProjectTemplateInfo> projectTemplateInfos = projectsViewModel.GetFormattedRequiredTemplates();

            this.commands += this.nugetService.GetNugetCommands(projectTemplateInfos);

            if (viewNugetCommands.Any())
            {
                foreach (string viewNugetCommand in viewNugetCommands)
                {
                    this.commands += viewNugetCommand + Environment.NewLine;
                }
            }

            this.PopulateNugetActions(applicationOptionsViewModel);
            this.PopulateNugetActions(ninjaCoderOptionsViewModel);
            this.PopulateNugetActions(applicationSamplesOptionsViewModel);
            this.PopulateNugetActions(pluginsViewModel);
            this.PopulateNugetActions(nugetPackagesViewModel);
            this.PopulateNugetActions(xamarinFormsLabsViewModel);
 
            this.cachingService.PostNugetCommands = this.postNugetCommands;
            this.cachingService.PostNugetFileOperations = this.postNugetFileOperations;

            //// a bit of (unnecessary) tidying up - replace double new lines!
            this.commands = this.commands.Replace(Environment.NewLine + Environment.NewLine, Environment.NewLine); 

            this.OutputNugetCommandsToReadMe();

            if (this.SettingsService.OutputReadMeToLogFile)
            {
                TraceService.WriteHeader("Start of NinjaReadMe.txt");

                foreach (string message in this.messages)
                {
                    TraceService.WriteLine(message);
                }

                TraceService.WriteHeader("End of NinjaReadMe.txt");
            }

            this.ReadMeService.AddLines(
                this.GetReadMePath(),
                "Add Projects",
                this.messages);

            TraceService.WriteHeader("RequestedNugetCommands=" + this.commands);

            if (this.SettingsService.ProcessNugetCommands)
            {
                this.ProcessNugetCommands();
            }

            TraceService.WriteLine("ProjectsController::Process END");
        }

        /// <summary>
        /// Processes the nuget commands.
        /// </summary>
        private void ProcessNugetCommands()
        {
            TraceService.WriteLine("ProjectsController::ProcessNugetCommands");

            this.nugetService.Execute(
                    this.GetReadMePath(), 
                    this.commands,
                    this.SettingsService.SuspendReSharperDuringBuild);
            
            string message = NinjaMessages.NugetDownload;

            if (this.SettingsService.UseLocalNuget)
            {
                message += " (using local " + this.SettingsService.LocalNugetName + ")";
            }

            this.VisualStudioService.WriteStatusBarMessage(message);
        }

        /// <summary>
        /// Outputs the nuget commands to read me.
        /// </summary>
        private void OutputNugetCommandsToReadMe()
        {
            TraceService.WriteLine("ProjectsController::OutputNugetCommandsToReadMe");

            if (this.SettingsService.OutputNugetCommandsToReadMe)
            {
                this.messages.Add(string.Empty);
                this.messages.Add(this.ReadMeService.GetSeperatorLine());
                this.messages.Add("Nuget Commands");
                this.messages.Add(this.ReadMeService.GetSeperatorLine());
                this.messages.Add(string.Empty);
                this.messages.Add(this.commands);
            }
        }

        /// <summary>
        /// Populates the nuget actions.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        internal void PopulateNugetActions(NugetPackagesBaseViewModel viewModel)
        {
            string viewModelName = string.Empty;
            string step = string.Empty;

            try
            {
                if (viewModel != null)
                {
                    viewModelName = viewModel.DisplayName;

                    TraceService.WriteLine("ProjectsController::PopulateNugetActions viewModel=" + viewModelName);

                    NugetActions nugetActions = viewModel.GetNugetActions();

                    step = "NugetCommands";

                    if (nugetActions.NugetCommands != string.Empty)
                    {
                        this.commands += nugetActions.NugetCommands;
                    }

                    step = "PostNugetCommands";

                    if (nugetActions.PostNugetCommands != null)
                    {
                        this.postNugetCommands.AddRange(nugetActions.PostNugetCommands);
                    }

                    step = "PostNugetFileOperations";

                    if (nugetActions.PostNugetFileOperations != null)
                    {
                        this.postNugetFileOperations.AddRange(nugetActions.PostNugetFileOperations);
                    }
                }
            }
            catch (Exception exception)
            {
                TraceService.WriteError("Error Adding Nuget Actions viewModel=" + viewModelName + "step=" + step + "  exception=" + exception.Message);
            }
        }
    }
}
