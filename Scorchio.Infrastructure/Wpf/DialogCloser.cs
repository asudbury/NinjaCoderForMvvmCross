// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the DialogCloser type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf
{
    using System.Windows;

    /// <summary>
    ///  Defines the DialogCloser type.
    /// </summary>
    public static class DialogCloser
    {
        /// <summary>
        /// The dialog result property
        /// </summary>
        public static readonly DependencyProperty DialogResultProperty =
            DependencyProperty.RegisterAttached(
            "DialogResult",
            typeof(bool?),
            typeof(DialogCloser),
            new PropertyMetadata(DialogResultChanged));

        /// <summary>
        /// Sets the dialog result.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="value">The value.</param>
        public static void SetDialogResult(
            Window window, 
            bool? value)
        {
            window.SetValue(DialogResultProperty, value);
        }

        /// <summary>
        /// Dialogs the result changed.
        /// </summary>
        /// <param name="d">The d.</param>
        /// <param name="e">The <see cref="DependencyPropertyChangedEventArgs"/> instance containing the event data.</param>
        private static void DialogResultChanged(
            DependencyObject d,
            DependencyPropertyChangedEventArgs e)
        {
            Window window = d as Window;

            if (window != null)
            {
                window.DialogResult = e.NewValue as bool?;
            }
        }
    }
}
