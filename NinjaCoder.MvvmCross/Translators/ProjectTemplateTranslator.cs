// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectTemplateTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Entities;
    using Services;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Xml.Linq;

    /// <summary>
    ///  Defines the ProjectTemplateTranslator type.
    /// </summary>
    public class ProjectTemplateTranslator : ITranslator<XElement, ProjectTemplateInfo>
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectTemplateTranslator"/> class.
        /// </summary>
        public ProjectTemplateTranslator()
            :this(new SettingsService())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectTemplateTranslator"/> class.
        /// </summary>
        public ProjectTemplateTranslator(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>A ProjectTemplateInfo.</returns>
        public ProjectTemplateInfo Translate(XElement @from)
        {
            ProjectTemplateInfo projectTemplateInfo = new ProjectTemplateInfo
            {
                Name = @from.GetSafeAttributeStringValue("Name"),
                ItemTemplates = this.GetTextTemplates(@from)
            };

            return projectTemplateInfo;
        }

        /// <summary>
        /// Gets the text templates.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal IEnumerable<TextTemplateInfo> GetTextTemplates(XElement element)
        {
            List<TextTemplateInfo> textTemplateInfos= new List<TextTemplateInfo>();

            XElement itemTeplatesElement = element.Element("ItemTemplates");

            IEnumerable<XElement> itemElements = itemTeplatesElement?.Elements("Item");

            if (itemElements != null)
            {
                foreach (XElement itemElement in itemElements)
                {
                    TextTemplateInfo textTemplateInfo = new TextTemplateInfo();

                    textTemplateInfos.Add(textTemplateInfo);
                }
            }

            return textTemplateInfos;
        }
    }
}
