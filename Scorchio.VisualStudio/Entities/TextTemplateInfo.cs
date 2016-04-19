// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TextTemplateInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Entities
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the TextTemplateInfo type.
    /// </summary>
    public class TextTemplateInfo
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextTemplateInfo"/> class.
        /// </summary>
        public TextTemplateInfo()
        {
            this.FileOperations = new List<FileOperation>();
            this.ChildItems = new List<TextTemplateInfo>();
        }

        /// <summary>
        /// Gets or sets the project suffix.
        /// </summary>
        public string ProjectSuffix { get; set; }

        /// <summary>
        /// Gets or sets the name of the template.
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// Gets or sets the short name of the template.
        /// </summary>
        public string ShortTemplateName { get; set; }

        /// <summary>
        /// Gets or sets the tokens.
        /// </summary>
        public Dictionary<string, string> Tokens { get; set; }

        /// <summary>
        /// Gets or sets the project folder.
        /// </summary>
        public string ProjectFolder { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the text output.
        /// </summary>
        public string TextOutput { get; set; }

        /// <summary>
        /// Gets the file operations.
        /// </summary>
        public List<FileOperation> FileOperations { get; }

        /// <summary>
        /// Gets the child items.
        /// </summary>
        public List<TextTemplateInfo> ChildItems { get; }
    }
}
