// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseWizard type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards
{
    using System.Collections.Generic;
    using System.Linq;

    using EnvDTE;
    using EnvDTE80;
    using Microsoft.VisualStudio.TemplateWizard;

    using NinjaCoder.MvvmCross.TemplateWizards.Services;

    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the BaseWizard type.
    /// </summary>
    public abstract class BaseWizard : IWizard
    {
        /// <summary>
        /// Gets or sets the DTE.
        /// </summary>
        protected DTE Dte { get; set; }

        /// <summary>
        /// Gets or sets the settings service.
        /// </summary>
        protected SettingsService SettingsService { get; set; }

        /// <summary>
        /// Gets or sets the replacements dictionary.
        /// </summary>
        protected Dictionary<string, string> ReplacementsDictionary { get; set; }

        /// <summary>
        /// Runs custom wizard logic at the beginning of a template wizard run.
        /// </summary>
        /// <param name="automationObject">The automation object being used by the template wizard.</param>
        /// <param name="replacementsDictionary">The list of standard parameters to be replaced.</param>
        /// <param name="runKind">A <see cref="T:Microsoft.VisualStudio.TemplateWizard.WizardRunKind" /> indicating the type of wizard run.</param>
        /// <param name="customParams">The custom parameters with which to perform parameter replacement in the project.</param>
        public void RunStarted(
            object automationObject, 
            Dictionary<string, string> replacementsDictionary, 
            WizardRunKind runKind, 
            object[] customParams)
        {
            ////TraceService.WriteLine("BaseWizard::RunStarted");

            this.SettingsService = new SettingsService();

            TraceService.Initialize(
                this.SettingsService.LogToTrace,
                false,
                this.SettingsService.LogToFile,
                this.SettingsService.LogFilePath,
                this.SettingsService.DisplayErrors);

            this.Dte = automationObject as DTE;
            this.ReplacementsDictionary = replacementsDictionary;

            this.OnRunStarted();
        }

        /// <summary>
        /// Indicates whether the specified project item should be added to the project.
        /// </summary>
        /// <returns>
        /// true if the project item should be added to the project; otherwise, false.
        /// </returns>
        /// <param name="filePath">The path to the project item.</param>
        public bool ShouldAddProjectItem(string filePath)
        {
            ////TraceService.WriteLine("BaseWizard::ShouldAddProjectItem");

            return this.OnShouldAddProjectItem(filePath);
        }

        /// <summary>
        /// Runs custom wizard logic when the wizard has completed all tasks.
        /// </summary>
        public void RunFinished()
        {
            ////TraceService.WriteLine("BaseWizard::RunFinished");

            if (this.SettingsService.ProcessWizard)
            {
                this.OnRunFinished();
            }
        }

        /// <summary>
        /// Runs custom wizard logic before opening an item in the template.
        /// </summary>
        /// <param name="projectItem">The project item that will be opened.</param>
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
            ////TraceService.WriteLine("BaseWizard::BeforeOpeningFile projectItem=" + projectItem.Name);

            if (this.SettingsService.ProcessWizard)
            {
                this.OnBeforeOpeningFile(projectItem);
            }
        }

        /// <summary>
        /// Runs custom wizard logic when a project item has finished generating.
        /// </summary>
        /// <param name="projectItem">The project item that finished generating.</param>
        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            ////TraceService.WriteLine("BaseWizard::ProjectItemFinishedGenerating projectItem=" + projectItem.Name);

            if (this.SettingsService.ProcessWizard)
            {
                this.OnProjectItemFinishedGenerating(projectItem);
            }
        }

        /// <summary>
        /// Runs custom wizard logic when a project has finished generating.
        /// </summary>
        /// <param name="project">The project that finished generating.</param>
        public void ProjectFinishedGenerating(Project project)
        {
            ////TraceService.WriteLine("BaseWizard::ProjectFinishedGenerating project=" + project.Name);

            if (this.SettingsService.ProcessWizard)
            {
                this.OnProjectFinishedGenerating(project);
            }
        }

        /// <summary>
        /// Does the project item exist.
        /// </summary>
        /// <param name="projectItem">The project item.</param>
        /// <param name="path">The path.</param>
        /// <returns>True or false.</returns>
        internal bool DoesProjectItemExist(
            ProjectItem projectItem, 
            string path)
        {
            string projectItemPath = projectItem.FileNames[1];

            return projectItemPath.Contains(path);
        }

        /// <summary>
        /// Adds the global.
        /// </summary>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        internal void AddGlobal(
            string key, 
            string value)
        {
           ////TraceService.WriteLine("BaseWizard::AddGlobal key=" + key + " value=" + value);

            this.Dte.Solution.Globals[key] = value;
        }

        /// <summary>
        /// Gets the active project.
        /// </summary>
        /// <returns>The active project if it has been set.</returns>
        protected Project GetActiveProject()
        {
            string activeProjectKey = this.SettingsService.ActiveProject;

            ////TraceService.WriteLine("BaseWizard::GetActiveProject Active project key=" + activeProjectKey);

           if (string.IsNullOrEmpty(activeProjectKey) == false)
           {
                Solution2 solution2 = this.Dte.Solution as Solution2;

                return solution2.GetProjects()
                    .FirstOrDefault(project => project.Name.Contains(activeProjectKey));
            }

            return null;
        }

        /// <summary>
        /// Runs custom wizard logic at the beginning of a template wizard run.
        /// </summary>
        protected virtual void OnRunStarted()
        {
        }

        /// <summary>
        /// Called when [should add project item].
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>True or false.</returns>
        protected virtual bool OnShouldAddProjectItem(string filePath)
        {
            return true;
        }

        /// <summary>
        /// Called when [run finished].
        /// </summary>
        protected virtual void OnRunFinished()
        {
        }

        /// <summary>
        /// Called when [before opening file].
        /// </summary>
        /// <param name="projectItem">The project item.</param>
        protected virtual void OnBeforeOpeningFile(ProjectItem projectItem)
        {
        }

        /// <summary>
        /// Called when [project item finished generating].
        /// </summary>
        /// <param name="projectItem">The project item.</param>
        protected virtual void OnProjectItemFinishedGenerating(ProjectItem projectItem)
        {
        }

        /// <summary>
        /// Called when [project finished generating].
        /// </summary>
        /// <param name="project">The project.</param>
        protected virtual void OnProjectFinishedGenerating(Project project)
        {
        }

        /// <summary>
        /// Does the file exist.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>True or false.</returns>
        protected bool DoesFileExist(string path)
        {
            ////TraceService.WriteLine("BaseWizard::DoesFileExist path=" + path);

            Solution2 solution2 = this.Dte.Solution as Solution2;

            foreach (Project project in solution2.GetProjects())
            {
                foreach (ProjectItem projectItem in project.ProjectItems)
                {
                    if (this.DoesProjectItemExist(projectItem, path))
                    {
                        return true;
                    }

                    if (projectItem.ProjectItems != null)
                    {
                        if (projectItem.ProjectItems
                            .Cast<ProjectItem>()
                            .Any(subProjectItem => this.DoesProjectItemExist(subProjectItem, path)))
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}
