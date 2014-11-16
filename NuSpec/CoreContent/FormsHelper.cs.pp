// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the FormsHelper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using Xamarin.Forms;
    using $rootnamespace$.Views;

    /// <summary>
    /// Defines the FormsHelper type.
    /// </summary>
    public static class FormsHelper
    {
        /// <summary>
        /// Gets the main page.
        /// </summary>
        /// <returns></returns>
        public static Page GetMainPage()
        {
            return new MainView();
        }
    }
}
