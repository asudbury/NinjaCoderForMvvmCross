// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ImagePickerViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.ViewModels
{
    using Entities;
    using EventArguments;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the ImagePickerViewModel type.
    /// </summary>
    public class ImagePickerViewModel : BaseViewModel
    {
        /// <summary>
        /// Occurs when [image changed].
        /// </summary>
        public event EventHandler<ImageChangedEventArgs> ImageChanged;
        
        /// <summary>
        /// The images.
        /// </summary>
        private IEnumerable<ImageItem> images;

        /// <summary>
        /// The selected image.
        /// </summary>
        private ImageItem selectedImage;

        /// <summary>
        /// Gets or sets the images.
        /// </summary>
        public IEnumerable<ImageItem> Images
        {
            get { return this.images; }
            set { this.SetProperty(ref this.images, value); }
        }

        /// <summary>
        /// Gets or sets the image of the selected.
        /// </summary>
        public ImageItem SelectedImage
        {
            get
            {
                return this.selectedImage;
            }

            set
            {
                this.SetProperty(ref this.selectedImage, value);
                this.UpdateImage();
            }
        }

        /// <summary>
        /// Updates the image.
        /// </summary>
        internal void UpdateImage()
        {
            if (this.SelectedImage != null)
            {
                if (this.ImageChanged != null)
                {
                    this.ImageChanged(this, new ImageChangedEventArgs { Image = this.SelectedImage });
                }
            }
        }
    }
}
