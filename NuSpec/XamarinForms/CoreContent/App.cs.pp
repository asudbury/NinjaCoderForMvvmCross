// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the App type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
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
            this.MainPage = new MainView();
        }
    }
}
