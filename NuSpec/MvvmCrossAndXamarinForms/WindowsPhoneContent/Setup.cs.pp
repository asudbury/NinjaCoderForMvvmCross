// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Setup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using Cirrious.MvvmCross.ViewModels;
    using Cirrious.MvvmCross.WindowsPhone.Platform;
    using Cirrious.MvvmCross.WindowsPhone.Views;

    using Microsoft.Phone.Controls;

    using CoreProject.Services;
    using FormsProject.Services;
    using $rootnamespace$.Presenters;

    /// <summary>
    ///    Defines the Setup type.
    /// </summary>
    public class Setup : MvxPhoneSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Setup"/> class.
        /// </summary>
        /// <param name="rootFrame">The root frame.</param>
        public Setup(PhoneApplicationFrame rootFrame)
            : base(rootFrame)
        {
        }

        /// <summary>
        /// Creates the app.
        /// </summary>
        /// <returns>An instance of IMvxApplication.</returns>
        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        /// <summary>
        /// Creates the view presenter.
        /// </summary>
        /// <param name="rootFrame">The root frame.</param>
        /// <returns></returns>
        protected override IMvxPhoneViewPresenter CreateViewPresenter(PhoneApplicationFrame rootFrame)
        {
            return new MvxFormsWindowsPhoneViewPresenter(
                new ViewModelService(),
                new PageService(),
                rootFrame);
        }
    }
}