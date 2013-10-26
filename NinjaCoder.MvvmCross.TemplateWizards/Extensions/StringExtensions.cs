// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the StringExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards.Extensions
{
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Defines the StringExtensions type.
    /// </summary>
    public static class StringExtensions
    {
        /// <summary>
        /// Lowers the case first character.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>A string with a lower case first character.</returns>
        public static string LowerCaseFirstCharacter(this string instance)
        {
            return instance.First().ToString(CultureInfo.InvariantCulture).ToLowerInvariant() +
                   instance.Substring(1);
        }
    }
}
