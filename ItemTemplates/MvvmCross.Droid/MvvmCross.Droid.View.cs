// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the  $safeitemrootname$ type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace MvvmCross.Droid
{
    using Android.App;
    using Android.OS;

    using Cirrious.MvvmCross.Droid.Views;

    /// <summary>
    /// Defines the  $safeitemrootname$ type.
    /// </summary>
    [Activity(Label = "View for  $safeitemrootname$Model")]
    public class FirstView : MvxActivity
    {
        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            this.SetContentView(Resource.Layout.FirstView);
        }
    }
}