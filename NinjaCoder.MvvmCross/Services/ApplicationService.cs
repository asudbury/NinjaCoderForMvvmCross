// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ApplicationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Interfaces;

    using NinjaCoder.MvvmCross.Entities;

    using Scorchio.VisualStudio.Services;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Abstractions;

    /// <summary>
    /// Defines the ApplicationService type.
    /// </summary>
    public class ApplicationService : IApplicationService
    {
        /// <summary>
        /// The visual studio service.
        /// </summary>
        private readonly IVisualStudioService visualStudioService;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The file system.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationService" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="fileSystem">The file system.</param>
        public ApplicationService(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IFileSystem fileSystem)
        {
            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
            this.fileSystem = fileSystem;
        }

        /// <summary>
        /// Checks for updates.
        /// </summary>
        public void CheckForUpdates()
        {
            TraceService.WriteLine("ApplicationService::CheckForUpdates");

            string path = this.settingsService.UpdateCheckerPath;

            bool exists = this.fileSystem.File.Exists(path);

            if (exists)
            {
                ProcessStartInfo startInfo = new ProcessStartInfo(path)
                                             {
                                                 WindowStyle = ProcessWindowStyle.Hidden
                                             };

                Process.Start(startInfo);
            }
        }

        /// <summary>
        /// Determines whether [is update available].
        /// </summary>
        /// <returns>True or false.</returns>
        public bool IsUpdateAvailable()
        {
            TraceService.WriteLine("ApplicationService::IsUpdateAvailable");

            string currentVersionString = this.settingsService.ApplicationVersion;
            string newVersionString = this.settingsService.LatestVersionOnGallery;

            if (string.IsNullOrEmpty(currentVersionString) == false &&
                string.IsNullOrEmpty(newVersionString) == false)
            {
                Version currentVersion;
                Version newVersion;

                Version.TryParse(currentVersionString, out currentVersion);

                Version.TryParse(newVersionString, out newVersion);
                 
                if (currentVersion != null && 
                    newVersion != null)
                {
                    if (newVersion > currentVersion)
                    {
                        TraceService.WriteLine("ApplicationService::IsUpdateAvailable Update is available");
                        return true;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Checks for updates if ready.
        /// </summary>
        public void CheckForUpdatesIfReady()
        {
        }

        /// <summary>
        /// Views the log file.
        /// </summary>
        public void ViewLogFile()
        {
            TraceService.WriteLine("ApplicationService::ViewLogFile");

            string logFilePath = this.settingsService.LogFilePath;

            if (File.Exists(logFilePath) == false)
            {
                File.Create(logFilePath);
            }

            Process.Start(logFilePath);
        }

        /// <summary>
        /// Clears the log file.
        /// </summary>
        public void ClearLogFile()
        {
            TraceService.WriteLine("ApplicationService::ClearLogFile");

            if (File.Exists(this.settingsService.LogFilePath))
            {
                File.Delete(this.settingsService.LogFilePath);
            }
        }

        /// <summary>
        /// Gets the application framework.
        /// </summary>
        /// <returns></returns>
        public FrameworkType GetApplicationFramework()
        {
            FrameworkType frameworkType = this.visualStudioService.GetFrameworkType();

            if (frameworkType == FrameworkType.NotSet)
            {
                //// new solution
                frameworkType = this.settingsService.FrameworkType;
            }

            return frameworkType;
        }
    }
}
