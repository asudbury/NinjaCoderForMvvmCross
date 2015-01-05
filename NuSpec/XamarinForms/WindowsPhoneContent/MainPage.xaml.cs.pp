// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MainPage.xaml type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using Xamarin.Forms;

    /// <summary>
    ///    Defines the MainPage.xaml type.
    /// </summary>
    public partial class MainPage : FormsApplicationPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            Forms.Init();

			this.LoadApplication(new $rootnamespace$.App());
        }
    }
}