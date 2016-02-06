// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MainPage.xaml type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace $rootnamespace$
{
    using Forms;

    /// <summary>
    ///    Defines the MainPage.xaml type.
    /// </summary>
    public partial class MainPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainPage"/> class.
        /// </summary>
        public MainPage()
        {
            this.InitializeComponent();

            Forms.Init();

            this.Content = FormsHelper.GetMainPage().ConvertPageToUIElement(this);
        }
    }
}