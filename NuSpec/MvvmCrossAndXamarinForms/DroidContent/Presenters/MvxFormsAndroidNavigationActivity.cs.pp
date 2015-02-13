// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvxFormsAndroidNavigationActivity type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Presenters
{
    using Android.App;
    using Android.OS;
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Android;
	using FormsProject;

    /// <summary>
    /// Defines the MvxFormsAndroidNavigationActivity type.
    /// </summary>
    [Activity(Label = "A View")]
    public class MvxFormsAndroidNavigationActivity : FormsApplicationActivity, IMvxFormsAndroidNavigationProvider
    {
        /// <summary>
        /// The navigation page.
        /// </summary>
        private NavigationPage navigationPage;

        /// <summary>
        /// Called when [create].
        /// </summary>
        /// <param name="bundle">The bundle.</param>
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            Forms.Init(this, bundle);

            Mvx.Resolve<IMvxFormsAndroidNavigationHost>().NavigationProvider = this;
            Mvx.Resolve<IMvxAppStart>().Start();
        }

        /// <summary>
        /// Pushes the specified page.
        /// </summary>
        /// <param name="page">The page.</param>
        public async void Push(Page page)
        {
            if (this.navigationPage != null)
            {
                await this.navigationPage.PushAsync(page);
                return;
            }

            this.navigationPage = new NavigationPage(page);
            
            this.LoadApplication(new App());
        }

        /// <summary>
        /// Pops this instance.
        /// </summary>
        public async void Pop()
        {
            if (this.navigationPage == null)
            {
                return;
            }

            await this.navigationPage.PopAsync();
        }
    }
}