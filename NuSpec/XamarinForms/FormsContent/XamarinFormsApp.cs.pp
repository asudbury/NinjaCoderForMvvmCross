// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the XamarinFormsApp type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using System;
    using System.Diagnostics;
    using Xamarin.Forms;
    using Views;

    /// <summary>
    /// Defines the XamarinFormsApp type.
    /// </summary>
    public class XamarinFormsApp : Application
    {
        /// <summary>
        /// Occurs when [start].
        /// </summary>
        public event EventHandler Start;

        /// <summary>
        /// Occurs when [sleep].
        /// </summary>
        public event EventHandler Sleep;

        /// <summary>
        /// Occurs when [resume].
        /// </summary>
        public event EventHandler Resume;

        /// <summary>
        /// Initializes a new instance of the <see cref="XamarinFormsApp"/> class.
        /// </summary>
        public XamarinFormsApp()
        {
            Debug.WriteLine("XamarinFormsApp::Constructor");
            this.MainPage = new MainView();
        }

        /// <summary>
        /// Called when [start].
        /// </summary>
        protected override void OnStart()
        {
            Debug.WriteLine("XamarinFormsApp::OnStart");

            EventHandler handler = this.Start;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when [sleep].
        /// </summary>
        protected override void OnSleep()
        {
            Debug.WriteLine("XamarinFormsApp::OnSleep");

            EventHandler handler = this.Sleep;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }

        /// <summary>
        /// Called when [resume].
        /// </summary>
        protected override void OnResume()
        {
            Debug.WriteLine("XamarinFormsApp::OnResume");

            EventHandler handler = this.Resume;

            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }
}

