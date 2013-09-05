// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseCodeService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Services
{
    using System.IO.Abstractions;
    using Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Translators;

    /// <summary>
    ///  Defines the BaseCodeService type.
    /// </summary>
    public abstract class BaseCodeService : BaseService
    {
        /// <summary>
        /// The translator.
        /// </summary>
        protected readonly ITranslator<string, CodeConfig> Translator;

        /// <summary>
        /// The file system.
        /// </summary>
        protected readonly IFileSystem FileSystem;

        /// <summary>
        /// The settings service.
        /// </summary>
        protected readonly ISettingsService SettingsService;

        /// <summary>
        /// The snippet service.
        /// </summary>
        protected readonly ISnippetService SnippetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCodeService"/> class.
        /// </summary>
        /// <param name="translator">The translator.</param>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="snippetService">The snippet service.</param>
        protected BaseCodeService(
            ITranslator<string, CodeConfig> translator,
            IFileSystem fileSystem, 
            ISettingsService settingsService,
            ISnippetService snippetService)
        {
            TraceService.WriteLine("BaseCodeService::Constructor");

            this.Translator = translator;
            this.FileSystem = fileSystem;
            this.SettingsService = settingsService;
            this.SnippetService = snippetService;
        }

        /// <summary>
        /// Creates the unit tests.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="codeSnippetsPath">The code snippets path.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="usingStatement">The using statement.</param>
        /// <returns>
        /// The project item service.
        /// </returns>
        protected virtual IProjectItemService CreateUnitTests(
            IProjectService projectService,
            string codeSnippetsPath,
            string viewModelName,
            string friendlyName,
            string usingStatement)
        {
            TraceService.WriteLine("BaseCodeService::CreateUnitTests viewModelName=" + viewModelName);

            CodeSnippet codeSnippet = this.SnippetService.GetUnitTestingSnippet(codeSnippetsPath);

            if (codeSnippet != null)
            {
                if (string.IsNullOrEmpty(usingStatement) == false)
                {
                    codeSnippet.UsingStatements.Add(usingStatement);
                }

                string fileName = "Test" + viewModelName;

                //// are we going to assume that the TestViewModel source file already exists?
                IProjectItemService projectItemService = projectService.GetProjectItem(fileName);

                if (projectItemService != null)
                {
                    projectItemService.ImplementUnitTestingCodeSnippet(
                        codeSnippet,
                        viewModelName,
                        this.SettingsService.RemoveDefaultFileHeaders,
                        this.SettingsService.RemoveDefaultComments);

                    this.Messages.Add(friendlyName + " test code added to " + fileName + ".cs in project " + projectService.Name + ".");

                    return projectItemService;
                }
            }
            else
            {
                TraceService.WriteError("BaseService::CreateUnitTests File Not Found=" + codeSnippetsPath);
            }

            return null;
        }
    }
}
