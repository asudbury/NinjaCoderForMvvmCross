// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseTemplateInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Entities
{
    /// <summary>
    ///  Defines the BaseTemplateInfo type.
    /// </summary>
    public abstract class BaseTemplateInfo
    {
        /// <summary>
        /// Gets or sets the name of the friendly.
        /// </summary>
        public string FriendlyName { get; set; }

        /// <summary>
        /// Gets or sets the name of the template.
        /// </summary>
        public string TemplateName { get; set; }

        /// <summary>
        /// Gets or sets the project suffix.
        /// </summary>
        public string ProjectSuffix { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the item is pre selected.
        /// </summary>
        public bool PreSelected { get; set; }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.FriendlyName ?? string.Empty;
        }
    }
}
