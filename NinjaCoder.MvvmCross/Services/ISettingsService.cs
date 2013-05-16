// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ISettingsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Services
{
    using System;

    /// <summary>
    /// Defines the ISettingsService type.
    /// </summary>
    public interface ISettingsService
    {
        /// <summary>
        /// Gets or sets a value indicating whether [log to file].
        /// </summary>
        bool LogToFile { get; set; }

        /// <summary>
        /// Gets the log file path.
        /// </summary>
        string LogFilePath { get; }

        /// <summary>
        /// Gets the converters templates path.
        /// </summary>
        string ConvertersTemplatesPath  { get; }

        /// <summary>
        /// Gets the plugins templates path.
        /// </summary>
        string PluginsTemplatesPath { get; }
    }
}
