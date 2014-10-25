// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the WizardStepViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.ViewModels.Wizard
{
    using System;

    /// <summary>
    /// Defines the WizardStepViewModel type.
    /// </summary>
    public class WizardStepViewModel : BaseViewModel, IProvideViewType
    {
        /// <summary>
        /// Gets or sets the view model.
        /// </summary>
        public BaseWizardStepViewModel ViewModel { get; set; }

        /// <summary>
        /// The class type of the actual xaml view to be used for this step
        /// </summary>
        public Type ViewType { get; set; }
    }
}
