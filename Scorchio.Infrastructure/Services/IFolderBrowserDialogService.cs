// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IFolderBrowserDialogService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Services
{
    using System;

    /// <summary>
    /// Defines the IFolderBrowserDialogService type.
    /// </summary>
    public interface IFolderBrowserDialogService
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        string Description { get; set; }

        /// <summary>
        /// Gets or sets the root folder.
        /// </summary>
        Environment.SpecialFolder RootFolder { get; set; }

        /// <summary>
        /// Gets or sets the selected path.
        /// </summary>
        string SelectedPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show new folder button].
        /// </summary>
        bool ShowNewFolderButton { get; set; }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <returns>True or false.</returns>
        bool? ShowDialog();
    }
}
