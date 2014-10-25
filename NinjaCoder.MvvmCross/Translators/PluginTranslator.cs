// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using Entities;

    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.Infrastructure.Translators;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using System.Linq;
    using System.Xml.Linq;

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
                XDocument doc = XDocument.Load(@from.FullName);

                IEnumerable<string> nugetCommands = this.GetNugetCommands(doc);
                IEnumerable<string> platforms = this.GetPlatforms(doc);
                string usingStatement = this.GetUsingStatement(doc);

                return new Plugin
                           {
                               FriendlyName = this.GetFriendlyName(name),
                               FileName = from.Name,
                               Source = from.FullName,
                               IsCommunityPlugin = this.IsCommunityPlugin(from),
                               IsUserPlugin = this.IsUserPlugin(from),
                               UsingStatement = usingStatement,
                               NugetCommands = nugetCommands,
                               Platforms = platforms
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
            return this.IsUserPlugin(fileInfoBase) == false && fileInfoBase.FullName.Contains("Community");
        }

        /// <summary>
        /// Determines whether [is user plugin] [the specified name].
        /// </summary>
        /// <param name="fileInfoBase">The file info base.</param>
        /// <returns>True or false.</returns>
        internal bool IsUserPlugin(FileInfoBase fileInfoBase)
        {
            return this.settingsService.UserPluginsPath != string.Empty && fileInfoBase.FullName.Contains(this.settingsService.UserPluginsPath);
        }

        /// <summary>
        /// Gets the using statement.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <returns></returns>
        internal string GetUsingStatement(XDocument doc)
        {
            XElement element = doc.Root.Element("UsingStatement");

            return element != null ? element.Value : string.Empty;
        }

        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <returns></returns>
        internal IEnumerable<string> GetNugetCommands(XDocument doc)
        {
            IEnumerable<XElement> elements = doc.Root.Elements("NugetPackage");

            return elements.Select(element => element.Value).ToList();
        }

        /// <summary>
        /// Gets the platforms.
        /// </summary>
        /// <param name="doc">The document.</param>
        /// <returns>
        /// The suported platforms.
        /// </returns>
        internal IEnumerable<string> GetPlatforms(XDocument doc)
        {
            XElement platformsElement = doc.Root.Element("Platforms");

            if (platformsElement != null)
            {
                IEnumerable<XElement> elements = platformsElement.Elements("Platform");

                return elements.Select(element => element.Value).ToList();
            }

            return new List<string>();
        }
    }
}
