using Cirrious.MvvmCross.Droid.Views;

using CoreProject;
using CoreProject.Presenters;
using CoreProject.Services.View;

using FormsProject.Services.View;

namespace $rootnamespace$.Presenters
{
    public class MvxFormsAndroidViewPresenter
        : MvxFormsBaseViewPresenter
        , IMvxAndroidViewPresenter
    {
        public MvxFormsAndroidViewPresenter(MvxFormsApp mvxFormsApp, IViewService viewService = null)
            : base(mvxFormsApp, viewService ?? new ViewService())
        {
        }
    }
}