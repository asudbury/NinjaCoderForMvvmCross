// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the XamarinLayoutFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using Constants;
    using Interfaces;
    using Scorchio.Infrastructure.Entities;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the XamarinLayoutFactory type.
    /// </summary>
    public class XamarinLayoutFactory : IXamarinLayoutFactory
    {
        /// <summary>
        /// Gets the layouts.
        /// </summary>
        public IEnumerable<ImageItemWithDescription> Layouts
        {
            get
            {
                List<ImageItemWithDescription> pages = new List<ImageItemWithDescription>
                {
                    new ImageItemWithDescription  
                        {
                            ImageUrl = this.GetUrlPath("StackLayout.png"),
                            Name = "Stack Layout",
                            Selected = true
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl =  this.GetUrlPath("AbsoluteLayout.png"),
                            Name = "Absolute Layout"
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl =  this.GetUrlPath("RelativeLayout.png"),
                            Name = "Relative Layout"
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl =  this.GetUrlPath("GridLayout.png"),
                            Name = "Grid Layout"
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl =  this.GetUrlPath("ContentView.png"),
                            Name = "Content View"
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl =  this.GetUrlPath("ScrollView.png"),
                            Name = "Scroll View"
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl =  this.GetUrlPath("Frame.png"),
                            Name = "Frame"
                        }
                };

                return pages.OrderBy(x => x.Name);
            }
        }

        /// <summary>
        /// Gets the URL path.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>the url of the image.</returns>
        internal string GetUrlPath(string image)
        {
            return string.Format("{0}/{1}", Settings.XamarinResourcePath, image);
        }
    }
}
