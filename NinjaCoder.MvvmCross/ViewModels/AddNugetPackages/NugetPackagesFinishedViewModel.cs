// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NugetPackagesFinishedViewModel.cs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.AddNugetPackages
{
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;

    /// <summary>
    /// Defines the NugetPackagesFinishedViewModel.cs type.
    /// </summary>
    public class NugetPackagesFinishedViewModel : BaseWizardStepViewModel
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
