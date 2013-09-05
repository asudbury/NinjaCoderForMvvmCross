// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ISnippetService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using Scorchio.VisualStudio.Entities;

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
    }
}