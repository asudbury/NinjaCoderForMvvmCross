// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TraceService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Scorchio.VisualStudio.Services
{
    using System;
    using System.Diagnostics;
    using System.IO;

    using Microsoft.Win32;

    /// <summary>
    ///  Defines the TraceService type.
    /// </summary>
    public static class TraceService
    {
        /// <summary>
        /// Gets a value indicating whether [log to file].
        /// </summary>
        internal static bool LogToFile
        {
            get 
            {
                string value = (string)Registry.CurrentUser.GetValue(@"SOFTWARE\Scorchio Limited\Ninja Coder for MvvmCross\LogToFile", "N");

                return value == "Y";
            }
        }

        /// <summary>
        /// Gets the log file.
        /// </summary>
        internal static string LogFile
        {
            get
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\ninja-coder-for-mvvmcross.log";
            }
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void WriteLine(string message)
        {
            Trace.WriteLine(string.Format("{0} {1}", DateTime.Now.ToString("dd MMM YYY hh:mm:ss"), message));

            if (LogToFile)
            {
                WriteMessage(message);
            }
        }

        /// <summary>
        /// Writes the Error.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void WriteError(string message)
        {
            Trace.WriteLine(string.Format("{0} {1}", DateTime.Now, message));

            if (LogToFile)
            {
                WriteMessage("**ERROR** " + message);
            }
        }

        /// <summary>
        /// Writes the message.
        /// </summary>
        /// <param name="message">The message.</param>
        internal static void WriteMessage(string message)
        {
            StreamWriter sw = new StreamWriter(LogFile, true);
            sw.WriteLine("{0} {1}", DateTime.Now, message);
            sw.Close();            
        }
    }
}
