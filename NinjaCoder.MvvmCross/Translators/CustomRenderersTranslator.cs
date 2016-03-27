// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the CustomRenderersTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using Entities;
    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Services;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    /// Defines the CustomRenderersTranslator type.
    /// </summary>
    public class CustomRenderersTranslator : ITranslator<string, CustomRenderers>
    {
        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns></returns>
        public CustomRenderers Translate(string @from)
        {
            TraceService.WriteLine("CustomRenderers::Translate " + @from);

            XDocument doc = XDocument.Load(@from);

            CustomRenderers customRenderers = new CustomRenderers();

            if (doc.Root != null)
            {
                customRenderers.HelpLink = this.GetHelpLink(doc.Root);
                customRenderers.Groups = this.GetGroups(doc.Root);
            }

            return customRenderers;
        }

        /// <summary>
        /// Gets the help link.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal string GetHelpLink(XElement element)
        {
            XElement xElement = element.Element("HelpLink");

            return xElement?.Value ?? string.Empty;
        }

        /// <summary>
        /// Gets the groups.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal IEnumerable<CustomerRendererGroup> GetGroups(XElement element)
        {
            IEnumerable<XElement> groupElements = element.Elements("CustomRendererGroup");

            return groupElements.Select(this.GetGroup).ToList();
        }

        /// <summary>
        /// Gets the group.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal CustomerRendererGroup GetGroup(XElement element)
        {
            CustomerRendererGroup group = new CustomerRendererGroup();

            group.Name = element.Attribute("Name").Value;

            string codeBlock = string.Empty;

            XElement codeBlockElement = element.Element("CodeBlock");

            if (codeBlockElement != null)
            {
                codeBlock = codeBlockElement.Value;
            }

            group.CodeBlock = codeBlock;

            IEnumerable<XElement> rendererElements = element.Elements("CustomRenderer");

            List<CustomerRenderer> renderers = rendererElements.Select(this.GetRenderer).ToList();

            group.Renderers = renderers;

            return group;
        }

        /// <summary>
        /// Gets the renderer.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal CustomerRenderer GetRenderer(XElement element)
        {
            string codeBlock = string.Empty;

            XElement codeBlockElement = element.Element("CodeBlock");

            if (codeBlockElement != null)
            {
                codeBlock = codeBlockElement.Value;
            }

            CustomerRenderer renderer = new CustomerRenderer
            {
                Name = element.Element("Name").Value,
                CodeBlock = codeBlock
            };

            return renderer;
        }
    }
}
