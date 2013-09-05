// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IUpdateVersionService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    /// <summary>
    ///  Defines the IUpdateVersionService type.
    /// </summary>
    public interface IUpdateVersionService
    {
        /// <summary>
        /// Runs the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="versionNumber">The version number.</param>
        void Run(
            string fileName, 
            string versionNumber);
    }
}