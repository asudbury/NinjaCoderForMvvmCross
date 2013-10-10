// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using System.IO;
    using System.IO.Abstractions;

    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Entities;

    /// <summary>
    ///  Defines the PluginTranslator type.
    /// </summary>
    public class PluginTranslator : ITranslator<FileInfoBase, Plugin>
    {
        /// <summary>
        /// Translates the object.
        /// </summary>
        /// <param name="from">The object to translate from.</param>
        /// <returns>The translated object.</returns>
        public Plugin Translate(FileInfoBase @from)
        {
            string name = Path.GetFileNameWithoutExtension(from.Name);

            if (string.IsNullOrEmpty(name) == false)
            {
                return new Plugin
                {
                    FriendlyName = name.Replace(Settings.PluginsAssemblyPrefix, string.Empty),
                    FileName = from.Name,
                    Source = from.FullName
                };
            }

            return null;
        }
    }
}
