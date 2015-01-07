// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MainActivity type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using Android.App;
    using Android.Content.PM;
    using Android.OS;
    using Xamarin.Forms.Platform.Android;

	using $rootnamespace$.Forms;
		
    /// <summary>
    /// Defines the MainActivity type.
    /// </summary>
    [Activity(Label = "$safeprojectname$", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {
        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Xamarin.Forms.Forms.Init(this, bundle);

			this.LoadApplication(new App());
        }
    }
}
