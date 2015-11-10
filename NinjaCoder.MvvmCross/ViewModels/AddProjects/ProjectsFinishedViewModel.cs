// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsFinishedViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddProjects
{
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;

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
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get { return "Finished"; }
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
        /// Initializes this instance.
        /// </summary>
        internal void Init()
        {
            this.SuspendReSharperDuringBuild = this.settingsService.SuspendReSharperDuringBuild;
            this.CreateTestProjectsSolutionFolder = this.settingsService.CreateTestProjectsSolutionFolder;
        }
    }
}
