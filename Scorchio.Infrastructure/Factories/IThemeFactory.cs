// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IThemeFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Factories
{
    using System.Collections.Generic;

    using Scorchio.Infrastructure.Entities;

    /// <summary>
    ///  Defines the IThemeFactory type.
    /// </summary>
    public interface IThemeFactory
    {
        /// <summary>
        /// Gets the themes.
        /// </summary>
        IEnumerable<AccentColor> Themes { get; }
    }
}