// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the FinishedViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddProjects
{
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;

    /// <summary>
    /// Defines the FinishedViewModel type.
    /// </summary>
    public class FinishedViewModel : BaseWizardStepViewModel
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
