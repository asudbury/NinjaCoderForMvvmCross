// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Services
{
    using System;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using Xamarin.Forms;

    public class ViewService : IViewService
    {
        /// <summary>
        /// The view suffix.
        /// </summary>
        private readonly string viewSuffix;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewService"/> class.
        /// </summary>
        /// <param name="viewSuffix">The view suffix.</param>
        public ViewService(string viewSuffix = "View")
        {
            this.viewSuffix = viewSuffix;
        }

        /// <summary>
        /// Get the View suffix (ex View or Page)
        /// </summary>
        public string ViewSuffix
        {
            get { return this.viewSuffix; }
        }

        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <param name="viewModelName">The name of the ViewModel</param>
        /// <param name="viewName">The name of the corresponding View</param>
        /// <returns>
        /// A Page and its corresponding ViewName
        /// </returns>
        public Page GetPage(
            string viewModelName, 
            out string viewName)
        {
            //// Get the View name
            viewName = viewModelName.Replace("ViewModel", this.ViewSuffix);

            //// Copy the View name for anonymous use
            string viewNameCopy = viewName;

            //// Get the View
            TypeInfo view = typeof(ViewService).GetTypeInfo().Assembly.DefinedTypes.FirstOrDefault(x => x.Name == viewNameCopy);

            if (view == null)
            {
                //// View not found
                Debug.WriteLine("View not found for {0}", viewName);
                return null;
            }

            //// Create and return a Page from the View
            return Activator.CreateInstance(view.AsType()) as ContentPage;
        }
    }
}

