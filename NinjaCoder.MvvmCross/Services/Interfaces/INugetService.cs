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
        /// Gets the init nuget messages.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The messages.</returns>
        IEnumerable<string> GetInitNugetMessages(string message);
 
        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="templates">The templates.</param>
        /// <param name="verboseOutput">if set to <c>true</c> [verbose output].</param>
        /// <param name="debug">if set to <c>true</c> [debug].</param>
        /// <returns>The nuget commands.</returns>
        string GetNugetCommands(
            IVisualStudioService visualStudioService,
            IEnumerable<ProjectTemplateInfo> templates,
            bool verboseOutput,
            bool debug);

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
            IEnumerable<string> commands,
            bool resumeReSharper,
            bool setupEventHandlers = true);

        /// <summary>
        /// Executes the specified visual studio service.
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
