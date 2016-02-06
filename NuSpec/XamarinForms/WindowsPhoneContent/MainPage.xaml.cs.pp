// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MainPage.xaml type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using Forms;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.WinPhone;

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

			this.LoadApplication(new XamarinFormsApp());
        }
    }
}