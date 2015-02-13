// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Setup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using Android.Content;
    using Cirrious.CrossCore;
    using Cirrious.CrossCore.Platform;
    using Cirrious.MvvmCross.Droid.Platform;
    using Cirrious.MvvmCross.Droid.Views;
    using Cirrious.MvvmCross.ViewModels;
	
    using $rootnamespace$.Presenters;

	using CoreProject.Services;
    using FormsProject.Services;

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
            return new Core.App();
        }

        /// <summary>
        /// Creates the view presenter.
        /// </summary>
        /// <returns></returns>
        protected override IMvxAndroidViewPresenter CreateViewPresenter()
        {
            MvxFormsAndroidViewPresenter presenter = new MvxFormsAndroidViewPresenter(
			    new ViewModelService(), 
                new PageService());

            Mvx.RegisterSingleton<IMvxFormsAndroidNavigationHost>(presenter);

            return presenter;
        }
    }
}