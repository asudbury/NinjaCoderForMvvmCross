// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the AppDelegate type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using Foundation;
    using UIKit;

    using Xamarin.Forms;

	using $rootnamespace$.Forms;
		
	using Xamarin.Forms.Platform.iOS;

    /// <summary>
    /// The UIApplicationDelegate for the application. This class is responsible for launching the 
    /// User Interface of the application, as well as listening (and optionally responding) to 
    /// application events from iOS.
    /// </summary>
    [Register("AppDelegate")]
    public class AppDelegate : FormsApplicationDelegate
    {
        /// <summary>
        /// Finished the launching.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <param name="options">The options.</param>
        /// <returns>True or false.</returns>
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            Forms.Init();

			//// Added to prevent iOS linker from removing core assembly out of deployed package.
            Core.Infrastructure.Init();

			this.LoadApplication(new App());

            return base.FinishedLaunching(app, options);
        }
    }
}