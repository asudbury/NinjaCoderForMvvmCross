// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CodeConfigTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using System.IO;
    using System.Xml;
    using System.Xml.Serialization;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the CodeConfigTranslator type.
    /// </summary>
    internal class CodeConfigTranslator : ITranslator<string, CodeConfig>
    {
        /// <summary>
        /// Translates the object.
        /// </summary>
        /// <param name="from">The object to translate from.</param>
        /// <returns>The translated object.</returns>
        public CodeConfig Translate(string @from)
        {
            if (File.Exists(from))
            {
                FileStream fileStream = new FileStream(from, FileMode.Open, FileAccess.Read);
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas());

                XmlSerializer serializer = new XmlSerializer(typeof(CodeConfig));
                return (CodeConfig)serializer.Deserialize(reader);
            }

            return null;
        }
    }
}
