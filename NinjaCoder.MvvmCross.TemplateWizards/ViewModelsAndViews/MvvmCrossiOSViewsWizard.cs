 // --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvvmCrossiOSViewsWizard type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards.ViewModelsAndViews
{
    using EnvDTE;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the MvvmCrossViewsWizard type.
    /// </summary>
    public class MvvmCrossiOSViewsWizard : BaseWizard
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
            TraceService.WriteHeader("MvvmCrossiOSViewsWizard::OnRunStarted");

            this.projectItems = new List<ProjectItem>();
        }

        /// <summary>
        /// Called when [should add project item].
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>True or false.</returns>
        protected override bool OnShouldAddProjectItem(string filePath)
        {
            TraceService.WriteLine("MvvmCrossiOSViewsWizard::OnShouldAddProjectItem path=" + filePath);

            return filePath.Contains(this.SettingsService.SelectedViewType);
        }

        /// <summary>
        /// Called when [project item finished generating].
        /// </summary>
        /// <param name="projectItem">The project item.</param>
        protected override void OnProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            TraceService.WriteLine("MvvmCrossiOSViewsWizard::OnProjectItemFinishedGenerating projectItem=" + projectItem.Name);

            this.projectItems.Add(projectItem);
        }

        /// <summary>
        /// Called when [run finished].
        /// </summary>
        protected override void OnRunFinished()
        {
            TraceService.WriteLine("MvvmCrossiOSViewsWizard::OnRunFinished");

            //// now sort out the files.

            foreach (ProjectItem projectItem in this.projectItems)
            {
                projectItem.Open();

                //// adjust the xamarin name spaces.
                if (this.SettingsService.iOSApiVersion == "Unified")
                {
                    TraceService.WriteLine("Replacing iOS using statements");

                    projectItem.ReplaceText("MonoTouch.Foundation", "Foundation");
                    projectItem.ReplaceText("MonoTouch.UIKit", "UIKit");
                    projectItem.ReplaceText("System.Drawing", "CoreGraphics");
                    projectItem.ReplaceText("RectangleF", "CGRect");
                }

                projectItem.Save();
            }
        }
    }
}