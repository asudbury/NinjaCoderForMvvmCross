// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsFinishedViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddProjects
{
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;

    /// <summary>
    /// Defines the ProjectsFinishedViewModel type.
    /// </summary>
    public class ProjectsFinishedViewModel : BaseWizardStepViewModel
    {
        /// <summary>
        /// Gets the display name.
        /// </summary>
        public override string DisplayName
        {
            get { return "Finished"; }
        }
    }
}
