// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Views
{
    using System.Windows;

    using MahApps.Metro;
    using MahApps.Metro.Controls;

    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Wpf.Views;

    /// <summary>
    /// Defines the BaseView type.
    /// </summary>
    public class BaseView : MetroWindow, IThemedDialog
    {
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
            this.Resources.SetLanguageDictionary(resourceDictionary);
        }
    }
}
