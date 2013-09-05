// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Presenters
{
    using System.Collections.Generic;
    using System.Linq;

    using Services.Interfaces;

    using Scorchio.VisualStudio.Entities;
    using Views.Interfaces;
    using Settings = Properties.Settings;

    /// <summary>
    ///  Defines the ProjectsPresenter type.
    /// </summary>
    public class ProjectsPresenter : BasePresenter
    {
        /// <summary>
        /// The view.
        /// </summary>
        private readonly IProjectsView view;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="settingsService">The settings service.</param>
        public ProjectsPresenter(
            IProjectsView view,
            ISettingsService settingsService)
        {
            this.view = view;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Gets the required templates.
        /// </summary>
        /// <returns>The required templates.</returns>
        public IEnumerable<ProjectTemplateInfo> GetRequiredTemplates()
        {
            List<ProjectTemplateInfo> templateInfos = new List<ProjectTemplateInfo>();

            string projectName = this.view.ProjectName;

            foreach (ProjectTemplateInfo projectInfo in this.view.RequiredProjects)
            {
                this.GetRequiredTemplate(projectName, projectInfo);

                templateInfos.Add(projectInfo);
            }

            return templateInfos;
        }

        /// <summary>
        /// Gets the solution path.
        /// </summary>
        /// <returns>The solution path.</returns>
        public string GetSolutionPath()
        {
            return string.Format(@"{0}{1}", this.view.Path, this.view.ProjectName);
        }

        /// <summary>
        /// Loads the settings.
        /// </summary>
        /// <param name="defaultProjectsLocation">The default projects location.</param>
        /// <param name="defaultProjectName">Default name of the project.</param>
        /// <param name="projectInfos">The project infos.</param>
        public void Load(
            string defaultProjectsLocation,
            string defaultProjectName,
            IEnumerable<ProjectTemplateInfo> projectInfos)
        {
            this.view.DisplayLogo = this.settingsService.DisplayLogo;
            this.view.UseNuget = this.settingsService.UseNugetForProjectTemplates;
            string defaultPath = Settings.Default[Constants.Settings.DefaultPath].ToString();

            this.view.Path = defaultPath != string.Empty ? defaultPath : defaultProjectsLocation;

            this.view.ProjectName = defaultProjectName;
            this.view.EnableProjectName = defaultProjectName.Length > 0;

            foreach (ProjectTemplateInfo projectInfo in projectInfos)
            {
                this.view.AddTemplate(projectInfo);
            }
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            Settings.Default[Constants.Settings.DefaultPath] = this.view.Path;
            Settings.Default.Save();

            this.settingsService.UseNugetForProjectTemplates = this.view.UseNuget;
        }

        /// <summary>
        /// Gets the required template.
        /// </summary>
        /// <param name="projectName">Name of the project.</param>
        /// <param name="projectInfo">The project info.</param>
        internal void GetRequiredTemplate(string projectName, ProjectTemplateInfo projectInfo)
        {
            projectInfo.Name = projectName + projectInfo.ProjectSuffix;
            projectInfo.UseNuget = this.view.UseNuget;

            if (projectInfo.NugetCommands != null)
            {
                List<string> newCommands =
                    projectInfo.NugetCommands.Select(
                        nugetCommand => string.Format("{0} {1}", nugetCommand, projectInfo.Name)).ToList();

                projectInfo.NugetCommands = newCommands;
            }
        }
    }
}
