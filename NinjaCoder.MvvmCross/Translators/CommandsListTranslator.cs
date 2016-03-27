// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CommandsListTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using Entities;
    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using System;
    using System.Collections.Generic;
    using System.Xml.Linq;

    /// <summary>
    /// Defines the CommandsListTranslator type.
    /// </summary>
    public class CommandsListTranslator : ITranslator<string, CommandsList>
    {
        /// <summary>
        /// The commands translator.
        /// </summary>
        private readonly ITranslator<XElement, IEnumerable<StudioCommand>> commandsTranslator;

        /// <summary>
        /// The file operations translator.
        /// </summary>
        private readonly ITranslator<XElement, IEnumerable<FileOperation>> fileOperationsTranslator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandsListTranslator"/> class.
        /// </summary>
        public CommandsListTranslator()
            : this(new CommandsTranslator(), new FileOperationsTranslator())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandsListTranslator"/> class.
        /// </summary>
        /// <param name="commandsTranslator">The commands translator.</param>
        /// <param name="fileOperationsTranslator">The file operations translator.</param>
        public CommandsListTranslator(
            ITranslator<XElement, IEnumerable<StudioCommand>> commandsTranslator,
            ITranslator<XElement, IEnumerable<FileOperation>> fileOperationsTranslator)
        {
            this.commandsTranslator = commandsTranslator;
            this.fileOperationsTranslator = fileOperationsTranslator;
        }

        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">Translate Source.</param>
        /// <returns>A Commands List.</returns>
        public CommandsList Translate(string @from)
        {
            TraceService.WriteLine("CommandsList::Translate " + @from);

            try
            {
                XDocument doc = XDocument.Load(from);

                if (doc.Root != null)
                {
                    return new CommandsList
                                {
                                    Commands = this.commandsTranslator.Translate(doc.Root),
                                    FileOperations = this.fileOperationsTranslator.Translate(doc.Root)
                                };
                }

                return null;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
