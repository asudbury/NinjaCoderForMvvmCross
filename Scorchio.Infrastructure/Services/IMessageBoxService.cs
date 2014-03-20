// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IMessageBoxService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Services
{
    using MahApps.Metro;

    /// <summary>
    ///  Defines the IMessageBoxService type.
    /// </summary>
    public interface IMessageBoxService
    {
        /// <summary>
        /// Shows the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="useWPFMessageBox">if set to <c>true</c> [use WPF message box].</param>
        /// <param name="theme">The theme.</param>
        /// <param name="themeColor">Color of the theme.</param>
        void Show(
            string text, 
            string caption,
            bool useWPFMessageBox,
            Theme theme,
            string themeColor);
    }
}