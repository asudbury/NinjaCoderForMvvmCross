// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsForm type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards
{
    using System.Windows.Forms;

    /// <summary>
    /// Defines the BaseForm type.
    /// </summary>
    public partial class BaseForm : Form
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseForm"/> class.
        /// </summary>
        public BaseForm()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Sets the logo visibility.
        /// </summary>
        /// <param name="logoControl">The logo control.</param>
        /// <param name="visibility">if set to <c>true</c> [visibility].</param>
        protected void SetLogoVisibility(
            Control logoControl,
            bool visibility)
        {
            logoControl.Visible = visibility;

            if (visibility == false)
            {
                this.Width = this.Width - logoControl.Width;

                foreach (Control control in this.Controls)
                {
                    control.Left = control.Left - logoControl.Width;
                }
            }
        }
    }
}
