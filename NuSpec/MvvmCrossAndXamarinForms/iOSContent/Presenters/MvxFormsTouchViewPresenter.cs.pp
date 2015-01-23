namespace $rootnamespace$.Presenters
{
    using System.Threading.Tasks;

    using Cirrious.MvvmCross.Touch.Views.Presenters;
    using Cirrious.MvvmCross.ViewModels;

	using CoreProject.Services;
    using FormsProject.Services;

    using UIKit;

    using Xamarin.Forms;

    /// <summary>
    /// Defines the MvxFormsTouchViewPresenter type.
    /// </summary>
    public class MvxFormsTouchViewPresenter : MvxTouchViewPresenter
    {
        /// <summary>
        /// The view model service.
        /// </summary>
        private readonly IViewModelService viewModelService;

        /// <summary>
        /// The page service.
        /// </summary>
        private readonly IPageService pageService;

        /// <summary>
        /// The navigation page.
        /// </summary>
        private NavigationPage navigationPage;

        /// <summary>
        /// Initializes a new instance of the <see cref="MvxFormsTouchViewPresenter" /> class.
        /// </summary>
        /// <param name="viewModelService">The view model service.</param>
        /// <param name="pageService">The page service.</param>
        /// <param name="applicationDelegate">The application delegate.</param>
        /// <param name="window">The window.</param>
        public MvxFormsTouchViewPresenter(
            IViewModelService viewModelService,
            IPageService pageService,
            UIApplicationDelegate applicationDelegate,
            UIWindow window)
            : base(applicationDelegate, window)
        {
            this.viewModelService = viewModelService;
            this.pageService = pageService;
        }

        /// <summary>
        /// Shows the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        public async override void Show(MvxViewModelRequest request)
        {
            if (await this.TryShowFormsPage(request))
            {
                return;
            }

            base.Show(request);
        }

        /// <summary>
        /// Tries the show page.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private async Task<bool> TryShowFormsPage(MvxViewModelRequest request)
        {
            Page page = this.pageService.GetPage(request.ViewModelType);

            if (page == null)
            {
                return false;
            }

            IMvxViewModel viewModel = this.viewModelService.GetViewModel(request);

            if (this.navigationPage == null)
            {
                Forms.Init();
                this.navigationPage = new NavigationPage(page);
                this.Window.RootViewController = this.navigationPage.CreateViewController();
            }
            else
            {
                await this.navigationPage.PushAsync(page);
            }

            page.BindingContext = viewModel;

            return true;
        }
    }
}