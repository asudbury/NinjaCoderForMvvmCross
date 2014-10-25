// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IPluginService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using Entities;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the IPluginService type.
    /// </summary>
    public interface IPluginService
    {
        /// <summary>
        /// Adds the project plugin.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="plugin">The plugin.</param>
        void AddProjectPlugin(
            IProjectService projectService, 
            Plugin plugin);
    }
}
