// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IDialogService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using System.Windows.Forms;

    /// <summary>
    ///  Defines the IDialogService type.
    /// </summary>
    internal interface IDialogService
    {
        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <param name="form">The form.</param>
        /// <returns>A dialog result</returns>
        DialogResult ShowDialog(Form form);
    }
}