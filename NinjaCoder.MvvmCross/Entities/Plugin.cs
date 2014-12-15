// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Plugin type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Entities
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

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
            this.NugetCommands = new List<string>();
            this.Platforms = new List<string>();
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
        public IEnumerable<string> NugetCommands { get; set; }

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
    }
}
