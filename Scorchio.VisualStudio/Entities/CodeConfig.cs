// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CodeConfig type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Defines the CodeConfig type.
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "CodeConfig", Namespace = "")]
    public class CodeConfig 
    {
        /// <summary>
        /// Gets or sets the references.
        /// </summary>
        [XmlArray(ElementName = "References")]
        [XmlArrayItem(ElementName = "Reference")]
        public List<string> References { get; set; }

        /// <summary>
        /// Gets or sets the nuget package.
        /// </summary>
        [XmlElement(ElementName = "NugetPackage")]
        public string NugetPackage { get; set; }
    }
}
