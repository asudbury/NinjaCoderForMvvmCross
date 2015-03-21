// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the XamarinFormsApp type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace $rootnamespace$
{
    /// <summary>
    /// Defines the XamarinFormsApp type.
    /// </summary>
    public class XamarinFormsApp : Application
    {
        public event EventHandler Start;
        public event EventHandler Sleep;
        public event EventHandler Resume;

        /// <summary>
        /// Initializes a new instance of the <see cref="XamarinFormsApp"/> class.
        /// </summary>
        public XamarinFormsApp()
        {
            Debug.WriteLine("MvxFormsApp::Constructor");
        }

		/// <summary>
        /// Called when [start].
        /// </summary>
		protected override void OnStart()
		{
            Debug.WriteLine("MvxFormsApp::OnStart");

            var handler = Start;
            if (handler != null)
                handler(this, EventArgs.Empty);
		}

		/// <summary>
        /// Called when [sleep].
        /// </summary>
		protected override void OnSleep()
		{
            Debug.WriteLine("MvxFormsApp::OnSleep");

            var handler = Sleep;
            if (handler != null)
                handler(this, EventArgs.Empty);
		}

		/// <summary>
        /// Called when [resume].
        /// </summary>
		protected override void OnResume()
		{
            Debug.WriteLine("MvxFormsApp::OnResume");

            var handler = Resume;
            if (handler != null)
                handler(this, EventArgs.Empty);
		}
    }
}
