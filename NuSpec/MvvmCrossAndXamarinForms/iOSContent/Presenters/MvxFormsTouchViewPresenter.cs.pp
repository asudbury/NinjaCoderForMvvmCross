using Cirrious.MvvmCross.Touch.Views.Presenters;

using CoreProject;
using CoreProject.Presenters;
using CoreProject.Services.View;

using FormsProject.Services.View;

using UIKit;

using Xamarin.Forms;

namespace $rootnamespace$.Presenters
{
    public class MvxFormsTouchViewPresenter
        : MvxFormsBaseViewPresenter
        , IMvxTouchViewPresenter
    {
        private readonly UIWindow _window;

        public MvxFormsTouchViewPresenter(MvxFormsApp mvxFormsApp, UIWindow window, IViewService viewService = null)
            : base(mvxFormsApp, viewService ?? new ViewService())
        {
            _window = window;
        }

        public virtual bool PresentModalViewController(UIViewController controller, bool animated)
        {
            return false;
        }

        public virtual void NativeModalViewControllerDisappearedOnItsOwn()
        {
        }

        protected override void CustomPlatformInitialization(NavigationPage mainPage)
        {
            _window.RootViewController = mainPage.CreateViewController();
        }
    }
}