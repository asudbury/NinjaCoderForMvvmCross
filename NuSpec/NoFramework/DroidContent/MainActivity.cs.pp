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

    /// <summary>
    /// Defines the MainActivity type.
    /// </summary>
    [Activity(Label = "$safeprojectname$", MainLauncher = true, ConfigurationChanges = ConfigChanges.Orientation)]
    public class MainActivity : Activity
    {
        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            //// Set our view from the "main" layout resource
            //// SetContentView(Resource.Layout.Main);
        }
    }
}
