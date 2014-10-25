// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CodeSnippetService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Factories.Interfaces;

    using Interfaces;
    using Scorchio.Infrastructure.Services.Testing.Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///  Defines the CodeSnippetService type.
    /// </summary>
    internal class CodeSnippetService : BaseService, ICodeSnippetService
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The mocking service.
        /// </summary>
        private readonly IMockingService mockingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeSnippetService" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="mockingServiceFactory">The mocking service factory.</param>
        public CodeSnippetService(
            ISettingsService settingsService,
            IMockingServiceFactory mockingServiceFactory)
        {
            TraceService.WriteLine("SnippetService::Constructor");

            this.settingsService = settingsService;
            this.mockingService = mockingServiceFactory.GetMockingService();
        }

        /// <summary>
        /// Applies the global variables.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="codeSnippet">The code snippet.</param>
        public void ApplyGlobals(
            IVisualStudioService visualStudioService,
            CodeSnippet codeSnippet)
        {
            TraceService.WriteLine("SnippetService::ApplyGlobals");

            bool hasGlobals = visualStudioService.DTEService.SolutionService.HasGlobals;

            if (hasGlobals)
            {
                Dictionary<string, string> dictionary = visualStudioService.DTEService.SolutionService.GetGlobalVariables();

                if (dictionary != null)
                {
                    foreach (KeyValuePair<string, string> keyValuePair in dictionary
                        .Where(keyValuePair => keyValuePair.Value != null))
                    {
                        codeSnippet.AddReplacementVariable(keyValuePair.Key, keyValuePair.Value);
                    }
                }
            }
        }

        /// <summary>
        /// Creates the unit tests.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="projectService">The project service.</param>
        /// <param name="codeSnippet">The code snippet.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="usingStatement">The using statement.</param>
        /// <returns>The project item service.</returns>
        public IProjectItemService CreateUnitTests(
            IVisualStudioService visualStudioService,
            IProjectService projectService,
            CodeSnippet codeSnippet,
            string viewModelName,
            string friendlyName,
            string usingStatement)
        {
            TraceService.WriteLine("SnippetService::CreateUnitTests viewModelName=" + viewModelName);

            if (codeSnippet != null)
            {
                if (string.IsNullOrEmpty(usingStatement) == false)
                {
                    if (codeSnippet.UsingStatements == null)
                    {
                        codeSnippet.UsingStatements = new List<string>();
                    }

                    codeSnippet.UsingStatements.Add(usingStatement);
                }

                if (this.settingsService.ReplaceVariablesInSnippets)
                {
                    this.ApplyGlobals(visualStudioService, codeSnippet);
                }

                this.mockingService.InjectMockingDetails(codeSnippet);
            }

            string fileName = "Test" + viewModelName;

            //// are we going to assume that the TestViewModel source file already exists?
            IProjectItemService projectItemService = projectService.GetProjectItem(fileName);

            if (projectItemService != null)
            {
                if (codeSnippet != null)
                {
                    projectItemService.ImplementUnitTestingCodeSnippet(
                        codeSnippet,
                        viewModelName,
                        this.settingsService.RemoveDefaultFileHeaders,
                        this.settingsService.RemoveDefaultComments,
                        this.settingsService.FormatFunctionParameters);

                    this.Messages.Add(friendlyName + " test code added to " + fileName + ".cs in project " + projectService.Name + ".");
                }

                return projectItemService;
            }

            return null;
        }
    }
}
