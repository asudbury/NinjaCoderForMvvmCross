// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IPluginService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using System.Collections.Generic;

    using NinjaCoder.MvvmCross.Entities;

    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the IPluginService type.
    /// </summary>
    public interface IPluginService
    {
        /// <summary>
        /// Gets the messages.
        /// </summary>
        List<string> Messages { get; }

        /// <summary>
        /// Adds the plugin.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="plugin">The plugin.</param>
        /// <param name="extensionName">Name of the extension.</param>
        /// <returns>True or false.</returns>
        bool AddPlugin(
            IProjectService projectService, 
            Plugin plugin, 
            string extensionName);

        /// <summary>
        /// Adds the project plugin.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="extensionName">Name of the extension.</param>
        /// <param name="plugin">The plugin.</param>
        /// <returns>Returns True or false.</returns>
        bool AddProjectPlugin(
            IProjectService projectService, 
            string folderName, 
            string extensionName, 
            Plugin plugin);

        /// <summary>
        /// Adds the ui plugin.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="plugin">The plugin.</param>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="extensionSource">The extension source.</param>
        /// <param name="extensionDestination">The extension destination.</param>
        /// <param name="addPluginFromLocalDisk">if set to <c>true</c> [add plugin from local disk].</param>
        /// <returns>True or false.</returns>
        bool AddUIPlugin(
            IProjectService projectService,
            Plugin plugin,
            string source,
            string destination,
            string extensionSource,
            string extensionDestination,
            bool addPluginFromLocalDisk);
    }
}
