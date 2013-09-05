// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SnippetService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Translators;

    /// <summary>
    ///  Defines the SnippetService type.
    /// </summary>
    public class SnippetService : BaseService, ISnippetService
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The file system.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// The translator.
        /// </summary>
        private readonly ITranslator<string, CodeSnippet> translator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SnippetService" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="translator">The translator.</param>
        public SnippetService(
            ISettingsService settingsService,
            IFileSystem fileSystem,
            ITranslator<string, CodeSnippet> translator)
        {
            this.settingsService = settingsService;
            this.fileSystem = fileSystem;
            this.translator = translator;
        }

        /// <summary>
        /// Gets the snippet.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The code snippet.</returns>
        public CodeSnippet GetSnippet(string path)
        {
            if (this.fileSystem.File.Exists(path))
            {
                FileInfoBase fileInfoBase = this.fileSystem.FileInfo.FromFileName(path);

                //// only do if the snippet contains some text :-)
                if (fileInfoBase.Length > 0)
                {
                    return this.translator.Translate(path);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the unit testing snippet.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The unit testing snippet.</returns>
        public CodeSnippet GetUnitTestingSnippet(string path)
        {
            CodeSnippet codeSnippet = this.GetSnippet(path);

            //// we grab the moq and currious.core files and add them to the test project.
            //// doing this way means we don't need them in the xml files
            string assemblies = this.settingsService.UnitTestingAssemblies;

            if (string.IsNullOrEmpty(assemblies) == false)
            {
                string[] parts = assemblies.Split(',');

                foreach (string part in parts)
                {
                    if (codeSnippet.UsingStatements == null)
                    {
                        codeSnippet.UsingStatements = new List<string>();
                    }

                    codeSnippet.UsingStatements.Add(part);
                }
            }

            //// add in the init method here- doing this way means we dont need it in the xml files
            codeSnippet.TestInitMethod = this.settingsService.UnitTestingInitMethod;

            return codeSnippet;
        }
    }
}
