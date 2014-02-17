// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseDialogViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.ViewModels
{
    /// <summary>
    /// Defines the BaseDialogViewModel type.
    /// </summary>
    public abstract class BaseDialogViewModel : BaseViewModel
    {
        /// <summary>
        /// The dialog result.
        /// </summary>
        private bool? dialogResult;

        /// <summary>
        /// Gets or sets the dialog result.
        /// </summary>
        public bool? DialogResult
        {
            get { return this.dialogResult; }
            set { this.SetProperty(ref this.dialogResult, value); }
        }

        /// <summary>
        /// Gets a value indicating whether [continue].
        /// </summary>
        public bool Continue
        {
            get
            {
                return this.dialogResult.HasValue &&
                       this.dialogResult == true;
            }
        }
        
        /// <summary>
        /// Called when ok button pressed.
        /// </summary>
        public virtual void OnOk()
        {
            this.DialogResult = true;
            this.OnNotify("DialogResult");
        }

        /// <summary>
        /// Called when cancel button pressed.
        /// </summary>
        public virtual void OnCancel()
        {
            this.DialogResult = false;
            this.OnNotify("DialogResult");
        }
    }
}
