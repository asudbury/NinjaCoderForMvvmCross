// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IPluginsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the IPluginsService type.
    /// </summary>
    public interface IPluginsService
    {
        /// <summary>
        /// Adds the plugins.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="createUnitTests">if set to <c>true</c> [create unit tests].</param>
        /// <returns>
        /// The messages.
        /// </returns>
        IEnumerable<string> AddPlugins(
            IEnumerable<Plugin> plugins, 
            string viewModelName,
            bool createUnitTests);

        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <param name="usePreRelease">if set to <c>true</c> [use pre release].</param>
        /// <returns></returns>
        IEnumerable<string> GetNugetCommands(
            IEnumerable<Plugin> plugins,
            bool usePreRelease);

        /// <summary>
        /// Gets the nuget messages.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <returns></returns>
        IEnumerable<string> GetNugetMessages(IEnumerable<Plugin> plugins);

        /// <summary>
        /// Gets the post nuget commands.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <returns></returns>
        IEnumerable<Command> GetPostNugetCommands(IEnumerable<Plugin> plugins);

        /// <summary>
        /// Gets the post nuget file operations.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <returns></returns>
        IEnumerable<FileOperation> GetPostNugetFileOperations(IEnumerable<Plugin> plugins);
    }
}
