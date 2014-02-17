// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the AccentTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Translators
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Media;

    using MahApps.Metro;

    using Scorchio.Infrastructure.Entities;

    /// <summary>
    ///  Defines the AccentTranslator type.
    /// </summary>
    public class AccentTranslator : ITranslator<IList<Accent>, IEnumerable<AccentColor>>
    {
        /// <summary>
        /// Translates the object.
        /// </summary>
        /// <param name="from">The object to translate from.</param>
        /// <returns>The translated object.</returns>
        public IEnumerable<AccentColor> Translate(IList<Accent> @from)
        {
            return from.ToList()
                        .Select(x => new AccentColor
                                {
                                    Name = x.Name, 
                                    ColorBrush = x.Resources["AccentColorBrush"] as Brush
                                })
                        .ToList();
        }
    }
}
