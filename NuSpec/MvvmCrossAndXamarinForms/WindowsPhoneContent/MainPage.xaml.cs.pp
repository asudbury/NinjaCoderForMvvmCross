namespace $rootnamespace$
{
	using Cirrious.CrossCore;
	using Cirrious.MvvmCross.Views;
	using $rootnamespace$.Presenters;
	using Xamarin.Forms.Platform.WinPhone;

    public partial class MainPage : FormsApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();

            var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsWindowsPhoneViewPresenter;

            if (presenter == null) return;

            LoadApplication(presenter.XamarinFormsApp);
        }
    }
}