// --------------------------------------------------------------------------------------------------------------------
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
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReadMeService" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="settingsService">The settings service.</param>
        public ReadMeService(
            IFileSystem fileSystem,
            ISettingsService settingsService)
        {
            TraceService.WriteLine("ReadMeService::Constructor");

            this.fileSystem = fileSystem;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Adds the lines.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="lines">The lines.</param>
        public void AddLines(
            string filePath,
            string functionName,
            IEnumerable<string> lines)
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
            sw.WriteLine(DateTime.Now.ToString("dd MMM yy HH:mm")  + " " + functionName);
            sw.WriteLine(this.GetSeperatorLine());
            sw.WriteLine(string.Empty);

            lines.ToList().ForEach(sw.WriteLine);
  
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
                "Ninja Coder for MvvmCross and Xamarin Forms v" + this.settingsService.ApplicationVersion,
                this.GetSeperatorLine(),
                string.Empty,
                "All feedback welcome, please get in touch via twitter.",
                string.Empty,
                "Thanks",
                string.Empty,
                "@asudbury"
            };
        }
    }
}
