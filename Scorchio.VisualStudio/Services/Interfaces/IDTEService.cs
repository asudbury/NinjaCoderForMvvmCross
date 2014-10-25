// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IDTEService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Services.Interfaces
{
    using EnvDTE;

    using Microsoft.VisualStudio.CommandBars;

    /// <summary>
    ///  Defines the IDTEService type.
    /// </summary>
    public interface IDTEService
    {
        /// <summary>
        /// Gets the solution service.
        /// </summary>
        ISolutionService SolutionService { get; }

        /// <summary>
        /// Activates the specified instance.
        /// </summary>
        void Activate();

        /// <summary>
        /// Determines whether the solution is loaded.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is solution loaded] [the specified instance]; otherwise, <c>false</c>.
        /// </returns>
        bool IsSolutionLoaded();

        /// <summary>
        /// Gets the solution.
        /// </summary>
        /// <returns>The solution.</returns>
        Solution GetSolution();

        /// <summary>
        /// Gets the active project.
        /// </summary>
        /// <returns>The active project.</returns>
        Project GetActiveProject();
        
        /// <summary>
        /// Gets the default projects location.
        /// </summary>
        /// <returns>The default projects location.</returns>
        string GetDefaultProjectsLocation();

        /// <summary>
        /// Creates the solution.
        /// </summary>
        /// <param name="solutionName">ProjectName of the solution.</param>
        void CreateSolution(string solutionName);

        /// <summary>
        /// Creates the new file.
        /// </summary>
        /// <param name="path">The path.</param>
        void CreateNewFile(string path);

        /// <summary>
        /// Closes the documents.
        /// </summary>
        void CloseDocuments();
        
        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <returns>The Tools Menu Bar Item.</returns>
        CommandBar GetMenuBar();

        /// <summary>
        /// Gets the tools menu item.
        /// </summary>
        /// <returns>The Tools Menu Bar Item.</returns>
        CommandBarControl GetToolsMenuItem();

        /// <summary>
        /// Gets the tools menu pop up.
        /// </summary>
        /// <returns>The Tools Menu Bar Pop Up.</returns>
        CommandBarPopup GetToolsMenuPopUp();

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <param name="url">The URL.</param>
        void NavigateTo(string url);

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="command">The command.</param>
        void ExecuteCommand(string command);

        /// <summary>
        /// Executes the nuget command.
        /// </summary>
        /// <param name="command">The command.</param>
        void ExecuteNugetCommand(string command);
        
        /// <summary>
        /// Replaces the text.
        /// </summary>
        /// <param name="findText">The find text.</param>
        /// <param name="replaceText">The replace text.</param>
        /// <param name="saveFiles">if set to <c>true</c> [save files].</param>
        /// <returns>True or false.</returns>
        bool ReplaceText(
            string findText, 
            string replaceText,
            bool saveFiles);

        /// <summary>
        /// Replaces the text in current document.
        /// </summary>
        /// <param name="findText">The find text.</param>
        /// <param name="replaceText">The replace text.</param>
        /// <param name="regularExpression">if set to <c>true</c> [regular expression].</param>
        /// <param name="saveFiles">if set to <c>true</c> [save files].</param>
        /// <returns>True or false.</returns>
         bool ReplaceTextInCurrentDocument(
            string findText,
            string replaceText,
            bool regularExpression,
            bool saveFiles);

        /// <summary>
        /// Saves all.
        /// </summary>
        void SaveAll();

        /// <summary>
        /// Closes the solution explorer window.
        /// </summary>
        void CloseSolutionExplorerWindow();
        
        /// <summary>
        /// Shows the solution explorer window.
        /// </summary>
        void ShowSolutionExplorerWindow();
        
        /// <summary>
        /// Collapses the solution.
        /// </summary>
        void CollapseSolution();
        
        /// <summary>
        /// Writes the status bar message.
        /// </summary>
        /// <param name="message">The message.</param>
        void WriteStatusBarMessage(string message);
        
        /// <summary>
        /// Gets the C sharp project items events.
        /// </summary>
        /// <returns>The item events object.</returns>
        ProjectItemsEvents GetCSharpProjectItemsEvents();

        /// <summary>
        /// Gets the project items events.
        /// </summary>
        /// <returns>the item events object.</returns>
        ProjectItemsEvents GetProjectItemsEvents();

        /// <summary>
        /// Gets the document events.
        /// </summary>
        /// <returns>the document events.</returns>
        DocumentEvents GetDocumentEvents();

        /// <summary>
        /// Opens the file.
        /// </summary>
        /// <param name="path">The path.</param>
        void OpenFile(string path);
    }
}
