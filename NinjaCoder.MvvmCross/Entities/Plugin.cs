// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Plugin type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Entities
{
    using System.Runtime.Serialization;

    /// <summary>
    ///  Defines the Plugin type.
    /// </summary>
    [DataContract]
    public class Plugin
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember]
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        [DataMember]
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the source.
        /// </summary>
        [DataMember]
        public string Source { get; set; }
    }
}
