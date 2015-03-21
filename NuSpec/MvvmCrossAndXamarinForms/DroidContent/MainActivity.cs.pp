using Android.App;
using Android.OS;
using Cirrious.CrossCore;
using Cirrious.MvvmCross.ViewModels;
using Cirrious.MvvmCross.Views;
using $rootnamespace$.Presenters;
using Xamarin.Forms.Platform.Android;

namespace $rootnamespace$
{
    [Activity(Label = "View for anyViewModel")]
    public class MainActivity
        : FormsApplicationActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            var presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsAndroidViewPresenter;
            if (presenter == null) return;

            LoadApplication(presenter.XamarinFormsApp);
            Mvx.Resolve<IMvxAppStart>().Start();
        }
    }
}