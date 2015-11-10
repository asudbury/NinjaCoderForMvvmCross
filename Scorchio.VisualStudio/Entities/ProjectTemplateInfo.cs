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
        /// Gets or sets the nuget commands.
        /// </summary>
        public IEnumerable<string> NugetCommands { get; set; }
        
        /// <summary>
        /// Gets or sets a value indicating whether [reference xamarin forms project].
        /// </summary>
        public bool ReferenceXamarinFormsProject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [reference core project].
        /// </summary>
        public bool ReferenceCoreProject { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [reference platform project].
        /// </summary>
        public bool ReferencePlatformProject { get; set; }
    }
}
