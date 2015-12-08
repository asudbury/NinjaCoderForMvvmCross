// --------------------------------------------------------------------------------------------------------------------
// <summary>
//      Defines the CustomerRendererGroup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the CustomerRendererGroup type.
    /// </summary>
    public class CustomerRendererGroup
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the renderers.
        /// </summary>
        public IEnumerable<CustomerRenderer> Renderers { get; set; }
    }
}
