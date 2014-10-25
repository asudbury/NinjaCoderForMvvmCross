 // --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsWizard type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards.Projects
{
    using EnvDTE;
    using Scorchio.VisualStudio.Services;


    /// <summary>
    ///  Defines the ProjectsWizard type.
    /// </summary>
    public class ProjectsWizard : BaseWizard
    {
        /// <summary>
        /// Runs custom wizard logic at the beginning of a template wizard run.
        /// </summary>
        protected override void OnRunStarted()
        {
            TraceService.WriteHeader("ProjectsWizard::OnRunStarted project=" + this.SettingsService.ActiveProject);
        }

        /// <summary>
        /// Called when [should add project item].
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>True or false.</returns>
        protected override bool OnShouldAddProjectItem(string filePath)
        {
            ////TraceService.WriteLine("ProjectsWizard::OnShouldAddProjectItem path=" + filePath);

            return true;
        }

        /// <summary>
        /// Called when [project finished generating].
        /// </summary>
        /// <param name="project">The project.</param>
        protected override void OnProjectFinishedGenerating(Project project)
        {
            TraceService.WriteLine("ProjectsWizard::OnProjectFinishedGenerating project=" + project.Name);
        }

        /// <summary>
        /// Called when [run finished].
        /// </summary>
        protected override void OnRunFinished()
        {
            TraceService.WriteHeader("ProjectsWizard::OnRunFinished project=" + this.SettingsService.ActiveProject);
        }
    }
}