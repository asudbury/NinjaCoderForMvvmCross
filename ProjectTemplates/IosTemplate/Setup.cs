// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Setup type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace IosTemplate
{
    using Cirrious.CrossCore.Platform;
    using Cirrious.MvvmCross.Touch.Platform;
    using Cirrious.MvvmCross.Touch.Views.Presenters;

    /// <summary>
    ///    Defines the Setup type.
    /// </summary>
    public class Setup : MvxTouchSetup
	{
        /// <summary>
        /// Initializes a new instance of the <see cref="Setup"/> class.
        /// </summary>
        /// <param name="applicationDelegate">The application delegate.</param>
        /// <param name="presenter">The presenter.</param>
		public Setup(MvxApplicationDelegate applicationDelegate, IMvxTouchViewPresenter presenter)
			: base(applicationDelegate, presenter)
		{
		}

        /// <summary>
        /// Creates the debug trace.
        /// </summary>
        /// <returns></returns>
        protected override IMvxTrace CreateDebugTrace()
        {
            return null;
        }

        /// <summary>
        /// Creates the app.
        /// </summary>
        /// <returns></returns>
		protected override Cirrious.MvvmCross.ViewModels.IMvxApplication CreateApp ()
		{
			return new Core.App();
		}
	}
}