// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SplashScreen type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace $rootnamespace$
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Cirrious.MvvmCross.Droid.Views;

    /// <summary> 
    /// Defines the SplashScreen type.
    /// </summary>
    [Activity(
        Label = "$rootnamespace$"
        , MainLauncher = true
        , Icon = "@drawable/icon"
        , Theme = "@style/Theme.Splash"
        , NoHistory = true
        , ScreenOrientation = ScreenOrientation.Portrait)]
    public class SplashScreen : MvxSplashScreenActivity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SplashScreen"/> class.
        /// </summary>
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }

        /// <summary>
        /// Initializations the complete.
        /// </summary>
        public override void InitializationComplete()
        {
            this.StartActivity(typeof(MainActivity));
        }

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            Xamarin.Forms.Forms.Init(this, bundle);
            // Leverage controls' StyleId attrib. to Xamarin.UITest
            Xamarin.Forms.Forms.ViewInitialized += (sender, e) =>
            {
                if (!string.IsNullOrWhiteSpace(e.View.StyleId))
                {
                    e.NativeView.ContentDescription = e.View.StyleId;
                }
            };

            base.OnCreate(bundle);
        }
    }
}