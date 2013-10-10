// -----------------------------------------------------------------------
// <summary>
//   Defines the ITranslator type.
// </summary>
// -----------------------------------------------------------------------
namespace MvvmCross.WebRequestService.Translators
{
    /// <summary>
    /// Defines the ITranslator type.
    /// </summary>
    /// <typeparam name="TFrom">The type of from.</typeparam>
    /// <typeparam name="TTo">The type of to.</typeparam>
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