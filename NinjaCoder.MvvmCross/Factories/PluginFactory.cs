// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using Entities;
    using Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the PluginFactory type.
    /// </summary>
    public class PluginFactory : IPluginFactory
    {
        /// <summary>
        /// The pluginsTranslator.
        /// </summary>
        private readonly ITranslator<string, Plugins> pluginsTranslator;

        /// <summary>
        /// The caching service.
        /// </summary>
        private readonly ICachingService cachingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginFactory" /> class.
        /// </summary>
        /// <param name="pluginsTranslator">The pluginsTranslator.</param>
        /// <param name="cachingService">The caching service.</param>
        public PluginFactory(
            ITranslator<string, Plugins> pluginsTranslator,
            ICachingService cachingService)
        {
            TraceService.WriteLine("PluginFactory::Constructor");

            this.pluginsTranslator = pluginsTranslator;
            this.cachingService = cachingService;
        }

        /// <summary>
        /// Gets the plugins.
        /// </summary>
        /// <returns>The plugins.</returns>
        public Plugins GetPlugins(string uri)
        {
            TraceService.WriteLine("PluginFactory::GetPlugins");

            if (this.cachingService.Plugins.ContainsKey(uri))
            {
                TraceService.WriteLine("Using cache");
                return this.cachingService.Plugins[uri];
            }

            Plugins plugins =  this.pluginsTranslator.Translate(uri);

            this.cachingService.Plugins.Add(uri, plugins);

            return plugins;
        }
    }
}
