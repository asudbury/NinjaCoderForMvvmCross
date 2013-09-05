// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using EnvDTE;
    using EnvDTE80;
    using Extensions;
    using Interfaces;
    using VSLangProj;

    /// <summary>
    /// Defines the ProjectService type.
    /// </summary>
    public class ProjectService : IProjectService
    {
        /// <summary>
        /// The project.
        /// </summary>
        private readonly Project project;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectService" /> class.
        /// </summary>
        /// <param name="project">The project.</param>
        public ProjectService(Project project)
        {
            this.project = project;
        }

        /// <summary>
        /// Gets the project.
        /// </summary>
        public Project Project 
        { 
            get { return this.project; } 
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name 
        { 
            get { return this.project.Name; } 
        }

        /// <summary>
        /// Gets the project path.
        /// </summary>
        /// <returns>The project path.</returns>
        public string GetProjectPath()
        {
            return this.project.GetProjectPath();
        }

        /// <summary>
        /// Gets the project items.
        /// </summary>
        /// <returns> The project items.</returns>
        public IEnumerable<IProjectItemService> GetProjectItems()
        {
            IEnumerable<ProjectItem> items = this.project.GetProjectItems();

            return items.Select(projectItem => new ProjectItemService(projectItem))
                .Cast<IProjectItemService>().ToList();
        }

        /// <summary>
        /// Gets the c# project items.
        /// </summary>
        /// <returns>The project items.</returns>
        public IEnumerable<IProjectItemService> GetCSharpProjectItems()
        {
            IEnumerable<ProjectItem> items = this.project.GetCSharpProjectItems();

            return items.Select(projectItem => new ProjectItemService(projectItem))
                .Cast<IProjectItemService>().ToList();
        }

        /// <summary>
        /// Gets the project item.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// The project item.</returns>
        public IProjectItemService GetProjectItem(string fileName)
        {
            ProjectItem item = this.project.GetProjectItem(fileName);

            return new ProjectItemService(item);
        }

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns>The folder.</returns>
        public IProjectItemService GetFolder(string folderName)
        {
            ProjectItem item = this.project.GetFolder(folderName);

            return new ProjectItemService(item);
        }

        /// <summary>
        /// Gets the project references.
        /// </summary>
        /// <returns>
        /// The project references.</returns>
        public IEnumerable<Reference> GetProjectReferences()
        {
            return this.project.GetProjectReferences();
        }

        /// <summary>
        /// Adds the project reference.
        /// </summary>
        /// <param name="referencedProjectService">The referenced project service.</param>
        /// <returns>The reference.</returns>
        public Reference AddProjectReference(IProjectService referencedProjectService)
        {
            return this.project.AddProjectReference(referencedProjectService.Project);
        }

        /// <summary>
        /// Adds to folder from template.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="createFolder">if set to <c>true</c> [create folder].</param>
        /// <returns> True or False.</returns>
        public bool AddToFolderFromTemplate(
            string folderName, 
            string templateName, 
            string fileName, 
            bool createFolder)
        {
            return this.project.AddToFolderFromTemplate(
                folderName,
                templateName,
                fileName,
                createFolder);
        }

        /// <summary>
        /// Adds to folder from file.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>True or False.</returns>
        public bool AddToFolderFromFile(
            string folderName, 
            string fileName)
        {
            return this.project.AddToFolderFromFile(
                folderName,
                fileName);
        }

        /// <summary>
        /// Adds the reference.
        /// </summary>
        /// <param name="destinationFolder">The destination folder.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="source">The source.</param>
        /// <param name="addFileToFolder">if set to <c>true</c> [add file to folder].</param>
        public void AddReference(
            string destinationFolder, 
            string destination, 
            string source,
            bool addFileToFolder)
        {
            this.project.AddReference(
                destinationFolder,
                destination,
                source,
                addFileToFolder);
        }

        /// <summary>
        /// Saves all.
        /// </summary>
        public void SaveAll()
        {
            DTE2 dte2 = this.project.DTE as DTE2;
            dte2.SaveAll();
        }

        /// <summary>
        /// Writes the status bar message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void WriteStatusBarMessage(string message)
        {
            DTE2 dte2 = this.project.DTE as DTE2;
            dte2.StatusBar.Text = message;
        }

        /// <summary>
        /// Gets the folder items.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="withFileExtensions">if set to <c>true</c> [with file extensions].</param>
        /// <returns>The folder items.</returns>
        public IEnumerable<string> GetFolderItems(
            string folderName, 
            bool withFileExtensions)
        {
            return this.project.GetFolderItems(folderName, withFileExtensions);
        }

        /// <summary>
        /// Removes the reference.
        /// </summary>
        /// <param name="referenceName">Name of the reference.</param>
        public void RemoveReference(string referenceName)
        {
            this.project.RemoveReference(referenceName);
        }

        /// <summary>
        /// Removes the folder.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns>The projectItem Service.</returns>
        public ProjectItemService RemoveFolder(string folderName)
        {
            ProjectItem projectItem = this.project.RemoveFolder(folderName);

            return projectItem != null ? new ProjectItemService(projectItem) : null;
        }
    }
}
