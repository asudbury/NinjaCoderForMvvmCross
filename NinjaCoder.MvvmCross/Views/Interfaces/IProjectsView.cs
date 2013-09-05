// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IProjectsView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Views.Interfaces
{
    using System.Collections.Generic;

    using Presenters;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the IProjectsView type.
    /// </summary>
    public interface IProjectsView
    {
        /// <summary>
        /// Gets or sets the presenter.
        /// </summary>
        /// <value>
        /// The presenter.
        /// </value>
        ProjectsPresenter Presenter { get; set; }

        /// <summary>
        /// Sets a value indicating whether [display logo].
        /// </summary>
        bool DisplayLogo { set; }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget].
        /// </summary>
        bool UseNuget { get; set; }

        /// <summary>
        /// Gets or sets the path.
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Gets or sets the name of the project.
        /// </summary>
        string ProjectName { get; set; }

        /// <summary>
        /// Sets a value indicating whether [enable project name].
        /// </summary>
        bool EnableProjectName { set;  }

        /// <summary>
        /// Gets the required projects.
        /// </summary>
        List<ProjectTemplateInfo> RequiredProjects { get; }

        /// <summary>
        /// Adds the template.
        /// </summary>
        /// <param name="projectInfo">The project info.</param>
        void AddTemplate(ProjectTemplateInfo projectInfo);
    }
}