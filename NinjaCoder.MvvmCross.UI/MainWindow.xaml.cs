// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MainWindow type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.UI
{
    using Scorchio.Infrastructure.Wpf.Views;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IDialog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }
    }
}
