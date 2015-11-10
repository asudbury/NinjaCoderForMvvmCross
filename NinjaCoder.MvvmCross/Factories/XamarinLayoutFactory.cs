// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the XamarinLayoutFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using Constants;
    using Interfaces;
    using NinjaCoder.MvvmCross.Entities;
    using Scorchio.Infrastructure.Entities;
    using Scorchio.Infrastructure.Extensions;
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
                            Name = XamarinLayout.StackLayout.GetDescription(),
                            Selected = true
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl = this.GetUrlPath("AbsoluteLayout.png"),
                            Name = XamarinLayout.AbsoluteLayout.GetDescription(),
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl = this.GetUrlPath("RelativeLayout.png"),
                            Name = XamarinLayout.RelativeLayout.GetDescription(),
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl = this.GetUrlPath("GridLayout.png"),
                            Name = XamarinLayout.GridLayout.GetDescription(),
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl = this.GetUrlPath("ContentView.png"),
                            Name = XamarinLayout.ContentView.GetDescription(),
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl = this.GetUrlPath("ScrollView.png"),
                            Name = XamarinLayout.ScrollView.GetDescription(),
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl = this.GetUrlPath("Frame.png"),
                            Name = XamarinLayout.Frame.GetDescription(),
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
