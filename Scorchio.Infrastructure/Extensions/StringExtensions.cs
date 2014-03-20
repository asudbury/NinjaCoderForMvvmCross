// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the StringExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Captialises the first character.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The string.</returns>
        public static string CaptialiseFirstCharacter(this string instance)
        {
            if (instance.Length > 0)
            {
                instance = char.ToUpper(instance[0]) + instance.Substring(1);
            }

            return instance;
        }

        /// <summary>
        /// Lowers the case first character.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The string.</returns>
        public static string LowerCaseFirstCharacter(this string instance)
        {
            if (instance.Length > 0)
            {
                instance = instance.Substring(0, 1).ToLower() + instance.Substring(1);
            }

            return instance;
        }
    }
}
