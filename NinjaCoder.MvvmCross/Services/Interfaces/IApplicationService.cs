// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IApplicationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    /// <summary>
    /// Defines the IApplicationService type.
    /// </summary>
    public interface IApplicationService
    {
        /// <summary>
        /// Checks for updates.
        /// </summary>
        void CheckForUpdates();
        
        /// <summary>
        /// Determines whether [is update available].
        /// </summary>
        /// <returns>True or false.</returns>
        bool IsUpdateAvailable();

        /// <summary>
        /// Checks for updates if ready.
        /// </summary>
        void CheckForUpdatesIfReady();

        /// <summary>
        /// Views the log file.
        /// </summary>
        void ViewLogFile();

        /// <summary>
        /// Clears the log file.
        /// </summary>
        void ClearLogFile();
    }
}
