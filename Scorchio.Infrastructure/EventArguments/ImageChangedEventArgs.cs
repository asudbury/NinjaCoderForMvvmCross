// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ImageChangedEventArgs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.EventArguments
{
    using System;

    using Scorchio.Infrastructure.Entities;

    /// <summary>
    ///  Defines the ImageChangedEventArgs type.
    /// </summary>
    public class ImageChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        public ImageItem Image { get; set; }
    }
}
