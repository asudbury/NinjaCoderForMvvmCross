﻿// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Setup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using MvvmCross.Core.ViewModels;
    using MvvmCross.iOS.Platform;
    using MvvmCross.iOS.Views;
    using UIKit;
    using Views;

    /// <summary>
    ///    Defines the Setup type.
    /// </summary>
    public class Setup : MvxIosSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Setup"/> class.
        /// </summary>
        /// <param name="applicationDelegate">The application delegate.</param>
        /// <param name="window">The window.</param>
        public Setup(MvxApplicationDelegate applicationDelegate, UIWindow window)
            : base(applicationDelegate, window)
        {
        }

        /// <summary>
        /// Creates the app.
        /// </summary>
        /// <returns>An instance of IMvxApplication</returns>
        protected override IMvxApplication CreateApp()
        {
            return new Core.App();
        }

        /// <summary>
        /// Creates the storyboard views container.
        /// </summary>
        /// <returns></returns>
        protected override IMvxIosViewsContainer CreateIosViewsContainer()
        {
            return new StoryBoardViewContainer();
        }
    }
}