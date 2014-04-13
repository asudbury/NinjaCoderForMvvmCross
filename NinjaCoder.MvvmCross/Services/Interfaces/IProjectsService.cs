// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IProjectsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the IProjectsService type.
    /// </summary>
    public interface IProjectsService
    {
        /// <summary>
        /// Adds the projects.
        /// </summary>
        /// <param name="visualStudioServiceInstance">The visual studio service.</param>
        /// <param name="path">The path.</param>
        /// <param name="projectsInfos">The projects infos.</param>
        /// <param name="referenceFirstProject">if set to <c>true</c> [reference first project].</param>
        /// <param name="solutionAlreadyCreated">if set to <c>true</c> [solution already created].</param>
        /// <returns>
        /// The messages.
        /// </returns>
        IEnumerable<string> AddProjects(
            IVisualStudioService visualStudioServiceInstance,
            string path,
            IEnumerable<ProjectTemplateInfo> projectsInfos,
            bool referenceFirstProject,
            bool solutionAlreadyCreated);
    }
}