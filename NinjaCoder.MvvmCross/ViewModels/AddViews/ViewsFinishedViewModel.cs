// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewsFinishedViewModel.cs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddViews
{
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using Services.Interfaces;

    /// <summary>
    /// Defines the ViewsFinishedViewModel.cs type.
    /// </summary>
    public class ViewsFinishedViewModel : BaseWizardStepViewModel
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
        /// Initializes a new instance of the <see cref="ViewsFinishedViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public ViewsFinishedViewModel(ISettingsService settingsService)
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
