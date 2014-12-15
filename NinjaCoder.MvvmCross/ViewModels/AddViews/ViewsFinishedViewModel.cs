// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewsFinishedViewModel.cs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddViews
{
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;

    /// <summary>
    /// Defines the ViewsFinishedViewModel.cs type.
    /// </summary>
    public class ViewsFinishedViewModel : BaseWizardStepViewModel
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
