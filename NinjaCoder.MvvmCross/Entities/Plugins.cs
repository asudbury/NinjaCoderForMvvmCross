// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Plugins type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Entities
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// Defines the Plugins type.
    /// </summary>
    [DataContract]
    public class Plugins
    {
        /// <summary>
        /// Gets or sets the plugins.
        /// </summary>
        [DataMember(Name = "Plugins")]
        public List<Plugin> Items { get; set; }
    }
}
