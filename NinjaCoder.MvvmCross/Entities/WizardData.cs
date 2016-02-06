// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the WizardData type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Entities
{
    using System.Collections.Generic;
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;

    /// <summary>
    /// Defines the WizardData type.
    /// </summary>
    public class WizardData : IWizardData
    {
        /// <summary>
        /// Gets or sets the window title.
        /// </summary>
        public string WindowTitle { get; set; }

        /// <summary>
        /// Gets or sets the height of the window.
        /// </summary>
        public int WindowHeight { get; set; }
        
        /// <summary>
        /// Gets or sets the width of the window.
        /// </summary>
        public int WindowWidth { get; set; }

        /// <summary>
        /// Gets or sets the wizard steps.
        /// </summary>
        public List<WizardStepViewModel> WizardSteps { get; set; }
    }
}
