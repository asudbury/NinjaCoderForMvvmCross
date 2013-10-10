// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseWizard type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards
{
    using System.Collections.Generic;
    using EnvDTE;
    using EnvDTE80;
    using Extensions;
    using Microsoft.VisualStudio.TemplateWizard;
    using Services;

    /// <summary>
    ///  Defines the BaseWizard type.
    /// </summary>
    public abstract class BaseWizard : IWizard
    {
        /// <summary>
        /// Gets the DTE.
        /// </summary>
        public DTE Dte { get; private set; }

        /// <summary>
        /// Gets the replacements dictionary.
        /// </summary>
        public Dictionary<string, string> ReplacementsDictionary { get; private set; }

        /// <summary>
        /// Gets the kind of the run.
        /// </summary>
        public WizardRunKind RunKind { get; private set; }

        /// <summary>
        /// Gets the custom params.
        /// </summary>
        public object[] CustomParams { get; private set; }

        /// <summary>
        /// Gets the settings service.
        /// </summary>
        public SettingsService SettingsService { get; private set; }

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
            this.SettingsService = new SettingsService();

            TraceService.Initialize(
                this.SettingsService.LogToTrace,
                false,
                this.SettingsService.LogToFile,
                this.SettingsService.LogFilePath,
                this.SettingsService.DisplayErrors);

            TraceService.WriteLine("BaseWizard::RunStarted");

            this.Dte = automationObject as DTE;
            this.ReplacementsDictionary = replacementsDictionary;
            this.RunKind = runKind;
            this.CustomParams = customParams;

            this.OnRunStarted();
        }

        /// <summary>
        /// Runs custom wizard logic at the beginning of a template wizard run.
        /// </summary>
         public virtual void OnRunStarted()
         {
             TraceService.WriteLine("BaseWizard::OnRunStarted");
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
            TraceService.WriteLine("BaseWizard::ShouldAddProjectItem filePath=" + filePath);

            return this.OnShouldAddProjectItem(filePath);
        }

        /// <summary>
        /// Called when [should add project item].
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>True or false.</returns>
        public virtual bool OnShouldAddProjectItem(string filePath)
        {
            TraceService.WriteLine("BaseWizard::OnShouldAddProjectItem filePath=" + filePath);

            return true;
        }

        /// <summary>
        /// Runs custom wizard logic when the wizard has completed all tasks.
        /// </summary>
        public void RunFinished()
        {
            TraceService.WriteLine("BaseWizard::RunFinished");

            this.OnRunFinished();
        }

        /// <summary>
        /// Called when [run finished].
        /// </summary>
        public virtual void OnRunFinished()
        {
            TraceService.WriteLine("BaseWizard::OnRunFinished");
        }

        /// <summary>
        /// Runs custom wizard logic before opening an item in the template.
        /// </summary>
        /// <param name="projectItem">The project item that will be opened.</param>
        public void BeforeOpeningFile(ProjectItem projectItem)
        {
            TraceService.WriteLine("BaseWizard::BeforeOpeningFile");

            this.OnBeforeOpeningFile(projectItem);
        }

        /// <summary>
        /// Called when [before opening file].
        /// </summary>
        /// <param name="projectItem">The project item.</param>
        public virtual void OnBeforeOpeningFile(ProjectItem projectItem)
        {
            TraceService.WriteLine("BaseWizard::OnBeforeOpeningFile projectItem=" + projectItem.Name);
        }

        /// <summary>
        /// Runs custom wizard logic when a project item has finished generating.
        /// </summary>
        /// <param name="projectItem">The project item that finished generating.</param>
        public void ProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            TraceService.WriteLine("BaseWizard::ProjectItemFinishedGenerating projectItem=" + projectItem.Name);

            this.OnProjectItemFinishedGenerating(projectItem);
        }

        /// <summary>
        /// Called when [project item finished generating].
        /// </summary>
        /// <param name="projectItem">The project item.</param>
        public virtual void OnProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            TraceService.WriteLine("BaseWizard::OnProjectItemFinishedGenerating projectItem=" + projectItem.Name);
        }

        /// <summary>
        /// Runs custom wizard logic when a project has finished generating.
        /// </summary>
        /// <param name="project">The project that finished generating.</param>
        public void ProjectFinishedGenerating(Project project)
        {
            TraceService.WriteLine("BaseWizard::ProjectFinishedGenerating");

            this.OnProjectFinishedGenerating(project);
        }

        /// <summary>
        /// Called when [project finished generating].
        /// </summary>
        /// <param name="project">The project.</param>
        public virtual void OnProjectFinishedGenerating(Project project)
        {
            TraceService.WriteLine("BaseWizard::OnProjectFinishedGenerating");
        }

        /// <summary>
        /// Does the file exist.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>True or false.</returns>
        public bool DoesFileExist(string path)
        {
            TraceService.WriteLine("BaseWizard::DoesFileExist path=" + path);

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
                        foreach (ProjectItem subProjectItem in projectItem.ProjectItems)
                        {
                            if (this.DoesProjectItemExist(subProjectItem, path))
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
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

            TraceService.WriteLine("BaseWizard::DoesProjectItemExist projectItemPath = " + projectItemPath + " new file path=" + path);

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
            TraceService.WriteLine("BaseWizard::AddGlobal key=" + key + " value=" + value);

            this.Dte.Solution.Globals[key] = value;
        }
    }
}
