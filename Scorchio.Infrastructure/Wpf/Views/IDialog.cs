// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IDialog type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.Views
{
    /// <summary>
    ///  Defines the IDialog type.
    /// </summary>
    public interface IDialog
    {
        /// <summary>
        /// Gets or sets the data context.
        /// </summary>
        object DataContext { get; set; }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        bool? ShowDialog();
    }
}