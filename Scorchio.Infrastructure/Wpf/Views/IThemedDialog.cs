// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IThemedDialog type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.Views
{
    using System.Windows;

    using MahApps.Metro;

    /// <summary>
    ///  Defines the IThemedDialog type.
    /// </summary>
    public interface IThemedDialog : IDialog
    {
        /// <summary>
        /// Changes the theme.
        /// </summary>
        /// <param name="theme">The theme.</param>
        /// <param name="themeColor">Color of the theme.</param>
        void ChangeTheme(
            Theme theme, 
            string themeColor);

        /// <summary>
        /// Sets the language dictionary.
        /// </summary>
        /// <param name="resourceDictionary">The resource dictionary.</param>
        void SetLanguageDictionary(ResourceDictionary resourceDictionary);
    }
}