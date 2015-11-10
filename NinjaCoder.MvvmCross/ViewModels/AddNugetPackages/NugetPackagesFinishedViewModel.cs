// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NugetPackagesFinishedViewModel.cs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddNugetPackages
{
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.ViewModels.AddProjects;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;

    /// <summary>
    /// Defines the NugetPackagesFinishedViewModel.cs type.
    /// </summary>
    public class NugetPackagesFinishedViewModel : BaseWizardStepViewModel
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
        /// Initializes a new instance of the <see cref="ProjectsFinishedViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="projectFactory">The project factory.</param>
        public NugetPackagesFinishedViewModel(
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
            get { return this.suspendReSharperDuringBuild; }
            set
            {
                this.SetProperty(ref this.suspendReSharperDuringBuild, value);
                this.settingsService.SuspendReSharperDuringBuild = value;
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
        /// Initializes this instance.
        /// </summary>
        internal void Init()
        {
            this.SuspendReSharperDuringBuild = this.settingsService.SuspendReSharperDuringBuild;
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
    }
}
