// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvxFormsWindowsPhoneViewPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Presenters
{
    using Cirrious.MvvmCross.ViewModels;
    using Cirrious.MvvmCross.WindowsPhone.Views;

    using Microsoft.Phone.Controls;

    using Xamarin.Forms;

    using System.Threading.Tasks;

    using CoreProject.Services;
    using FormsProject.Services;

    /// <summary>
    /// Defines the MvxFormsWindowsPhoneViewPresenter type.
    /// </summary>
    public class MvxFormsWindowsPhoneViewPresenter : MvxPhoneViewPresenter
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
        /// Initializes a new instance of the <see cref="MvxFormsWindowsPhoneViewPresenter"/> class.
        /// </summary>
        /// <param name="viewModelService">The view model service.</param>
        /// <param name="pageService">The page service.</param>
        /// <param name="rootFrame">The root frame.</param>
        public MvxFormsWindowsPhoneViewPresenter(
            IViewModelService viewModelService,
            IPageService pageService,
            PhoneApplicationFrame rootFrame)
            :base(rootFrame)
        {
            this.viewModelService = viewModelService;
            this.pageService = pageService;
        }

        /// <summary>
        /// Shows the specified request.
        /// </summary>
        /// <param name="request">The request.</param>
        public override async void Show(MvxViewModelRequest request)
        {
            if (await this.TryShowPage(request))
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
        private async Task<bool> TryShowPage(MvxViewModelRequest request)
        {
            Page page = this.pageService.GetPage(request.ViewModelType);

            if (page != null)
            {
                IMvxViewModel viewModel = this.viewModelService.GetViewModel(request);

                if (this.navigationPage == null)
                {
                    Forms.Init();

                    this.navigationPage = new NavigationPage(page);
                }
                else
                {
                    await this.navigationPage.PushAsync(page);
                }

                page.BindingContext = viewModel;
                return true;
            }

            return false;
        }
    }
}
