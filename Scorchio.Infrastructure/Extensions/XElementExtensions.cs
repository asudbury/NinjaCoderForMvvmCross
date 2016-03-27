// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the XElementExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Extensions
{
    using System.Xml.Linq;

    /// <summary>
    ///  Defines the XElementExtensions type.
    /// </summary>
    public static class XElementExtensions
    {
        /// <summary>
        /// Gets the safe attribute string value.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="attributeName">Name of the attribute.</param>
        /// <returns>The value of the attribute.</returns>
        public static string GetSafeAttributeStringValue(
            this XElement instance,
            string attributeName)
        {
            string value = string.Empty;

            XAttribute attribute = instance.Attribute(attributeName);

            if (attribute != null)
            {
                value  = attribute.Value;
            }

            return value;
        }
    }
}