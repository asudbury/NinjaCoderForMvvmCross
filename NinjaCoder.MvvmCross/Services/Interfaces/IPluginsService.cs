// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IPluginsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using Entities;
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Entities;

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
        IEnumerable<StudioCommand> GetPostNugetCommands(IEnumerable<Plugin> plugins);

        /// <summary>
        /// Gets the post nuget file operations.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <returns></returns>
        IEnumerable<FileOperation> GetPostNugetFileOperations(IEnumerable<Plugin> plugins);
    }
}
