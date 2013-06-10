// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IOptionsView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Views.Interfaces
{
    /// <summary>
    ///  Defines the IOptionsView type.
    /// </summary>
    public interface IOptionsView
    {
        /// <summary>
        /// Gets or sets a value indicating whether [log to file].
        /// </summary>
        bool LogToFile { get; set; }

        /// <summary>
        /// Gets or sets the log file path.
        /// </summary>
        string LogFilePath { get; set; }
    }
}