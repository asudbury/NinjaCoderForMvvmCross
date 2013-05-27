// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ConfigurationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System;
    using System.IO;

    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    /// <summary>
    /// Defines the ConfigurationService type.
    /// </summary>
    public class ConfigurationService : IConfigurationService
    {
        /// <summary>
        /// Creates the user directories.
        /// </summary>
        public void CreateUserDirectories()
        {
            string myDocumentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            string parentPath = CreateDirectoryIfNotExist(myDocumentsPath, Settings.ApplicationName);
            CreateDirectoryIfNotExist(parentPath, "CodeSnippets");
            string mvvmCrossPath = CreateDirectoryIfNotExist(parentPath, "MvvmCross");
            string assembliesPath = CreateDirectoryIfNotExist(mvvmCrossPath, "Assemblies");
            CreatePluginsDirectoryIfNotExist(assembliesPath, Settings.Core);
            CreatePluginsDirectoryIfNotExist(assembliesPath, Settings.Droid);
            CreatePluginsDirectoryIfNotExist(assembliesPath, Settings.iOS);
            CreatePluginsDirectoryIfNotExist(assembliesPath, Settings.WindowsPhone);
            CreatePluginsDirectoryIfNotExist(assembliesPath, Settings.WindowsStore);
            CreatePluginsDirectoryIfNotExist(assembliesPath, Settings.Wpf);
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
            string path = string.Format(@"{0}\{1}", parentPath, directoryName);

            if (Directory.Exists(path) == false)
            {
                Directory.CreateDirectory(path);
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
            string path = CreateDirectoryIfNotExist(parentPath, directoryName);
            return CreateDirectoryIfNotExist(path, "Plugins");
        }
    }
}
