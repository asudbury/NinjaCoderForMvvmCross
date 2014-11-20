// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IPluginsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using Entities;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System.Collections.Generic;

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
        /// <param name="createUnitTests">if set to <c>true</c> [create unit tests].</param>
        /// <returns>
        /// The messages.
        /// </returns>
        IEnumerable<string> AddPlugins(
            IVisualStudioService visualStudioService, 
            IEnumerable<Plugin> plugins, 
            string viewModelName,
            bool createUnitTests);

        /// <summary>
        /// Adds the project plugins.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="plugins">The plugins.</param>
        /// <param name="addBootstrapFile">if set to <c>true</c> [add bootstrap file].</param>
        /// <returns>
        /// The added plugins.
        /// </returns>
        void AddProjectPlugins(
            IProjectService projectService,
            IEnumerable<Plugin> plugins, 
            bool addBootstrapFile);
    }
}
