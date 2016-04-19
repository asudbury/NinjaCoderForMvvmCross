// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsFinishedViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddProjects
{
    using Factories.Interfaces;
    using Messages;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Scorchio.VisualStudio.Entities;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    using NinjaCoder.MvvmCross.Entities;

    using Scorchio.Infrastructure.Extensions;

    using TinyMessenger;

    /// <summary>
    /// Defines the ProjectsFinishedViewModel type.
    /// </summary>
    public class ProjectsFinishedViewModel : BaseWizardStepViewModel
    {
        /// <summary>
        /// The settings service
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The visual studio service.
        /// </summary>
        private readonly IVisualStudioService visualStudioService;

        /// <summary>
        /// The project factory.
        /// </summary>
        private readonly IProjectFactory projectFactory;

        /// <summary>
        /// The caching service.
        /// </summary>
        private readonly ICachingService cachingService;

        /// <summary>
        /// The tiny messenger hub.
        /// </summary>
        private readonly ITinyMessengerHub tinyMessengerHub;

        /// <summary>
        /// The suspend re sharper during build.
        /// </summary>
        private bool suspendReSharperDuringBuild;

        /// <summary>
        /// The create test projects solution folder.
        /// </summary>
        private bool createTestProjectsSolutionFolder;

        /// <summary>
        /// The projects.
        /// </summary>
        private List<string> projects;

        /// <summary>
        /// The start up projects.
        /// </summary>
        private List<string> startUpProjects;

        /// <summary>
        /// The selected start up project.
        /// </summary>
        private string selectedStartUpProject;

        /// <summary>
        /// The remove default file headers.
        /// </summary>
        private bool removeDefaultFileHeaders;

        /// <summary>
        /// The remove default comments.
        /// </summary>
        private bool removeDefaultComments;

        /// <summary>
        /// The remove this pointer.
        /// </summary>
        private bool removeThisPointer;

        /// <summary>
        /// The expand code formatting options.
        /// </summary>
        private bool expandCodeFormattingOptions;

        /// <summary>
        /// The tiny message subscription token.
        /// </summary>
        private TinyMessageSubscriptionToken tinyMessageSubscriptionToken;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsFinishedViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="projectFactory">The project factory.</param>
        /// <param name="cachingService">The caching service.</param>
        /// <param name="tinyMessengerHub">The tiny messenger hub.</param>
        public ProjectsFinishedViewModel(
            ISettingsService settingsService,
            IVisualStudioService visualStudioService,
            IProjectFactory projectFactory,
            ICachingService cachingService,
            ITinyMessengerHub tinyMessengerHub)
        {
            this.settingsService = settingsService;
            this.visualStudioService = visualStudioService;
            this.projectFactory = projectFactory;
            this.cachingService = cachingService;
            this.tinyMessengerHub = tinyMessengerHub;
            this.Init();
        }

        /// <summary>
        /// Gets or sets a value indicating whether [suspend re sharper during build].
        /// </summary>
        public bool SuspendReSharperDuringBuild
        {
            get
            {
                return this.suspendReSharperDuringBuild;
            }

            set
            {
                this.SetProperty(ref this.suspendReSharperDuringBuild, value);
                this.settingsService.SuspendReSharperDuringBuild = value;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [create test projects solution folder].
        /// </summary>
        public bool CreateTestProjectsSolutionFolder
        {
            get
            {
                return this.createTestProjectsSolutionFolder;
            }

            set
            {
                this.SetProperty(ref this.createTestProjectsSolutionFolder, value);
                this.settingsService.CreateTestProjectsSolutionFolder = value;
            }
        }

        /// <summary>
        /// Gets or sets the projects.
        /// </summary>
        public List<string> Projects
        {
            get { return this.projects; }
            set { this.SetProperty(ref this.projects, value); }
        }

        /// <summary>
        /// Gets or sets the start up projects.
        /// </summary>
        public List<string> StartUpProjects
        {
            get { return this.startUpProjects; }
            set { this.SetProperty(ref this.startUpProjects, value); }
        }

        /// <summary>
        /// Gets or sets the selected start up project.
        /// </summary>
        public string SelectedStartUpProject
        {
            get { return this.selectedStartUpProject; }
            set { this.SetProperty(ref this.selectedStartUpProject, value); }
        }
        
        /// <summary>
        /// Gets or sets a value indicating whether [remove default file headers].
        /// </summary>
        public bool RemoveDefaultFileHeaders
        {
            get { return this.removeDefaultFileHeaders; }
            set { this.SetProperty(ref this.removeDefaultFileHeaders, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [remove default comments].
        /// </summary>
        public bool RemoveDefaultComments
        {
            get { return this.removeDefaultComments; }
            set { this.SetProperty(ref this.removeDefaultComments, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [remove this pointer].
        /// </summary>
        public bool RemoveThisPointer
        {
            get { return this.removeThisPointer; }
            set { this.SetProperty(ref this.removeThisPointer, value); }
        }

        /// <summary>
        /// Gets a value indicating whether [display start up project].
        /// </summary>
        public bool DisplayStartUpProject
        {
            get { return this.StartUpProjects.Count > 0 && this.visualStudioService.SolutionAlreadyCreated == false; }
        }

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get { return "Finished"; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [expand pre release options].
        /// </summary>
        public bool ExpandCodeFormattingOptions
        {
            get { return this.expandCodeFormattingOptions; }
            set { this.SetProperty(ref this.expandCodeFormattingOptions, value); }
        }

        /// <summary>
        /// For when yous need to save some values that can't be directly bound to UI elements.
        /// Not called when moving previous (see WizardViewModel.MoveToNextStep).
        /// </summary>
        /// <returns>
        /// An object that may modify the route
        /// </returns>
        public override RouteModifier OnPrevious()
        {
            return this.projectFactory.GetRouteModifier(this.settingsService.FrameworkType);
        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        public override void OnInitialize()
        {
            base.OnInitialize();

            this.projects = new List<string>();

            IEnumerable<ProjectTemplateInfo> projectTemplateInfos = this.cachingService.Projects;

            IEnumerable<ProjectTemplateInfo> templateInfos = projectTemplateInfos as ProjectTemplateInfo[] ?? projectTemplateInfos.ToArray();

            foreach (ProjectTemplateInfo projectTemplateInfo in templateInfos)
            {
                this.Projects.Add(projectTemplateInfo.Name);
            }

            this.startUpProjects = new List<string>();
            
            if (templateInfos.FirstOrDefault(x => x.ProjectSuffix == this.settingsService.WpfProjectSuffix) != null)
            {
                this.AddStartUpProject(this.settingsService.WpfProjectSuffix);
            }

            if (templateInfos.FirstOrDefault(x => x.ProjectSuffix == this.settingsService.WindowsPhoneProjectSuffix) != null)
            {
                this.AddStartUpProject(this.settingsService.WindowsPhoneProjectSuffix);
            }

            if (templateInfos.FirstOrDefault(x => x.ProjectSuffix == this.settingsService.DroidProjectSuffix) != null)
            {
                this.AddStartUpProject(this.settingsService.DroidProjectSuffix);
            }

            if (templateInfos.FirstOrDefault(x => x.ProjectSuffix == this.settingsService.iOSProjectSuffix) != null)
            {
                this.AddStartUpProject(this.settingsService.iOSProjectSuffix);
            }

            if (templateInfos.FirstOrDefault(x => x.ProjectSuffix == this.settingsService.WindowsUniversalProjectSuffix) != null)
            {
                this.AddStartUpProject(this.settingsService.WindowsUniversalProjectSuffix);
            }

            //// we try and use the project the customer used before if in the list!
            if (this.settingsService.StartUpProject != string.Empty &&
                this.StartUpProjects.Contains(this.settingsService.StartUpProject))
            {
                this.SelectedStartUpProject = this.settingsService.StartUpProject;
            }
            
            if (this.settingsService.RemoveDefaultComments ||
                this.settingsService.RemoveDefaultFileHeaders ||
                this.settingsService.RemoveThisPointer)
            {
                this.ExpandCodeFormattingOptions = true;
            }
        }

        /// <summary>
        /// Called when [save].
        /// </summary>
        public override void OnSave()
        {
            base.OnSave();

            if (this.DisplayStartUpProject)
            {
                this.settingsService.StartUpProject = this.selectedStartUpProject;
            }

            this.settingsService.RemoveDefaultFileHeaders = this.removeDefaultFileHeaders;
            this.settingsService.RemoveDefaultComments = this.removeDefaultComments;
            this.settingsService.RemoveThisPointer = this.removeThisPointer;

            this.tinyMessengerHub.Unsubscribe<ProjectSuffixesUpdatedMessage>(this.tinyMessageSubscriptionToken);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal void Init()
        {
            this.SuspendReSharperDuringBuild = this.settingsService.SuspendReSharperDuringBuild;
            this.CreateTestProjectsSolutionFolder = this.settingsService.CreateTestProjectsSolutionFolder;
            this.RemoveDefaultFileHeaders = this.settingsService.RemoveDefaultFileHeaders;
            this.RemoveDefaultComments = this.settingsService.RemoveDefaultComments;
            this.RemoveThisPointer = this.settingsService.RemoveThisPointer;

            this.tinyMessageSubscriptionToken = this.tinyMessengerHub.Subscribe<ProjectSuffixesUpdatedMessage>(m => { this.UpdateProjectSuffixes(); });
        }

        /// <summary>
        /// Updates the project suffixes.
        /// </summary>
        internal void UpdateProjectSuffixes()
        {
            //// not currently implemented.
        }

        /// <summary>
        /// Adds the start up project.
        /// </summary>
        /// <param name="projectSuffix">The project suffix.</param>
        internal void AddStartUpProject(string projectSuffix)
        {
            this.StartUpProjects.Insert(0, projectSuffix.Substring(1));
            this.SelectedStartUpProject = projectSuffix.Substring(1);
        }
    }
}
