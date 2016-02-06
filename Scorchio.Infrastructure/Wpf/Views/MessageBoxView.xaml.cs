// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MessageBoxView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.Views
{
    using System.Windows;

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
