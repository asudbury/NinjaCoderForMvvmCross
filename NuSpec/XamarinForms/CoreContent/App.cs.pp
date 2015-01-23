// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the App type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using System.Diagnostics;
    using Xamarin.Forms;
    using $rootnamespace$.Views;

    /// <summary>
    /// Defines the App type.
    /// </summary>
    public class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// </summary>
        public App()
        {
			Debug.WriteLine("App::Constructor");

            this.MainPage = new MainView();
        }

		/// <summary>
        /// Called when [start].
        /// </summary>
		protected override void OnStart()
		{
			Debug.WriteLine("App::OnStart");
		}

		/// <summary>
        /// Called when [sleep].
        /// </summary>
		protected override void OnSleep()
		{
			Debug.WriteLine("App::OnSleep");
		}

		/// <summary>
        /// Called when [resume].
        /// </summary>
		protected override void OnResume()
		{
			Debug.WriteLine("App::OnResume");
		}
    }
}
