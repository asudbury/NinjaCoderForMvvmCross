// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectTemplateInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Entities
{
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the ProjectTemplateInfo type.
    /// </summary>
    public class ProjectTemplateInfo : BaseTemplateInfo
    {
        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to use nuget.
        /// </summary>
        public bool UseNuget { get; set; }

        /// <summary>
        /// Gets or sets the nuget commands.
        /// </summary>
        public List<string> NugetCommands { get; set; }

        /// <summary>
        /// Gets or sets the non MVX assemblies.
        /// </summary>
        public IEnumerable<string> NonMvxAssemblies { get; set; }
    }
}
