// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using System;
    using System.IO.Abstractions;
    using System.Linq;

    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Services;

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
        private readonly ITranslator<Tuple<DirectoryInfoBase, DirectoryInfoBase>, Plugins> pluginsTranslator;

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
            ITranslator<Tuple<DirectoryInfoBase, DirectoryInfoBase>, Plugins> pluginsTranslator,
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

            return this.pluginsTranslator.Translate(this.GetDirectories());
        }

        /// <summary>
        /// Gets the name of the plugin by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The plugin.</returns>
        public Plugin GetPluginByName(string name)
        {
            TraceService.WriteLine("PluginFactory::GetPluginByFriendlyName name=" + name);

            Tuple<DirectoryInfoBase, DirectoryInfoBase> directories = this.GetDirectories();

            FileInfoBase fileInfoBase = null;

            //// first check the users directory.
            if (directories.Item2 != null)
            {
                fileInfoBase = directories.Item2.GetFiles()
                                 .FirstOrDefault(x => x.Name.Contains(name));
            }

            //// now the core directory.
            if (fileInfoBase == null)
            {
                fileInfoBase = directories.Item1.GetFiles()
                                 .FirstOrDefault(x => x.Name.Contains(name));
            }

            return fileInfoBase != null ? this.pluginTranslator.Translate(fileInfoBase) : null;
        }

        /// <summary>
        /// Gets the directories.
        /// </summary>
        /// <returns>The directories.</returns>
        internal Tuple<DirectoryInfoBase, DirectoryInfoBase> GetDirectories()
        {
            TraceService.WriteLine("PluginFactory::GetDirectories");

            DirectoryInfoBase directoryInfoBase1 = fileSystem.DirectoryInfo.FromDirectoryName(this.settingsService.MvvmCrossAssembliesPath);

            string userPluginsPath = this.settingsService.UserPluginsPath;
            DirectoryInfoBase directoryInfoBase2 = null;

            //// check user plugins directory actually exists.
            if (fileSystem.Directory.Exists(userPluginsPath))
            {
                directoryInfoBase2 = fileSystem.DirectoryInfo.FromDirectoryName(userPluginsPath);
            }

            return new Tuple<DirectoryInfoBase, DirectoryInfoBase>(directoryInfoBase1, directoryInfoBase2);
        }
    }
}
