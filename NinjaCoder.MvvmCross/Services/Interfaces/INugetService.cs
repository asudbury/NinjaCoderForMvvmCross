// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the INugetService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the INugetService type.
    /// </summary>
    public interface INugetService
    {
        /// <summary>
        /// Gets the init nuget messages.
        /// </summary>
        /// <returns>
        /// The messages.
        /// </returns>
        IEnumerable<string> GetInitNugetMessages();

        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        /// <param name="templates">The templates.</param>
        /// <returns>
        /// The nuget commands.
        /// </returns>
        string GetNugetCommands(IEnumerable<ProjectTemplateInfo> templates);

        /// <summary>
        /// Opens the nuget window.
        /// </summary>
        void OpenNugetWindow();

        /// <summary>
        /// Executes the specified commands.
        /// </summary>
        /// <param name="readMePath">The read me path.</param>
        /// <param name="commands">The commands.</param>
        void Execute(
            string readMePath,
            IEnumerable<string> commands);

        /// <summary>
        /// Executes the specified visual studio service.
        /// </summary>
        /// <param name="readMePath">The read me path.</param>
        /// <param name="commands">The commands.</param>
        void Execute(
            string readMePath,
            string commands);
    }
}
