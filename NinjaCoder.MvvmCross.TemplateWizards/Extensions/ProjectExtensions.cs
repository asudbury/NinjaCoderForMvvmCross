// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using EnvDTE;

    using NinjaCoder.MvvmCross.TemplateWizards.Services;

    using VSLangProj;

    /// <summary>
    /// Defines the ProjectExtensions type.
    /// </summary>
    public static class ProjectExtensions
    {
        /// <summary>
        /// Gets the project references.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The project references.</returns>
        public static IEnumerable<Reference> GetProjectReferences(this Project instance)
        {
            TraceService.WriteLine("ProjectExtensions::GetProjectReferences project=" + instance.Name);

            VSProject project = instance.Object as VSProject;

            return project != null ? project.References.Cast<Reference>() : null;
        }
    }
}
