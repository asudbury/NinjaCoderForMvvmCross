// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ItemTemplateInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Entities
{
    /// <summary>
    ///  Defines the ItemTemplateInfo type.
    /// </summary>
    public class ItemTemplateInfo : BaseTemplateInfo
    {
        /// <summary>
        /// Gets or sets the name of the folder.
        /// </summary>
        public string FolderName { get; set; }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        public string FileName { get; set; }
    }
}
