// --------------------------------------------------------------------------------------------------------------------
// <summary>
//      Defines the FileOperation type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Entities
{
    /// <summary>
    /// Defines the Command type.
    /// </summary>
    public class FileOperation : Command
    {
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
