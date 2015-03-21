using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Xamarin.Forms;

namespace $rootnamespace$.Services.View
{
    public class ViewService : IViewService
    {
        public ViewService(string viewSuffix = "View")
        {
            ViewSuffix = viewSuffix;
        }

        public string ViewSuffix { get; private set; }

        public Page GetPage(string viewModelName, out string viewName)
        {
            // Get the View name
            viewName = viewModelName.Replace("ViewModel", ViewSuffix);

            // Copy the View name for anonymous use
            var viewNameCopy = viewName;

            // Get the View
            var view = typeof(ViewService).GetTypeInfo().Assembly.DefinedTypes.FirstOrDefault(x => x.Name == viewNameCopy);
            if (view == null)
            {
                // View not found
                Debug.WriteLine("View not found for {0}", viewName);
                return null;
            }

            // Create and return a Page from the View
            return Activator.CreateInstance(view.AsType()) as ContentPage;
        }
    }
}
