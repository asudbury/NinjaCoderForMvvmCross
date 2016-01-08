// --------------------------------- -----------------------------------------------------------------------------------
// <summary>
//    Defines the FileOperationsTranslator type.
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
    /// Defines the FileOperationsTranslator type.
    /// </summary>
    public class FileOperationsTranslator : ITranslator<XElement, IEnumerable<FileOperation>>
    {
        /// <summary>
        /// The file operation translator.
        /// </summary>
        private readonly ITranslator<XElement, FileOperation> fileOperationTranslator;

        /// <summary>
        /// Initializes a new instance of the <see cref="FileOperationsTranslator"/> class.
        /// </summary>
        public FileOperationsTranslator()
            :this(new FileOperationTranslator())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FileOperationsTranslator"/> class.
        /// </summary>
        /// <param name="fileOperationTranslator">The file operation translator.</param>
        public FileOperationsTranslator(ITranslator<XElement, FileOperation> fileOperationTranslator)
        {
            this.fileOperationTranslator = fileOperationTranslator;
        }

        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns></returns>
        public IEnumerable<FileOperation> Translate(XElement @from)
        {
            List<FileOperation> fileOperations = new List<FileOperation>();

            XElement element = @from.Element("FileOperations");

            if (element != null)
            {
                IEnumerable<XElement> elements = element.Elements("FileOperation");

                fileOperations.AddRange(elements.Select(fileOperationElement => this.fileOperationTranslator.Translate(fileOperationElement)));
            }

            return fileOperations;
        }
    }
}
