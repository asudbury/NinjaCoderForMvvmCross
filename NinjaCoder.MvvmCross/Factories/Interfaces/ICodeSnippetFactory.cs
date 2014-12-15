// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ICodeSnippetFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using Entities;
    using Services.Interfaces;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    /// Defines the ICodeSnippetFactory type.
    /// </summary>
    public interface ICodeSnippetFactory
    {
        /// <summary>
        /// Gets the code snippet service.
        /// </summary>
        /// <returns>The code snippet service.</returns>
        ICodeSnippetService GetCodeSnippetService();

        /// <summary>
        /// Gets the snippet.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The code snippet.</returns>
        CodeSnippet GetSnippet(string path);

        /// <summary>
        /// Gets the plugin snippet.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <returns>The code snippet.</returns>
        CodeSnippet GetPluginSnippet(Plugin plugin);

        /// <summary>
        /// Gets the plugin test snippet.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <returns>The code snippet.</returns>
        CodeSnippet GetPluginTestSnippet(Plugin plugin);
    }
}
