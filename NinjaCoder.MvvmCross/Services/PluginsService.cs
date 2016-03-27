// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Entities;
    using Interfaces;
    using NinjaCoder.MvvmCross.Extensions;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Defines the PluginsService type.
    /// </summary>
    public class PluginsService : BaseService, IPluginsService
    {
        /// <summary>
        /// The visual studio service.
        /// </summary>
        private readonly IVisualStudioService visualStudioService;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsService" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        public PluginsService(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService)
        {
            TraceService.WriteLine("PluginsService::Constructor");

            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <param name="usePreRelease">if set to <c>true</c> [use pre release].</param>
        /// <returns>A list of nuget commands.</returns>
        public IEnumerable<string> GetNugetCommands(
            IEnumerable<Plugin> plugins, 
            bool usePreRelease)
        {
            TraceService.WriteLine("PluginsService::GetNugetCommands");

            IEnumerable<Plugin> pluginsArray = plugins as Plugin[] ?? plugins.ToArray();

            return pluginsArray.Select(plugin => plugin.GetNugetCommandStrings(
                this.visualStudioService,
                this.settingsService,
                usePreRelease)).ToList();
        }

        /// <summary>
        /// Gets the post nuget commands.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <returns>A list of StudioCommands.</returns>
        public IEnumerable<StudioCommand> GetPostNugetCommands(IEnumerable<Plugin> plugins)
        {
            TraceService.WriteLine("PluginsService::GetPostNugetCommands");

            List<StudioCommand> commands = new List<StudioCommand>();

            if (plugins != null)
            {
                List<Plugin> requiredPlugins = plugins.ToList();

                if (requiredPlugins.Any())
                {
                    foreach (Plugin requiredPlugin in requiredPlugins.Where(requiredPlugin => requiredPlugin.Commands.Any()))
                    {
                        commands.AddRange(requiredPlugin.Commands);
                    }
                }
            }

            return commands;
        }

        /// <summary>
        /// Gets the post nuget file operations.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <returns>A list of FileOperations.</returns>
        public IEnumerable<FileOperation> GetPostNugetFileOperations(IEnumerable<Plugin> plugins)
        {
            TraceService.WriteLine("PluginsService::GetPostNugetFileOperations");

            List<FileOperation> fileOperations = new List<FileOperation>();

            if (plugins != null)
            { 
                List<Plugin> requiredPlugins = plugins.ToList();

                if (requiredPlugins.Any())
                {
                    foreach (Plugin requiredPlugin in requiredPlugins
                        .Where(requiredPlugin => requiredPlugin.FileOperations.Any()))
                    {
                        fileOperations.AddRange(requiredPlugin.FileOperations);
                    }
                }
            }

            return fileOperations;
        }
    }
}
