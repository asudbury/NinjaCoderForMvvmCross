// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Setup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
	using Cirrious.CrossCore;
	using Cirrious.MvvmCross.Touch.Platform;
	using Cirrious.MvvmCross.Touch.Views.Presenters;
	using Cirrious.MvvmCross.ViewModels;
	using Cirrious.MvvmCross.Views;
	using CoreProject;
	using FormsProject;
	using $rootnamespace$.Presenters;
	using UIKit;

    /// <summary>
    ///    Defines the Setup type.
    /// </summary>
    public class Setup : MvxTouchSetup
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
        protected override IMvxTouchViewPresenter CreatePresenter()
        {
            Xamarin.Forms.Forms.Init();

            MvxFormsTouchViewPresenter presenter = new MvxFormsTouchViewPresenter(
                new XamarinFormsApp(), 
                this.Window);

            Mvx.RegisterSingleton<IMvxViewPresenter>(presenter);

            return presenter;
        }
    }
}