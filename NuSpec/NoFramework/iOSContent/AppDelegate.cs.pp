// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the AppDelegate type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using Foundation;
    using UIKit;

    /// <summary>
    /// The UIApplicationDelegate for the application. This class is responsible for launching the 
    /// User Interface of the application, as well as listening (and optionally responding) to 
    /// application events from iOS.
    /// </summary>
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        /// <summary>
        /// The window.
        /// </summary>
        private UIWindow window;

        /// <summary>
        /// Finished the launching.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <param name="options">The options.</param>
        /// <returns>True or false.</returns>
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            // create a new window instance based on the screen size
            window = new UIWindow (UIScreen.MainScreen.Bounds);
                       
            // If you have defined a view, add it here:
            // window.AddSubview (navigationController.View);
                       
            // make the window visible
            window.MakeKeyAndVisible();
                       
            return true;
        }
    }
}