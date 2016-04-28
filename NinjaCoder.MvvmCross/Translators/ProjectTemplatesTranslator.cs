// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectTemplatesTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    ///  Defines the ProjectTemplatesTranslator type.
    /// </summary>
    public class ProjectTemplatesTranslator : IProjectTemplatesTranslator
    {
        /// <summary>
        /// The translator.
        /// </summary>
        private readonly IProjectTemplateTranslator translator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectTemplatesTranslator" /> class.
        /// </summary>
        /// <param name="translator">The translator.</param>
        public ProjectTemplatesTranslator(IProjectTemplateTranslator translator)
        {
            this.translator = translator;
        }

        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns></returns>
        public IEnumerable<ProjectTemplateInfo> Translate(string from)
        {
            List<ProjectTemplateInfo> projectTemplateInfos = new List<ProjectTemplateInfo>();

            try
            {
                XDocument doc = XDocument.Load(@from);

                if (doc.Root != null)
                {
                    TraceService.WriteDebugLine(doc.Root.Value);

                    IEnumerable<XElement> projectElements = doc.Root.Elements("Project");

                    projectTemplateInfos.AddRange(projectElements.Select(projectElement => this.translator.Translate(projectElement)));
                }
            }
            catch (Exception exception)
            {
                TraceService.WriteError("Failed to load project templates document=" + from + " exception=" + exception.StackTrace);
            } 

            return projectTemplateInfos;
        }
    }
}
