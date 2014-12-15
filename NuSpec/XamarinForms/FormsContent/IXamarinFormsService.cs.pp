// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IXamarinFormsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Services
{
    using Cirrious.MvvmCross.ViewModels;

    using Xamarin.Forms;

    /// <summary>
    /// Defines the IXamarinFormsService type.
    /// </summary>
    public interface IXamarinFormsService
    {
        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <returns>A Page.</returns>
        Page GetPage(MvxViewModelRequest request);
    }
}
