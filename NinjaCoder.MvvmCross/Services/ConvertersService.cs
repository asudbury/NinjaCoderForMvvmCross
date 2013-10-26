// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ConvertersService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System.Collections.Generic;

    using Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the ConvertersService type.
    /// </summary>
    public class ConvertersService : BaseService, IConvertersService
    {
        /// <summary>
        /// Adds the converters.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="templatesPath">The templates path.</param>
        /// <param name="templateInfos">The template infos.</param>
        /// <returns>The messages.</returns>
        public IEnumerable<string> AddConverters(
            IProjectService projectService, 
            string templatesPath,
            List<ItemTemplateInfo> templateInfos)
        {
            if (projectService != null)
            {
                List<string> messages = new List<string>();

                templateInfos
                    .ForEach(x => this.AddConverter(projectService, templatesPath, messages, x));

                return messages;
            }

            TraceService.WriteError("ConvertersService::AddConverters cannot find Core project");
            return null;
        }

        /// <summary>
        /// Adds the converter.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="templatesPath">The templates path.</param>
        /// <param name="messages">The messages.</param>
        /// <param name="templateInfo">The template info.</param>
        internal void AddConverter(
            IProjectService projectService, 
            string templatesPath, 
            List<string> messages, 
            ItemTemplateInfo templateInfo)
        {
            TraceService.WriteLine("ConvertersService::AddConverters adding from template path " + templatesPath + " template=" + templateInfo.FileName);

            string fileName = templateInfo.FriendlyName + ".cs";

            projectService.AddToFolderFromTemplate("Converters", templateInfo.FileName, fileName, false);

            IProjectItemService projectItemService = projectService.GetProjectItem(fileName);

            //// if we find the project item replace the text in it.
            if (projectItemService != null)
            {
                projectItemService.ReplacePattern("MvvmCross." + templateInfo.FriendlyName, projectService.Name);
                projectItemService.Save();
            }

            messages.Add(@"Converters\" + fileName + " added to project " + projectService.Name + ".");
        }
    }
}
