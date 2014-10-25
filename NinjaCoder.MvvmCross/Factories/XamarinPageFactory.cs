// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the XamarinPageFactory type.
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
    /// Defines the XamarinPageFactory type.
    /// </summary>
    public class XamarinPageFactory : IXamarinPageFactory
    {
        /// <summary>
        /// Gets the pages.
        /// </summary>
        public IEnumerable<ImageItemWithDescription> Pages
        {
            get
            {
                List<ImageItemWithDescription> pages = new List<ImageItemWithDescription>
                {
                    new ImageItemWithDescription  
                        {
                            ImageUrl = this.GetUrlPath("ContentPage.png"),
                            Name = "Content Page",
                            Selected = true
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl =  this.GetUrlPath("MasterDetailPage.png"),
                            Name = "Master Detail Page"
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl =  this.GetUrlPath("NavigationPage.png"),
                            Name = "Navigation Page"
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl =  this.GetUrlPath("TabbedPage.png"),
                            Name = "Tabbed Page"
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl =  this.GetUrlPath("CarouselPage.png"),
                            Name = "Carousel Page"
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
