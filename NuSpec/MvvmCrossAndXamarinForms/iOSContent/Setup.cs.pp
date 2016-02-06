// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Setup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using Core;
    using Forms;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Core.Views;
    using MvvmCross.iOS.Platform;
    using MvvmCross.iOS.Views.Presenters;
    using MvvmCross.Platform;
    using Presenters;
    using UIKit;

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
            return new App();
        }

        /// <summary>
        /// Creates the presenter.
        /// </summary>
        /// <returns></returns>
        protected override IMvxIosViewPresenter CreatePresenter()
        {
            Xamarin.Forms.Forms.Init();

            MvxFormsIosViewPresenter presenter = new MvxFormsIosViewPresenter(
                new XamarinFormsApp(), 
                this.Window);

            Mvx.RegisterSingleton<IMvxViewPresenter>(presenter);

            return presenter;
        }
    }
}