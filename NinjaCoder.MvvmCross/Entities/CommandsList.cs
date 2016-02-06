// --------------------------------------------------------------------------------------------------------------------
// <summary>
//      Defines the CommandsList type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Entities
{
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    /// Defines the CommandsList type.
    /// </summary>
    public class CommandsList
    {
        /// <summary>
        /// Gets or sets the commands.
        /// </summary>
        public IEnumerable<StudioCommand> Commands { get; set; }

        /// <summary>
        /// Gets or sets the file operations.
        /// </summary>
        public IEnumerable<FileOperation> FileOperations { get; set; }
    }
}
