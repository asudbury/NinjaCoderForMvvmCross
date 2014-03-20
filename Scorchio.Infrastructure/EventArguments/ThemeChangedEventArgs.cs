// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ThemeChangeEventArgs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.EventArguments
{
    using System;

    using MahApps.Metro;

    /// <summary>
    ///  Defines the ThemeChangedEventArgs type.
    /// </summary>
    public class ThemeChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the theme.
        /// </summary>
        public Theme Theme { get; set; }

        /// <summary>
        /// Gets or sets the accent.
        /// </summary>
        public Accent Accent { get; set; }
    }
}
