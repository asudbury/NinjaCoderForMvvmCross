// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the XamarinFormsPage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Entities
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the XamarinFormsPage type.
    /// </summary>
    public enum XamarinFormsPage
    {
        /// <summary>
        /// The ContentPage.
        /// </summary>
        [Description("Content Page")]
        ContentPage,

        /// <summary>
        /// The MasterDetailPage.
        /// </summary>
        [Description("Master Detail Page")]
        MasterDetailPage,

        /// <summary>
        /// The NavigationPage.
        /// </summary>
        [Description("Navigation Page")]
        NavigationPage,

        /// <summary>
        /// The TabbedPage.
        /// </summary>
        [Description("Tabbed Page")]
        TabbedPage,

        /// <summary>
        /// The CarouselPage.
        /// </summary>
        [Description("Carousel Page")]
        CarouselPage
    }
}