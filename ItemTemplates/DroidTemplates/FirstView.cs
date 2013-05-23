// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the $safeitemname$ type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace DroidTemplates
{
    using Android.App;
    using Android.OS;

    /// <summary>
    /// Defines the $safeitemname$ type.
    /// </summary>
    [Activity(Label = "View for  $safeitemname$ Model")]
    public class  $safeitemname$ : BaseView
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