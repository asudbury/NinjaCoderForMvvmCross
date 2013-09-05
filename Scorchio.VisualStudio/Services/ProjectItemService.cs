// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectItemService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Services
{
    using System.Collections.Generic;
    using Entities;
    using EnvDTE;
    using EnvDTE80;
    using Extensions;
    using Interfaces;

    /// <summary>
    ///  Defines the ProjectItemService type.
    /// </summary>
    public class ProjectItemService : IProjectItemService
    {
        /// <summary>
        /// The project.
        /// </summary>
        private readonly ProjectItem projectItem;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectItemService" /> class.
        /// </summary>
        /// <param name="projectItem">The project item.</param>
        public ProjectItemService(ProjectItem projectItem)
        {
            this.projectItem = projectItem;
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return this.projectItem.Name; }
        }

        /// <summary>
        /// Gets the kind.
        /// </summary>
        public string Kind
        {
            get { return this.projectItem.Kind; }
        }

        /// <summary>
        /// Gets the project item.
        /// </summary>
        public ProjectItem ProjectItem
        {
            get { return this.projectItem; }
        }

        /// <summary>
        /// Gets the sub project items.
        /// </summary>
        /// <returns>The sub project items.</returns>
        public IEnumerable<ProjectItem> GetSubProjectItems()
        {
            return this.projectItem.GetSubProjectItems();
        }

        /// <summary>
        /// Gets the name space.
        /// </summary>
        /// <returns>The name space.</returns>
        public CodeNamespace GetNameSpace()
        {
            return this.projectItem.GetFirstNameSpace();
        }

        /// <summary>
        /// Adds the name space.
        /// </summary>
        /// <param name="nameSpace">The name space.</param>
        /// <returns>
        /// The created name space.
        /// </returns>
        public CodeNamespace AddNameSpace(string nameSpace)
        {
            return this.projectItem.AddNameSpace(nameSpace);
        }

        /// <summary>
        /// Gets the first code element.
        /// </summary>
        /// <returns>The first code element</returns>
        public CodeElement GetFirstCodeElement()
        {
            return this.projectItem.GetFirstCodeElement();
        }

        /// <summary>
        /// Gets the first name space.
        /// </summary>
        /// <returns>The first namespace.</returns>
        public CodeNamespace GetFirstNameSpace()
        {
            return this.projectItem.GetFirstNameSpace();
        }

        /// <summary>
        /// Gets the first class.
        /// </summary>
        /// <returns>The first class.</returns>
        public CodeClass GetFirstClass()
        {
            return this.projectItem.GetFirstClass();
        }

        /// <summary>
        /// Gets the first interface.
        /// </summary>
        /// <returns>The first interface.</returns>
        public CodeInterface GetFirstInterface()
        {
            return this.projectItem.GetFirstInterface();
        }

        /// <summary>
        /// Creates the interface.
        /// </summary>
        /// <returns>The interface.</returns>
        public CodeInterface CreateInterface()
        {
            return this.projectItem.CreateInterface();
        }

        /// <summary>
        /// Adds the header comment.
        /// </summary>
        /// <param name="headerComment">The header comment.</param>
        public void AddHeaderComment(string headerComment)
        {
            this.projectItem.AddHeaderComment(headerComment);
        }

        /// <summary>
        /// Implements the code snippet.
        /// </summary>
        /// <param name="codeSnippet">The code snippet.</param>
        public void ImplementCodeSnippet(CodeSnippet codeSnippet)
        {
            this.projectItem.ImplementCodeSnippet(codeSnippet);
        }

        /// <summary>
        /// Implements the unit testing code snippet.
        /// </summary>
        /// <param name="codeSnippet">The code snippet.</param>
        /// <param name="codeFile">The code file.</param>
        /// <param name="removeHeader">if set to <c>true</c> [remove header].</param>
        /// <param name="removeComments">if set to <c>true</c> [remove comments].</param>
        public void ImplementUnitTestingCodeSnippet(
            CodeSnippet codeSnippet,
            string codeFile,
            bool removeHeader,
            bool removeComments)
        {
            this.ImplementCodeSnippet(codeSnippet);

            //// add in the reference to the plugin - doing this way means we don't need it in the xml files
            codeSnippet.UsingStatements.Add(codeFile);

            //// this really shouldn't be done - templates now have a mind of their own - please fix!!
            DTE2 dte2 = this.projectItem.ContainingProject.DTE as DTE2;

            //// change the testable placeholders!
            string instanceName = "this." + codeFile.Substring(0, 1).ToLower() + codeFile.Substring(1);
            bool replaced = dte2.ReplaceText("this.TestableObject", instanceName, true);

            //// sometimes the find/replace doesnt work - god knows why - seems intermittent :-(
            if (replaced == false)
            {
                this.ReplaceText("this.TestableObject", instanceName);
            }
            
            if (removeHeader)
            {
                this.ProjectItem.RemoveHeader();
            }

            if (removeComments)
            {
                this.ProjectItem.RemoveComments();
            }
        }

        /// <summary>
        /// Adds the using statement.
        /// </summary>
        /// <param name="usingStatement">The using statement.</param>
        public void AddUsingStatement(string usingStatement)
        {
            this.projectItem.AddUsingStatement(usingStatement);
        }

        /// <summary>
        /// Gets the using statements.
        /// </summary>
        /// <returns>A list of using statements.</returns>
        public IEnumerable<string> GetUsingStatements()
        {
            return this.projectItem.GetUsingStatements();
        }

        /// <summary>
        /// Moves the using statements.
        /// </summary>
        public void MoveUsingStatements()
        {
            this.projectItem.MoveUsingStatements();
        }

        /// <summary>
        /// Sorts the and remove using statements.
        /// </summary>
        public void SortAndRemoveUsingStatements()
        {
            this.projectItem.SortAndRemoveUsingStatements();
        }

        /// <summary>
        /// Replaces the text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="replacementText">The replacement text.</param>
        public void ReplaceText(
            string text, 
            string replacementText)
        {
           this.projectItem.ReplaceText(text, replacementText);
        }

        /// <summary>
        /// Replaces the pattern.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="replacementText">The replacement text.</param>
        public void ReplacePattern(
            string text, 
            string replacementText)
        {
            TextSelection textSelection = this.projectItem.DTE.ActiveDocument.Selection;
            textSelection.SelectAll();
            textSelection.ReplacePattern(text, replacementText);
        }

        /// <summary>
        /// Opens this instance.
        /// </summary>
        public void Open()
        {
            this.projectItem.Open();
        }

        /// <summary>
        /// Saves this instance.
        /// </summary>
        public void Save()
        {
            this.projectItem.Save();
        }

        /// <summary>
        /// Fixes the using statements.
        /// </summary>
        public void FixUsingStatements()
        {
            this.projectItem.FixUsingStatements();
        }

        /// <summary>
        /// Removes the comments.
        /// </summary>
        public void RemoveComments()
        {
            this.projectItem.RemoveComments();
        }

        /// <summary>
        /// Removes the header.
        /// </summary>
        public void RemoveHeader()
        {
            this.projectItem.RemoveHeader();
        }

        /// <summary>
        /// Determines whether [is C sharp file].
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is C sharp file]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsCSharpFile()
        {
            return this.projectItem.IsCSharpFile();
        }

        /// <summary>
        /// Removes this instance.
        /// </summary>
        public void Remove()
        {
            this.projectItem.Remove();
        }

        /// <summary>
        /// Removes the and delete.
        /// </summary>
        public void RemoveAndDelete()
        {
            this.projectItem.RemoveAndDelete();
        }

        /// <summary>
        /// Deletes the folder.
        /// </summary>
        /// <param name="excludeFiles">The exclude files.</param>
        public void DeleteFolder(IEnumerable<string> excludeFiles = null)
        {
            this.projectItem.DeleteFolder(excludeFiles);
        }
    }
}
