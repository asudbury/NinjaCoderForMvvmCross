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
            Trace.WriteLine(GetTimedMessage(message));

            if (LogToFile)
            {
                WriteMessageToLogFile(GetTimedMessage(message));
            }
        }

        /// <summary>
        /// Writes the Error.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void WriteError(string message)
        {
            string timedMessage = "**ERROR** " + GetTimedMessage(message);

            Trace.WriteLine(timedMessage);

            if (LogToFile)
            {
                WriteMessageToLogFile(timedMessage);
            }
        }

        /// <summary>
        /// Writes the message to logfile.
        /// </summary>
        /// <param name="message">The message.</param>
        internal static void WriteMessageToLogFile(string message)
        {
            StreamWriter sw = new StreamWriter(LogFile, true);
            sw.WriteLine(message);
            sw.Close();            
        }

        /// <summary>
        /// Gets the timed message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>A timed stamped message</returns>
        internal static string GetTimedMessage(string message)
        {
            return string.Format("{0} {1}", DateTime.Now.ToString("dd MMM yyyy hh:mm:ss"), message);
        }
   }
}
