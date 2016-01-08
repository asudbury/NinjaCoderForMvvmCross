// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvvmCrossViewFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Factories
{
    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using Scorchio.Infrastructure.Entities;
    using System.Collections.ObjectModel;

    /// <summary>
    /// Defines the MvvmCrossViewFactory type.
    /// </summary>
    public class MvvmCrossViewFactory : IMvvmCrossViewFactory
    {
        /// <summary>
        /// Gets the views.
        /// </summary>
        public ObservableCollection<ImageItemWithDescription> Views
        {
            get
            {
                ObservableCollection<ImageItemWithDescription> pages = new ObservableCollection<ImageItemWithDescription>
                {
                    new ImageItemWithDescription  
                        {
                            ImageUrl = this.GetUrlPath("ContentPage.png"),
                            Name = "Blank",
                            Description = "Blank Views",
                            Selected = true
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl = this.GetUrlPath("MasterDetailPage.png"),
                            Description = "Views with sample data",
                            Name = "SampleData"
                        },
                    new ImageItemWithDescription
                        {
                            ImageUrl = this.GetUrlPath("NavigationPage.png"),
                            Description = "Web View to host html",
                            Name = "Web"
                        }
                };

                return pages;
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
