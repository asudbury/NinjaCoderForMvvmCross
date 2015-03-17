using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;

using $rootnamespace$.Services.View;

using Xamarin.Forms;

namespace $rootnamespace$.Presenters
{
    public abstract class MvxFormsBaseViewPresenter
        : IMvxViewPresenter
    {
        private readonly IViewService _viewService;
        public readonly Application MvxFormsApp;

        protected MvxFormsBaseViewPresenter(Application mvxFormsApp, IViewService viewService)
        {
            MvxFormsApp = mvxFormsApp;
            _viewService = viewService;
        }

        public async void ChangePresentation(MvxPresentationHint hint)
        {
            if (!(hint is MvxClosePresentationHint)) return;

            var mainPage = MvxFormsApp.MainPage as NavigationPage;

            if (mainPage == null)
            {
                Mvx.TaggedTrace("MvxFormsViewPresenter:ChangePresentation()", "Shit, son! Don't know what to do");
            }
            else
            {
                // TODO - perhaps we should do more here... also async void is a boo boo
                await mainPage.PopAsync();
            }
        }

        public async void Show(MvxViewModelRequest request)
        {
            if (await TryShowPage(request)) return;

            Mvx.Error("Skipping request for {0}", request.ViewModelType.Name);
        }

        private async Task<bool> TryShowPage(MvxViewModelRequest request)
        {
            // Get the ViewModel from the request
            var viewModel = Mvx.Resolve<IMvxViewModelLoader>().LoadViewModel(request, null);
            if (viewModel == null)
            {
                Mvx.Error("Failed to load {0}", request.ViewModelType.Name);
                return false;
            }

            // Get the Page from the ViewModel name
            string viewName;
            var page = _viewService.GetPage(request.ViewModelType.Name, out viewName);
            if (page == null)
            {
                Mvx.Error("Failed to create a Page from {0}", viewName);
                return false;
            }

            // Get the MainPage
            var mainPage = MvxFormsApp.MainPage as NavigationPage;

            // Set the MainPage if not yet defined (first load)
            if (mainPage == null)
            {
                MvxFormsApp.MainPage = new NavigationPage(page);
                mainPage = (NavigationPage)MvxFormsApp.MainPage;
                CustomPlatformInitialization(mainPage);
            }
            else
            {
                // Show the Page
                await mainPage.PushAsync(page);
            }

            // Set the Page context to the corresponding ViewModel
            page.BindingContext = viewModel;
            return true;
        }

        /// <summary>
        /// To implement on each platform if needed
        /// </summary>
        /// <param name="mainPage"></param>
        protected virtual void CustomPlatformInitialization(NavigationPage mainPage)
        {
        }
    }
}
