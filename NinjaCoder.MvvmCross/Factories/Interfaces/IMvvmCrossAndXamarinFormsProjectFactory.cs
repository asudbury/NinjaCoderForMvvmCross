// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IMvvmCrossAndXamarinFormsProjectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the IMvvmCrossAndXamarinFormsProjectFactory type.
    /// </summary>
    public interface IMvvmCrossAndXamarinFormsProjectFactory
    {
        /// <summary>
        /// Gets the allowed projects.
        /// </summary>
        /// <returns>The allowed projects.</returns>
        IEnumerable<ProjectTemplateInfo> GetAllowedProjects();
    }
}
