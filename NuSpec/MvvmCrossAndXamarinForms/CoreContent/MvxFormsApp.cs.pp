// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the App type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using Cirrious.CrossCore;

using Xamarin.Forms;

namespace $rootnamespace$
{
    /// <summary>
    /// Defines the App type.
    /// </summary>
    public class MvxFormsApp : Application
    {
        public event EventHandler Start;
        public event EventHandler Sleep;
        public event EventHandler Resume;

        /// <summary>
        /// Initializes a new instance of the <see cref="MvxFormsApp"/> class.
        /// </summary>
        public MvxFormsApp()
        {
            Mvx.TaggedTrace("MvxFormsApp", "Constructor");
        }

		/// <summary>
        /// Called when [start].
        /// </summary>
		protected override void OnStart()
		{
            Mvx.TaggedTrace("MvxFormsApp", "OnStart");

            var handler = Start;
            if (handler != null)
                handler(this, EventArgs.Empty);
		}

		/// <summary>
        /// Called when [sleep].
        /// </summary>
		protected override void OnSleep()
		{
            Mvx.TaggedTrace("MvxFormsApp", "OnSleep");

            var handler = Sleep;
            if (handler != null)
                handler(this, EventArgs.Empty);
		}

		/// <summary>
        /// Called when [resume].
        /// </summary>
		protected override void OnResume()
		{
            Mvx.TaggedTrace("MvxFormsApp", "OnResume");

            var handler = Resume;
            if (handler != null)
                handler(this, EventArgs.Empty);
		}
    }
}
