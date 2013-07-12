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
        /// Sets a value indicating whether [display logo].
        /// </summary>
        bool DisplayLogo { set; }

        bool TraceOutputEnabled { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [log to file].
        /// </summary>
        bool LogToFile { get; set; }

        /// <summary>
        /// Gets or sets the log file path.
        /// </summary>
        string LogFilePath { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [include lib folder in projects].
        /// </summary>
        bool IncludeLibFolderInProjects { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [display errors].
        /// </summary>
        bool DisplayErrors { get; set; }
    }
}