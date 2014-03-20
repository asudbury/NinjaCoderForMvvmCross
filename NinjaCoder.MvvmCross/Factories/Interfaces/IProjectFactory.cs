// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IProjectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the IProjectFactory type.
    /// </summary>
    public interface IProjectFactory
    {
        /// <summary>
        /// Gets the allowed projects.
        /// </summary>
        /// <returns>The allowed projects.</returns>
        IEnumerable<ProjectTemplateInfo> GetAllowedProjects();
    }
}