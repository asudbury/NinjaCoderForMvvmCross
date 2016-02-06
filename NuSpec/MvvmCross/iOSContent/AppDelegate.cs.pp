// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the AppDelegate type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using Foundation;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.iOS.Platform;
    using MvvmCross.Platform;
    using UIKit;

    /// <summary>
    /// The UIApplicationDelegate for the application. This class is responsible for launching the 
    /// User Interface of the application, as well as listening (and optionally responding) to 
    /// application events from iOS.
    /// </summary>
    [Register("AppDelegate")]
    public partial class AppDelegate : MvxApplicationDelegate
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
            this.window = new UIWindow(UIScreen.MainScreen.Bounds);

            Setup setup = new Setup(this, this.window);
            setup.Initialize();

            IMvxAppStart startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            this.window.MakeKeyAndVisible();

            return true;
        }
    }
}