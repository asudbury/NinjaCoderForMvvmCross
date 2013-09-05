// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ISolutionService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Services.Interfaces
{
    using System.Collections.Generic;

    using EnvDTE;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the ISolutionService type.
    /// </summary>
    public interface ISolutionService
    {
        /// <summary>
        /// Gets the full name.
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// Creates the empty solution.
        /// </summary>
        /// <param name="solutionPath">The solution path.</param>
        /// <param name="solutionName">Name of the solution.</param>
        void CreateEmptySolution(
            string solutionPath, 
            string solutionName);

        /// <summary>
        /// Gets the directory name.
        /// </summary>
        /// <returns>The Directory path.</returns>
        string GetDirectoryName();

        /// <summary>
        /// Gets the project template.
        /// </summary>
        /// <param name="templateName">ProjectName of the template.</param>
        /// <returns>The path of the template.</returns>
        string GetProjectTemplate(string templateName);

        /// <summary>
        /// Gets the item template.
        /// </summary>
        /// <param name="templateName">ProjectName of the template.</param>
        /// <returns>The path of the template.</returns>
        string GetProjectItemTemplate(string templateName);

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="solutionFolder">The solution folder.</param>
        /// <param name="path">The path.</param>
        /// <returns>
        /// The project item service..
        /// </returns>
        IProjectItemService AddSolutionItem(
            string solutionFolder, 
            string path);

        /// <summary>
        /// Adds the project to solution.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="templatePath">The template path.</param>
        /// <param name="projectName">ProjectName of the project.</param>
        void AddProjectToSolution(
            string path, 
            string templatePath, 
            string projectName);

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>The projects.</returns>
        IEnumerable<Project> GetProjects();

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <returns>The project.</returns>
        Project GetProject(string projectName);

        /// <summary>
        /// Gets the project service.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <returns>The project service.</returns>
        IProjectService GetProjectService(string projectName);

        /// <summary>
        /// Adds the item template to project.
        /// </summary>
        /// <param name="templateInfos">The template infos.</param>
        /// <param name="createFolder">if set to <c>true</c> [create folder].</param>
        /// <returns> The messages. </returns>
        IEnumerable<string> AddItemTemplateToProjects(
            IEnumerable<ItemTemplateInfo> templateInfos, 
            bool createFolder);

        /// <summary>
        /// Removes the folder.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        void RemoveFolder(string folderName);

        /// <summary>
        /// Removes the comments.
        /// </summary>
        void RemoveFileHeaders();

        /// <summary>
        /// Removes the comments.
        /// </summary>
        void RemoveComments();

        /// <summary>
        /// Gets the solution item path.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <returns>The solution item path.</returns>
        string GetSolutionItemPath(string file);

        /// <summary>
        /// Writes the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The contents.</param>
        void WriteFile(
            string path, 
            string contents);

        /// <summary>
        /// Creates the file.
        /// </summary>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        void CreateFile(
            string file, 
            string contents);

        /// <summary>
        /// Gets the project item service.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The project item service.</returns>
        IProjectItemService GetProjectItemService(string path);
    }
}