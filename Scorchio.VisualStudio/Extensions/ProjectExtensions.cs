// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Scorchio.VisualStudio.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using EnvDTE;

    using EnvDTE80;

    using Scorchio.VisualStudio.Services;

    using VSLangProj;

    /// <summary>
    ///  Defines the ProjectExtensions type.
    /// </summary>
    public static class ProjectExtensions
    {
        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The associated icon.</returns>
        /*public static ImageSource GetIcon(this Project instance)
        {
            IconService iconService = new IconService();

            return iconService.GetImageSource(instance.ProjectName, false) ??
                   iconService.GetFolderImageSource(false);
        }*/

        public static string GetProjectPath(this Project instance)
        {
            string path = string.Empty;

            if (instance == null)
            {
                return path;
            }

            try
            {
                path = System.IO.Path.GetDirectoryName(instance.FullName);
            }
            catch
            {
            }

            return path;
        }

        /// <summary>
        /// Gets the project items.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The project items.</returns>
        public static IEnumerable<ProjectItem> GetProjectItems(this Project instance)
        {
            return instance.ProjectItems != null ? instance.ProjectItems.Cast<ProjectItem>().ToList() : null;
        }

        /// <summary>
        /// Gets the c# project items.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The project items.</returns>
        public static IEnumerable<ProjectItem> GetCSharpProjectItems(this Project instance)
        {
            return instance.ProjectItems.Cast<ProjectItem>().Where(x => x.Name.EndsWith(".cs")).ToList();
        }

        /// <summary>
        /// Gets the project references.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The project references.</returns>
        public static IEnumerable<Reference> GetProjectReferences(this Project instance)
        {
            VSProject vsProject = instance.Object as VSProject;

            return vsProject != null ? vsProject.References.Cast<Reference>() : null;
        }

        /// <summary>
        /// Adds the project reference.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="referencedProject">The referenced project.</param>
        /// <returns>The reference.</returns>
        public static Reference AddProjectReference(
            this Project instance, 
            Project referencedProject)
        {

            VSProject vsproject = (VSProject)instance.Object;

            return vsproject.References.AddProject(referencedProject);
        }

        /// <summary>
        /// Adds to folder from template.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>True or False.</returns>
        public static bool AddToFolderFromTemplate(
            this Project instance,
            string folderName,
            string templateName,
            string fileName)
        {
            ProjectItem folderProjectItem = instance.ProjectItems
                .Cast<ProjectItem>()
                .FirstOrDefault(projectItem => projectItem.Name == folderName);

            //// if the folder doesn't exist create it.
            if (folderProjectItem == null)
            {
                folderProjectItem = instance.ProjectItems.AddFolder(folderName);
            }

            Solution2 solution = instance.DTE.Solution as Solution2;

            string templatePath = solution.GetProjectItemTemplate(templateName);

            if (templatePath != null)
            {
                folderProjectItem.ProjectItems.AddFromTemplate(templatePath, fileName);
                return true;
            }

            TraceService.WriteError("ProjectExtensions::AddToFolderFromTemplate Cannot find template " + templateName);

            return false;
        }
    }
}
