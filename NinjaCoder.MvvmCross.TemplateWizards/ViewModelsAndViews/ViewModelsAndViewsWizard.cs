 // --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewModelsAndViewsWizard type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards.ViewModelsAndViews
{
    using System.Collections.Generic;
    using System.IO;

    using EnvDTE;

    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the ViewModelsAndViewsWizard type.
    /// </summary>
    public class ViewModelsAndViewsWizard : BaseWizard
    {
        /// <summary>
        /// The project items.
        /// </summary>
        private List<ProjectItem> projectItems;
 
        /// <summary>
        /// Runs custom wizard logic at the beginning of a template wizard run.
        /// </summary>
        protected override void OnRunStarted()
        {
            TraceService.WriteHeader("ViewModelsAndViewsWizard::OnRunStarted");

            this.projectItems = new List<ProjectItem>();
        }

        /// <summary>
        /// Called when [should add project item].
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>True or false.</returns>
        protected override bool OnShouldAddProjectItem(string filePath)
        {
            TraceService.WriteLine("ViewModelsAndViewsWizard::OnShouldAddProjectItem path=" + filePath);

            return filePath.Contains(this.SettingsService.SelectedViewType);
        }

        /// <summary>
        /// Called when [project item finished generating].
        /// </summary>
        /// <param name="projectItem">The project item.</param>
        protected override void OnProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            TraceService.WriteLine("ViewModelsAndViewsWizard::OnProjectItemFinishedGenerating projectItem=" + projectItem.Name);

            this.projectItems.Add(projectItem);
        }

        /// <summary>
        /// Called when [run finished].
        /// </summary>
        protected override void OnRunFinished()
        {
            TraceService.WriteLine("ViewModelsAndViewsWizard::OnRunFinished");

            //// now sort out the files.

            foreach (ProjectItem projectItem in this.projectItems)
            {
                projectItem.Open();

                string oldPath = projectItem.FileNames[0];

                string fileName = oldPath;

                //// if the file starts with the view type (prefixed Z) name we want it.
                if (fileName.StartsWith("Z" + this.SettingsService.SelectedViewType))
                {
                    //// remove the view name from the front.
                    fileName = fileName.Replace(
                        "Z" + this.SettingsService.SelectedViewType,
                        string.Empty);
                }

                else
                {
                    fileName = fileName.Replace(
                        this.SettingsService.SelectedViewType,
                        this.SettingsService.SelectedViewPrefix);
                }

                TraceService.WriteLine("oldPath=" + oldPath);
                TraceService.WriteLine("newPath=" + fileName);

                //// replace the First text place holders too!!
                projectItem.ReplaceText("First", this.SettingsService.SelectedViewPrefix);

                projectItem.SaveAs(fileName);

                File.Delete(oldPath);
            }
        }
    }
}