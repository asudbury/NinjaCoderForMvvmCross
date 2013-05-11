// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the DTEExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Scorchio.VisualStudio.Extensions
{
    using System;
    using System.Linq;

    using EnvDTE;
    using EnvDTE80;

    using Microsoft.VisualStudio.CommandBars;

    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the DTEExtensions type.
    /// </summary>
    public static class DTEExtensions
    {
        /// <summary>
        /// Solutions Window.
        /// </summary>
        private const string vsext_wk_SProjectWindow = "{3AE79031-E1BC-11D0-8F78-00A0C9110057}";

        /// <summary>
        /// Activates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void Activate(this DTE2 instance)
        {
            instance.MainWindow.Activate();
        }

        /// <summary>
        /// Determines whether the solution is loaded.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>
        ///   <c>true</c> if [is solution loaded] [the specified instance]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsSolutionLoaded(this DTE2 instance)
        {
            return instance != null && instance.Solution != null;
        }

        /// <summary>
        /// Gets the solution.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The solution.</returns>
        public static Solution GetSolution(this DTE2 instance)
        {
            return instance.Solution;
        }

        /// <summary>
        /// Gets the default projects location.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The default projects location.</returns>
        public static string GetDefaultProjectsLocation(this DTE2 instance)
        {
            Properties properties = instance.Properties["Environment", "ProjectsAndSolution"];

            foreach (Property property in properties.Cast<Property>()
                .Where(property => property.Name == "ProjectsLocation"))
            {
                return property.Value.ToString();
            }

            return string.Empty;
        }

        /// <summary>
        /// Creates the solution.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="solutionName">ProjectName of the solution.</param>
        public static void CreateSolution(
            this DTE instance, 
            string solutionName)
        {
            Solution solution = instance.Solution;
            string tempPath = System.IO.Path.GetTempPath();
            solution.Create(tempPath, solutionName);
        }

        /// <summary>
        /// Gets the active project.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The active project.</returns>
        public static Project GetActiveProject(this DTE2 instance)
        {
            Array array = instance.ActiveSolutionProjects as Array;

            return array != null ? array.GetValue(0) as Project : null;
        }

        /// <summary>
        /// Creates the new file.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="path">The path.</param>
        public static void CreateNewFile(
            this DTE2 instance,
            string path)
        {
            instance.ItemOperations.NewFile(path, string.Empty); 
        }

        /// <summary>
        /// Gets the menu.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The Tools Menu Bar Item.</returns>
        public static CommandBar GetMenuBar(this DTE2 instance)
        {
            CommandBar menuBarCommandBar = ((CommandBars)instance.CommandBars)["MenuBar"];

            return menuBarCommandBar;
        }

        /// <summary>
        /// Gets the tools menu item.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The Tools Menu Bar Item.</returns>
        public static CommandBarControl GetToolsMenuItem(this DTE2 instance)
        {
            const string ToolsMenuName = "Tools";

            //// Place the command on the tools menu.
            //// Find the MenuBar command bar, which is the top-level command bar holding all the main menu items:
            CommandBar menuBarCommandBar = instance.GetMenuBar();

            return menuBarCommandBar.Controls[ToolsMenuName];
        }

        /// <summary>
        /// Gets the tools menu pop up.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The Tools Menu Bar Pop Up.</returns>
        public static CommandBarPopup GetToolsMenuPopUp(this DTE2 instance)
        {
            return (CommandBarPopup)instance.GetToolsMenuItem();
        }

        /// <summary>
        /// Navigates to.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="url">The URL.</param>
        public static void NavigateTo(
            this DTE2 instance,
            string url)
        {
            instance.ItemOperations.Navigate(url);
        }

        /// <summary>
        /// Executes the nuget command.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="command">The command.</param>
        public static void ExecuteNugetCommand(
            this DTE2 instance, 
            string command)
        {
            instance.ExecuteCommand("View.PackageManagerConsole  " + command);
        }

        /// <summary>
        /// Replaces the text.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="findText">The find text.</param>
        /// <param name="replaceText">The replace text.</param>
        /// <param name="saveFiles">if set to <c>true</c> [save files].</param>
        public static void ReplaceText(
            this DTE2 instance,
            string findText, 
            string replaceText,
            bool saveFiles)
        {
            instance.ExecuteCommand("Edit.ReplaceInFiles");
            instance.Find.Action = vsFindAction.vsFindActionReplaceAll;
            instance.Find.FindWhat = findText;
            instance.Find.ReplaceWith = replaceText;
            instance.Find.Target = vsFindTarget.vsFindTargetSolution;
            instance.Find.MatchCase = false;
            instance.Find.MatchWholeWord = false;
            instance.Find.MatchInHiddenText = true;
            instance.Find.PatternSyntax = vsFindPatternSyntax.vsFindPatternSyntaxRegExpr;
            instance.Find.SearchPath = "Entire Solution";
            instance.Find.SearchSubfolders = true;
            instance.Find.KeepModifiedDocumentsOpen = false;
            instance.Find.FilesOfType = "*.cs";
            instance.Find.ResultsLocation = vsFindResultsLocation.vsFindResultsNone;

            vsFindResult findResults = instance.Find.Execute();

            if (findResults == vsFindResult.vsFindResultNotFound)
            {
                TraceService.WriteLine("Unable to replace text from:-" + findText + " to:- " + replaceText);
            }

            instance.Windows.Item("{CF2DDC32-8CAD-11D2-9302-005345000000}").Close();

            if (saveFiles)
            {
                instance.SaveAll();
            }
        }

        /// <summary>
        /// Saves all.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void SaveAll(this DTE2 instance)
        {
            instance.ExecuteCommand("File.SaveAll");
        }

        /// <summary>
        /// Closes the solution explorer window.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void CloseSolutionExplorerWindow(this DTE instance)
        {
            instance.Windows.Item(vsext_wk_SProjectWindow).Close();
        }

        /// <summary>
        /// Shows the solution explorer window.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void ShowSolutionExplorerWindow(this DTE instance)
        {
            instance.ExecuteCommand("View.SolutionExplorer");
        }

        /// <summary>
        /// Collapses the solution.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void CollapseSolution(this DTE2 instance)
        {
            Window window = instance.Windows.Item(vsext_wk_SProjectWindow);

            UIHierarchy uiHierarchy = (UIHierarchy)window.Object;

            if (uiHierarchy.UIHierarchyItems.Count > 0)
            {
                UIHierarchyItem rootItem = uiHierarchy.UIHierarchyItems.Item(1);

                foreach (UIHierarchyItem item in rootItem.UIHierarchyItems)
                {
                    item.UIHierarchyItems.Expanded = false;
                }
            }
        }
    }
}
