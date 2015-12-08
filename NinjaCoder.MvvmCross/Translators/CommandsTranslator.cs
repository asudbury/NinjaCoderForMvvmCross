// --------------------------------- -----------------------------------------------------------------------------------
// <summary>
//    Defines the CommandTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Translators
{
    using NinjaCoder.MvvmCross.Entities;
    using Scorchio.Infrastructure.Translators;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Linq;

    /// <summary>
    ///  Defines the CommandTranslator type.
    /// </summary>
    public class CommandsTranslator : ITranslator<XElement, IEnumerable<Command>>
    {
        /// <summary>
        /// The command translator.
        /// </summary>
        private readonly ITranslator<XElement, Command> commandTranslator;

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
        public CommandsTranslator(ITranslator<XElement, Command> commandTranslator)
        {
            this.commandTranslator = commandTranslator;
        }

        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns></returns>
        public IEnumerable<Command> Translate(XElement @from)
        {
            List<Command> commands = new List<Command>();

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
