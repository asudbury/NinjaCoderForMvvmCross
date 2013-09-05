// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the AboutBoxForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Views
{
    using System.Windows.Forms;

    /// <summary>
    /// Define the AboutBoxForm type.
    /// </summary>
    public partial class AboutBoxForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AboutBoxForm"/> class.
        /// </summary>
        public AboutBoxForm()
        {
            this.InitializeComponent();
        }
        
        /// <summary>
        /// Buttons the OK click.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void ButtonOKClick(object sender, System.EventArgs e)
        {
            this.Close();
        }
    }
}
