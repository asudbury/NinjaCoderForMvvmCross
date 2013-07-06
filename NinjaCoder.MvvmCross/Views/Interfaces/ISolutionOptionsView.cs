// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ISolutionOptionsView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Views.Interfaces
{
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the ISolutionOptionsView type.
    /// </summary>
    public interface ISolutionOptionsView
    {
        /// <summary>
        /// Sets a value indicating whether [display logo].
        /// </summary>
        bool DisplayLogo { set; }

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