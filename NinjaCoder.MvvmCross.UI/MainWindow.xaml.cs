// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MainWindow type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.UI
{
    using MahApps.Metro;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Wpf.Views;
    using System.Windows;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IThemedDialog
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Changes the theme.
        /// </summary>
        /// <param name="theme">The theme.</param>
        /// <param name="themeColor">Color of the theme.</param>
        public void ChangeTheme(
            Theme theme, 
            string themeColor)
        {
            ThemeManagerExtensions.ChangeTheme(this, theme, themeColor);
        }

        /// <summary>
        /// Sets the language dictionary.
        /// </summary>
        /// <param name="resourceDictionary">The resource dictionary.</param>
        public void SetLanguageDictionary(ResourceDictionary resourceDictionary)
        {
        }
    }
}
