// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SolutionExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using EnvDTE;

    using EnvDTE80;

    using Services;

    /// <summary>
    ///  Defines the SolutionExtensions type.
    /// </summary>
    public static class SolutionExtensions
    {
        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The projects.</returns>
        public static IEnumerable<Project> GetProjects(this Solution2 instance)
        {
            TraceService.WriteLine("SolutionExtensions::GetProjects");

            return instance.Projects.Cast<Project>().ToList();
        }
    }
}
