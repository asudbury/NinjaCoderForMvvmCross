// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using System;
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
                    FriendlyName = this.GetFriendlyName(name),
                    FileName = from.Name,
                    Source = from.FullName,
                    IsCommunityPlugin = this.IsCommunityPlugin(from.Name)
                };
            }

            return null;
        }

        /// <summary>
        /// Gets the name of the friendly.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>The friendly name.</returns>
        internal string GetFriendlyName(string name)
        {
            string friendlyName = name;

            int index = name.IndexOf("Plugins", StringComparison.OrdinalIgnoreCase);

            if (index != -1)
            {
                //// uppercase first character.
                friendlyName = name.Substring(index + 8, 1).ToUpper() + name.Substring(index + 9);
            }

            else
            {
                index = name.IndexOf("Plugin", StringComparison.OrdinalIgnoreCase);

                if (index != -1)
                {
                    //// uppercase first character.
                    friendlyName = name.Substring(index + 7, 1).ToUpper() + name.Substring(index + 8);
                }
            }

            return friendlyName;
        }

        /// <summary>
        /// Determines whether [is community plugin] [the specified name].
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns>True or false.</returns>
        internal bool IsCommunityPlugin(string name)
        {
            return name.StartsWith(Settings.CorePluginsAssemblyPrefix) == false;
        }
    }
}
