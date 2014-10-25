// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using Entities;

    using Interfaces;
    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.Linq;

    /// <summary>
    ///  Defines the PluginFactory type.
    /// </summary>
    public class PluginFactory : IPluginFactory
    {
        /// <summary>
        /// The plugin service.
        /// </summary>
        private readonly IPluginsService pluginsService;

        /// <summary>
        /// The file system.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The pluginsTranslator.
        /// </summary>
        private readonly ITranslator<IEnumerable<DirectoryInfoBase>, Plugins> pluginsTranslator;

        /// <summary>
        /// The plugin translator
        /// </summary>
        private readonly ITranslator<FileInfoBase, Plugin> pluginTranslator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginFactory" /> class.
        /// </summary>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="pluginsTranslator">The pluginsTranslator.</param>
        /// <param name="pluginTranslator">The plugin translator.</param>
        public PluginFactory(
            IPluginsService pluginsService,
            IFileSystem fileSystem,
            ISettingsService settingsService,
            ITranslator<IEnumerable<DirectoryInfoBase>, Plugins> pluginsTranslator,
            ITranslator<FileInfoBase, Plugin> pluginTranslator)
        {
            TraceService.WriteLine("PluginFactory::Constructor");

            this.pluginsService = pluginsService;
            this.fileSystem = fileSystem;
            this.settingsService = settingsService;
            this.pluginsTranslator = pluginsTranslator;
            this.pluginTranslator = pluginTranslator;
        }

        /// <summary>
        /// Gets the plugins service.
        /// </summary>
        /// <returns>The plugin service.</returns>
        public IPluginsService GetPluginsService()
        {
            return this.pluginsService;
        }

        /// <summary>
        /// Gets the plugins.
        /// </summary>
        /// <returns>The plugins.</returns>
        public Plugins GetPlugins()
        {
            TraceService.WriteLine("PluginFactory::GetPlugins");

            IEnumerable<DirectoryInfoBase> directories = this.GetDirectories();

            return this.pluginsTranslator.Translate(directories);
        }

        /// <summary>
        /// Gets the name of the plugin by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The plugin.</returns>
        public Plugin GetPluginByName(string name)
        {
            TraceService.WriteLine("PluginFactory::GetPluginByFriendlyName name=" + name);

            FileInfoBase fileInfoBase = null;

            IEnumerable<DirectoryInfoBase> directories =  this.GetDirectories();

            foreach (DirectoryInfoBase directoryInfoBase in directories.Reverse())
            {
                fileInfoBase = directoryInfoBase
                    .GetFiles() 
                    .FirstOrDefault(x => x.Name.Contains(name));

                if (fileInfoBase != null)
                {
                    break;
                }
            }

            return fileInfoBase != null ? this.pluginTranslator.Translate(fileInfoBase) : null;
        }

        /// <summary>
        /// Gets the directories.
        /// </summary>
        /// <returns>The directories.</returns>
        internal IEnumerable<DirectoryInfoBase> GetDirectories()
        {
            TraceService.WriteLine("PluginFactory::GetDirectories");

            List<DirectoryInfoBase> directories = new List<DirectoryInfoBase>
                                                      {
                                                          this.fileSystem.DirectoryInfo.FromDirectoryName(this.settingsService.PluginsConfigPath),
                                                          this.fileSystem.DirectoryInfo.FromDirectoryName(this.settingsService.PluginsConfigPath + @"Community\")
                                                      };

            string userPluginsPath = this.settingsService.UserPluginsPath;

            //// check user plugins directory actually exists.
            if (fileSystem.Directory.Exists(userPluginsPath))
            {
                directories.Add(this.fileSystem.DirectoryInfo.FromDirectoryName(userPluginsPath));
            }

            return directories;
        }
    }
}
