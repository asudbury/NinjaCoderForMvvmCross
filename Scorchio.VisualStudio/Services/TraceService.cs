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
    using System.Windows.Forms;

    /// <summary>
    ///  Defines the TraceService type.
    /// </summary>
    public static class TraceService
    {
        /// <summary>
        /// The log to trace setting.
        /// </summary>
        private static bool logToTrace = true;

        /// <summary>
        /// The log to file setting.
        /// </summary>
        private static bool logToFile;

        /// <summary>
        /// Gets a value indicating whether to log to trace.
        /// </summary>
        internal static bool LogToTrace
        {
            get { return logToTrace; }
        }

        /// <summary>
        /// Gets a value indicating whether [log to console].
        /// </summary>
        internal static bool LogToConsole { get; private set; }

        /// <summary>
        /// Gets a value indicating whether to log to file.
        /// </summary>
        internal static bool LogToFile
        {
            get { return logToFile; }
        }

        /// <summary>
        /// Gets the log file.
        /// </summary>
        internal static string LogFile { get; private set; }

        /// <summary>
        /// Gets a value indicating whether to display Errors.
        /// </summary>
        internal static bool DisplayErrors { get; private set; }

        /// <summary>
        /// Initializes the specified settings.
        /// </summary>
        /// <param name="logToTraceSetting">if set to <c>true</c> [log to trace setting].</param>
        /// <param name="logToConsoleSetting">if set to <c>true</c> [log to console setting].</param>
        /// <param name="logToFileSetting">if set to <c>true</c> [log to file setting].</param>
        /// <param name="logFileSetting">The log file setting.</param>
        /// <param name="displayErrorsSetting">if set to <c>true</c> [display errors setting].</param>
        public static void Initialize(
            bool logToTraceSetting,
            bool logToConsoleSetting,
            bool logToFileSetting,
            string logFileSetting,
            bool displayErrorsSetting)
        {
            logToTrace = logToTraceSetting;
            LogToConsole = logToConsoleSetting;
            logToFile = logToFileSetting;
            LogFile = logFileSetting;
            DisplayErrors = displayErrorsSetting;
        }

        /// <summary>
        /// Writes the line.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void WriteLine(string message)
        {
            if (LogToTrace)
            {
                Trace.WriteLine(GetTimedMessage(string.Empty, message));
            }

            if (LogToConsole)
            {
                Console.WriteLine(GetTimedMessage(string.Empty, message));
            }

            if (LogToFile)
            {
                WriteMessageToLogFile(GetTimedMessage(string.Empty, message));
            }
        }

        /// <summary>
        /// Writes a header message.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void WriteHeader(string message)
        {
            WriteLine("--------------------------------------------------------");
            WriteLine(message);
            WriteLine("--------------------------------------------------------");
        }
        
        /// <summary>
        /// Writes the Error.
        /// </summary>
        /// <param name="message">The message.</param>
        public static void WriteError(string message)
        {
            WriteLine("ERROR ERROR ERROR ERROR ERROR ERROR ERROR ERROR");

            string timedMessage = GetTimedMessage("** ERROR **", message);

            if (LogToTrace)
            {
                Trace.WriteLine(timedMessage);
            }

            if (LogToConsole)
            {
                Console.WriteLine(timedMessage);
            }

            if (LogToFile)
            {
                WriteMessageToLogFile(timedMessage);
            }

            if (DisplayErrors)
            {
                MessageBox.Show(message, "Ninja Coder for MvvmCross and Xamarin Forms");
            }

            WriteLine("--------------------------------------------------------");
        }

        /// <summary>
        /// Writes the message to the log file.
        /// </summary>
        /// <param name="message">The message.</param>
        internal static void WriteMessageToLogFile(string message)
        {
            try
            {
                if (string.IsNullOrEmpty(LogFile) == false)
                {
                    StreamWriter sw = new StreamWriter(LogFile, true);
                    sw.WriteLine(message);
                    sw.Close();
                }
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Gets the timed message.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="message">The message.</param>
        /// <returns>
        /// A timed stamped message
        /// </returns>
        internal static string GetTimedMessage(
            string type,
            string message)
        {
            return string.Format("{0} {1} {2}", DateTime.Now.ToString("dd MMM yy HH:mm:ss fff"), type, message);
        }
   }
}
