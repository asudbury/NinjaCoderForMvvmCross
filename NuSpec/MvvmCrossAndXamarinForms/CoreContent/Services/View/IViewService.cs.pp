using Xamarin.Forms;

namespace $rootnamespace$.Services.View
{
    public interface IViewService
    {
        /// <summary>
        /// Get the View suffix (ex View or Page)
        /// </summary>
        string ViewSuffix { get; }

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <param name="viewModelName">The name of the ViewModel</param>
        /// <param name="viewName">The name of the corresponding View</param>
        /// <returns>
        /// A Page and its corresponding ViewName
        /// </returns>
        Page GetPage(string viewModelName, out string viewName);
    }
}
