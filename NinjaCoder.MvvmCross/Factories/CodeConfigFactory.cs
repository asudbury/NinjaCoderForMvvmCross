// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CodeConfigFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using Interfaces;
    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System.IO.Abstractions;

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
                fileName);
        }

        /// <summary>
        /// Gets the config.
        /// </summary>
        /// <param name="coreDirectory">The core directory.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The code snippet.</returns>
        internal CodeConfig GetConfig(
            string coreDirectory,
            string fileName)
        {
            TraceService.WriteLine("CodeConfigFactory::GetConfig fileName=" + fileName);

            //// use the core if no user version of the snippet.
            string pluginPath = string.Format(
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
