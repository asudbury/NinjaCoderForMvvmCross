// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the IWizardData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Entities
{
    using System.Collections.Generic;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;

    /// <summary>
    /// Defines the IWizardData type.
    /// </summary>
    public interface IWizardData
    {
        /// <summary>
        /// Gets or sets the window title.
        /// </summary>
        string WindowTitle { get; set; }

        /// <summary>
        /// Gets or sets the height of the window.
        /// </summary>
        int WindowHeight { get; set; }
        
        /// <summary>
        /// Gets or sets the width of the window.
        /// </summary>
        int WindowWidth { get; set; }

        /// <summary>
        /// Gets or sets the wizard steps.
        /// </summary>
        List<WizardStepViewModel> WizardSteps { get; set; }
    }
}
