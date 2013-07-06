// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IProjectService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Services.Interfaces
{
    using System.Collections.Generic;

    using EnvDTE;

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
        /// <param name="referencedProject">The referenced project.</param>
        /// <returns>The reference.</returns>
        Reference AddProjectReference(Project referencedProject);

        /// <summary>
        /// Adds to folder from template.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="createFolder">if set to <c>true</c> [create folder].</param>
        /// <returns>True or False.</returns>
        bool AddToFolderFromTemplate(
            string folderName,
            string templateName,
            string fileName,
            bool createFolder);

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
    }
}
