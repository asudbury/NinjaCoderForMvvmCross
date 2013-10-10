 // --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsWizard type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards.Projects
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using EnvDTE;

    using NinjaCoder.MvvmCross.TemplateWizards.Extensions;
    using NinjaCoder.MvvmCross.TemplateWizards.Services;

    using VSLangProj;

    /// <summary>
    ///  Defines the ProjectsWizard type.
    /// </summary>
    public class ProjectsWizard : BaseWizard
    {
        /// <summary>
        /// The files to delete.
        /// </summary>
        private List<string> filesToDelete;

        /// <summary>
        /// Runs custom wizard logic at the beginning of a template wizard run.
        /// </summary>
        public override void OnRunStarted()
        {
            TraceService.WriteLine("ProjectsWizard::OnRunStarted");

            this.filesToDelete = new List<string>();
        }

        /// <summary>
        /// Called when [should add project item].
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>True or false.</returns>
        public override bool OnShouldAddProjectItem(string filePath)
        {
            TraceService.WriteLine("ProjectsWizard::OnShouldAddProjectItem path=" + filePath);

            if (this.SettingsService.RemoveAssembliesIfNugetUsed)
            {
                if (this.SettingsService.UseNugetForProjectTemplates)
                {
                    //// dont add the Mvx assemblies.
                    if (filePath.StartsWith("Cirrious"))
                    {
                        TraceService.WriteLine("File to be removed path=" + filePath);
                        this.filesToDelete.Add(filePath);
                    }
                }
            }

            return true;
        }

        /// <summary>
        /// Called when [project finished generating].
        /// </summary>
        /// <param name="project">The project.</param>
        public override void OnProjectFinishedGenerating(Project project)
        {
            TraceService.WriteLine("ProjectsWizard::OnProjectFinishedGenerating");

            foreach (string path in this.filesToDelete.Where(File.Exists))
            {
                File.Delete(path);
            }

            //// now remove the references.
            if (this.SettingsService.RemoveAssembliesIfNugetUsed)
            {
                if (this.SettingsService.UseNugetForProjectTemplates)
                {
                    IEnumerable<Reference> references = project.GetProjectReferences();

                    foreach (Reference reference in references)
                    {
                        if (reference.Name.StartsWith("Cirrious"))
                        {
                            reference.Remove();
                        }
                    }
                }
            }
        }
    }
}