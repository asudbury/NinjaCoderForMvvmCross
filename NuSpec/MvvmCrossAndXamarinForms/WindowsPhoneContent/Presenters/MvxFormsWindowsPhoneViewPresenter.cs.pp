// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvxFormsWindowsPhoneViewPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Threading.Tasks;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.WindowsPhone.Views;
using Microsoft.Phone.Controls;
using CoreProject.Services.ViewModel;
using FormsProject;
using FormsProject.Services.View;
using Xamarin.Forms;

namespace $rootnamespace$.Presenters
{
    /// <summary>
    /// Defines the MvxFormsWindowsPhoneViewPresenter type.
    /// </summary>
    public class MvxFormsWindowsPhoneViewPresenter
        : IMvxPhoneViewPresenter
    {
        public readonly XamarinFormsApp XamarinFormsApp;
        private readonly PhoneApplicationFrame _rootFrame;
        private readonly IViewService _viewService;
        private readonly IViewModelService _viewModelService;

        public MvxFormsWindowsPhoneViewPresenter(XamarinFormsApp xamarinFormsApp, PhoneApplicationFrame rootFrame, IViewService viewService = null, IViewModelService viewModelService = null)
        {
            XamarinFormsApp = xamarinFormsApp;
            _rootFrame = rootFrame;
            _viewService = viewService ?? new ViewService();
            _viewModelService = viewModelService ?? new ViewModelService();
        }

        public async void ChangePresentation(MvxPresentationHint hint)
        {
            if (!(hint is MvxClosePresentationHint)) return;

            var mainPage = XamarinFormsApp.MainPage as NavigationPage;

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
            var viewModel = _viewModelService.GetViewModel(request);
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
            var mainPage = XamarinFormsApp.MainPage as NavigationPage;

            // Set the MainPage if not yet defined (first load)
            if (mainPage == null)
            {
                XamarinFormsApp.MainPage = new NavigationPage(page);
                _rootFrame.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
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
    }
}
