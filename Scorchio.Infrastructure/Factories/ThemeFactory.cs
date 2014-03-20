// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ThemeFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Factories
{
    using System.Collections.Generic;
    using System.Windows.Media;

    using Scorchio.Infrastructure.Entities;

    /// <summary>
    ///  Defines the ThemeFactory type.
    /// </summary>
    public class ThemeFactory : IThemeFactory
    {
        /// <summary>
        /// Gets the themes.
        /// </summary>
        public IEnumerable<AccentColor> Themes
        {
            get
            {
                return new List<AccentColor>
                {
                    new AccentColor
                        {
                            Name = "Light",
                            ColorBrush = new SolidColorBrush(Colors.White)
                        },
                    new AccentColor
                        {
                            Name = "Dark",
                            ColorBrush = new SolidColorBrush(Colors.Black)
                        }
                };
            }
        }
    }
}
