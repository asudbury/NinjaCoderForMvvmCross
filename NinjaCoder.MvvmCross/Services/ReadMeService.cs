﻿// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ReadMeService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Interfaces;
    using Scorchio.VisualStudio.Services;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using System.Linq;

    /// <summary>
    /// Defines the ReadMeService type.
    /// </summary>
    internal class ReadMeService : BaseService, IReadMeService
    {
        /// <summary>
        /// The file system.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMeService" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="settingsService">The settings service.</param>
        public ReadMeService(
            IFileSystem fileSystem,
            ISettingsService settingsService)
            : base(settingsService)
        {
            TraceService.WriteLine("ReadMeService::Constructor");

            this.fileSystem = fileSystem;
        }

        /// <summary>
        /// Adds the lines.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="lines">The lines.</param>
        /// <param name="errorMessages">The error messages.</param>
        /// <param name="traceMessages">The trace messages.</param>
        public void AddLines(
            string filePath,
            string functionName,
            IEnumerable<string> lines,
            IEnumerable<string> errorMessages,
            IEnumerable<string> traceMessages)
        {
            TraceService.WriteLine("ReadMeService::AddLines functionName=" + functionName);

            string currentLines = string.Empty;

            if (this.fileSystem.File.Exists(filePath))
            {
                currentLines = this.fileSystem.File.ReadAllText(filePath);
            }
            else
            {
                Stream stream = this.fileSystem.File.Create(filePath);
                stream.Close();
            }

            //// first write the new lines

            StreamWriter sw = new StreamWriter(filePath, false);

            sw.WriteLine(string.Empty);
            sw.WriteLine(this.GetSeperatorLine());
            sw.WriteLine(DateTime.Now.ToString("dd MMM yy HH:mm") + " " + functionName);
            sw.WriteLine(this.GetSeperatorLine());

            lines.ToList().ForEach(sw.WriteLine);
            
            //// now write the errors

            IEnumerable<string> messages = errorMessages as string[] ?? errorMessages.ToArray();

            if (messages.Any())
            {
                sw.WriteLine(string.Empty);
                sw.WriteLine(this.GetSeperatorLine());
                sw.WriteLine("Error Messages");
                sw.WriteLine(this.GetSeperatorLine());
                messages.ToList().ForEach(sw.WriteLine);
            }

            //// now write the trace messages 

            messages = traceMessages as string[] ?? traceMessages.ToArray();

            if (messages.Any())
            {
                sw.WriteLine(string.Empty);
                sw.WriteLine(this.GetSeperatorLine());
                sw.WriteLine("Trace Messages");
                sw.WriteLine(this.GetSeperatorLine());
                messages.ToList().ForEach(sw.WriteLine);
            }

            //// now write the old lines or add footer

            if (string.IsNullOrEmpty(currentLines) == false)
            {
                sw.Write(currentLines);
            }
            else
            {
                this.GetFooterLines().ForEach(sw.WriteLine);
            }

            sw.Close();
        }

        /// <summary>
        /// Gets the header line.
        /// </summary>
        /// <param name="headerText">The header text.</param>
        /// <returns>The header line.</returns>
        public string GetHeaderLine(string headerText)
        {
            return this.GetSeperatorLine() + Environment.NewLine + headerText + Environment.NewLine + this.GetSeperatorLine();
        }

        /// <summary>
        /// Gets the seperator line.
        /// </summary>
        /// <returns>the seperator line.</returns>
        public string GetSeperatorLine()
        {
            return "----------------------------------------------------------------------------------------------------";
        }

        /// <summary>
        /// Gets the footer.
        /// </summary>
        /// <returns>The footer text.</returns>
        internal List<string> GetFooterLines()
        {
            TraceService.WriteLine("ReadMeService::GetFooterLines");

            return new List<string>
            {
                string.Empty,
                this.GetSeperatorLine(),
                "Ninja Coder for MvvmCross and Xamarin Forms v" + this.SettingsService.ApplicationVersion,
                this.GetSeperatorLine(),
                string.Empty,
                "All feedback welcome, please get in touch via twitter.",
                string.Empty,
                "Issues Log http://github.com/asudbury/NinjaCoderForMvvmCross/issues",
                string.Empty,
                "Thanks",
                string.Empty,
                "@asudbury"
            };
        }
    }
}
