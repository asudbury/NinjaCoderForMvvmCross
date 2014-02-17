 // --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ConfigurationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System;
    using System.IO.Abstractions;

    using Constants;
    using Interfaces;

    using NinjaCoder.MvvmCross.Infrastructure.Services;

    using Scorchio.VisualStudio.Services;

    /// <summary>
    /// Defines the ConfigurationService type.
    /// </summary>
    internal class ConfigurationService : BaseService, IConfigurationService
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
        /// Initializes a new instance of the <see cref="ConfigurationService" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="settingsService">The settings service.</param>
        public ConfigurationService(
            IFileSystem fileSystem,
            ISettingsService settingsService
        )
        {
            TraceService.WriteLine("ConfigurationService::Constructor");

            this.fileSystem = fileSystem;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Creates the user directories.
        /// </summary>
        public void CreateUserDirectories()
        {
            TraceService.WriteLine("ConfigurationService::CreateUserDirectories");

            //// we only need to do the once!
            if (this.settingsService.DefaultUsersPathsSet == false)
            {
                string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

                string applicationPath = this.CreateDirectoryIfNotExist(myDocumentsPath, Settings.ApplicationName);

                string path = this.CreateDirectoryIfNotExist(applicationPath, @"Plugins\");
                this.settingsService.DefaultUserPluginsPath = path;

                path = this.CreateDirectoryIfNotExist(applicationPath, @"Services\");
                this.settingsService.DefaultUserServicesPath = path;
                
                string parentPath = this.CreateDirectoryIfNotExist(applicationPath, @"CodeSnippets\");

                path = this.CreateDirectoryIfNotExist(parentPath, @"Plugins\");
                this.settingsService.DefaultUserCodeSnippetsPluginsPath = path;

                path = this.CreateDirectoryIfNotExist(parentPath, @"Services\");
                this.settingsService.DefaultUserCodeSnippetsServicesPath = path;
                
                parentPath = this.CreateDirectoryIfNotExist(applicationPath, @"Config\");

                path = this.CreateDirectoryIfNotExist(parentPath, @"Plugins\");
                this.settingsService.DefaultUserCodeConfigPluginsPath = path;

                path = this.CreateDirectoryIfNotExist(parentPath, @"Services\");
                this.settingsService.DefaultUserCodeConfigServicesPath = path;

                this.settingsService.DefaultUsersPathsSet = true;
            }
        }

        /// <summary>
        /// Creates the directory if it doesn't exist.
        /// </summary>
        /// <param name="parentPath">The parent path.</param>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns> The new directory path. </returns>
        internal string CreateDirectoryIfNotExist(
            string parentPath,
            string directoryName)
        {
            if (parentPath.EndsWith(@"\"))
            {
                parentPath = parentPath.Remove(parentPath.Length-1);
            }

            string path = string.Format(@"{0}\{1}", parentPath, directoryName);

            TraceService.WriteLine("ConfigurationService::CreateDirectoryIfNotExist path=" + path);

            if (this.fileSystem.Directory.Exists(path) == false)
            {
                try
                {
                    this.fileSystem.Directory.CreateDirectory(path);
                }
                catch (Exception exception)
                {
                    TraceService.WriteError("Error Creating directory Error=" + exception.Message);
                }
            }

            return path;
        }
    }
}
