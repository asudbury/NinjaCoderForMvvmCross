// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ICodeSnippetService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the ICodeSnippetService type.
    /// </summary>
    public interface ICodeSnippetService
    {
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
        /// <param name="codeSnippets">The code snippets.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="usingStatement">The using statement.</param>
        /// <returns>The project item service.</returns>
        IProjectItemService CreateUnitTests(
            IVisualStudioService visualStudioService,
            IProjectService projectService,
            CodeSnippet codeSnippets,
            string viewModelName,
            string friendlyName,
            string usingStatement);
    }
}