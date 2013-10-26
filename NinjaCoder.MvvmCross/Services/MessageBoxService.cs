// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MessageBoxService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System.Windows.Forms;
    using Interfaces;

    /// <summary>
    ///  Defines the MessageBoxService type.
    /// </summary>
    public class MessageBoxService : IMessageBoxService
    {
        /// <summary>
        /// Shows the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        public void Show(string text, string caption)
        {
            MessageBox.Show(text, caption);
        }
    }
}
