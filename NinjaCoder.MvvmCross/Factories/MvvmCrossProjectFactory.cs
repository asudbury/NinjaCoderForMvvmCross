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
    public class MvvmCrossProjectFactory : BaseProjectFactory, IMvvmCrossProjectFactory
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
        public MvvmCrossProjectFactory(
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

            if (this.settingsService.CreatePlatformTestProjects &&
                this.visualStudioService.DroidTestsProjectService == null)
            {
                projectInfos.Add(this.GetPlatformTestsProject(
                    ProjectSuffix.DroidTests.GetDescription(),
                    ProjectType.DroidTests.GetDescription()));
            }

            if (this.visualStudioService.iOSProjectService == null)
            {
                projectInfos.Add(this.GetiOSProject());
            }

            if (this.settingsService.CreatePlatformTestProjects &&
                this.visualStudioService.iOSTestsProjectService == null)
            {
                projectInfos.Add(this.GetPlatformTestsProject(
                    ProjectSuffix.iOSTests.GetDescription(),
                    ProjectType.iOSTests.GetDescription()));
            }
            if (this.visualStudioService.WindowsPhoneProjectService == null)
            {
                projectInfos.Add(this.GetWindowsPhoneProject());
            }

            if (this.settingsService.CreatePlatformTestProjects &&
                this.visualStudioService.WindowsPhoneTestsProjectService == null)
            {
                projectInfos.Add(this.GetPlatformTestsProject(
                    ProjectSuffix.WindowsPhoneTests.GetDescription(),
                    ProjectType.WindowsPhoneTests.GetDescription()));
            }

            if (this.visualStudioService.WindowsStoreProjectService == null)
            {
                projectInfos.Add(this.GetWindowsStoreProject());
            }

            if (this.settingsService.CreatePlatformTestProjects &&
                this.visualStudioService.WindowsStoreTestsProjectService == null)
            {
                projectInfos.Add(this.GetPlatformTestsProject(
                    ProjectSuffix.WindowsStoreTests.GetDescription(),
                    ProjectType.WindowsStoreTests.GetDescription()));
            }

            if (this.visualStudioService.WpfProjectService == null)
            {
                projectInfos.Add(this.GetWpfProject());
            }

            if (this.settingsService.CreatePlatformTestProjects &&
                this.visualStudioService.WpfTestsProjectService == null)
            {
                projectInfos.Add(this.GetPlatformTestsProject(
                    ProjectSuffix.WpfTests.GetDescription(),
                    ProjectType.WindowsWpfTests.GetDescription()));
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
                TemplateName = MvvmCrossProjectTemplate.Core.GetDescription(),
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossCoreCommands()
            };
        }

        /// <summary>
        /// Gets the core tests project.
        /// </summary>
        /// <returns>A unit tests project.</returns>
        internal ProjectTemplateInfo GetCoreTestsProject()
        {
            return this.GetTestsProject(
                FrameworkType.MvvmCross,
                this.settingsService.TestingFramework,
                this.nugetCommandsService.GetMvvmCrossTestsCommands(),
                ProjectSuffix.CoreTests.GetDescription(),
                ProjectType.CoreTests.GetDescription());
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
                TemplateName = MvvmCrossProjectTemplate.Droid.GetDescription(),
                ReferenceCoreProject = true,
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossDroidCommands()
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
                FriendlyName = ProjectType.iOS.GetDescription() + " (" + this.settingsService.iOSApiVersion + ")",
                ProjectSuffix = ProjectSuffix.iOS.GetDescription(),
                TemplateName = this.GetiOSProjectTemplate(this.settingsService.iOSApiVersion),
                ReferenceCoreProject = true,
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossiOSCommands()
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
                TemplateName = MvvmCrossProjectTemplate.WindowsPhone.GetDescription(),
                ReferenceCoreProject = true,
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossWindowsPhoneCommands()
            };
        }

        /// <summary>
        /// Gets the windows store project.
        /// </summary>
        /// <returns>A windows store project.</returns>
        internal ProjectTemplateInfo GetWindowsStoreProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.WindowsStore.GetDescription(),
                ProjectSuffix = ProjectSuffix.WindowsStore.GetDescription(),
                TemplateName = MvvmCrossProjectTemplate.WindowsStore.GetDescription(),
                ReferenceCoreProject = true,
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossWindowsStoreCommands()
            };
        }

        /// <summary>
        /// Gets the WPF project.
        /// </summary>
        /// <returns>A wpf project.</returns>
        internal ProjectTemplateInfo GetWpfProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.WindowsWpf.GetDescription(),
                ProjectSuffix = ProjectSuffix.Wpf.GetDescription(),
                TemplateName = MvvmCrossProjectTemplate.WindowsWpf.GetDescription(),
                ReferenceCoreProject = true,
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossWpfCommands()
            };
        }

        /// <summary>
        /// Gets the core tests project.
        /// </summary>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <returns>
        /// A unit tests project.
        /// </returns>
        internal ProjectTemplateInfo GetPlatformTestsProject(
            string projectSuffix,
            string projectType)
        {
            return this.GetPlatFormTestsProject(
                FrameworkType.MvvmCross,
                this.settingsService.TestingFramework,
                this.nugetCommandsService.GetTestCommands(),
                projectSuffix,
                projectType);
        }
    }
}
