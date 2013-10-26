// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ISnippetService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the ISnippetService type.
    /// </summary>
    public interface ISnippetService
    {
        /// <summary>
        /// Gets the snippet.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The code snippet.</returns>
        CodeSnippet GetSnippet(string path);

        /// <summary>
        /// Gets the unit testing snippet.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The unit testing snippet.</returns>
        CodeSnippet GetUnitTestingSnippet(string path);

        /// <summary>
        /// Applies the global variables.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="codeSnippet">The code snippet.</param>
        void ApplyGlobals(
            IVisualStudioService visualStudioService, 
            CodeSnippet codeSnippet);

        /// <summary>
        /// Creates the unit tests.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="projectService">The project service.</param>
        /// <param name="codeSnippetsPath">The code snippets path.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="usingStatement">The using statement.</param>
        /// <returns>The project item service.</returns>
        IProjectItemService CreateUnitTests(
            IVisualStudioService visualStudioService,
            IProjectService projectService,
            string codeSnippetsPath,
            string viewModelName,
            string friendlyName,
            string usingStatement);
    }
}