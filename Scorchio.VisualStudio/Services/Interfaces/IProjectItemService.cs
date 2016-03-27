// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IProjectItemService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Services.Interfaces
{
    using Entities;
    using EnvDTE;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the IProjectItemService type.
    /// </summary>
    public interface IProjectItemService
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Gets the kind.
        /// </summary>
        string Kind { get; }
        
        /// <summary>
        /// Gets the project item.
        /// </summary>
        ProjectItem ProjectItem { get; }

        /// <summary>
        /// Gets the sub project items.
        /// </summary>
        /// <returns>The sub project items.</returns>
        IEnumerable<ProjectItem> GetSubProjectItems();

        /// <summary>
        /// Gets the name space.
        /// </summary>
        /// <returns>The name space.</returns>
        CodeNamespace GetNameSpace();

        /// <summary>
        /// Adds the name space.
        /// </summary>
        /// <param name="nameSpace">The name space.</param>
        /// <returns>
        /// The created name space.
        /// </returns>
        CodeNamespace AddNameSpace(string nameSpace);

        /// <summary>
        /// Gets the first code element.
        /// </summary>
        /// <returns>The first code element</returns>
        CodeElement GetFirstCodeElement();

        /// <summary>
        /// Gets the first name space.
        /// </summary>
        /// <returns>The first namespace.</returns>
        CodeNamespace GetFirstNameSpace();

        /// <summary>
        /// Gets the first class.
        /// </summary>
        /// <returns>The first class.</returns>
        CodeClass GetFirstClass();

        /// <summary>
        /// Gets the first interface.
        /// </summary>
        /// <returns>The first interface.</returns>
        CodeInterface GetFirstInterface();

        /// <summary>
        /// Creates the interface.
        /// </summary>
        /// <returns>The interface.</returns>
        CodeInterface CreateInterface();

        /// <summary>
        /// Adds the header comment.
        /// </summary>
        /// <param name="headerComment">The header comment.</param>
        void AddHeaderComment(string headerComment);

        /// <summary>
        /// Implements the code snippet.
        /// </summary>
        /// <param name="codeSnippet">The code snippet.</param>
        /// <param name="formatFunctionParameters">if set to <c>true</c> [format function parameters].</param>
        void ImplementCodeSnippet(
            CodeSnippet codeSnippet,
            bool formatFunctionParameters);

        /// <summary>
        /// Implements the unit testing code snippet.
        /// </summary>
        /// <param name="codeSnippet">The code snippet.</param>
        /// <param name="codeFile">The code file.</param>
        /// <param name="removeHeader">if set to <c>true</c> [remove header].</param>
        /// <param name="removeComments">if set to <c>true</c> [remove comments].</param>
        /// <param name="formatFunctionParameters">if set to <c>true</c> [format function parameters].</param>
        void ImplementUnitTestingCodeSnippet(
            CodeSnippet codeSnippet, 
            string codeFile,
            bool removeHeader,
            bool removeComments,
            bool formatFunctionParameters);

        /// <summary>
        /// Adds the using statement.
        /// </summary>
        /// <param name="usingStatement">The using statement.</param>
        void AddUsingStatement(string usingStatement);

        /// <summary>
        /// Gets the using statements.
        /// </summary>
        /// <returns>A list of using statements.</returns>
        IEnumerable<string> GetUsingStatements();

        /// <summary>
        /// Moves the using statements.
        /// </summary>
        void MoveUsingStatements();

        /// <summary>
        /// Sorts the and remove using statements.
        /// </summary>
        void SortAndRemoveUsingStatements();

        /// <summary>
        /// Replaces the text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="replacementText">The replacement text.</param>
        void ReplaceText(
            string text, 
            string replacementText);

        /// <summary>
        /// Replaces the pattern.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="replacementText">The replacement text.</param>
        void ReplacePattern(
            string text, 
            string replacementText);

        /// <summary>
        /// Opens this instance.
        /// </summary>
        void Open();

        /// <summary>
        /// Saves this instance.
        /// </summary>
        void Save();

        /// <summary>
        /// Fixes the using statements.
        /// </summary>
        void FixUsingStatements();
  
        /// <summary>
        /// Removes the comments.
        /// </summary>
        void RemoveComments();

        /// <summary>
        /// Removes the header.
        /// </summary>
        void RemoveHeader();

        /// <summary>
        /// Determines whether [is C sharp file].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is C sharp file]; otherwise, <c>false</c>.
        /// </returns>
        bool IsCSharpFile();

        /// <summary>
        /// Removes this instance.
        /// </summary>
        void Remove();

        /// <summary>
        /// Removes the and delete.
        /// </summary>
        void RemoveAndDelete();

        /// <summary>
        /// Deletes the folder.
        /// </summary>
        /// <param name="excludeFiles">The exclude files.</param>
        void DeleteFolder(IEnumerable<string> excludeFiles = null);

        /// <summary>
        /// Removes the double blank lines.
        /// </summary>
        void RemoveDoubleBlankLines();

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <returns>The folder name.</returns>
        string GetFolder();

        /// <summary>
        /// Gets the containing project service.
        /// </summary>
        IProjectService ContainingProjectService { get; }

        /// <summary>
        /// Gets the folder project items.
        /// </summary>
        /// <returns>A list of project item services.</returns>
        IEnumerable<IProjectItemService> GetFolderProjectItems();

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns>A project item service.</returns>
        IProjectItemService GetFolder(string folderName);

        /// <summary>
        /// Gets the folder or create.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns>A project item service.</returns>
        IProjectItemService GetFolderOrCreate(string folderName);

        /// <summary>
        /// Gets the project item.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>A project item service.</returns>
        IProjectItemService GetProjectItem(string name);
    }
}