// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NinjaWizardViewModel.cs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels.Wizard
{
    using Scorchio.Infrastructure.Wpf.ViewModels.Wizard;
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the NinjaWizardViewModel.cs type.
    /// </summary>
    public class NinjaWizardViewModel : WizardViewModel
    {
        /// <summary>
        /// Occurs when [on cancel].
        /// </summary>
        public event EventHandler OnCancel;

        /// <summary>
        /// Occurs when [on finish].
        /// </summary>
        public event EventHandler OnFinish;

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjaWizardViewModel" /> class.
        /// </summary>
        /// <param name="steps">The steps.</param>
        public NinjaWizardViewModel(List<WizardStepViewModel> steps)
        {
            this.Steps = steps;
        }

        /// <summary>
        /// Cancels this instance.
        /// </summary>
        public override void Cancel()
        {
            EventHandler handler = this.OnCancel;

            if (handler != null)
            {
                handler(this, null);
            }
        }

        /// <summary>
        /// Finishes this instance.
        /// </summary>
        protected override void Finish()
        {
            EventHandler handler = this.OnFinish;

            if (handler != null)
            {
                handler(this, null);
            }
        }
    }
}
