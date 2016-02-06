// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvxFormsAndroidViewPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Presenters
{
    using Core.Services;
    using Forms;
    using Forms.Services;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Droid.Views;
    using MvvmCross.Platform;
    using System;
    using System.Threading.Tasks;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the MvxFormsAndroidViewPresenter type.
    /// </summary>
    public class MvxFormsAndroidViewPresenter
        : IMvxAndroidViewPresenter
    {
         /// <summary>
        /// The xamarin forms application.
        /// </summary>
        public readonly XamarinFormsApp XamarinFormsApp;

        /// <summary>
        /// The view service.
        /// </summary>
        private readonly IViewService viewService;

        /// <summary>
        /// The view model service/
        /// </summary>
        private readonly IViewModelService viewModelService;
        
        /// <summary>
        /// Initializes a new instance of the <see cref="MvxFormsAndroidViewPresenter"/> class.
        /// </summary>
        /// <param name="xamarinFormsApp">The xamarin forms application.</param>
        /// <param name="viewService">The view service.</param>
        /// <param name="viewModelService">The view model service.</param>
        public MvxFormsAndroidViewPresenter(
            XamarinFormsApp xamarinFormsApp, 
            IViewService viewService = null, 
            IViewModelService viewModelService = null)
        {
            this.XamarinFormsApp = xamarinFormsApp;
            this.viewService = viewService ?? new ViewService();
            this.viewModelService = viewModelService ?? new ViewModelService();
        }

        /// <summary>
        /// Changes the presentation.
        /// </summary>
        /// <param name="hint">The hint.</param>
        public async void ChangePresentation(MvxPresentationHint hint)
        {
            if (!(hint is MvxClosePresentationHint)) return;

            var mainPage = this.XamarinFormsApp.MainPage as NavigationPage;

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

        /// <summary>
        /// Add Presentation Hint Handler.
        /// </summary>
        /// <param name="hint">The hint.</param>
        public void AddPresentationHintHandler<THint>(Func<THint, bool> action) where THint : MvxPresentationHint
        {
        }

        /// <summary>
        /// Shows the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        public async void Show(MvxViewModelRequest request)
        {
            if (await this.TryShowPage(request)) return;

            Mvx.Error("Skipping request for {0}", request.ViewModelType.Name);
        }

        /// <summary>
        /// Tries the show page.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private async Task<bool> TryShowPage(MvxViewModelRequest request)
        {
            // Get the ViewModel from the request
            var viewModel = this.viewModelService.GetViewModel(request);

            if (viewModel == null)
            {
                Mvx.Error("Failed to load {0}", request.ViewModelType.Name);
                return false;
            }

            // Get the Page from the ViewModel name
            string viewName;

            Page page = this.viewService.GetPage(request.ViewModelType.Name, out viewName);

            if (page == null)
            {
                Mvx.Error("Failed to create a Page from {0}", viewName);
                return false;
            }

            // Get the MainPage
            NavigationPage mainPage = this.XamarinFormsApp.MainPage as NavigationPage;

            // Set the MainPage if not yet defined (first load)
            if (mainPage == null)
            {
                this.XamarinFormsApp.MainPage = new NavigationPage(page);
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