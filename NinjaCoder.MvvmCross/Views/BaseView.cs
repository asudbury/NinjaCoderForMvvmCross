// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Views
{
    using System.Windows.Forms;

    /// <summary>
    /// Defines the BaseView type.
    /// </summary>
    public class BaseView : Form
    {
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
