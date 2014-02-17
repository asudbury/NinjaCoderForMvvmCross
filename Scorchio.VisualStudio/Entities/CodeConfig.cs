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
    [XmlRoot(ElementName = "CodeConfig")]
    public class CodeConfig 
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeConfig" /> class.
        /// </summary>
        public CodeConfig()
        {
            this.References = new List<string>();
            this.DependentPlugins = new List<string>();
            this.CodeDependencies = new List<CodeSnippet>();
        }

        /// <summary>
        /// Gets or sets the references.
        /// </summary>
        [XmlArray(ElementName = "References")]
        [XmlArrayItem(ElementName = "Reference")]
        public List<string> References { get; set; }

        /// <summary>
        /// Gets or sets the nuget installation mandatory.
        /// </summary>
        [XmlElement(ElementName = "NugetInstallationMandatory")]
        public string NugetInstallationMandatory { get; set; }

        /// <summary>
        /// Gets or sets the nuget package.
        /// </summary>
        [XmlElement(ElementName = "NugetPackage")]
        public string NugetPackage { get; set; }

        /// <summary>
        /// Gets or sets the bootstrap file name override.
        /// </summary>
        [XmlElement(ElementName = "BootstrapFileNameOverride")]
        public string BootstrapFileNameOverride { get; set; }

        /// <summary>
        /// Gets or sets the dependent plugins.
        /// </summary>
        [XmlArray(ElementName = "DependentPlugins")]
        [XmlArrayItem(ElementName = "DependentPlugin")]
        public List<string> DependentPlugins { get; set; }

        /// <summary>
        /// Gets or sets the code dependencies.
        /// </summary>
        [XmlArray(ElementName = "CodeDependencies")]
        [XmlArrayItem(ElementName = "CodeSnippet")]
        public List<CodeSnippet> CodeDependencies { get; set; }
    }
}
