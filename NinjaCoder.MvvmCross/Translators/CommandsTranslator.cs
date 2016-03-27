// --------------------------------- -----------------------------------------------------------------------------------
// <summary>
//    Defines the CommandTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Translators
{
    using Scorchio.Infrastructure.Translators;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the CommandTranslator type.
    /// </summary>
    public class CommandsTranslator : ITranslator<XElement, IEnumerable<StudioCommand>>
    {
        /// <summary>
        /// The command translator.
        /// </summary>
        private readonly ITranslator<XElement, StudioCommand> commandTranslator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandsTranslator"/> class.
        /// </summary>
        public CommandsTranslator()
            : this(new CommandTranslator())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandsTranslator"/> class.
        /// </summary>
        /// <param name="commandTranslator">The command translator.</param>
        public CommandsTranslator(ITranslator<XElement, StudioCommand> commandTranslator)
        {
            this.commandTranslator = commandTranslator;
        }

        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">Translate Source.</param>
        /// <returns>A list StudioCommands.</returns>
        public IEnumerable<StudioCommand> Translate(XElement @from)
        {
            List<StudioCommand> commands = new List<StudioCommand>();

            XElement commandsElement = @from.Element("Commands");

            if (commandsElement != null)
            {
                IEnumerable<XElement> elements = commandsElement.Elements("Command");

                commands.AddRange(elements.Select(commandElement => this.commandTranslator.Translate(commandElement)));
            }

            return commands;
        }
    }
}
