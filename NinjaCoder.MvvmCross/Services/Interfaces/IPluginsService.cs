// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IPluginsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using System.Collections.Generic;
    using NinjaCoder.MvvmCross.Entities;

    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    /// Defines the IPluginsService type.
    /// </summary>
    public interface IPluginsService
    {
        /// <summary>
        /// Adds the plugins.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="plugins">The plugins.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="codeSnippetsPath">The code snippets path.</param>
        /// <param name="createUnitTests">if set to <c>true</c> [create unit tests].</param>
        /// <returns>
        /// The messages.
        /// </returns>
        IEnumerable<string> AddPlugins(
            IVisualStudioService visualStudioService, 
            List<Plugin> plugins, 
            string viewModelName,
            string codeSnippetsPath,
            bool createUnitTests);

        /// <summary>
        /// Adds the project plugins.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="plugins">The plugins.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="extensionName">Name of the extension.</param>
        void AddProjectPlugins(
            IProjectService projectService,
            List<Plugin> plugins, 
            string folderName, 
            string extensionName);
    }
}
