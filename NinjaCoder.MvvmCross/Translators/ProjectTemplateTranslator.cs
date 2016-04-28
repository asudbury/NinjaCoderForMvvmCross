// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectTemplateTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using Interfaces;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Xml.Linq;

    /// <summary>
    ///  Defines the ProjectTemplateTranslator type.
    /// </summary>
    public class ProjectTemplateTranslator : IProjectTemplateTranslator
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The visual studio service.
        /// </summary>
        private readonly IVisualStudioService visualStudioService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectTemplateTranslator" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        public ProjectTemplateTranslator(
            ISettingsService settingsService,
            IVisualStudioService visualStudioService)
        {
            this.settingsService = settingsService;
            this.visualStudioService = visualStudioService;
        }

        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>A ProjectTemplateInfo.</returns>
        public ProjectTemplateInfo Translate(XElement from)
        {
            string projectName = from.GetSafeAttributeStringValue("Name");

            ProjectTemplateInfo projectTemplateInfo = new ProjectTemplateInfo
            {
                Name = projectName,
                ItemTemplates = this.GetTextTemplates(projectName, projectName, from)
            };

            return projectTemplateInfo;
        }

        /// <summary>
        /// Gets the text templates.
        /// </summary>
        /// <param name="nameSpace">The name space.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal IEnumerable<TextTemplateInfo> GetTextTemplates(
            string nameSpace,
            string projectName,
            XElement element)
        {
            List<TextTemplateInfo> textTemplateInfos= new List<TextTemplateInfo>();

            XElement itemTeplatesElement = element.Element("ItemTemplates");

            IEnumerable<XElement> itemElements = itemTeplatesElement?.Elements("Item");

            if (itemElements != null)
            {
                foreach (XElement itemElement in itemElements)
                {
                    TextTemplateInfo textTemplateInfo = new TextTemplateInfo
                    {
                        TemplateName =this.GetTemplatePath(projectName,itemElement),
                        ProjectSuffix = projectName, 
                        ProjectFolder = itemElement.GetSafeElementStringValue("Folder"),
                        FileName = itemElement.GetSafeElementStringValue("Target"),
                        Tokens = new Dictionary<string, string>
                                     {
                                        { "NameSpace",
                                           this.GetNameSpace(projectName)
                                        }
                                     }
                    };

                    textTemplateInfos.Add(textTemplateInfo);
                }
            }

            return textTemplateInfos;
        }

        /// <summary>
        /// Gets the name space.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <returns>The namespace.</returns>
        internal string GetNameSpace(string projectName)
        {
            //// at this moment we dont know the name space - will be amended later!

            //// this will always return null!
            IProjectService projectService = this.visualStudioService.GetProjectServiceBySuffix(projectName);

            return projectService != null ? projectService.Name : string.Empty;
        }

        /// <summary>
        /// Gets the template path.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The template path.
        /// </returns>
        internal string GetTemplatePath(
            string projectName,
            XElement element)
        {
            string fileName = element.GetSafeElementStringValue("Source");

            if (fileName != string.Empty)
            {
                string baseDirectory = this.settingsService.ProjectTemplateItemsDirectory;
                string subDirectory = this.settingsService.FrameworkType.ToString();

                return $@"{baseDirectory}\{projectName}\{subDirectory}\{fileName}";
            }

            return string.Empty;
        }
    }
}
