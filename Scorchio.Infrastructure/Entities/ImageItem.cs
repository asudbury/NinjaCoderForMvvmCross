// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ImageItem type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Entities
{
    /// <summary>
    ///  Defines the ImageItem type.
    /// </summary>
    public class ImageItem
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ImageItem"/> is selected.
        /// </summary>
        public bool Selected { get; set; }
    }
}
