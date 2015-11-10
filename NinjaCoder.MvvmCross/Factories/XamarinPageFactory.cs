// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the XamarinPageFactory type.
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
                            Name = XamarinFormsPage.ContentPage.GetDescription(),
                            Selected = true
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl = this.GetUrlPath("MasterDetailPage.png"),
                            Name = XamarinFormsPage.MasterDetailPage.GetDescription(),
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl = this.GetUrlPath("NavigationPage.png"),
                            Name = XamarinFormsPage.NavigationPage.GetDescription(),
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl = this.GetUrlPath("TabbedPage.png"),
                            Name = XamarinFormsPage.TabbedPage.GetDescription(),
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl = this.GetUrlPath("CarouselPage.png"),
                            Name = XamarinFormsPage.CarouselPage.GetDescription(),
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
