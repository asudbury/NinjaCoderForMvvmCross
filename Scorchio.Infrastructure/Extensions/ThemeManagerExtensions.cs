// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ThemeManagerExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Extensions
{
    using System.Linq;
    using System.Windows;

    using MahApps.Metro;

    /// <summary>
    /// Defines the ThemeManagerExtensions type.
    /// </summary>
    public static class ThemeManagerExtensions
    {
        /// <summary>
        /// Changes the theme.
        /// </summary>
        /// <param name="window">The window.</param>
        /// <param name="theme">The theme.</param>
        /// <param name="themeColor">Color of the theme.</param>
        public static void ChangeTheme(
            Window window,
            Theme theme,
            string themeColor)
        {
            Accent accent = ThemeManager.DefaultAccents.First(x => x.Name == themeColor);

            ThemeManager.ChangeTheme(window.Resources, accent, theme);
        }
    }
}
