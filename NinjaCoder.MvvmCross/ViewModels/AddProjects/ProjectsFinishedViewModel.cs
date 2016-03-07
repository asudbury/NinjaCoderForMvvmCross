// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsFinishedViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddProjects
{
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using System.Collections.Generic;

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
        /// The project factory.
        /// </summary>
        private readonly IProjectFactory projectFactory;

        /// <summary>
        /// The suspend re sharper during build.
        /// </summary>
        private bool suspendReSharperDuringBuild;

        /// <summary>
        /// The create test projects solution folder.
        /// </summary>
        private bool createTestProjectsSolutionFolder;

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
        /// Initializes a new instance of the <see cref="ProjectsFinishedViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="projectFactory">The project factory.</param>
        public ProjectsFinishedViewModel(
            ISettingsService settingsService,
            IProjectFactory projectFactory)
        {
            this.settingsService = settingsService;
            this.projectFactory = projectFactory;
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

        public override void OnInitialize()
        {
            base.OnInitialize();

            this.startUpProjects = new List<string>();
            
            if (this.settingsService.AddWpfProject)
            {
                this.AddStartUpProject(ProjectSuffix.Wpf);
            }

            if (this.settingsService.AddWindowsPhoneProject)
            {
                this.AddStartUpProject(ProjectSuffix.WindowsPhone);
            }

            if (this.settingsService.AddAndroidProject)
            {
                this.AddStartUpProject(ProjectSuffix.Droid);
            }

            if (this.settingsService.AddiOSProject)
            {
                this.AddStartUpProject(ProjectSuffix.iOS);
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
            this.settingsService.StartUpProject = this.selectedStartUpProject;
            this.settingsService.RemoveDefaultFileHeaders = this.removeDefaultFileHeaders;
            this.settingsService.RemoveDefaultComments = this.removeDefaultComments;
            this.settingsService.RemoveThisPointer = this.removeThisPointer;
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
        }

        /// <summary>
        /// Adds the start up project.
        /// </summary>
        /// <param name="projectSuffix">The project suffix.</param>
        internal void AddStartUpProject(ProjectSuffix projectSuffix)
        {
            this.StartUpProjects.Insert(0, projectSuffix.GetDescription().Substring(1));
            this.SelectedStartUpProject = projectSuffix.GetDescription().Substring(1);
        }
    }
}
