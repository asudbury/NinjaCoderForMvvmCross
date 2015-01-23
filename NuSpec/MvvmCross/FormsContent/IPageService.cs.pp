// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IPageService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Services
{
    using Cirrious.MvvmCross.ViewModels;

    using Xamarin.Forms;

    /// <summary>
    /// Defines the IPageService type.
    /// </summary>
    public interface IPageService
    {
        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <returns>A Page.</returns>
        Page GetPage(MvxViewModelRequest request);
    }
}
