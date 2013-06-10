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
        /// Gets or sets a value indicating whether this <see cref="SolutionOptionsForm"/> is continue.
        /// </summary>
        public bool Continue { get; set; }
    }
}
