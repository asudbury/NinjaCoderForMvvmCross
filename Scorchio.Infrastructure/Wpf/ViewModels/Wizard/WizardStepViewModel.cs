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
        /// Gets the type of the view.
        /// </summary>
        public Type ViewType { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}
