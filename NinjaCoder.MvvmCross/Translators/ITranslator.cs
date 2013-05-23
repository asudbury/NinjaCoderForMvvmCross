// -----------------------------------------------------------------------
// <summary>
//   Defines the ITranslator type.
// </summary>
// -----------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Translators
{
    /// <summary>
    ///   Defines the ITranslator type.
    /// </summary>
    public interface ITranslator<in TFrom, out TTo>
    {
        /// <summary>
        /// Translates the object.
        /// </summary>
        /// <param name="from">The object to translate from.</param>
        /// <returns>The translated object.</returns>
        TTo Translate(TFrom from);
    }
}
