// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Setup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using Android.Content;
    using Core;
    using Forms;
    using MvvmCross.Core.ViewModels;
    using MvvmCross.Core.Views;
    using MvvmCross.Droid.Platform;
    using MvvmCross.Droid.Views;
    using MvvmCross.Platform;
    using Presenters;

    /// <summary>
    /// Defines the Setup type.
    /// </summary>
    public class Setup : MvxAndroidSetup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Setup"/> class.
        /// </summary>
        /// <param name="applicationContext">The application context.</param>
        public Setup(Context applicationContext)
            : base(applicationContext)
        {
        }

        /// <summary>
        /// Creates the application.
        /// </summary>
        /// <returns></returns>
        protected override IMvxApplication CreateApp()
        {
            return new App();
        }

        /// <summary>
        /// Creates the view presenter.
        /// </summary>
        /// <returns></returns>
        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            var presenter = new MvxFormsAndroidViewPresenter(new XamarinFormsApp());
            Mvx.RegisterSingleton<IMvxViewPresenter>(presenter);

            return presenter;
        }
    }
}