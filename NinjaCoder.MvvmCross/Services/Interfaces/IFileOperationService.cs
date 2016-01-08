// --------------------------------------------------------------------------------------------------------------------
// <summary>
// 	Defines the IFileOperationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the IFileOperationService type.
    /// </summary>
    public interface IFileOperationService
    {
        /// <summary>
        /// Processes the command.
        /// </summary>
        /// <param name="fileOperation">The file operation.</param>
        void ProcessCommand(FileOperation fileOperation);
    }
}
