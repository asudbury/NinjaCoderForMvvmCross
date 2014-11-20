// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvvmCrossProjectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Factories
{
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary> 
    ///  Defines the MvvmCrossProjectFactory type. 
    /// </summary>
    public class NoFrameworkProjectFactory : BaseProjectFactory, INoFrameworkProjectFactory
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
        /// The nuget commands service.
        /// </summary>
        private readonly INugetCommandsService nugetCommandsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MvvmCrossProjectFactory" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param> 
        /// <param name="nugetCommandsService">The nuget commands service.</param>
        public NoFrameworkProjectFactory(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            INugetCommandsService nugetCommandsService)
        {
            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
            this.nugetCommandsService = nugetCommandsService;
        }

        /// <summary>
        /// Gets the allowed projects.
        /// </summary>
        /// <returns>
        /// The allowed projects.
        /// </returns>
        public IEnumerable<ProjectTemplateInfo> GetAllowedProjects()
        {
            List<ProjectTemplateInfo> projectInfos = new List<ProjectTemplateInfo>();

            if (this.visualStudioService.CoreProjectService == null)
            {
                projectInfos.Add(this.GetCoreProject());
            }

            if (this.visualStudioService.CoreTestsProjectService == null)
            {
                projectInfos.Add(this.GetCoreTestsProject());
            }

            if (this.visualStudioService.DroidProjectService == null)
            {
                projectInfos.Add(this.GetDroidProject());
            }

            if (this.visualStudioService.iOSProjectService == null)
            {
                projectInfos.Add(this.GetiOSProject());
            }

            if (this.visualStudioService.WindowsPhoneProjectService == null)
            {
                projectInfos.Add(this.GetWindowsPhoneProject());
            }

            return projectInfos;
        }

        /// <summary>
        /// Gets the core project.
        /// </summary>
        /// <returns>A core project.</returns>
        internal ProjectTemplateInfo GetCoreProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.Core.GetDescription() + " (Profile " + this.settingsService.PCLProfile + ")",
                ProjectSuffix = ProjectSuffix.Core.GetDescription(),
                TemplateName = ProjectTemplate.Core.GetDescription(),
                PreSelected = true,
                NugetCommands = new List<string>()
            };
        }

        /// <summary>
        /// Gets the core tests project.
        /// </summary>
        /// <returns>A unit tests project.</returns>
        internal ProjectTemplateInfo GetCoreTestsProject()
        {
            return this.GetCoreTestsProject(
                FrameworkType.NoFramework,
                this.settingsService.TestingFramework,
                this.settingsService.MockingFramework,
                this.nugetCommandsService.GetTestCommands());
        }

        /// <summary>
        /// Gets the droid project.
        /// </summary>
        /// <returns>An android project.</returns>
        internal ProjectTemplateInfo GetDroidProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = "Android",
                ProjectSuffix = ProjectSuffix.Droid.GetDescription(),
                TemplateName = ProjectTemplate.Droid.GetDescription(),
                ReferenceCoreProject = true,
                NugetCommands = this.nugetCommandsService.GetNoFrameworksCommands()
            };
        }

        /// <summary>
        /// Gets the ios project.
        /// </summary>
        /// <returns>An iOs project.</returns>
        internal ProjectTemplateInfo GetiOSProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.iOS.GetDescription() + " " + this.settingsService.iOSBuildVersion,
                ProjectSuffix = ProjectSuffix.iOS.GetDescription(),
                TemplateName = ProjectTemplate.iOS.GetDescription(),
                ReferenceCoreProject = true,
                NugetCommands = this.nugetCommandsService.GetNoFrameworksCommands()
            };
        }

        /// <summary>
        /// Gets the windows phone project.
        /// </summary>
        /// <returns>A windows phone project.</returns>
        internal ProjectTemplateInfo GetWindowsPhoneProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.WindowsPhone.GetDescription() + " " + this.settingsService.WindowsPhoneBuildVersion,
                ProjectSuffix = ProjectSuffix.WindowsPhone.GetDescription(),
                TemplateName = ProjectTemplate.WindowsPhone.GetDescription(),
                ReferenceCoreProject = true,
                NugetCommands = this.nugetCommandsService.GetNoFrameworksCommands()
            };
        }
    }
}
