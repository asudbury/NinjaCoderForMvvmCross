// --------------------------------------------------------------------------------------------------------------------
// <summary>
// 	Defines the NugetActions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Entities
{
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the NugetActions type.
    /// </summary>
    public class NugetActions
    {
        /// <summary>
        /// Gets or sets the nuget commands.
        /// </summary>
        public string NugetCommands { get; set; }

        /// <summary>
        /// Gets or sets the post nuget commands.
        /// </summary>
        public IEnumerable<StudioCommand> PostNugetCommands { get; set; }

        /// <summary>
        /// Gets or sets the post nuget file operations.
        /// </summary>
        public IEnumerable<FileOperation> PostNugetFileOperations { get; set; }
    }
}
