// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IWizardViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.ViewModels.Wizard
{
    using System.Windows.Input;

    /// <summary>
    /// Defines the IWizardViewModel type.
    /// </summary>
    public interface IWizardViewModel
    {
        /// <summary>
        /// Gets the move next command.
        /// </summary>
        ICommand MoveNextCommand { get; }

        /// <summary>
        /// Gets the move previous command.
        /// </summary>
        ICommand MovePreviousCommand { get; }

        /// <summary>
        /// Gets the cancel command.
        /// </summary>
        ICommand CancelCommand { get; }

        /// <summary>
        /// Gets a value indicating whether this instance is on last step.
        /// </summary>
        bool IsOnLastStep { get; }

        /// <summary>
        /// Cancels this instance.
        /// </summary>
        void Cancel();
    }
}
