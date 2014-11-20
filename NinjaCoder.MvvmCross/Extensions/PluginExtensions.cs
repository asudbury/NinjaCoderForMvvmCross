// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Extensions
{
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System;

    /// <summary>
    ///  Defines the PluginExtensions type.
    /// </summary>
    public static class PluginExtensions
    {
        /// <summary>
        /// The nuget install package.
        /// </summary>
        internal const string NugetInstallPackage = "Install-Package %s -FileConflictAction ignore -ProjectName";

        /// <summary>
        /// Gets the nuget command strings.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <returns></returns>
        internal static string GetNugetCommandStrings(
            this Plugin instance,
            IVisualStudioService visualStudioService)
        {
            string commands = string.Empty;

            foreach (string platform in instance.Platforms)
            {
                IProjectService projectService = visualStudioService.GetProjectServiceBySuffix(platform);

                if (projectService != null)
                {
                    foreach (string nugetCommand in instance.NugetCommands)
                    {
                        commands += NugetInstallPackage.Replace("%s", nugetCommand) + " " + projectService.Name + Environment.NewLine;
                    }
                }
            }

            return commands;
        }

        /// <summary>
        /// Gets the nuget command messages.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <returns></returns>
        internal static string GetNugetCommandMessages(
            this Plugin instance,
            IVisualStudioService visualStudioService)
        {
            string commands = string.Empty;

            foreach (string platform in instance.Platforms)
            {
                IProjectService projectService = visualStudioService.GetProjectServiceBySuffix(platform);

                if (projectService != null)
                {
                    foreach (string nugetCommand in instance.NugetCommands)
                    {
                        commands += nugetCommand + " nuget package added to  " + projectService.Name + " project.";
                    }
                }
            }

            return commands;
        }
    }
}
