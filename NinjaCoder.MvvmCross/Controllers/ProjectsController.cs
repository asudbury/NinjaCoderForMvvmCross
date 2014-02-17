// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    using Constants;

    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.ViewModels;
    using NinjaCoder.MvvmCross.Views;

    using Scorchio.Infrastructure.Services;
    using Scorchio.Infrastructure.Services.Testing.Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Services.Interfaces;

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
        /// <param name="readMeService">The read me service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="mockingServiceFactory">The mocking service factory.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="viewModelViewsService">The view model views service.</param>
        /// <param name="viewModelAndViewsFactory">The view model and views factory.</param>
        public ProjectsController(
            IConfigurationService configurationService,
            IProjectsService projectsService,
            INugetService nugetService,
            IVisualStudioService visualStudioService,
            IReadMeService readMeService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IMockingServiceFactory mockingServiceFactory,
            IResolverService resolverService,
            IViewModelViewsService viewModelViewsService,
            IViewModelAndViewsFactory viewModelAndViewsFactory)
            : base(
            configurationService,
            visualStudioService,
            readMeService,
            settingsService,
            messageBoxService,
            resolverService)
        {
            TraceService.WriteLine("ProjectsController::Constructor");

            this.projectsService = projectsService;
            this.nugetService = nugetService;
            this.viewModelViewsService = viewModelViewsService;
            this.viewModelAndViewsFactory = viewModelAndViewsFactory;
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

            ProjectsViewModel viewModel = this.ShowDialog<ProjectsViewModel>(new ProjectsView());

            if (viewModel.Continue)
            {
                this.Process(
                    viewModel.GetSolutionPath(),
                    viewModel.Project,
                    viewModel.GetFormattedRequiredTemplates());
            }
        }

        /// <summary>
        /// Processes the specified solution path.
        /// </summary>
        /// <param name="solutionPath">The solution path.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="requireTemplates">The template infos.</param>
        internal void Process(
            string solutionPath,
            string projectName,
            IEnumerable<ProjectTemplateInfo> requireTemplates)
        {
            TraceService.WriteLine("ProjectsController::Process");

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            if (this.SettingsService.SuspendReSharperDuringBuild)
            {
                this.VisualStudioService.DTEService.ExecuteCommand(Settings.SuspendReSharperCommand);
            }

            //// create the solution if we don't have one!
            if (string.IsNullOrEmpty(this.VisualStudioService.SolutionService.FullName))
            {
                this.VisualStudioService.SolutionService.CreateEmptySolution(solutionPath, projectName);
            }

            List<string> messages =
                this.projectsService.AddProjects(
                    this.VisualStudioService,
                    solutionPath,
                    requireTemplates,
                    true).ToList();

            //// not sure if this is the correct place to do this!
            IProjectService testProjectService = this.VisualStudioService.CoreTestsProjectService;

            if (testProjectService != null)
            {
                this.mockingService.UpdateProjectReferences(testProjectService);
            }

            //// there is a bug in the xamarin iOS code that means it doesnt apply a couple of xml elements
            //// in the info.plist - here we fix that issue.

            IProjectService iosProjectService = this.VisualStudioService.iOSProjectService;

            if (iosProjectService != null)
            {
                IProjectItemService projectItemService = iosProjectService.GetProjectItem("Info.plist");

                if (projectItemService != null)
                {
                    this.FixInfoPlist(projectItemService, iosProjectService.Name);
                }
            }

            //// now we need to call the add viewmodel and views process

            if (this.SettingsService.AddViewModelAndViews)
            {
                this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.AddingViewModelAndViews);

                IEnumerable<ItemTemplateInfo> itemTemplateInfos =
                    this.viewModelAndViewsFactory.GetRequiredViewModelAndViews(
                        "FirstViewModel",
                        this.viewModelAndViewsFactory.AllowedUIViews,
                        true);

                this.viewModelViewsService.AddViewModelAndViews(
                    this.VisualStudioService.CoreProjectService,
                    this.VisualStudioService,
                    itemTemplateInfos,
                    "FirstViewModel",
                    true,
                    null,
                    null);
            }

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

            this.VisualStudioService.CodeTidyUp(
                this.SettingsService.RemoveDefaultFileHeaders,
                this.SettingsService.RemoveDefaultComments);

            //// create the version files.
            this.VisualStudioService.SolutionService.CreateFile(Settings.NinjaVersionFile, this.SettingsService.ApplicationVersion);
            this.VisualStudioService.SolutionService.CreateFile(Settings.MvxVersionFile, this.SettingsService.MvvmCrossVersion);

            if (this.SettingsService.UseNugetForProjectTemplates)
            {
                //// we shouldnt have to do this!
                //// but we have so many problems with nuget and iOS AND droid projects
                //// we try and make it as simple as possible by removing the mvx references!

                //// THIS CODE IS ALSO IN THE TEMPLATE WIZARD! 
                this.RemoveMvxReferences(this.VisualStudioService.CoreProjectService);
                this.RemoveMvxReferences(this.VisualStudioService.CoreTestsProjectService);
                this.RemoveMvxReferences(this.VisualStudioService.iOSProjectService);
                this.RemoveMvxReferences(this.VisualStudioService.DroidProjectService);
                this.RemoveMvxReferences(this.VisualStudioService.WindowsPhoneProjectService);
                this.RemoveMvxReferences(this.VisualStudioService.WindowsStoreProjectService);
                this.RemoveMvxReferences(this.VisualStudioService.WpfProjectService);

                string commands = this.nugetService.GetNugetCommands(
                    this.VisualStudioService,
                    requireTemplates,
                    this.SettingsService.VerboseNugetOutput,
                    this.SettingsService.DebugNuget,
                    this.SettingsService.UsePreReleaseNugetPackages);

                TraceService.WriteLine("nugetCommands=" + commands);

                if (this.SettingsService.ProcessNugetCommands)
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
                this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.SolutionBuilt);

                if (this.SettingsService.SuspendReSharperDuringBuild)
                {
                    this.VisualStudioService.DTEService.ExecuteCommand(Settings.ResumeReSharperCommand);
                }
            }

            //// show the readme.
            this.ShowReadMe("Add Projects", messages, this.SettingsService.UseNugetForProjectTemplates);
        }

        /// <summary>
        /// Removes the MVX references.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        internal void RemoveMvxReferences(IProjectService projectService)
        {
            if (projectService != null)
            {
                TraceService.WriteLine("ProjectsController::RemoveMvxReferences Project=" + projectService.Name);

                projectService.GetProjectReferences()
                    .Where(x => x.Name.StartsWith("Cirrious"))
                    .ToList()
                    .ForEach(x => x.Remove());
            }
        }

        /// <summary>
        /// Fixes the info plist.
        /// </summary>
        /// <param name="projectItemService">The project item service.</param>
        /// <param name="projectName">Name of the project.</param>
        internal void FixInfoPlist(
            IProjectItemService projectItemService,
            string projectName)
        {
            TraceService.WriteLine("ProjectsController::FixInfoPlist Project=" + projectName);

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
                        element.Add(new XElement("string", projectName));
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
