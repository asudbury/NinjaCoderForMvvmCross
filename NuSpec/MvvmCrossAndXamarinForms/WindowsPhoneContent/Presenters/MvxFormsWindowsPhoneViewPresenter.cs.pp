// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvxFormsWindowsPhoneViewPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Ytttttt.WindowsPhone.Presenters
{
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

    /// <summary>
    /// Defines the MvxFormsWindowsPhoneViewPresenter type.
    /// </summary>
    public class MvxFormsWindowsPhoneViewPresenter
        : IMvxPhoneViewPresenter
    {
        /// <summary>
        /// The xamarin forms application.
        /// </summary>
        public readonly XamarinFormsApp XamarinFormsApp;

        /// <summary>
        /// The root frame.
        /// </summary>
        private readonly PhoneApplicationFrame rootFrame;

        /// <summary>
        /// The view service.
        /// </summary>
        private readonly IViewService viewService;

        /// <summary>
        /// The view model service.
        /// </summary>
        private readonly IViewModelService viewModelService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MvxFormsWindowsPhoneViewPresenter"/> class.
        /// </summary>
        /// <param name="xamarinFormsApp">The xamarin forms application.</param>
        /// <param name="rootFrame">The root frame.</param>
        /// <param name="viewService">The view service.</param>
        /// <param name="viewModelService">The view model service.</param>
        public MvxFormsWindowsPhoneViewPresenter(
            XamarinFormsApp xamarinFormsApp, 
            PhoneApplicationFrame rootFrame, 
            IViewService viewService = null, 
            IViewModelService viewModelService = null)
        {
            this.XamarinFormsApp = xamarinFormsApp;
            this.rootFrame = rootFrame;
            this.viewService = viewService ?? new ViewService();
            this.viewModelService = viewModelService ?? new ViewModelService();
        }

        /// <summary>
        /// Changes the presentation.
        /// </summary>
        /// <param name="hint">The hint.</param>
        public async void ChangePresentation(MvxPresentationHint hint)
        {
            if (!(hint is MvxClosePresentationHint))
            {
                return;
            }

            NavigationPage mainPage = this.XamarinFormsApp.MainPage as NavigationPage;

            if (mainPage == null)
            {
                Mvx.TaggedTrace("MvxFormsViewPresenter:ChangePresentation()", "Shit, son! Don't know what to do");
            }
            else
            {
                //// TODO - perhaps we should do more here... also async void is a boo boo
                await mainPage.PopAsync();
            }
        }

        /// <summary>
        /// Shows the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        public async void Show(MvxViewModelRequest request)
        {
            if (await this.TryShowPage(request))
            {
                return;
            }

            Mvx.Error("Skipping request for {0}", request.ViewModelType.Name);
        }

        private async Task<bool> TryShowPage(MvxViewModelRequest request)
        {
            //// Get the ViewModel from the request
            IMvxViewModel viewModel = this.viewModelService.GetViewModel(request);

            if (viewModel == null)
            {
                Mvx.Error("Failed to load {0}", request.ViewModelType.Name);
                return false;
            }

            //// Get the Page from the ViewModel name
            string viewName;

            Page page = this.viewService.GetPage(request.ViewModelType.Name, out viewName);

            if (page == null)
            {
                Mvx.Error("Failed to create a Page from {0}", viewName);
                return false;
            }

            //// Get the MainPage
            NavigationPage mainPage = this.XamarinFormsApp.MainPage as NavigationPage;

            //// Set the MainPage if not yet defined (first load)
            if (mainPage == null)
            {
                this.XamarinFormsApp.MainPage = new NavigationPage(page);
                this.rootFrame.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
            }
            else
            {
                //// Show the Page
                await mainPage.PushAsync(page);
            }

            //// Set the Page context to the corresponding ViewModel
            page.BindingContext = viewModel;
            return true;
        }
    }
}

