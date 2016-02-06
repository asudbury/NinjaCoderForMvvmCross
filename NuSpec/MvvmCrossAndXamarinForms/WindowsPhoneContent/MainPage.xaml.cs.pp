// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MainPagel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using MvvmCross.Core.Views;
    using MvvmCross.Platform;
    using Presenters;
    using Xamarin.Forms.Platform.WinPhone;

    /// <summary>
    ///  Defines the MainPagel type.
    /// </summary>
    public partial class MainPage : FormsApplicationPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            MvxFormsWindowsPhoneViewPresenter presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsWindowsPhoneViewPresenter;

            if (presenter == null) 
            {
                return;
            }

            this.LoadApplication(presenter.XamarinFormsApp);
        }
    }
}