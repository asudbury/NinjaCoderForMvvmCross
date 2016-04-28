// --------------------------------- -----------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using Entities;
    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// Defines the PluginsTranslator type.
    /// </summary>
    internal class PluginsTranslator : ITranslator<string, Plugins>
    {
        /// <summary>
        /// The translator.
        /// </summary>
        private readonly ITranslator<XElement, Plugin> translator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsTranslator"/> class.
        /// </summary>
        public PluginsTranslator()
            : this(new PluginTranslator())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsTranslator" /> class.
        /// </summary>
        /// <param name="translator">The translator.</param>
        public PluginsTranslator(ITranslator<XElement, Plugin> translator)
        {
            this.translator = translator;
        }

        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">The translation source..</param>
        /// <returns>A collection of Plugins.</returns>
        public Plugins Translate(string from)
        {
            try
            {
                XDocument doc = XDocument.Load(from);

                if (doc.Root != null)
                {
                    TraceService.WriteDebugLine(doc.Root.Value);

                    IEnumerable<XElement> elements = doc.Root.Elements("Plugin");

                    List<Plugin> items = elements.Select(element => this.translator.Translate(element)).ToList();

                    Plugins plugins = new Plugins { Items = items };

                    return plugins;
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}