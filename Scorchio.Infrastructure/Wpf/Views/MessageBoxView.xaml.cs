// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MessageBoxView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.Views
{
    using System.Windows;

    using MahApps.Metro;

    using Scorchio.Infrastructure.Extensions;

    /// <summary>
    /// Interaction logic for MessageBoxView.xaml
    /// </summary>
    public partial class MessageBoxView 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBoxView"/> class.
        /// </summary>
        public MessageBoxView()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Changes the theme.
        /// </summary>
        /// <param name="theme">The theme.</param>
        /// <param name="themeColor">Color of the theme.</param>
        public void ChangeTheme(
            Theme theme,
            string themeColor)
        {
            ThemeManagerExtensions.ChangeTheme(
                this, 
                theme, 
                themeColor);
        }

        /// <summary>
        /// Oks the button click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="RoutedEventArgs"/> instance containing the event data.</param>
        private void OKButtonClick(
            object sender, 
            RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
