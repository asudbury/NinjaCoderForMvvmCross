// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Plugin type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Entities
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the Plugin type.
    /// </summary>
    [DataContract]
    public class Plugin
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Plugin" /> class.
        /// </summary>
        public Plugin()
        {
            this.NugetCommands = new List<NugetCommand>();
            this.Platforms = new List<string>();
            this.NinjaSamples = new List<Plugin>();
            this.Commands = new List<StudioCommand>();
            this.FileOperations = new List<FileOperation>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [is community plugin].
        /// </summary>
        public bool IsCommunityPlugin { get; set; }

        /// <summary>
        /// Gets or sets the using statement.
        /// </summary>
        public string UsingStatement { get; set; }

        /// <summary>
        /// Gets or sets the nuget commands.
        /// </summary>
        public IEnumerable<NugetCommand> NugetCommands { get; set; }

        /// <summary>
        /// Gets or sets the platforms.
        /// </summary>
        public IEnumerable<string> Platforms { get; set; }
        
        /// <summary>
        /// Gets or sets the frameworks.
        /// </summary>
        public IEnumerable<FrameworkType> Frameworks { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [overwrite files].
        /// </summary>
        public bool OverwriteFiles { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Gets or sets the ninja samples.
        /// </summary>
        public IEnumerable<Plugin> NinjaSamples { get; set; }

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
