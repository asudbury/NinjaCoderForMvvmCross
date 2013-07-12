// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ConfigurationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System;
    using System.IO;

    using Constants;
    using Interfaces;

    using Scorchio.VisualStudio.Services;

    /// <summary>
    /// Defines the ConfigurationService type.
    /// </summary>
    public class ConfigurationService : BaseService, IConfigurationService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationService" /> class.
        /// </summary>
        public ConfigurationService()
        {
            TraceService.WriteLine("ConfigurationService::Constructor");
        }

        /// <summary>
        /// Creates the user directories.
        /// </summary>
        public void CreateUserDirectories()
        {
            TraceService.WriteLine("ConfigurationService::CreateUserDirectories");

            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string parentPath = this.CreateDirectoryIfNotExist(myDocumentsPath, Settings.ApplicationName);
            this.CreateDirectoryIfNotExist(parentPath, "CodeSnippets");
            string mvvmCrossPath = this.CreateDirectoryIfNotExist(parentPath, "MvvmCross");
            string assembliesPath = this.CreateDirectoryIfNotExist(mvvmCrossPath, "Assemblies");
            this.CreatePluginsDirectoryIfNotExist(assembliesPath, Settings.Core);
            this.CreatePluginsDirectoryIfNotExist(assembliesPath, Settings.Droid);
            this.CreatePluginsDirectoryIfNotExist(assembliesPath, Settings.iOS);
            this.CreatePluginsDirectoryIfNotExist(assembliesPath, Settings.WindowsPhone);
            this.CreatePluginsDirectoryIfNotExist(assembliesPath, Settings.WindowsStore);
            this.CreatePluginsDirectoryIfNotExist(assembliesPath, Settings.Wpf);
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
            TraceService.WriteLine("ConfigurationService::CreateDirectoryIfNotExist directoryName=" + directoryName);

            string path = string.Format(@"{0}\{1}", parentPath, directoryName);

            if (Directory.Exists(path) == false)
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch (Exception exception)
                {
                    TraceService.WriteError("Error Creating directory Error=" + exception.Message);
                }
            }

            return path;
        }

        /// <summary>
        /// Creates the plugins directory if not exist.
        /// </summary>
        /// <param name="parentPath">The parent path.</param>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns>The new directory path.</returns>
        internal string CreatePluginsDirectoryIfNotExist(
            string parentPath,
            string directoryName)
        {
            TraceService.WriteLine("ConfigurationService::CreatePluginsDirectoryIfNotExist directoryName=" + directoryName);

            string path = this.CreateDirectoryIfNotExist(parentPath, directoryName);
            return this.CreateDirectoryIfNotExist(path, "Plugins");
        }
    }
}
