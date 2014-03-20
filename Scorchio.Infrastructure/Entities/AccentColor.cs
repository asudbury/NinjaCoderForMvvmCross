// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the AccentColor type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Entities
{
    using System.Windows.Media;

    /// <summary>
    ///  Defines the AccentColorMenuData type.
    /// </summary>
    public class AccentColor
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the color brush.
        /// </summary>
        public Brush ColorBrush { get; set; }
    }
}
