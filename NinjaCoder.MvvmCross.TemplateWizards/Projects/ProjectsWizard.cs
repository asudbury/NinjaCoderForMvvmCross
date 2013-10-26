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

    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;

    using VSLangProj;

    /// <summary>
    ///  Defines the ProjectsWizard type.
    /// </summary>
    public class ProjectsWizard : BaseWizard
    {
        /// <summary>
        /// The MvvmCross assembly prefix.
        /// </summary>
        private const string MvxAssemblyPrefix = "Cirrious";

        /// <summary>
        /// The files to delete.
        /// </summary>
        private List<string> filesToDelete;

        /// <summary>
        /// Runs custom wizard logic at the beginning of a template wizard run.
        /// </summary>
        protected override void OnRunStarted()
        {
            TraceService.WriteHeader("ProjectsWizard::OnRunStarted");

            this.filesToDelete = new List<string>();
        }

        /// <summary>
        /// Called when [should add project item].
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>True or false.</returns>
        protected override bool OnShouldAddProjectItem(string filePath)
        {
            TraceService.WriteLine("ProjectsWizard::OnShouldAddProjectItem path=" + filePath);

            if (this.SettingsService.RemoveAssembliesIfNugetUsed)
            {
                if (this.SettingsService.UseNugetForProjectTemplates)
                {
                    //// dont add the MvvmCross assemblies.
                    if (filePath.StartsWith(MvxAssemblyPrefix))
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
        protected override void OnProjectFinishedGenerating(Project project)
        {
            TraceService.WriteLine("ProjectsWizard::OnProjectFinishedGenerating");

            //// remove the lib folder if that's what the developer wants to happen.
            //// if the develop has selected use nuget then also remove the project

            if (this.SettingsService.IncludeLibFolderInProjects == false ||
                this.SettingsService.UseNugetForProjectTemplates)
            {
                project.RemoveFolder("Lib");
            }

            //// delete the mvx files if we are using nuget.
            foreach (string path in this.filesToDelete.Where(File.Exists))
            {
                TraceService.WriteLine("DeleteFile path" + path);
                File.Delete(path);
            }

            //// now remove the references if we using nuget.
            if (this.SettingsService.RemoveAssembliesIfNugetUsed &&
                this.SettingsService.UseNugetForProjectTemplates)
            {
                TraceService.WriteLine("RemoveAssembliesIfNugetUsed");
                TraceService.WriteLine("UseNugetForProjectTemplates");

                this.RemoveMvvmCrossReferences(project);
            }
            else if (this.SettingsService.CopyAssembliesToLibFolder == false)
            {
                TraceService.WriteLine("CopyAssembliesToLibFolder");

                //// grab the list of mvvmcross assembly names.
                IEnumerable<string> assembliesToAdd = this.GetAssembliesToAddList(project);

                //// remove references of mvvmcross assemblies
                this.RemoveMvvmCrossReferences(project);

                //// point references back to assemblies in the ninja coder folder.
                project.AddAssemblies(
                    this.SettingsService.MvvmCrossAssembliesPath,
                    assembliesToAdd);
            }
        }

        /// <summary>
        /// Called when [run finished].
        /// </summary>
        protected override void OnRunFinished()
        {
            TraceService.WriteHeader("ProjectsWizard::OnRunFinished");
        }

        /// <summary>
        /// Removes the MvvmCross references.
        /// </summary>
        /// <param name="project">The project.</param>
        private void RemoveMvvmCrossReferences(Project project)
        {
            TraceService.WriteLine("ProjectsWizard::RemoveMvvmCrossReferences");

            project.GetProjectReferences()
                .Where(x => x.Name.StartsWith(MvxAssemblyPrefix))
                .ToList()
                .ForEach(r => r.Remove());
        }

        /// <summary>
        /// Assemblies to Add.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <returns>A list of assemblies to add to the project.</returns>
        private IEnumerable<string> GetAssembliesToAddList(Project project)
        {
            TraceService.WriteLine("ProjectsWizard::GetAssembliesToAddList");

            IEnumerable<Reference> references = project.GetProjectReferences()
                .Where(x => x.Name.StartsWith(MvxAssemblyPrefix));

            return references.Select(reference => reference.Name).ToList();
        }
    }
}