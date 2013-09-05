// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the INugetService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    /// Defines the INugetService type.
    /// </summary>
    public interface INugetService
    {
        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        /// <param name="templates">The templates.</param>
        /// <returns>The nuget commands.</returns>
        string GetNugetCommands(
            IVisualStudioService visualStudioService,
            IEnumerable<ProjectTemplateInfo> templates);

        /// <summary>
        /// Opens the nuget window.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        void OpenNugetWindow(IVisualStudioService visualStudioService);

        /// <summary>
        /// Executes the specified commands.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="readMePath">The read me path.</param>
        /// <param name="commands">The commands.</param>
        /// <param name="resumeReSharper">if set to <c>true</c> [resume re sharper].</param>
        /// <param name="setupEventHandlers">if set to <c>true</c> [setup event handlers].</param>
        void Execute(
            IVisualStudioService visualStudioService,
            string readMePath,
            string commands,
            bool resumeReSharper,
            bool setupEventHandlers = true);
    }
}
