// --------------------------------------------------------------------------------------------------------------------
// <summary>
//      Defines the CustomRenderers type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the CustomRenderers type.
    /// </summary>
    public class CustomRenderers
    {
        /// <summary>
        /// Gets or sets the help link.
        /// </summary>
        public string HelpLink { get; set; }

        /// <summary>
        /// Gets or sets the groups.
        /// </summary>
        public IEnumerable<CustomerRendererGroup> Groups { get; set; }
    }
}
