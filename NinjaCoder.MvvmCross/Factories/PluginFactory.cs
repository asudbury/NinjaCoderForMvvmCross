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
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The pluginsTranslator.
        /// </summary>
        private readonly ITranslator<string, Plugins> pluginsTranslator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginFactory" /> class.
        /// </summary>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="pluginsTranslator">The pluginsTranslator.</param>
        public PluginFactory(
            IPluginsService pluginsService,
            ISettingsService settingsService,
            ITranslator<string, Plugins> pluginsTranslator)
        {
            TraceService.WriteLine("PluginFactory::Constructor");

            this.pluginsService = pluginsService;
            this.settingsService = settingsService;
            this.pluginsTranslator = pluginsTranslator;
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
        public Plugins GetPlugins(string uri)
        {
            TraceService.WriteLine("PluginFactory::GetPlugins");
            
            return this.pluginsTranslator.Translate(uri);
        }
    }
}
