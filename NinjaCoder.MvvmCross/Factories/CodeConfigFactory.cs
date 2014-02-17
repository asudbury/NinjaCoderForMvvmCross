// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CodeConfigFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using System.IO.Abstractions;

    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the CodeConfigFactory type.
    /// </summary>
    public  class CodeConfigFactory : ICodeConfigFactory
    {
        /// <summary>
        /// The code config service.
        /// </summary>
        private readonly ICodeConfigService codeConfigService;

        /// <summary>
        /// The file system.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The translator.
        /// </summary>
        private readonly ITranslator<string, CodeConfig> translator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeConfigFactory" /> class.
        /// </summary>
        /// <param name="codeConfigService">The code config service.</param>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="translator">The translator.</param>
        public CodeConfigFactory(
            ICodeConfigService codeConfigService,
            IFileSystem fileSystem,
            ISettingsService settingsService,
            ITranslator<string, CodeConfig> translator)
        {
            TraceService.WriteLine("CodeConfigFactory::Constructor");

            this.codeConfigService = codeConfigService;
            this.fileSystem = fileSystem;
            this.settingsService = settingsService;
            this.translator = translator;
        }

        /// <summary>
        /// Gets the code config service.
        /// </summary>
        /// <returns>The code config service.</returns>
        public ICodeConfigService GetCodeConfigService()
        {
            TraceService.WriteLine("CodeConfigFactory::GetCodeConfigService");

            return this.codeConfigService;
        }

        /// <summary>
        /// Gets the plugin config.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <returns>The code config.</returns>
        public CodeConfig GetPluginConfig(Plugin plugin)
        {
            TraceService.WriteLine("CodeConfigFactory::GetPluginConfig plugin=" + plugin.FriendlyName);

            string fileName = string.Format(@"Config.Plugin.{0}.xml", plugin.FriendlyName);

            return this.GetConfig(
                this.settingsService.PluginsConfigPath,
                this.settingsService.UserCodeConfigPluginsPath,
                fileName);
        }

        /// <summary>
        /// Gets the service config.
        /// </summary>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <returns>The code config.</returns>
        public CodeConfig GetServiceConfig(string friendlyName)
        {
            TraceService.WriteLine("CodeConfigFactory::GetServiceConfig name=" + friendlyName);

            string fileName = string.Format("Config.Service.{0}.xml", friendlyName);

            return this.GetConfig(
                this.settingsService.ServicesConfigPath,
                this.settingsService.UserCodeConfigServicesPath,
                fileName);
        }

        /// <summary>
        /// Gets the config.
        /// </summary>
        /// <param name="coreDirectory">The core directory.</param>
        /// <param name="overrideDirectory">The override directory.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The code snippet.</returns>
        internal CodeConfig GetConfig(
            string coreDirectory,
            string overrideDirectory,
            string fileName)
        {
            TraceService.WriteLine("CodeConfigFactory::GetConfig fileName=" + fileName);

            //// first check the user directory
            string pluginPath = string.Format(
                "{0}{1}",
                overrideDirectory,
                fileName);

            if (this.fileSystem.File.Exists(pluginPath))
            {
                TraceService.WriteLine("path=" + pluginPath);

                return this.GetCodeConfigFromPath(pluginPath);
            }

            //// use the core if no user version of the snippet.
            pluginPath = string.Format(
                "{0}{1}",
                coreDirectory,
                fileName);

            TraceService.WriteLine("path=" + pluginPath);

            return this.GetCodeConfigFromPath(pluginPath);
        }

        /// <summary>
        /// Gets the code config from path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The code config.</returns>
        internal CodeConfig GetCodeConfigFromPath(string path)
        {
            TraceService.WriteLine("CodeConfigFactory::GetCodeConfigFromPath path=" + path);

            return this.translator.Translate(path);
        }
    }
}
