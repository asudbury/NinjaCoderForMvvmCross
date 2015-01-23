// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvxFormsAndroidViewPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Presenters
{
    using Cirrious.MvvmCross.Droid.Views;
    using Cirrious.MvvmCross.ViewModels;
    using Xamarin.Forms;

	using CoreProject.Services;
    using FormsProject.Services;

    /// <summary>
    /// Defines the MvxFormsAndroidViewPresenter type.
    /// </summary>
    public class MvxFormsAndroidViewPresenter : MvxAndroidViewPresenter, IMvxFormsAndroidNavigationHost
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
        /// Initializes a new instance of the <see cref="MvxFormsAndroidViewPresenter" /> class.
        /// </summary>
        /// <param name="viewModelService">The view model service.</param>
        /// <param name="pageService">The page service.</param>
        public MvxFormsAndroidViewPresenter(
            IViewModelService viewModelService,
            IPageService pageService)
        {
            this.viewModelService = viewModelService;
            this.pageService = pageService;
        }

        /// <summary>
        /// Gets or sets the navigation provider.
        /// </summary>
        public IMvxFormsAndroidNavigationProvider NavigationProvider { get; set; }

        /// <summary>
        /// Shows the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        public override void Show(MvxViewModelRequest request)
        {
            if (this.TryShowPage(request))
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
        private bool TryShowPage(MvxViewModelRequest request)
        {
            if (this.NavigationProvider == null)
            {
                return false;
            }

			Page page = this.pageService.GetPage(request.ViewModelType);

            if (page == null)
            {
                return false;
            }

			IMvxViewModel viewModel = this.viewModelService.GetViewModel(request);

             page.BindingContext = viewModel;

            this.NavigationProvider.Push(page);

            return true;
        }

        /// <summary>
        /// Closes the specified view model.
        /// </summary>
        /// <param name="viewModel">The view model.</param>
        public override void Close(IMvxViewModel viewModel)
        {
            if (this.NavigationProvider == null)
            {
                return;
            }

            this.NavigationProvider.Pop();
        }
    }
}