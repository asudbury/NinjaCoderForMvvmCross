// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvxFormsIosViewPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$.Presenters
{
    using MvvmCross.Core.ViewModels;
    using MvvmCross.iOS.Views.Presenters;
    using MvvmCross.Platform;
    using Core.Services;
    using Forms;
    using Forms.Services;
    using System;
    using System.Threading.Tasks;
    using UIKit;
    using Xamarin.Forms;

    /// <summary>
    /// Defines the MvxFormsIosViewPresenter type.
    /// </summary>
    public class MvxFormsIosViewPresenter
        : IMvxIosViewPresenter
    {
        /// <summary>
        /// The xamarin forms application.
        /// </summary>
        public readonly XamarinFormsApp XamarinFormsApp;

        /// <summary>
        /// The window.
        /// </summary>
        private readonly UIWindow window;

        /// <summary>
        /// The view service.
        /// </summary>
        private readonly IViewService viewService;

        /// <summary>
        /// The view model service.
        /// </summary>
        private readonly IViewModelService viewModelService;
                
        /// <summary>
        /// the forms ios view presenter.
        /// </summary>
        /// <param name="xamarinFormsApp">The xamarin forms application.</param>
        /// <param name="window">The window.</param>
        /// <param name="viewService">The view service.</param>
        /// <param name="viewModelService">The view model service.</param>
        public MvxFormsIosViewPresenter(
            XamarinFormsApp xamarinFormsApp, 
            UIWindow window, 
            IViewService viewService = null, 
            IViewModelService viewModelService = null)
        {
            this.XamarinFormsApp = xamarinFormsApp;
            this.window = window;
            this.viewService = viewService ?? new ViewService();
            this.viewModelService = viewModelService ?? new ViewModelService();
        }

        /// <summary>
        /// Presents the modal view controller.
        /// </summary>
        /// <param name="controller">The controller.</param>
        /// <param name="animated">if set to <c>true</c> [animated].</param>
        /// <returns></returns>
        public virtual bool PresentModalViewController(UIViewController controller, bool animated)
        {
            return false;
        }

        /// <summary>
        /// Natives the modal view controller disappeared on its own.
        /// </summary>
        public virtual void NativeModalViewControllerDisappearedOnItsOwn()
        {
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
                Mvx.TaggedTrace("MvxFormsViewPresenter:ChangePresentation()", "Don't know what to do");
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
        /// <param name="action">The action.</param>
        public void AddPresentationHintHandler<THint>(Func<THint, bool> action) where THint : MvxPresentationHint
        {
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

        /// <summary>
        /// Tries the show page.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private async Task<bool> TryShowPage(MvxViewModelRequest request)
        {
            // Get the ViewModel from the request
            IMvxViewModel viewModel = this.viewModelService.GetViewModel(request);

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
            var mainPage = this.XamarinFormsApp.MainPage as NavigationPage;

            // Set the MainPage if not yet defined (first load)
            if (mainPage == null)
            {
                this.XamarinFormsApp.MainPage = new NavigationPage(page);
                mainPage = (NavigationPage)this.XamarinFormsApp.MainPage;
                this.window.RootViewController = mainPage.CreateViewController();
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
