// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using Entities;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Translators;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    ///  Defines the PluginTranslator type.
    /// </summary>
    internal class PluginTranslator : ITranslator<XElement, Plugin>
    {
        /// <summary>
        /// Translates the object.
        /// </summary>
        /// <param name="from">The object to translate from.</param>
        /// <returns>The translated object.</returns>
        public Plugin Translate(XElement @from)
        {
            IEnumerable<string> nugetCommands = this.GetNugetCommands(from);
            IEnumerable<string> platforms = this.GetPlatforms(from);

            IEnumerable<FrameworkType> frameworks = this.GetFrameworks(from);
                
            string usingStatement = this.GetUsingStatement(from);

            return new Plugin
                        {
                            FriendlyName = this.GetFriendlyName(from),
                            IsCommunityPlugin = this.IsCommunityPlugin(from),
                            UsingStatement = usingStatement,
                            NugetCommands = nugetCommands,
                            Platforms = platforms,
                            Frameworks = frameworks,
                            OverwriteFiles = this.GetOverwriteFiles(from)
                        };
        }

        /// <summary>
        /// Gets the name of the friendly.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The friendly name.
        /// </returns>
        internal string GetFriendlyName(XElement element)
        {
            XElement nameElement = element.Element("Name");

            return nameElement != null ? nameElement.Value : "No Name";
        }

        /// <summary>
        /// Determines whether [is community plugin] [the specified name].
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// True or false.
        /// </returns>
        internal bool IsCommunityPlugin(XElement element)
        {
            XElement communityPluginElement = element.Element("CommunityPlugin");

            return communityPluginElement != null;
        }

        /// <summary>
        /// Gets the using statement.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal string GetUsingStatement(XElement element)
        {
            XElement usingElement = element.Element("UsingStatement");

            return usingElement != null ? usingElement.Value : string.Empty;
        }

        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal IEnumerable<string> GetNugetCommands(XElement element)
        {
            IEnumerable<XElement> elements = element.Elements("NugetPackage");

            return elements.Select(e => e.Value).ToList();
        }

        /// <summary>
        /// Gets the platforms.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The suported platforms.
        /// </returns>
        internal IEnumerable<string> GetPlatforms(XElement element)
        {
            XElement platformsElement = element.Element("Platforms");

            if (platformsElement != null)
            {
                IEnumerable<XElement> elements = platformsElement.Elements("Platform");

                return elements.Select(e => e.Value).ToList();
            }

            return new List<string>();
        }

        /// <summary>
        /// Gets the frameworks.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns>
        /// The supported frameworks
        /// </returns>
        internal IEnumerable<FrameworkType> GetFrameworks(XElement element)
        {
            List<FrameworkType> frameworkTypes = new List<FrameworkType>();

            XElement frameWorksEement = element.Element("Frameworks");

            if (frameWorksEement != null)
            {
                IEnumerable<XElement> elements = frameWorksEement.Elements("Framework");

                foreach (XElement e in elements)
                {
                    FrameworkType frameworkType = FrameworkType.MvvmCross.GetValueFromDescription<FrameworkType>(e.Value);

                    frameworkTypes.Add(frameworkType);    
                }
            }

            return frameworkTypes;
        }

        /// <summary>
        /// Overwrites the files.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal bool GetOverwriteFiles(XElement element)
        {
            bool overWrite = false;

            XElement overwriteFileElement = element.Element("OverwriteFiles");

            if (overwriteFileElement != null &&
                overwriteFileElement.Value == "Y")
            {
                overWrite = true;
            }

            return overWrite;
        }
    }
}
