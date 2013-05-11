// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SplashScreen type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DroidTemplate
{
    using Android.App;

    using Cirrious.MvvmCross.Droid.Views;

    /// <summary>
    ///    Defines the SplashScreen type.
    /// </summary>
    [Activity(Label = "Your App Name", MainLauncher = true, Icon = "@drawable/icon")]
    public class SplashScreen : MvxSplashScreenActivity
    {
        public SplashScreen()
            : base(Resource.Layout.SplashScreen)
        {
        }
    }
}