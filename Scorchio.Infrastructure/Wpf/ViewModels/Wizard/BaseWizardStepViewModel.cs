// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseWizardStepViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.ViewModels.Wizard
{
    /// <summary>
    /// Defines the BaseWizardStepViewModel type.
    /// </summary>
    public abstract class BaseWizardStepViewModel : BaseViewModel
    {
        /// <summary>
        /// The is current step.
        /// </summary>
        private bool isCurrentStep;

        /// <summary>
        /// Gets the display name.
        /// </summary>
        public abstract string DisplayName { get; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is current step.
        /// </summary>
        public bool IsCurrentStep
        {
            get { return this.isCurrentStep; }
            set { this.SetProperty(ref this.isCurrentStep, value); }
        }

        /// <summary>
        /// Called when [initialize].
        /// </summary>
        public virtual void OnInitialize()
        {
        }

        /// <summary>
        /// Called when [save].
        /// </summary>
        public virtual void OnSave()
        {
        }

        /// <summary>
        /// For when yous need to save some values that can't be directly bound to UI elements.
        /// Not called when moving previous (see WizardViewModel.MoveToNextStep).
        /// </summary>
        /// <returns>An object that may modify the route</returns>
        public virtual RouteModifier OnNext()
        {
            return null;
        }

        /// <summary>
        /// For when yous need to save some values that can't be directly bound to UI elements.
        /// Not called when moving previous (see WizardViewModel.MoveToNextStep).
        /// </summary>
        /// <returns>An object that may modify the route</returns>
        public virtual RouteModifier OnPrevious()
        {
            return null;
        }

        /// <summary>
        /// Determines whether this instance [can move to next page].
        /// </summary>
        /// <returns>True or false.</returns>
        public virtual bool CanMoveToNextPage()
        {
            return true;
        }

        /// <summary>
        /// Determines whether this instance [can move to previous page].
        /// </summary>
        /// <returns>True or false.</returns>
        public virtual bool CanMoveToPreviousPage()
        {
            return true;
        }
    }
}
