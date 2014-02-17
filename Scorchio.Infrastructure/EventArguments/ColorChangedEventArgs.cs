// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ColorChangedEventArgs type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.EventArguments
{
    using System;

    using Scorchio.Infrastructure.Entities;

    /// <summary>
    ///  Defines the ColorChangedEventArgs type.
    /// </summary>
    public class ColorChangedEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the accent.
        /// </summary>
        public AccentColor Color { get; set; }
    }
}
