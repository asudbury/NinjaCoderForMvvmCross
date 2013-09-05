// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the DTEService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Services
{
    using EnvDTE;
    using EnvDTE80;
    using Extensions;
    using Interfaces;
    using Microsoft.VisualStudio.CommandBars;

    /// <summary>
    ///  Defines the DTEService type.
    /// </summary>
    public class DTEService : IDTEService
    {
        /// <summary>
        /// The dte2.
        /// </summary>
        private readonly DTE2 dte2;

        /// <summary>
        /// Initializes a new instance of the <see cref="DTEService" /> class.
        /// </summary>
        /// <param name="dte2">The dte2.</param>
        public DTEService(DTE2 dte2)
        {
            this.dte2 = dte2;
        }

        /// <summary>
        /// Gets the DTE.
        /// </summary>
        public DTE DTE 
        {
            get { return this.dte2 as DTE; }
        }

        /// <summary>
        /// Activates the specified instance.
        /// </summary>
        public void Activate()
        {
            this.dte2.Activate();
        }

        /// <summary>
        /// Determines whether the solution is loaded.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if [is solution loaded] [the specified instance]; otherwise, <c>false</c>.
        /// </returns>
        public bool IsSolutionLoaded()
        {
            return this.dte2.IsSolutionLoaded();
        }

        /// <summary>
        /// Gets the solution.
        /// </summary>
        /// <returns>The solution.</returns>
        public Solution GetSolution()
        {
            return this.dte2.GetSolution();
        }

        /// <summary>
        /// Gets the active project.
        /// </summary>
        /// <returns>The active project.</returns>
        public Project GetActiveProject()
        {
            return this.dte2.GetActiveProject();
        }

        /// <summary>
        /// Gets the default projects location.
        /// </summary>
        /// <returns>The default projects location.</returns>
        public string GetDefaultProjectsLocation()
        {
            return this.dte2.GetDefaultProjectsLocation();
        }

        /// <summary>
        /// Creates the solution.
        /// </summary>
        /// <param name="solutionName">ProjectName of the solution.</param>
        public void CreateSolution(string solutionName)
        {
            DTE dte = this.dte2 as DTE;
            dte.CreateSolution(solutionName);
        }

        /// <summary>
        /// Creates the new file.
        /// </summary>
        /// <param name="path">The path.</param>
        public void CreateNewFile(string path)
        {
            this.dte2.CreateNewFile(path);
        }

        /// <summary>
        /// Closes the documents.
        /// </summary>
        public void CloseDocuments()
        {
            this.dte2.CloseDocuments();
        }

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <returns>The Tools Menu Bar Item.</returns>
        public CommandBar GetMenuBar()
        {
            return this.dte2.GetMenuBar();
        }

        /// <summary>
        /// Gets the tools menu item.
        /// </summary>
        /// <returns>The Tools Menu Bar Item.</returns>
        public CommandBarControl GetToolsMenuItem()
        {
            return this.dte2.GetToolsMenuItem();
        }

        /// <summary>
        /// Gets the tools menu pop up.
        /// </summary>
        /// <returns>The Tools Menu Bar Pop Up.</returns>
        public CommandBarPopup GetToolsMenuPopUp()
        {
            return this.dte2.GetToolsMenuPopUp();
        }

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <param name="url">The URL.</param>
        public void NavigateTo(string url)
        {
            this.dte2.NavigateTo(url);
        }

        /// <summary>
        /// Executes the command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void ExecuteCommand(string command)
        {
            this.dte2.ExecuteCommand(command);
        }

        /// <summary>
        /// Executes the nuget command.
        /// </summary>
        /// <param name="command">The command.</param>
        public void ExecuteNugetCommand(string command)
        {
            this.dte2.ExecuteNugetCommand(command);
        }

        /// <summary>
        /// Replaces the text.
        /// </summary>
        /// <param name="findText">The find text.</param>
        /// <param name="replaceText">The replace text.</param>
        /// <param name="saveFiles">if set to <c>true</c> [save files].</param>
        /// <returns>True or false.</returns>
        public bool ReplaceText(
            string findText, 
            string replaceText, 
            bool saveFiles)
        {
            return this.dte2.ReplaceText(findText, replaceText, saveFiles);
        }

        /// <summary>
        /// Replaces the text in current document.
        /// </summary>
        /// <param name="findText">The find text.</param>
        /// <param name="replaceText">The replace text.</param>
        /// <param name="regularExpression">if set to <c>true</c> [regular expression].</param>
        /// <param name="saveFiles">if set to <c>true</c> [save files].</param>
        /// <returns>True or false.</returns>
        public bool ReplaceTextInCurrentDocument(
            string findText, 
            string replaceText, 
            bool regularExpression, 
            bool saveFiles)
        {
            return this.dte2.ReplaceTextInCurrentDocument(findText, replaceText, regularExpression, saveFiles);
        }

        /// <summary>
        /// Saves all.
        /// </summary>
        public void SaveAll()
        {
            this.dte2.SaveAll();
        }

        /// <summary>
        /// Closes the solution explorer window.
        /// </summary>
        public void CloseSolutionExplorerWindow()
        {
            this.DTE.CloseSolutionExplorerWindow();
        }

        /// <summary>
        /// Shows the solution explorer window.
        /// </summary>
        public void ShowSolutionExplorerWindow()
        {
            this.DTE.ShowSolutionExplorerWindow();
        }

        /// <summary>
        /// Collapses the solution.
        /// </summary>
        public void CollapseSolution()
        {
            this.dte2.CollapseSolution();
        }

        /// <summary>
        /// Writes the status bar message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void WriteStatusBarMessage(string message)
        {
            this.dte2.WriteStatusBarMessage(message);
        }

        /// <summary>
        /// Gets the C sharp project items events.
        /// </summary>
        /// <returns>The item events object.</returns>
        public ProjectItemsEvents GetCSharpProjectItemsEvents()
        {
            return this.dte2.GetCSharpProjectItemsEvents();
        }

        /// <summary>
        /// Gets the project items events.
        /// </summary>
        /// <returns>the item events object.</returns>
        public ProjectItemsEvents GetProjectItemsEvents()
        {
            return this.dte2.GetProjectItemEvents();
        }

        /// <summary>
        /// Gets the document events.
        /// </summary>
        /// <returns>the document events.</returns>
        public DocumentEvents GetDocumentEvents()
        {
            return this.dte2.Events.DocumentEvents;
        }
    }
}
