// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IPageService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Services
{
    using System;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the IPageService type.
    /// </summary>
    public interface IPageService
    {
        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <param name="viewModelType">Type of the view model.</param>
        /// <returns>
        /// A Page.
        /// </returns>
        Page GetPage(Type viewModelType);
    }
}
