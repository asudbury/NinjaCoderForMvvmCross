// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the App type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using System.Windows;
    using Xamarin.Forms;
    using Xamarin.Forms.Platform.Wpf;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
	    /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow" /> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Forms.Init();

            Platform platform = FormsHelper.GetMainPage().GetPlatform(this); 

            contentPresenter.Content = (UIElement)platform;
        }
    }
}


