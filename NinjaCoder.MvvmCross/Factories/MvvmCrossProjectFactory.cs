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
            this.ProjectTemplateInfos = new List<ProjectTemplateInfo>();

            this.AddProjectIf(
                this.visualStudioService.CoreProjectService == null,
                this.GetCoreProject());

            this.AddProjectIf(
                this.visualStudioService.CoreTestsProjectService == null,
                this.GetCoreTestsProject());
            
            this.AddProjectIf(
                this.visualStudioService.DroidProjectService == null,
                this.GetDroidProject());

            this.AddProjectIf(
                this.settingsService.CreatePlatformTestProjects &&
                this.visualStudioService.DroidTestsProjectService == null,
                this.GetPlatformTestsProject(
                    ProjectSuffix.DroidTests.GetDescription(),
                    ProjectType.DroidTests.GetDescription()));

            this.AddProjectIf(
                this.visualStudioService.iOSProjectService == null,
                this.GetiOSProject());

            this.AddProjectIf(
                this.settingsService.CreatePlatformTestProjects &&
                this.visualStudioService.iOSTestsProjectService == null,
                this.GetPlatformTestsProject(
                    ProjectSuffix.iOSTests.GetDescription(),
                    ProjectType.iOSTests.GetDescription()));

            this.AddProjectIf(
                this.visualStudioService.WindowsPhoneProjectService == null,
                this.GetWindowsPhoneProject());

            this.AddProjectIf(
                this.settingsService.CreatePlatformTestProjects &&
                this.visualStudioService.WindowsPhoneTestsProjectService == null,
                this.GetPlatformTestsProject(
                    ProjectSuffix.WindowsPhoneTests.GetDescription(),
                    ProjectType.WindowsPhoneTests.GetDescription()));
            
            this.AddProjectIf(
                this.visualStudioService.WpfProjectService == null,
                this.GetWpfProject());

            this.AddProjectIf(
                this.settingsService.CreatePlatformTestProjects &&
                this.visualStudioService.WpfTestsProjectService == null,
                this.GetPlatformTestsProject(
                    ProjectSuffix.WpfTests.GetDescription(),
                    ProjectType.WindowsWpfTests.GetDescription()));

            return this.ProjectTemplateInfos;
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
                this.settingsService.AddCoreTestsProject,
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
                TemplateName = ProjectTemplate.Droid.GetDescription(),
                ReferenceCoreProject = true,
                PreSelected = this.settingsService.AddAndroidProject,
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
                FriendlyName = ProjectType.iOS.GetDescription(),
                ProjectSuffix = ProjectSuffix.iOS.GetDescription(),
                TemplateName = ProjectTemplate.iOS.GetDescription(),
                ReferenceCoreProject = true,
                PreSelected = this.settingsService.AddiOSProject,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossiOSCommands()
            };
        }

        /// <summary>
        /// Gets the windows phone project.
        /// </summary>
        /// <returns>A windows phone project.</returns>
        internal ProjectTemplateInfo GetWindowsPhoneProject()
        {
            string templateName = ProjectTemplate.WindowsPhone.GetDescription();

            if (this.settingsService.UsePreReleaseNinjaNugetPackages)
            {
                templateName = ProjectTemplate.WindowsPhone.GetDescription();
            }

            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.WindowsPhone.GetDescription() + " " + this.settingsService.WindowsPhoneBuildVersion,
                ProjectSuffix = ProjectSuffix.WindowsPhone.GetDescription(),
                TemplateName = templateName,
                ReferenceCoreProject = true,
                PreSelected = this.settingsService.AddWindowsPhoneProject,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossWindowsPhoneCommands()
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
                TemplateName = ProjectTemplate.Wpf.GetDescription(),
                ReferenceCoreProject = true,
                PreSelected = this.settingsService.AddWpfProject,
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
                true,
                projectSuffix,
                projectType);
        }
    }
}
