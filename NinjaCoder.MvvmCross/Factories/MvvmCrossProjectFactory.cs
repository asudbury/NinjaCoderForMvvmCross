// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvvmCrossProjectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Factories
{
    using Entities;
    using Interfaces;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Entities;
    using Services.Interfaces;
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
        /// The nuget commands service.
        /// </summary>
        private readonly INugetCommandsService nugetCommandsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MvvmCrossProjectFactory" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="nugetCommandsService">The nuget commands service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="translator">The translator.</param>
        public MvvmCrossProjectFactory(
            IVisualStudioService visualStudioService,
            INugetCommandsService nugetCommandsService,
            ISettingsService settingsService,
            ITranslator<string, IEnumerable<ProjectTemplateInfo>> translator)
            :base(settingsService, translator)
        {
            this.visualStudioService = visualStudioService;
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
                this.SettingsService.CreatePlatformTestProjects &&
                this.visualStudioService.DroidTestsProjectService == null,
                this.GetPlatformTestsProject(
                    this.SettingsService.DroidTestsProjectSuffix,
                    ProjectType.DroidTests.GetDescription()));

            this.AddProjectIf(
                this.visualStudioService.iOSProjectService == null,
                this.GetiOSProject());

            this.AddProjectIf(
                this.SettingsService.CreatePlatformTestProjects &&
                this.visualStudioService.iOSTestsProjectService == null,
                this.GetPlatformTestsProject(
                    this.SettingsService.iOSTestsProjectSuffix,
                    ProjectType.iOSTests.GetDescription()));

            this.AddProjectIf(
                this.visualStudioService.WindowsPhoneProjectService == null,
                this.GetWindowsPhoneProject());

            this.AddProjectIf(
                this.SettingsService.CreatePlatformTestProjects &&
                this.visualStudioService.WindowsPhoneTestsProjectService == null,
                this.GetPlatformTestsProject(
                    this.SettingsService.WindowsPhoneTestsProjectSuffix,
                    ProjectType.WindowsPhoneTests.GetDescription()));
            
            this.AddProjectIf(
                this.visualStudioService.WpfProjectService == null,
                this.GetWpfProject());

            this.AddProjectIf(
                this.SettingsService.CreatePlatformTestProjects &&
                this.visualStudioService.WpfTestsProjectService == null,
                this.GetPlatformTestsProject(
                    this.SettingsService.WpfTestsProjectSuffix,
                    ProjectType.WindowsWpfTests.GetDescription()));

            if (this.SettingsService.BetaTesting)
            {
                this.AddProjectIf(
                    this.visualStudioService.WindowsUniversalProjectService == null,
                    this.GetWindowsUniversalProject());

                this.AddProjectIf(
                    this.SettingsService.CreatePlatformTestProjects
                    && this.visualStudioService.WindowsUniversalTestsProjectService == null,
                    this.GetPlatformTestsProject(
                        this.SettingsService.WindowsUniversalTestsProjectSuffix,
                        ProjectType.WindowsUniversalTests.GetDescription()));
            }

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
                FriendlyName = ProjectType.Core.GetDescription() + " (Profile " + this.SettingsService.PCLProfile + ")",
                ProjectSuffix = this.SettingsService.CoreProjectSuffix,
                TemplateName = ProjectTemplate.Core.GetDescription(),
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossCoreCommands(),
                ItemTemplates = this.GetProjectItems(FrameworkType.MvvmCross, ProjectType.Core)
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
                this.SettingsService.TestingFramework,
                this.nugetCommandsService.GetMvvmCrossTestsCommands(),
                this.SettingsService.AddCoreTestsProject,
                this.SettingsService.CoreTestsProjectSuffix,
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
                ProjectSuffix = this.SettingsService.DroidProjectSuffix,
                TemplateName = ProjectTemplate.Droid.GetDescription(),
                ReferenceCoreProject = true,
                PreSelected = this.SettingsService.AddAndroidProject,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossDroidCommands(),
                ItemTemplates = this.GetProjectItems(FrameworkType.MvvmCross, ProjectType.Droid)
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
                ProjectSuffix = this.SettingsService.iOSProjectSuffix,
                TemplateName = ProjectTemplate.iOS.GetDescription(),
                ReferenceCoreProject = true,
                PreSelected = this.SettingsService.AddiOSProject,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossiOSCommands(),
                ItemTemplates = this.GetProjectItems(FrameworkType.MvvmCross, ProjectType.iOS)
            };
        }

        /// <summary>
        /// Gets the windows phone project.
        /// </summary>
        /// <returns>A windows phone project.</returns>
        internal ProjectTemplateInfo GetWindowsPhoneProject()
        {
            string templateName = ProjectTemplate.WindowsPhone.GetDescription();

            if (this.SettingsService.UsePreReleaseNinjaNugetPackages)
            {
                templateName = ProjectTemplate.WindowsPhone.GetDescription();
            }

            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.WindowsPhone.GetDescription() + " " + this.SettingsService.WindowsPhoneBuildVersion,
                ProjectSuffix = this.SettingsService.WindowsPhoneProjectSuffix,
                TemplateName = templateName,
                ReferenceCoreProject = true,
                PreSelected = this.SettingsService.AddWindowsPhoneProject,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossWindowsPhoneCommands(),
                ItemTemplates = this.GetProjectItems(FrameworkType.MvvmCross, ProjectType.WindowsPhone)
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
                ProjectSuffix = this.SettingsService.WpfProjectSuffix,
                TemplateName = ProjectTemplate.Wpf.GetDescription(),
                ReferenceCoreProject = true,
                PreSelected = this.SettingsService.AddWpfProject,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossWpfCommands(),
                ItemTemplates = this.GetProjectItems(FrameworkType.MvvmCross, ProjectType.WindowsWpf)
            };
        }

        /// <summary>
        /// Gets the windows universal project.
        /// </summary>
        /// <returns>A Windows Universal project.</returns>
        internal ProjectTemplateInfo GetWindowsUniversalProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.WindowsUniversal.GetDescription(),
                ProjectSuffix = this.SettingsService.WindowsUniversalProjectSuffix,
                TemplateName = ProjectTemplate.WindowsUniversal.GetDescription(),
                ReferenceCoreProject = true,
                PreSelected = this.SettingsService.AddWindowsUniversalProject,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossWpfCommands(),
                ItemTemplates = this.GetProjectItems(FrameworkType.MvvmCross, ProjectType.WindowsUniversal)
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
                this.SettingsService.TestingFramework,
                this.nugetCommandsService.GetTestCommands(),
                true,
                projectSuffix,
                projectType);
        }
    }
}
