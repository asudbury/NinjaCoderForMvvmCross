// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CodeSnippet type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Entities
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using System.Xml.Serialization;

    /// <summary>
    /// Defines the CodeSnippet type.
    /// </summary>
    [Serializable]
    [XmlRoot(ElementName = "CodeSnippet", Namespace = "")]
    public class CodeSnippet
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CodeSnippet" /> class.
        /// </summary>
        public CodeSnippet()
        {
            this.UsingStatements = new List<string>();
        }

        /// <summary>
        /// Gets or sets the references.
        /// </summary>
        [XmlArray(ElementName = "References")]
        [XmlArrayItem(ElementName = "Reference")]
        public List<string> References { get; set; }

        /// <summary>
        /// Gets or sets the using statements.
        /// </summary>
        [XmlArray(ElementName = "UsingStatements")]
        [XmlArrayItem(ElementName = "Statement")]
        public List<string> UsingStatements { get; set; }

        /// <summary>
        /// Gets or sets the interfaces.
        /// </summary>
        [DataMember]
        [XmlArray(ElementName = "Interfaces")]
        [XmlArrayItem(ElementName = "Interface")]
        public List<string> Interfaces { get; set; }

        /// <summary>
        /// Gets or sets the variables.
        /// </summary>
        [DataMember]
        [XmlArray(ElementName = "Variables")]
        [XmlArrayItem(ElementName = "Variable")]
        public List<string> Variables { get; set; }

        /// <summary>
        /// Gets or sets the mock variables.
        /// </summary>
        [DataMember]
        [XmlArray(ElementName = "MockVariables")]
        [XmlArrayItem(ElementName = "MockVariable")]
        public List<string> MockVariables { get; set; }

        /// <summary>
        ///  Gets or sets the test init method.
        /// </summary>
        [XmlElement(ElementName = "TestInitMethod")]
        public string TestInitMethod { get; set; }

        /// <summary>
        /// Gets or sets the project.
        /// </summary>
        [XmlElement(ElementName = "Project")]
        public string Project { get; set; }

        /// <summary>
        /// Gets or sets the class.
        /// </summary>
        [XmlElement(ElementName = "Class")]
        public string Class { get; set; }

        /// <summary>
        /// Gets or sets the method.
        /// </summary>
        [XmlElement(ElementName = "Method")]
        public string Method { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [XmlElement(ElementName = "Code")]
        public string Code { get; set; }

        /// <summary>
        /// Gets or sets the replacement variables.
        /// </summary>
        public List<KeyValuePair<string, string>> ReplacementVariables { get; set; }

        /// <summary>
        /// Gets or sets the mocking variable declaration.
        /// </summary>
        public string MockingVariableDeclaration { get; set; }

        /// <summary>
        /// Gets or sets the mock constructor code.
        /// </summary>
        public string MockConstructorCode { get; set; }

        /// <summary>
        /// Gets or sets the mock init code.
        /// </summary>
        public string MockInitCode { get; set; }
    }
}
