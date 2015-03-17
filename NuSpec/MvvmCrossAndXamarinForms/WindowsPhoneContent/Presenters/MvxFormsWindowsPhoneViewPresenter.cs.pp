// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvxFormsWindowsPhoneViewPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

using Cirrious.MvvmCross.WindowsPhone.Views;

using Microsoft.Phone.Controls;

using CoreProject.Presenters;
using CoreProject.Services.View;

using FormsProject.Services.View;

using Xamarin.Forms;

namespace $rootnamespace$.Presenters
{
    /// <summary>
    /// Defines the MvxFormsWindowsPhoneViewPresenter type.
    /// </summary>
    public class MvxFormsWindowsPhoneViewPresenter
        : MvxFormsBaseViewPresenter
        , IMvxPhoneViewPresenter
    {
        private readonly PhoneApplicationFrame _rootFrame;

        public MvxFormsWindowsPhoneViewPresenter(Application mvxFormsApp, PhoneApplicationFrame rootFrame, IViewService viewService = null)
            : base(mvxFormsApp, viewService ?? new ViewService())
        {
            _rootFrame = rootFrame;
        }

        protected override void CustomPlatformInitialization(NavigationPage mainPage)
        {
            _rootFrame.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }
    }
}
