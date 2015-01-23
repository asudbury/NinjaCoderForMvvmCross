// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PageService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Services
{
    using System;
    using System.Linq;
    using System.Reflection;

    using Xamarin.Forms;

    /// <summary>
    ///  Defines the PageService type.
    /// </summary>
    public class PageService : IPageService
    {
        /// <summary>
        /// Gets the page.
        /// </summary>
        /// <param name="viewModelType">Type of the view model.</param>
        /// <returns>
        /// A Page.
        /// </returns>
        public Page GetPage(Type viewModelType)
        {
            string viewName = viewModelType.Name.Replace("ViewModel", "View");

            Assembly assembly = typeof(PageService).GetTypeInfo().Assembly;

            TypeInfo typeInfo = assembly.DefinedTypes.FirstOrDefault(x => x.Name == viewName);

            if (typeInfo != null)
            {
                Type type = typeInfo.AsType();

                if (type != null)
                {
                    return Activator.CreateInstance(type) as Page;
                }
            }

            return null;
        }
    }
}
