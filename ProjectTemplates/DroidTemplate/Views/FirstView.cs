// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the FirstView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DroidTemplate.Views
{
    using Android.App;
    using Android.OS;

    using Cirrious.MvvmCross.Droid.Views;

    /// <summary>
    /// Defines the FirstView type.
    /// </summary>
    [Activity(Label = "View for FirstViewModel")]
    public class FirstView : MvxActivity
    {
        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.FirstView);
        }
    }
}