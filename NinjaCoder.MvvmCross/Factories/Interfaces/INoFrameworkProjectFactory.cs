// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the INoFrameworkProjectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the INoFrameworkProjectFactory type.
    /// </summary>
    public interface INoFrameworkProjectFactory
    {
        /// <summary>
        /// Gets the allowed projects.
        /// </summary>
        /// <returns>The allowed projects.</returns>
        IEnumerable<ProjectTemplateInfo> GetAllowedProjects();
    }
}
