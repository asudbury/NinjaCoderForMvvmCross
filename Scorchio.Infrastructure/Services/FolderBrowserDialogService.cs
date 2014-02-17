// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the FolderBrowserDialogService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Services
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// Defines the FolderBrowserDialogService type.
    /// </summary>
    public class FolderBrowserDialogService : IFolderBrowserDialogService
    {
        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the root folder.
        /// </summary>
        public Environment.SpecialFolder RootFolder { get; set; }

        /// <summary>
        /// Gets or sets the selected path.
        /// </summary>
        public string SelectedPath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show new folder button].
        /// </summary>
        public bool ShowNewFolderButton { get; set; }

        /// <summary>
        /// Shows the dialog.
        /// </summary>
        /// <returns>
        /// True or false.
        /// </returns>
        public bool? ShowDialog()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog
            {
                ShowNewFolderButton = this.ShowNewFolderButton,
                RootFolder = RootFolder
            };

            bool result = dialog.ShowDialog() == DialogResult.OK;

            this.SelectedPath = dialog.SelectedPath;

            return result;
        }
    }
}
