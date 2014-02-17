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
    using NinjaCoder.MvvmCross.Infrastructure.Services;

    using Scorchio.Infrastructure.Translators;

    /// <summary>
    ///  Defines the PluginTranslator type.
    /// </summary>
    internal class PluginTranslator : ITranslator<FileInfoBase, Plugin>
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginTranslator" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public PluginTranslator(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

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
                    IsCommunityPlugin = this.IsCommunityPlugin(from),
                    IsUserPlugin = this.IsUserPlugin(from)
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
        /// <param name="fileInfoBase">The file info base.</param>
        /// <returns>True or false.</returns>
        internal bool IsCommunityPlugin(FileInfoBase fileInfoBase)
        {
            if (this.IsUserPlugin(fileInfoBase) == false)
            {
                return fileInfoBase.Name.StartsWith(Settings.CorePluginsAssemblyPrefix) == false;
            }

            return false;
        }

        /// <summary>
        /// Determines whether [is user plugin] [the specified name].
        /// </summary>
        /// <param name="fileInfoBase">The file info base.</param>
        /// <returns>True or false.</returns>
        internal bool IsUserPlugin(FileInfoBase fileInfoBase)
        {
            if (this.settingsService.UserPluginsPath != string.Empty)
            {
                return fileInfoBase.FullName.Contains(this.settingsService.UserPluginsPath);
            }

            return false;
        }
    }
}
