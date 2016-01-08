// --------------------------------------------------------------------------------------------------------------------
// <summary>
//      Defines the StudioCommand type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Scorchio.VisualStudio.Entities
{
    /// <summary>
    /// Defines the StudioCommand type.
    /// </summary>
    public class StudioCommand
    {
        /// <summary>
        /// Gets or sets the plat form.
        /// </summary>
        public string PlatForm { get; set; }

        /// <summary>
        /// Gets or sets the type of the command.
        /// </summary>
        public string CommandType { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the file.
        /// </summary>
        public string File { get; set; }

        /// <summary>
        /// Gets or sets the directory.
        /// </summary>
        public string Directory { get; set; }

        /// <summary>
        /// Gets or sets from.
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Gets or sets to.
        /// </summary>
        public string To { get; set; }
    }
}
