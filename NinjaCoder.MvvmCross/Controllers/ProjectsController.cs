// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using Constants;
    using EnvDTE;
    using EnvDTE80;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;
    using Services;
    using Views;

    /// <summary>
    ///  Defines the ProjectsController type.
    /// </summary>
    public class ProjectsController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsController" /> class.
        /// </summary>
        public ProjectsController()
            : base(new VisualStudioService(), new ReadMeService(), new SettingsService())
        {
            TraceService.WriteLine("ProjectsController::Constructor");
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            this.AddTraceHeader("ProjectsController", "Run");

            string defaultLocation;
            string defaultProjectName = this.GetDefaultProjectName();

            //// if this fails it will almost certainly be the COM integration with VStudio.
            try
            {
                defaultLocation = this.VisualStudioService.DTE2.GetDefaultProjectsLocation();
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"Error Communicating with Visual Studio Error:-" + exception.Message);
                return;
            }

            List<ProjectTemplateInfo> projectTemplateInfos = this.VisualStudioService.AllowedProjectTemplates;

            if (projectTemplateInfos.Count > 0)
            {
                SolutionOptionsForm form = new SolutionOptionsForm(
                    defaultLocation, 
                    defaultProjectName, 
                    projectTemplateInfos, 
                    this.SettingsService.DisplayLogo);

                form.ShowDialog();

                if (form.Continue)
                {
                    this.WriteStatusBarMessage("Ninja Coder is running....");

                    Solution2 solution2 = this.VisualStudioService.DTE2.GetSolution() as Solution2;

                    //// create the solution if we don't have one!
                    if (string.IsNullOrEmpty(solution2.FullName))
                    {
                        string solutionPath = form.Presenter.GetSolutionPath();

                        if (Directory.Exists(solutionPath) == false)
                        {
                            Directory.CreateDirectory(solutionPath);
                        }

                        solution2.Create(solutionPath, form.ProjectName);
                    }

                    IEnumerable<string> messages = solution2.AddProjects(
                        form.Presenter.GetSolutionPath(),
                        form.Presenter.GetRequiredTemplates(), 
                        true,
                        this.SettingsService.IncludeLibFolderInProjects);

                    this.WriteStatusBarMessage("Ninja Coder is updating files...");

                    //// create the version files.
                    this.CreateNinjaVersionFile(this.SettingsService.ApplicationVersion);
                    this.CreateMvvmCrossVersionFile(this.SettingsService.MvvmCrossVersion);

                    //// show the readme.
                    this.ShowReadMe("Add Projects", messages);

                    this.WriteStatusBarMessage("Ninja Coder has completed the build of the MvvmCross solution.");
                }
            }
            else
            {
                MessageBox.Show(@"This solution has already been setup with the MvvmCross projects", Settings.ApplicationName);
            }
        }
        
        /// <summary>
        /// Gets the default name of the project.
        /// </summary>
        /// <returns>The default project name.</returns>
        internal string GetDefaultProjectName()
        {
            Project project = this.VisualStudioService.CoreProject;

            if (project != null)
            {
                TraceService.WriteLine("ProjectsController::GetDefaultProjectName defaultProject=" + project.Name);

                return project.Name.Replace(ProjectSuffixes.Core, string.Empty);
            }

            return string.Empty;
        }
    }
}
