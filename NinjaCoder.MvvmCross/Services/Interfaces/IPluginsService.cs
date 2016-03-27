// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IPluginsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using Entities;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the IPluginsService type.
    /// </summary>
    public interface IPluginsService
    {
        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <param name="usePreRelease">if set to <c>true</c> [use pre release].</param>
        /// <returns>A list of nuget commands.</returns>
        IEnumerable<string> GetNugetCommands(
            IEnumerable<Plugin> plugins,
            bool usePreRelease);

        /// <summary>
        /// Gets the post nuget commands.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <returns>A list of StudioCommands.</returns>
        IEnumerable<StudioCommand> GetPostNugetCommands(IEnumerable<Plugin> plugins);

        /// <summary>
        /// Gets the post nuget file operations.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <returns>A list of FileOperations.</returns>
        IEnumerable<FileOperation> GetPostNugetFileOperations(IEnumerable<Plugin> plugins);
    }
}
