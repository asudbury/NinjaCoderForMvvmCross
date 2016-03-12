// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Extensions
{
    using Constants;
    using Entities;
    using Services.Interfaces;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System;

    /// <summary>
    ///  Defines the PluginExtensions type.
    /// </summary>
    public static class PluginExtensions
    {
        /// <summary>
        /// Gets the nuget command strings.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="usePreRelease">if set to <c>true</c> [use pre release].</param>
        /// <returns></returns>
        internal static string GetNugetCommandStrings(
            this Plugin instance,
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            bool usePreRelease)
        {
            string commands = string.Empty;

            foreach (string platform in instance.Platforms)
            {
                IProjectService projectService = visualStudioService.GetProjectServiceBySuffix(platform);

                if (projectService != null)
                {
                    foreach (NugetCommand nugetCommand in instance.NugetCommands)
                    {
                        if (IsCommandRequired(nugetCommand, platform))
                        {
                            string pluginNugetCommand = nugetCommand.Command;

                            //// check to see if we are going to use local nuget

                            if (settingsService.UseLocalNuget &&
                                settingsService.LocalNugetName != string.Empty)
                            {
                                pluginNugetCommand = pluginNugetCommand.Replace(
                                    "-ProjectName",
                                    " -Source " + settingsService.LocalNugetName + " -ProjectName");
                            }

                            if (usePreRelease)
                            {
                                pluginNugetCommand += Settings.NugetIncludePreRelease;
                            }

                            if (instance.OverwriteFiles)
                            {
                                commands += Settings.NugetInstallPackageOverwriteFiles.Replace("%s", pluginNugetCommand) + " " + projectService.Name + Environment.NewLine;
                            }
                            else
                            {
                                commands += Settings.NugetInstallPackage.Replace("%s", pluginNugetCommand) + " " + projectService.Name + Environment.NewLine;
                            }
                        }
                    }
                }
            }

            return commands;
        }

        /// <summary>
        /// Determines whether [is command required] [the specified nuget command].
        /// </summary>
        /// <param name="nugetCommand">The nuget command.</param>
        /// <param name="platform">The platform.</param>
        /// <returns></returns>
        internal static bool IsCommandRequired(
            NugetCommand nugetCommand, 
            string platform)
        {
            bool process = true;

            if (string.IsNullOrEmpty(nugetCommand.PlatForm) == false)
            {
                if (nugetCommand.PlatForm != platform)
                {
                    process = false;
                }
            }

            return process;
        }
    }
}
