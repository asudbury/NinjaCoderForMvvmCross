// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IProjectsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the IProjectsService type.
    /// </summary>
    public interface IProjectsService
    {
        /// <summary>
        /// Gets or sets a value indicating whether this instance is new solution.
        /// </summary>
        bool IsNewSolution { get; set; }

        /// <summary>
        /// Adds the projects.
        /// </summary>
        /// <param name="visualStudioServiceInstance">The visual studio service.</param>
        /// <param name="path">The path.</param>
        /// <param name="projectsInfos">The projects infos.</param>
        /// <returns>
        /// The messages.
        /// </returns>
        IEnumerable<string> AddProjects(
            IVisualStudioService visualStudioServiceInstance,
            string path,
            IEnumerable<ProjectTemplateInfo> projectsInfos);

        /// <summary>
        /// Sets the start up project.
        /// </summary>
        void SetStartUpProject();
    }
}