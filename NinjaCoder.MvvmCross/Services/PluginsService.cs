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
        /// The nuget service.
        /// </summary>
        private readonly INugetService nugetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsService" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="nugetService">The nuget service.</param>
        public PluginsService(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            INugetService nugetService)
        {
            TraceService.WriteLine("PluginsService::Constructor");

            this.visualStudioService = visualStudioService;
            this.nugetService = nugetService;
        }

        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <param name="usePreRelease">if set to <c>true</c> [use pre release].</param>
        /// <returns></returns>
        public IEnumerable<string> GetNugetCommands(
            IEnumerable<Plugin> plugins, 
            bool usePreRelease)
        {
            IEnumerable<Plugin> pluginsArray = plugins as Plugin[] ?? plugins.ToArray();

            return pluginsArray.Select(plugin => plugin.GetNugetCommandStrings(
                this.visualStudioService,
                usePreRelease)).ToList();
        }

        /// <summary>
        /// Gets the nuget messages.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public IEnumerable<string> GetNugetMessages(IEnumerable<Plugin> plugins)
        {
            IEnumerable<Plugin> pluginsArray = plugins as Plugin[] ?? plugins.ToArray();

            return pluginsArray.Select(plugin => plugin.GetNugetCommandMessages(this.visualStudioService)).ToList();
        }

        /// <summary>
        /// Gets the post nuget commands.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <returns></returns>
        public IEnumerable<StudioCommand> GetPostNugetCommands(IEnumerable<Plugin> plugins)
        {
            List<StudioCommand> commands = new List<StudioCommand>();

            List<Plugin> requiredPlugins = plugins.ToList();

            if (requiredPlugins.Any())
            {
                foreach (Plugin requiredPlugin in requiredPlugins
                    .Where(requiredPlugin => requiredPlugin.Commands.Any()))
                {
                    commands.AddRange(requiredPlugin.Commands);
                }
            }

            return commands;
        }

        /// <summary>
        /// Gets the post nuget file operations.
        /// </summary>
        /// <param name="plugins">The plugins.</param>
        /// <returns></returns>
        public IEnumerable<FileOperation> GetPostNugetFileOperations(IEnumerable<Plugin> plugins)
        {
            List<FileOperation> fileOperations = new List<FileOperation>();

            List<Plugin> requiredPlugins = plugins.ToList();

            if (requiredPlugins.Any())
            {
                foreach (Plugin requiredPlugin in requiredPlugins
                    .Where(requiredPlugin => requiredPlugin.FileOperations.Any()))
                {
                    fileOperations.AddRange(requiredPlugin.FileOperations);
                }
            }

            return fileOperations;
        }
    }
}
