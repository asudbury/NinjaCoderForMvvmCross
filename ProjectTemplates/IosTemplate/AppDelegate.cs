// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the AppDelegate type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IosTemplate
{
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.ViewModels;
    using MonoTouch.Foundation;
    using MonoTouch.UIKit;
    using Cirrious.MvvmCross.Touch.Platform;
    using Cirrious.MvvmCross.Touch.Views.Presenters;

    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public class AppDelegate : MvxApplicationDelegate
    {
        private UIWindow window;

        /// <summary>
        /// Finisheds the launching.
        /// </summary>
        /// <param name="app">The app.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            window = new UIWindow(UIScreen.MainScreen.Bounds);

            MvxTouchViewPresenter presenter = new MvxTouchViewPresenter(this, window);

            Setup setup = new Setup(this, presenter);
            setup.Initialize();

            IMvxAppStart startup = Mvx.Resolve<IMvxAppStart>();
            startup.Start();

            this.window.MakeKeyAndVisible();

            return true;
        }
    }
}