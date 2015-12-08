// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IProjectService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Services.Interfaces
{
    using EnvDTE;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;
    using VSLangProj;

    /// <summary>
    /// Defines the IProjectService type.
    /// </summary>
    public interface IProjectService
    {
        /// <summary>
        /// Gets the project.
        /// </summary>
        Project Project { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the project path.
        /// </summary>
        /// <returns>The project path.</returns>
        string GetProjectPath();

        /// <summary>
        /// Gets the project items.
        /// </summary>
        /// <returns>The project items.</returns>
        IEnumerable<IProjectItemService> GetProjectItems();

        /// <summary>
        /// Gets the c# project items.
        /// </summary>
        /// <returns>The project items.</returns>
        IEnumerable<IProjectItemService> GetCSharpProjectItems();
        
        /// <summary>
        /// Gets the project item.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The project item.</returns>
        IProjectItemService GetProjectItem(string fileName);

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns>The folder.</returns>
        IProjectItemService GetFolder(string folderName);

        /// <summary>
        /// Gets the project references.
        /// </summary>
        /// <returns>The project references.</returns>
        IEnumerable<Reference> GetProjectReferences();

        /// <summary>
        /// Adds the project reference.
        /// </summary>
        /// <param name="referencedProjectService">The referenced project service.</param>
        /// <returns>The reference.</returns>
        Reference AddProjectReference(IProjectService referencedProjectService);

        /// <summary>
        /// Adds the item to folder from template.
        /// </summary>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>True or False.</returns>
        bool AddItemToFolderFromTemplate(
            string templateName,
            string fileName);

        /// <summary>
        /// Adds the project to folder from template.
        /// </summary>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>True or False.</returns>
        bool AddProjectToFolderFromTemplate(
            string templateName,
            string path,
            string fileName);

        /// <summary>
        /// Adds to folder from file.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>True or False.</returns>
        bool AddToFolderFromFile(
             string folderName,
             string fileName);

        /// <summary>
        /// Adds the reference.
        /// </summary>
        /// <param name="destinationFolder">The destination folder.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="source">The source.</param>
        void AddReference(
            string destinationFolder,
            string destination,
            string source);

        /// <summary>
        /// Saves all.
        /// </summary>
        void SaveAll();

        /// <summary>
        /// Writes the status bar message.
        /// </summary>
        /// <param name="message">The message.</param>
        void WriteStatusBarMessage(string message);

        /// <summary>
        /// Gets the folder items.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="withFileExtensions">if set to <c>true</c> [with file extensions].</param>
        /// <returns>The folder items.</returns>
        IEnumerable<string> GetFolderItems(
            string folderName, 
            bool withFileExtensions);

        /// <summary>
        /// Removes the reference.
        /// </summary>
        /// <param name="referenceName">Name of the reference.</param>
        void RemoveReference(string referenceName);

        /// <summary>
        /// Removes the reference.
        /// </summary>
        /// <param name="value">The value.</param>
        void RemoveReferences(string value);

        /// <summary>
        /// Removes the folder.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns>The projectItem Service.</returns>
        IProjectItemService RemoveFolder(string folderName);

        /// <summary>
        /// Removes the folder item.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="itemName">Name of the item.</param>
        void RemoveFolderItem(
            string folderName, 
            string itemName);

        /// <summary>
        /// Removes the folder item.
        /// </summary>
        /// <param name="itemName">Name of the item.</param>
        void RemoveFolderItem(string itemName);

        /// <summary>
        /// Gets the sub projects.
        /// </summary>
        IEnumerable<IProjectService> GetSubProjects();

        /// <summary>
        /// Gets the sub folders.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        IEnumerable<IProjectItemService> GetSubFolders(string folderName);

        IEnumerable<IProjectItemService> GetFolderProjectItems();

        /// <summary>
        /// Gets the folder or create.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        IProjectItemService GetFolderOrCreate(string folderName);

        /// <summary>
        /// Adds the text template.
        /// </summary>
        /// <param name="textTemplateInfo">The text template information.</param>
        /// <returns></returns>
        string AddTextTemplate(TextTemplateInfo textTemplateInfo);
    }
}
