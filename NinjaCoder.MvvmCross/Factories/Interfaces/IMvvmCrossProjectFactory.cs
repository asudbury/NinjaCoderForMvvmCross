// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IMvvmCrossProjectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the IMvvmCrossProjectFactory type.
    /// </summary>
    public interface IMvvmCrossProjectFactory
    {
        /// <summary>
        /// Gets the allowed projects.
        /// </summary>
        /// <returns>The allowed projects.</returns>
        IEnumerable<ProjectTemplateInfo> GetAllowedProjects();
    }
}
