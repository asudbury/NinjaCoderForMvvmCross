// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the LinkerPleaseInclude type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using Android.App;
    using Android.OS;
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.ViewModels;
    using Cirrious.MvvmCross.Views;
    using $rootnamespace$.Presenters;
    using Xamarin.Forms.Platform.Android;

    /// <summary>
    /// Defines the MainActivity type.
    /// </summary>
    [Activity(Label = "View for anyViewModel")]
    public class MainActivity
        : FormsApplicationActivity
    {
        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            MvxFormsAndroidViewPresenter presenter = Mvx.Resolve<IMvxViewPresenter>() as MvxFormsAndroidViewPresenter;

            if (presenter == null)
            {
                return;
            }

            this.LoadApplication(presenter.XamarinFormsApp);

            Mvx.Resolve<IMvxAppStart>().Start();
        }
    }
}