// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NugetPackagesFinishedViewModel.cs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddNugetPackages
{
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
        /// The suspend re sharper during build.
        /// </summary>
        private bool suspendReSharperDuringBuild;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsFinishedViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public NugetPackagesFinishedViewModel(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
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
    }
}
