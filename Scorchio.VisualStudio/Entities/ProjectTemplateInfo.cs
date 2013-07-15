// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectTemplateInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Entities
{
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
        /// Gets or sets the nuget command.
        /// </summary>
        public string NugetCommand { get; set; }
    }
}
