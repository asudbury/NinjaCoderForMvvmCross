// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Extensions
{
    using System;

    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.VisualStudio.Services.Interfaces;

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

            IProjectService coreProjectService = visualStudioService.CoreProjectService;

            if (coreProjectService != null)
            {
                foreach (string nugetCommand in instance.NugetCommands)
                {
                    commands +=  NugetInstallPackage.Replace("%s", nugetCommand) + " " + coreProjectService.Name + Environment.NewLine; 
                }
            }

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
    }
}
