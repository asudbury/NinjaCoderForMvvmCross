// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$.Views
{
    using Cirrious.MvvmCross.Touch.Views;

    using Foundation;
    using UIKit;

    /// <summary>
    /// Defines the BaseView type.
    /// </summary>
    public abstract class BaseView : MvxViewController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseView" /> class.
        /// </summary>
        protected BaseView()
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseView"/> class.
        /// </summary>
        /// <param name="nibName">Name of the nib.</param>
        /// <param name="bundle">The bundle.</param>
        protected BaseView(string nibName, NSBundle bundle)
            : base(nibName, bundle)
        {
            this.Initialize();
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        private void Initialize()
        {
            this.EdgesForExtendedLayout = UIRectEdge.None;
        }
    }
}
