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
    using Scorchio.Infrastructure.Services.Testing.Interfaces;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using ViewModels;
    using ViewModels.AddProjects;
    using Views;

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
        /// The view model and views factory.
        /// </summary>
        private readonly IViewModelAndViewsFactory viewModelAndViewsFactory;

        /// <summary>
        /// The read me service.
        /// </summary>
        private readonly IReadMeService readMeService;

        /// <summary>
        /// The mocking service.
        /// </summary>
        private readonly IMockingService mockingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController" /> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="projectsService">The projects service.</param>
        /// <param name="nugetService">The nuget service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="mockingServiceFactory">The mocking service factory.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="viewModelViewsService">The view model views service.</param>
        /// <param name="viewModelAndViewsFactory">The view model and views factory.</param>
        /// <param name="readMeService">The read me service.</param>
        public ProjectsController(
            IConfigurationService configurationService,
            IProjectsService projectsService,
            INugetService nugetService,
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IMockingServiceFactory mockingServiceFactory,
            IResolverService resolverService,
            IViewModelViewsService viewModelViewsService,
            IViewModelAndViewsFactory viewModelAndViewsFactory,
            IReadMeService readMeService)
            : base(
            configurationService,
            visualStudioService,
            settingsService,
            messageBoxService,
            resolverService,
            readMeService)
        {
            TraceService.WriteLine("ProjectsController::Constructor");

            this.projectsService = projectsService;
            this.nugetService = nugetService;
            this.viewModelViewsService = viewModelViewsService;
            this.viewModelAndViewsFactory = viewModelAndViewsFactory;
            this.readMeService = readMeService;
            this.mockingService = mockingServiceFactory.GetMockingService();
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteHeader("ProjectsController::Run");
          
            //// we open the nuget package manager console so we don't have
            //// a wait condition later!
            
            this.nugetService.OpenNugetWindow(this.VisualStudioService);

            ProjectsViewModel2 viewModel2 = this.ShowDialog<ProjectsViewModel2>(new ProjectsView2());

            if (viewModel2.Continue)
            {
                //// this is all a bit ugly!!!
                
                WizardStepViewModel wizardStepViewModel = viewModel2.ProjectsWizardViewModel.Steps
                                                            .FirstOrDefault(x => x.ViewModel.ToString().Contains("ProjectsViewModel"));

                if (wizardStepViewModel != null)
                {
                    ProjectsViewModel projectsViewModel = (ProjectsViewModel)wizardStepViewModel.ViewModel;

                    wizardStepViewModel = viewModel2.ProjectsWizardViewModel.Steps
                                            .FirstOrDefault(x => x.ViewModel.ToString().Contains("ViewsViewModel"));

                    if (wizardStepViewModel != null)
                    {
                        ViewsViewModel viewsViewModel = (ViewsViewModel)wizardStepViewModel.ViewModel;

                        this.Process(projectsViewModel, viewsViewModel);
                    }
                }
            }
        }

        /// <summary>
        /// Processes the specified solution path.
        /// </summary>
        /// <param name="projectsViewModel">The projects view model.</param>
        /// <param name="viewsViewModel">The views view model.</param>
        internal void Process(
            ProjectsViewModel projectsViewModel,
            ViewsViewModel viewsViewModel)
        {
            TraceService.WriteLine("ProjectsController::Process");

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            bool solutionAlreadyCreated = !string.IsNullOrEmpty(this.VisualStudioService.SolutionService.FullName);

            //// create the solution if we don't have one!
            if (solutionAlreadyCreated == false)
            {
                TraceService.WriteLine("CreateEmptySolution");

                this.VisualStudioService.SolutionService.CreateEmptySolution(projectsViewModel.GetSolutionPath(), projectsViewModel.Project);
            }

            TraceService.WriteLine("AddProjects");

            List<string> messages =
                this.projectsService.AddProjects(
                    this.VisualStudioService,
                    projectsViewModel.GetSolutionPath(),
                    projectsViewModel.GetFormattedRequiredTemplates(),
                    solutionAlreadyCreated).ToList();

            this.VisualStudioService.DTE2.OutputMessage(Settings.ApplicationName, string.Join(Environment.NewLine, messages));

            //// not sure if this is the correct place to do this!
            IProjectService testProjectService = this.VisualStudioService.CoreTestsProjectService;

            if (testProjectService != null && 
                solutionAlreadyCreated == false)
            {
                TraceService.WriteLine("UpdateProjectReferences"); 

                this.mockingService.UpdateProjectReferences(testProjectService);
            }

            //// there is a bug in the xamarin iOS code that means it doesnt apply a couple of xml elements
            //// in the info.plist - here we fix that issue.

            if (this.SettingsService.FixInfoPlist)
            {
                this.FixInfoPlist(projectsViewModel);
            }

            //// now we need to call the add viewmodel and views process
            //// if the developer has requested it.

            TraceService.WriteLine("AddViewModelAndViews");

            //// for now lets just set the view type to sample data
            this.SettingsService.SelectedViewType = "SampleData";

            if (this.SettingsService.ProcessViewModelAndViews)
            {
                this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.AddingViewModelAndViews);

                foreach (View view in viewsViewModel.Views)
                {
                    string viewModelName = view.Name + "ViewModel";

                    TraceService.WriteLine("GetRequiredViewModelAndViews");

                    IEnumerable<ItemTemplateInfo> itemTemplateInfos =
                        this.viewModelAndViewsFactory.GetRequiredViewModelAndViews(
                            view,
                            viewModelName,
                            this.viewModelAndViewsFactory.AllowedUIViews,
                            solutionAlreadyCreated);

                    TraceService.WriteLine("AddViewModelAndViews " + viewModelName);

                    IEnumerable<string> viewModelMessages = this.viewModelViewsService.AddViewModelAndViews(
                        this.VisualStudioService.CoreProjectService,
                        this.VisualStudioService,
                        itemTemplateInfos,
                        viewModelName,
                        true,
                        null,
                        null);

                    messages.AddRange(viewModelMessages);
                }
            }

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

            TraceService.WriteLine("CodeTidyUp");

            this.VisualStudioService.CodeTidyUp(
                this.SettingsService.RemoveDefaultFileHeaders,
                this.SettingsService.RemoveDefaultComments);

            TraceService.WriteLine("CreateReadMe");

            this.readMeService.AddLines(
                this.GetReadMePath(),
                "Add Projects",
                messages);
            
            TraceService.WriteLine("GetNugetCommands");
                
            string commands = this.nugetService.GetNugetCommands(
                this.VisualStudioService,
                    projectsViewModel.GetFormattedRequiredTemplates());

            
            TraceService.WriteLine("nugetCommands=" + commands);

            if (this.SettingsService.ProcessNugetCommands)
            {
                TraceService.WriteLine("ProcessNugetCommands");

                this.nugetService.Execute(
                    this.VisualStudioService,
                    this.GetReadMePath(),
                    commands);
            }

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NugetDownload);
        }

        /// <summary>
        /// Fixes the information plist.
        /// </summary>
        /// <param name="projectsViewModel">The projects view model.</param>
        private void FixInfoPlist(ProjectsViewModel projectsViewModel)
        {
            TraceService.WriteLine("ProjectsController::FixInfoPlist");

            IProjectService iosProjectService = this.VisualStudioService.iOSProjectService;

            if (iosProjectService != null)
            {
                //// first check its in the new projects being added!

                ProjectTemplateInfo projectTemplateInfo =
                    projectsViewModel.GetFormattedRequiredTemplates()
                        .FirstOrDefault(x => x.ProjectSuffix == ProjectSuffix.iOS.GetDescription());

                if (projectTemplateInfo != null)
                {
                    IProjectItemService projectItemService = iosProjectService.GetProjectItem("Info.plist");

                    if (projectItemService != null)
                    {
                        TraceService.WriteLine("FixInfoPlist");

                        XDocument doc = XDocument.Load(projectItemService.FileName);

                        if (doc.Root != null)
                        {
                            XElement element = doc.Root.Element("dict");

                            if (element != null)
                            {
                                //// first look for the elements

                                XElement childElement = element.Elements("key").FirstOrDefault(x => x.Value == "CFBundleDisplayName");

                                if (childElement == null)
                                {
                                    element.Add(new XElement("key", "CFBundleDisplayName"));
                                    element.Add(new XElement("string", iosProjectService.Name));
                                }

                                childElement = element.Elements("key").FirstOrDefault(x => x.Value == "CFBundleVersion");

                                if (childElement == null)
                                {
                                    element.Add(new XElement("key", "CFBundleVersion"));
                                    element.Add(new XElement("string", "1.0"));
                                }

                                childElement = element.Elements("key").FirstOrDefault(x => x.Value == "CFBundleIdentifier");

                                if (childElement == null)
                                {
                                    element.Add(new XElement("key", "CFBundleIdentifier"));
                                    element.Add(new XElement("string", "1"));
                                }
                            }

                            doc.Save(projectItemService.FileName);
                        }
                    }
                }
            }
        }
    }
}
