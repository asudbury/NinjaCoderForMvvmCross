// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectTemplatesTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    ///  Defines the ProjectTemplatesTranslator type.
    /// </summary>
    public class ProjectTemplatesTranslator : ITranslator<string, IEnumerable<ProjectTemplateInfo>>
    {
        /// <summary>
        /// The translator.
        /// </summary>
        private readonly ITranslator<XElement, ProjectTemplateInfo> translator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectTemplatesTranslator"/> class.
        /// </summary>
        public ProjectTemplatesTranslator()
            :this(new ProjectTemplateTranslator())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectTemplatesTranslator" /> class.
        /// </summary>
        /// <param name="translator">The translator.</param>
        public ProjectTemplatesTranslator(ITranslator<XElement, ProjectTemplateInfo> translator)
        {
            this.translator = translator;
        }

        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns></returns>
        public IEnumerable<ProjectTemplateInfo> Translate(string @from)
        {
            List<ProjectTemplateInfo> projectTemplateInfos = new List<ProjectTemplateInfo>();

            XDocument doc = XDocument.Load(@from);

            if (doc.Root != null)
            {
                IEnumerable<XElement> projectElements = doc.Root.Elements("Project");

                projectTemplateInfos.AddRange(projectElements.Select(projectElement => this.translator.Translate(projectElement)));
            }

            return projectTemplateInfos;
        }
    }
}
