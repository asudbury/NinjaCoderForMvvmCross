// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SolutionOptionsPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Presenters
{
    using System.Collections.Generic;
    using System.Configuration;

    using Views.Interfaces;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;

    using Settings = Properties.Settings;

    /// <summary>
    ///  Defines the SolutionOptionsPresenter type.
    /// </summary>
    public class SolutionOptionsPresenter : BasePresenter
    {
        /// <summary>
        /// The view.
        /// </summary>
        private readonly ISolutionOptionsView view;

        /// <summary>
        /// The default projects location.
        /// </summary>
        private readonly string defaultProjectsLocation;

        /// <summary>
        /// The default project name.
        /// </summary>
        private readonly string defaultProjectName;

        /// <summary>
        /// The project infos.
        /// </summary>
        private readonly List<ProjectTemplateInfo> projectInfos;

        /// <summary>
        /// The display logo.
        /// </summary>
        private readonly bool displayLogo;

        /// <summary>
        /// Initializes a new instance of the <see cref="SolutionOptionsPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="defaultProjectsLocation">The default projects location.</param>
        /// <param name="defaultProjectName">Default name of the project.</param>
        /// <param name="projectInfos">The project infos.</param>
        /// <param name="displayLogo">if set to <c>true</c> [display logo].</param>
        public SolutionOptionsPresenter(
            ISolutionOptionsView view,
            string defaultProjectsLocation,
            string defaultProjectName,
            List<ProjectTemplateInfo> projectInfos,
            bool displayLogo)
        {
            this.view = view;
            this.defaultProjectsLocation = defaultProjectsLocation;
            this.defaultProjectName = defaultProjectName;
            this.projectInfos = projectInfos;
            this.displayLogo = displayLogo;
        }

        /// <summary>
        /// Gets the required templates.
        /// </summary>
        /// <returns>The required templates.</returns>
        public List<ProjectTemplateInfo> GetRequiredTemplates()
        {
            this.projectInfos.Clear();

            string projectName = this.view.ProjectName;

            foreach (ProjectTemplateInfo projectInfo in this.view.RequiredProjects)
            {
                projectInfo.Name = projectName + projectInfo.ProjectSuffix;
                this.projectInfos.Add(projectInfo);
            }

            return this.projectInfos;
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
        public void Load()
        {
            try
            {
                this.view.DisplayLogo = this.displayLogo;
                string defaultPath = Settings.Default[Constants.Settings.DefaultPath].ToString();

                this.view.Path = defaultPath != string.Empty ? defaultPath : this.defaultProjectsLocation;

                this.view.ProjectName = this.defaultProjectName;
                this.view.EnableProjectName = this.defaultProjectName.Length > 0;

                foreach (ProjectTemplateInfo projectInfo in this.projectInfos)
                {
                    this.view.AddTemplate(projectInfo);
                }
            }
            catch (SettingsPropertyNotFoundException exception)
            {
                TraceService.WriteLine("Setting not found :-" + exception.Message);
            }
        }

        /// <summary>
        /// Saves the settings.
        /// </summary>
        public void SaveSettings()
        {
            Settings.Default[Constants.Settings.DefaultPath] = this.view.Path;
            Settings.Default.Save();
        }
    }
}
