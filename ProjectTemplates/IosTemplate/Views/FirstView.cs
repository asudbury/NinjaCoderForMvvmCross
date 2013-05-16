// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the FirstView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IosTemplate.Views
{
    using System.Drawing;

    using Cirrious.MvvmCross.Binding.BindingContext;

    using Core.ViewModels;

    using MonoTouch.UIKit;
    using MonoTouch.Foundation;

    /// <summary>
    /// Defines the FirstView type.
    /// </summary>
    [Register("FirstView")]
    public class FirstView : BaseView
    {
        /// <summary>
        /// Views the did load.
        /// </summary>
        /// <summary>
        /// Called when the View is first loaded
        /// </summary>
        public override void ViewDidLoad()
        {
            base.ViewDidLoad();

            UILabel uiLabel = new UILabel(new RectangleF(0, 0, 320, 50));
            View.AddSubview(uiLabel);
            UITextField uiTextField = new UITextField(new RectangleF(0, 50, 320, 50));
            View.AddSubview(uiTextField);

            var set = this.CreateBindingSet<FirstView, FirstViewModel>();
            set.Bind(uiLabel).To(vm => vm.MyProperty);
            set.Bind(uiTextField).To(vm => vm.MyProperty);
            set.Apply();
        }
    }
}