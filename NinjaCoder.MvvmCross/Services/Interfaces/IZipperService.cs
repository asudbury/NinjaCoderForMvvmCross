// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IZipperService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    /// <summary>
    /// Defines the IZipperService type.
    /// </summary>
    public interface IZipperService
    {
        /// <summary>
        /// Updates the directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="updatesDirectory">The updates directory.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="createLogFile">if set to <c>true</c> [create log file].</param>
        /// <param name="replaceFiles">if set to <c>true</c> [replace files].</param>
        void UpdateDirectory(
            string directory, 
            string updatesDirectory, 
            string folderName,
            bool createLogFile,
            bool replaceFiles);

        /// <summary>
        /// Updates the zip.
        /// </summary>
        /// <param name="zipName">Name of the zip.</param>
        /// <param name="updatesDirectory">The updates directory.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="createLogFile">if set to <c>true</c> [create log file].</param>
        /// <param name="replaceFiles">if set to <c>true</c> [replace files].</param>
        void UpdateZip(
            string zipName, 
            string updatesDirectory, 
            string folderName,
            bool createLogFile,
            bool replaceFiles);
    }
}
