// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the DialogService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System.Windows.Forms;

    using NinjaCoder.MvvmCross.Services.Interfaces;

    /// <summary>
    ///  Defines the DialogService type.
    /// </summary>
    public class DialogService : IDialogService
    {
        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>A dialog result</returns>
        public DialogResult ShowDialog(Form form)
        {
            return form.ShowDialog();
        }
    }
}
