// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ApplicationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Interfaces;
    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Entities;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.IO.Abstractions;
    using System.Linq;
    using System.Xml.Linq;

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

        /// <summary>
        /// Suspends the resharper if requested.
        /// </summary>
        public void SuspendResharperIfRequested()
        {
            if (this.settingsService.SuspendReSharperDuringBuild)
            {
                TraceService.WriteLine("SuspendResharper");

                try
                {
                    //// this could fail so catch exception.
                    this.visualStudioService.DTEService.ExecuteCommand(Settings.SuspendReSharperCommand);
                }
                catch (Exception exception)
                {
                    TraceService.WriteError("Error Suspending ReSharper exception=" + exception.Message);
                }
            }
        }

        /// <summary>
        /// Fixes the information p list.
        /// </summary>
        /// <param name="projectTemplateInfo">The project template information.</param>
        public void FixInfoPList(ProjectTemplateInfo projectTemplateInfo)
        {
            TraceService.WriteLine("ApplicationService::FixInfoPlist");

            IProjectService iosProjectService = this.visualStudioService.iOSProjectService;

            if (iosProjectService != null)
            {
                if (projectTemplateInfo != null)
                {
                    IProjectItemService projectItemService = iosProjectService.GetProjectItem("Info.plist");

                    if (projectItemService != null)
                    {
                        XDocument doc = XDocument.Load(projectItemService.FileName);

                        if (doc.Root != null)
                        {
                            XElement element = doc.Root.Element("dict");

                            if (element != null)
                            {
                                //// first look for the elements

                                XElement childElement = element.Elements("key").FirstOrDefault(x => x.Value == "CFBundleDisplayName");

                                if (childElement == null)
                                {
                                    element.Add(new XElement("key", "CFBundleDisplayName"));
                                    element.Add(new XElement("string", iosProjectService.Name));
                                }

                                childElement = element.Elements("key").FirstOrDefault(x => x.Value == "CFBundleVersion");

                                if (childElement == null)
                                {
                                    element.Add(new XElement("key", "CFBundleVersion"));
                                    element.Add(new XElement("string", "1.0"));
                                }

                                childElement = element.Elements("key").FirstOrDefault(x => x.Value == "CFBundleIdentifier");

                                if (childElement == null)
                                {
                                    element.Add(new XElement("key", "CFBundleIdentifier"));
                                    element.Add(new XElement("string", "1"));
                                }
                            }

                            doc.Save(projectItemService.FileName);
                        }
                    }
                }
            }
        }
    }
}
