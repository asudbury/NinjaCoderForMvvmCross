// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the ObjectExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Scorchio.Infrastructure.Extensions
{
    using System.IO;
    using System.Xml.Serialization;

    /// <summary>
    /// Defines the ObjectExtensions type.
    /// </summary>
    public static class ObjectExtensions
    {
        /// <summary>
        /// Serializes to string.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns></returns>
        public static string SerializeToString(this object instance)
        {
            if (instance == null)
            {
                return string.Empty;
            }

            using (StringWriter stringwriter = new StringWriter())
            {
                XmlSerializer serializer = new XmlSerializer(instance.GetType());
                serializer.Serialize(stringwriter, instance);
                return stringwriter.ToString();
            }
        }
    }
}
