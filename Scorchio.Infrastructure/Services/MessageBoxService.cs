// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MessageBoxService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Services
{
    using System.Windows.Forms;

    using MahApps.Metro;

    using Scorchio.Infrastructure.Wpf.Views;

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
        /// <param name="useWPFMessageBox">if set to <c>true</c> [use WPF message box].</param>
        /// <param name="theme">The theme.</param>
        /// <param name="themeColor">Color of the theme.</param>
        public void Show(
            string text, 
            string caption,
            bool useWPFMessageBox,
            Theme theme,
            string themeColor)
        {
            if (useWPFMessageBox)
            {
                MessageBoxView view = new MessageBoxView
                                          {
                                              Title = caption, 
                                              Message = { Text = text }
                                          };

                view.ChangeTheme(theme, themeColor);

                view.ShowDialog();
            }
            else
            {
                MessageBox.Show(text, caption);
            }
        }
    }
}
