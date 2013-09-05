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
    using Services;

    /// <summary>
    ///  Defines the DTEExtensions type.
    /// </summary>
    public static class DTEExtensions
    {
        /// <summary>
        /// Activates the specified instance.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void Activate(this DTE2 instance)
        {
            TraceService.WriteLine("DTEExtensions::Activate");

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
            TraceService.WriteLine("DTEExtensions::IsSolutionLoaded");

            return instance != null && instance.Solution != null;
        }

        /// <summary>
        /// Gets the solution.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The solution.</returns>
        public static Solution GetSolution(this DTE2 instance)
        {
            TraceService.WriteLine("DTEExtensions::GetSolution");

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
            TraceService.WriteLine("DTEExtensions::CreateSolution");

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
            TraceService.WriteLine("DTEExtensions::GetActiveProject");

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
            TraceService.WriteLine("DTEExtensions::CreateNewFile");

            instance.ItemOperations.NewFile(path, string.Empty); 
        }

        /// <summary>
        /// Closes the documents.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void CloseDocuments(this DTE2 instance)
        {
            TraceService.WriteLine("DTEExtensions::CloseDocuments");

            Documents documents = instance.Documents;

            foreach (Document document in documents)
            {
                document.Close(vsSaveChanges.vsSaveChangesYes);    
            }
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
            TraceService.WriteLine("DTEExtensions::NavigateTo");

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
            TraceService.WriteLine("DTEExtensions::ExecuteNugetCommand command=" + command);

            instance.ExecuteCommand("View.PackageManagerConsole  " + command);
        }

        /// <summary>
        /// Replaces the text.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="findText">The find text.</param>
        /// <param name="replaceText">The replace text.</param>
        /// <param name="saveFiles">if set to <c>true</c> [save files].</param>
        /// <returns>True or false.</returns>
        public static bool ReplaceText(
            this DTE2 instance,
            string findText, 
            string replaceText,
            bool saveFiles)
        {
            TraceService.WriteLine("DTEExtensions::ReplaceText from '" + findText + "' to '" + replaceText + "'");

            bool replaced = true;

            Find2 find2 = (Find2)instance.Find;

            vsFindResult findResults = find2.FindReplace(
                                vsFindAction.vsFindActionReplaceAll,
                                findText,
                                (int)vsFindOptions.vsFindOptionsFromStart,
                                replaceText,
                                vsFindTarget.vsFindTargetSolution,
                                string.Empty,
                                string.Empty,
                                vsFindResultsLocation.vsFindResultsNone);
            
            if (findResults == vsFindResult.vsFindResultNotFound)
            {
                replaced = false;
                TraceService.WriteError("Unable to replace text from:-" + findText + " to:- " + replaceText);
            }

            if (saveFiles)
            {
                instance.SaveAll();
            }

            return replaced;
        }

        /// <summary>
        /// Replaces the text in current document.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="findText">The find text.</param>
        /// <param name="replaceText">The replace text.</param>
        /// <param name="regularExpression">if set to <c>true</c> [regular expression].</param>
        /// <param name="saveFiles">if set to <c>true</c> [save files].</param>
        /// <returns>True or false.</returns>
         public static bool ReplaceTextInCurrentDocument(
            this DTE2 instance,
            string findText,
            string replaceText,
            bool regularExpression,
            bool saveFiles)
        {
            TraceService.WriteLine("DTEExtensions::ReplaceTextInCurrentDocument from:-" + findText + " to:- " + replaceText);

            bool replaced = true;

            Find2 find2 = (Find2)instance.Find;

            vsFindResult findResults = find2.FindReplace(
                                vsFindAction.vsFindActionReplaceAll,
                                findText,
                                (int)vsFindOptions.vsFindOptionsFromStart,
                                replaceText,
                                vsFindTarget.vsFindTargetCurrentDocument,
                                string.Empty,
                                string.Empty,
                                vsFindResultsLocation.vsFindResultsNone);

            if (findResults == vsFindResult.vsFindResultNotFound)
            {
                replaced = false;
                TraceService.WriteError("Unable to replace text from:-" + findText + " to:- " + replaceText);
            }

            if (saveFiles)
            {
                instance.SaveAll();
            }

            return replaced;
        }

        /// <summary>
        /// Saves all.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void SaveAll(this DTE2 instance)
        {
            TraceService.WriteLine("DTEExtensions::SaveAll");

            instance.ExecuteCommand("File.SaveAll");
        }

        /// <summary>
        /// Closes the solution explorer window.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void CloseSolutionExplorerWindow(this DTE instance)
        {
            TraceService.WriteLine("DTEExtensions::CloseSolutionExplorerWindow");

            instance.Windows.Item(VSConstants.VsWindowKindSolutionExplorer).Close();
        }

        /// <summary>
        /// Shows the solution explorer window.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void ShowSolutionExplorerWindow(this DTE instance)
        {
            TraceService.WriteLine("DTEExtensions::ShowSolutionExplorerWindow");

            instance.ExecuteCommand("View.SolutionExplorer");
        }

        /// <summary>
        /// Collapses the solution.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void CollapseSolution(this DTE2 instance)
        {
            TraceService.WriteLine("DTEExtensions::CollapseSolution");

            Window window = instance.Windows.Item(VSConstants.VsWindowKindSolutionExplorer);

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

        /// <summary>
        /// Writes the status bar message.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="message">The message.</param>
        public static void WriteStatusBarMessage(
            this DTE2 instance,
            string message)
        {
            instance.StatusBar.Text = message;
        }

        /// <summary>
        /// Gets the C sharp project items events.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The item events object.</returns>
        public static ProjectItemsEvents GetCSharpProjectItemsEvents(this DTE2 instance)
        {
            TraceService.WriteLine("DTEExtensions::GetCSharpProjectItemsEvents");

            return instance.Events.GetObject("CSharpProjectItemsEvents") as ProjectItemsEvents;
        }

        /// <summary>
        /// Gets the project item events.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The item events object.</returns>
        public static ProjectItemsEvents GetProjectItemEvents(this DTE2 instance)
        {
            TraceService.WriteLine("DTEExtensions::GetProjectItemEvents");

            Events2 events2 = instance.Events as Events2;

            return events2 != null ? events2.ProjectItemsEvents : null;
        }
    }
}
