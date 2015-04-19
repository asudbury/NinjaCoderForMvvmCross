 // --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsWizard type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards.Plugins
{
    using System.IO;

    using EnvDTE;

    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the PluginsWizard type.
    /// </summary>
    public class PluginsWizard : BaseWizard
    {
        /// <summary>
        /// Runs custom wizard logic at the beginning of a template wizard run.
        /// </summary>
        protected override void OnRunStarted()
        {
            TraceService.WriteHeader("PluginsWizard::OnRunStarted");
        }

        /// <summary>
        /// Called when [should add project item].
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>True or false.</returns>
        protected override bool OnShouldAddProjectItem(string filePath)
        {
            TraceService.WriteLine("PluginsWizard::OnShouldAddProjectItem path=" + filePath);

            Project activeProject = this.GetActiveProject();

            if (activeProject != null)
            {
                string projectPath = activeProject.GetProjectPath();
                string path = string.Format(@"{0}\{1}", projectPath, filePath);

                if (File.Exists(path))
                {
                    return false;
                }
            }

            string pluginsString = this.SettingsService.PluginsToAdd;

            if (string.IsNullOrEmpty(pluginsString) == false)
            {
                FileInfo fileInfo = new FileInfo(filePath);

                return pluginsString.Contains(fileInfo.Name);
            }

            return false;
        }

        /// <summary>
        /// Called when [project item finished generating].
        /// </summary>
        /// <param name="projectItem">The project item.</param>
        protected override void OnProjectItemFinishedGenerating(ProjectItem projectItem)
        {
            TraceService.WriteLine("PluginsWizard::OnProjectItemFinishedGenerating name=" + projectItem.Name);

            if (this.SettingsService.RemoveDefaultComments)
            {
                projectItem.RemoveComments();
            }

            if (this.SettingsService.RemoveDefaultFileHeaders)
            {
                projectItem.RemoveHeader();
            }
        }

        /// <summary>
        /// Called when [run finished].
        /// </summary>
        protected override void OnRunFinished()
        {
            TraceService.WriteHeader("PluginsWizard::OnRunFinished");
        }
    }
}