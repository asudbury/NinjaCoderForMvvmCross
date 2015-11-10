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
    using System.Data;
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
            return new Plugin
                        {
                            FriendlyName = this.GetFriendlyName(from),
                            IsCommunityPlugin = this.IsCommunityPlugin(from),
                            UsingStatement = this.GetUsingStatement(from),
                            NugetCommands = this.GetNugetCommands(from),
                            Platforms = this.GetPlatforms(from),
                            Frameworks = this.GetFrameworks(from),
                            OverwriteFiles = this.GetOverwriteFiles(from),
                            Description = this.GetDescription(from),
                            Category = this.GetCategory(from),
                            NinjaSamples = this.GetNinjaSamples(from),
                            Commands = this.GetCommands(from),
                            FileOperations = this.GetFileOperations(from)
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
        internal IEnumerable<NugetCommand> GetNugetCommands(XElement element)
        {
            List<NugetCommand> nugetCommands = new List<NugetCommand>();

            IEnumerable<XElement> elements = element.Elements("NugetPackage");

            foreach (XElement nugetPackageElement in elements)
            {
                NugetCommand nugetCommand = new NugetCommand
                {
                    Command = nugetPackageElement.Value,
                    PlatForm = nugetPackageElement.GetSafeAttributeStringValue("Platform")
                };

                nugetCommands.Add(nugetCommand);
            }

            return nugetCommands;
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

        /// <summary>
        /// Gets the description.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal string GetDescription(XElement element)
        {
            XElement descriptionElement = element.Element("Description");

            return descriptionElement != null ? descriptionElement.Value : string.Empty;
        }

        /// <summary>
        /// Gets the category.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal string GetCategory(XElement element)
        {
            XElement categoryElement = element.Element("Category");

            return categoryElement != null ? categoryElement.Value : string.Empty;
        }

        /// <summary>
        /// Gets the ninja nuget commands.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal IEnumerable<Plugin> GetNinjaSamples(XElement element)
        {
            XElement samplesElement = element.Element("NinjaSamples");

            if (samplesElement != null)
            {
                IEnumerable<XElement> elements = samplesElement.Elements("NinjaSample");

                return elements.Select(this.Translate).ToList();
            }

            return new List<Plugin>();
        }

        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal IEnumerable<Command> GetCommands(XElement element)
        {
            List<Command> commands = new List<Command>();

            XElement commandsElement = element.Element("Commands");

            if (commandsElement != null)
            {
                IEnumerable<XElement> elements = commandsElement.Elements("Command");

                foreach (XElement commandElement in elements)
                {
                    Command command = new Command
                    {
                        PlatForm = commandElement.GetSafeAttributeStringValue("Platform"),
                        CommandType = commandElement.GetSafeAttributeStringValue("Type"),
                        Name = commandElement.GetSafeAttributeStringValue("Name")
                    };

                    commands.Add(command);
                }
            }

            return commands;
        }

        /// <summary>
        /// Gets the file operations.
        /// </summary>
        /// <param name="element">The element.</param>
        /// <returns></returns>
        internal IEnumerable<FileOperation> GetFileOperations(XElement element)
        {
            List<FileOperation> fileOperations = new List<FileOperation>();

            XElement commandsElement = element.Element("FileOperations");

            if (commandsElement != null)
            {
                IEnumerable<XElement> elements = commandsElement.Elements("FileOperation");

                foreach (XElement commandElement in elements)
                {
                    FileOperation fileOperation = new FileOperation
                    {
                        PlatForm = commandElement.GetSafeAttributeStringValue("Platform"),
                        CommandType = commandElement.GetSafeAttributeStringValue("Type"),
                        Name = commandElement.GetSafeAttributeStringValue("Name"),
                        File = commandElement.GetSafeAttributeStringValue("File"),
                        Directory = commandElement.GetSafeAttributeStringValue("Directory"),
                        From = commandElement.GetSafeAttributeStringValue("From"),
                        To = commandElement.GetSafeAttributeStringValue("To")
                    };

                    fileOperations.Add(fileOperation);
                }
            }

            return fileOperations;
        }
    }
}
