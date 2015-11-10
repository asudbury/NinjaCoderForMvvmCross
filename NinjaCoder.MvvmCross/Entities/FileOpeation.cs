// --------------------------------------------------------------------------------------------------------------------
// <summary>
// 		Defines the Command type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Entities
{
    /// <summary>
    /// Defines the Command type.
    /// </summary>
    public class Command
    {
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the command.
        /// </summary>
        public string CommandType { get; set; }

        /// <summary>
        /// Gets or sets the plat form.
        /// </summary>
        public string PlatForm { get; set; }
    }
}
