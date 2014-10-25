// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IXamarinFormsProjectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the IXamarinFormsProjectFactory type.
    /// </summary>
    public interface IXamarinFormsProjectFactory
    {
        /// <summary>
        /// Gets the allowed projects.
        /// </summary>
        /// <returns>The allowed projects.</returns>
        IEnumerable<ProjectTemplateInfo> GetAllowedProjects();
    }
}
