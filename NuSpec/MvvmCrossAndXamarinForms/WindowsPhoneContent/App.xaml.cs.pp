// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the App.xaml type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using Microsoft.Phone.Controls;
    using Microsoft.Phone.Shell;
    using MvvmCross.Platform;
    using System.Windows;
    using System.Windows.Navigation;

    /// <summary>
    ///    Defines the App.xaml type.
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// The phone application initialized
        /// </summary>
        private bool phoneApplicationInitialized;

        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
            // Global handler for uncaught exceptions. 
            this.UnhandledException += this.Application_UnhandledException;

            // Standard Silverlight initialization
            this.InitializeComponent();

            // Phone-specific initialization
            this.InitializePhoneApplication();

            // Show graphics profiling information while debugging.
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // Display the current frame rate counters.
                Application.Current.Host.Settings.EnableFrameRateCounter = true;

                // Show the areas of the app that are being redrawn in each frame.
                // Application.Current.Host.Settings.EnableRedrawRegions = true;

                // Enable non-production analysis visualization mode, 
                // which shows areas of a page that are handed off to GPU with a colored overlay.
                // Application.Current.Host.Settings.EnableCacheVisualization = true;

                // Disable the application idle detection by setting the UserIdleDetectionMode property of the
                // application's PhoneApplicationService object to Disabled.
                // Caution:- Use this under debug mode only. Application that disables user idle detection will continue to run
                // and consume battery power when the user is not using the phone.
                PhoneApplicationService.Current.UserIdleDetectionMode = IdleDetectionMode.Disabled;
            }

            Setup setup = new Setup(this.RootFrame);
            setup.Initialize();
        }

        /// <summary>
        /// Gets the root frame.
        /// </summary>
        public PhoneApplicationFrame RootFrame { get; private set; }

        /// <summary>
        /// Handles the Launching event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="LaunchingEventArgs"/> instance containing the event data.</param>
        private void Application_Launching(object sender, LaunchingEventArgs e)
        {
            this.RootFrame.Navigating += this.RootFrameOnNavigating;
        }

        /// <summary>
        /// Roots the frame on navigating.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The <see cref="NavigatingCancelEventArgs"/> instance containing the event data.</param>
        private void RootFrameOnNavigating(object sender, NavigatingCancelEventArgs args)
        {
            args.Cancel = true;
            this.RootFrame.Navigating -= this.RootFrameOnNavigating;
            this.RootFrame.Dispatcher.BeginInvoke(() => { Mvx.Resolve<MvvmCross.Core.ViewModels.IMvxAppStart>().Start(); });
        }

        /// <summary>
        /// Handles the NavigationFailed event of the RootFrame control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="NavigationFailedEventArgs"/> instance containing the event data.</param>
        private void RootFrame_NavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // A navigation has failed; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        /// <summary>
        /// Handles the Activated event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ActivatedEventArgs"/> instance containing the event data.</param>
        private void Application_Activated(object sender, ActivatedEventArgs e)
        {
        }

        /// <summary>
        /// Handles the Deactivated event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="DeactivatedEventArgs"/> instance containing the event data.</param>
        private void Application_Deactivated(object sender, DeactivatedEventArgs e)
        {
        }

        // Code to execute when the application is closing (eg, user hit Back)
        // This code will not execute when the application is deactivated
        private void Application_Closing(object sender, ClosingEventArgs e)
        {
        }

        /// <summary>
        /// Handles the UnhandledException event of the Application control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ApplicationUnhandledExceptionEventArgs"/> instance containing the event data.</param>
        private void Application_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        /// <summary>
        /// Initializes the phone application.
        /// </summary>
        private void InitializePhoneApplication()
        {
            if (this.phoneApplicationInitialized)
            {
                return;
            }

            // Create the frame but don't set it as RootVisual yet; this allows the splash
            // screen to remain active until the application is ready to render.
            this.RootFrame = new PhoneApplicationFrame();
            this.RootFrame.Navigated += this.CompleteInitializePhoneApplication;

            // Handle navigation failures
            this.RootFrame.NavigationFailed += this.RootFrame_NavigationFailed;

            // Handle reset requests for clearing the backstack
            this.RootFrame.Navigated += this.CheckForResetNavigation;

            // Ensure we don't initialize again
            this.phoneApplicationInitialized = true;
        }

        /// <summary>
        /// Completes the initialize phone application.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NavigationEventArgs"/> instance containing the event data.</param>
        private void CompleteInitializePhoneApplication(object sender, NavigationEventArgs e)
        {
            // Set the root visual to allow the application to render
            if (this.RootVisual != this.RootFrame)
            {
                this.RootVisual = this.RootFrame;
            }

            // Remove this handler since it is no longer needed
            this.RootFrame.Navigated -= this.CompleteInitializePhoneApplication;
        }

        /// <summary>
        /// Checks for reset navigation.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NavigationEventArgs"/> instance containing the event data.</param>
        private void CheckForResetNavigation(object sender, NavigationEventArgs e)
        {
            // If the app has received a 'reset' navigation, then we need to check
            // on the next navigation to see if the page stack should be reset
            if (e.NavigationMode == NavigationMode.Reset)
            {
                this.RootFrame.Navigated += this.ClearBackStackAfterReset;
            }
        }

        /// <summary>
        /// Clears the back stack after reset.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="NavigationEventArgs"/> instance containing the event data.</param>
        private void ClearBackStackAfterReset(object sender, NavigationEventArgs e)
        {
            // Unregister the event so it doesn't get called again
            this.RootFrame.Navigated -= this.ClearBackStackAfterReset;

            // Only clear the stack for 'new' (forward) and 'refresh' navigations
            if (e.NavigationMode != NavigationMode.New && e.NavigationMode != NavigationMode.Refresh)
                return;

            // For UI consistency, clear the entire page stack
            while (this.RootFrame.RemoveBackEntry() != null)
            {
                // do nothing
            }
        }
    }
}