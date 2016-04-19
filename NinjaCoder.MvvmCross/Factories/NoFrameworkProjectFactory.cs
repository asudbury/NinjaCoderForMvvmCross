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
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
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
        /// The nuget commands service.
        /// </summary>
        private readonly INugetCommandsService nugetCommandsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NoFrameworkProjectFactory" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="nugetCommandsService">The nuget commands service.</param>
        /// <param name="translator">The translator.</param>
        public NoFrameworkProjectFactory(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            INugetCommandsService nugetCommandsService,
            ITranslator<string, IEnumerable<ProjectTemplateInfo>> translator)
            :base(settingsService, translator)
        {
            TraceService.WriteLine("NoFrameworkProjectFactory::Constructor");

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
            TraceService.WriteLine("NoFrameworkProjectFactory::GetAllowedProjects");

            this.ProjectTemplateInfos = new List<ProjectTemplateInfo>();

            this.AddProjectIf(
                this.visualStudioService.CoreProjectService == null,
                this.GetCoreProject());

            this.AddProjectIf(
                this.visualStudioService.CoreTestsProjectService == null,
                this.GetTestsProject(
                    this.SettingsService.CoreTestsProjectSuffix,
                    ProjectType.CoreTests.GetDescription()));

            this.AddProjectIf(
                this.visualStudioService.DroidProjectService == null,
                this.GetDroidProject());

            this.AddProjectIf(
                this.SettingsService.CreatePlatformTestProjects &&
                this.visualStudioService.DroidTestsProjectService == null,
                this.GetPlatFormTestsProject(
                           FrameworkType.NoFramework,
                           this.SettingsService.TestingFramework,
                           this.nugetCommandsService.GetTestCommands(),
                           true,
                           this.SettingsService.DroidTestsProjectSuffix,
                           ProjectType.DroidTests.GetDescription()));

            this.AddProjectIf(
                this.visualStudioService.iOSProjectService == null,
                this.GetiOSProject());

            this.AddProjectIf(
                this.SettingsService.CreatePlatformTestProjects &&
                this.visualStudioService.iOSTestsProjectService == null,
                this.GetPlatFormTestsProject(
                      FrameworkType.NoFramework,
                      this.SettingsService.TestingFramework,
                      this.nugetCommandsService.GetTestCommands(),
                      true,
                      this.SettingsService.iOSTestsProjectSuffix,
                      ProjectType.iOSTests.GetDescription()));

            this.AddProjectIf(
                 this.visualStudioService.WindowsPhoneProjectService == null,
                 this.GetWindowsPhoneProject());

            this.AddProjectIf(
                this.SettingsService.CreatePlatformTestProjects &&
                this.visualStudioService.WindowsPhoneTestsProjectService == null,
                this.GetPlatFormTestsProject(
                      FrameworkType.NoFramework,
                      this.SettingsService.TestingFramework,
                      this.nugetCommandsService.GetTestCommands(),
                      true,
                      this.SettingsService.WindowsPhoneTestsProjectSuffix,
                      ProjectType.WindowsPhoneTests.GetDescription()));

            this.AddProjectIf(
                this.visualStudioService.WpfProjectService == null,
                this.GetWindowsWpfProject());

            this.AddProjectIf(
                this.SettingsService.CreatePlatformTestProjects &&
                this.visualStudioService.WpfTestsProjectService == null,
                this.GetPlatFormTestsProject(
                      FrameworkType.NoFramework,
                      this.SettingsService.TestingFramework,
                      this.nugetCommandsService.GetTestCommands(),
                      true,
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
                    this.GetPlatFormTestsProject(
                        FrameworkType.NoFramework,
                        this.SettingsService.TestingFramework,
                        this.nugetCommandsService.GetTestCommands(),
                        true,
                        this.SettingsService.WindowsUniversalTestsProjectSuffix,
                        ProjectType.WindowsUniversalTests.GetDescription()));
            }

            TraceService.WriteLine("NoFrameworkProjectFactory::GetAllowedProjects END");

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
                NugetCommands = this.nugetCommandsService.GetNoFrameworksCoreCommands(),
                ItemTemplates = this.GetProjectItems(FrameworkType.NoFramework, ProjectType.Core)

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
        internal ProjectTemplateInfo GetTestsProject(
            string projectSuffix,
            string projectType)
        {
            return this.GetTestsProject(
                FrameworkType.NoFramework,
                this.SettingsService.TestingFramework,
                this.nugetCommandsService.GetTestCommands(),
                true,
                projectSuffix,
                projectType);
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
                NugetCommands = this.nugetCommandsService.GetNoFrameworksCommands(),
                ItemTemplates = this.GetProjectItems(FrameworkType.NoFramework, ProjectType.Droid)
            };
        }

        /// <summary>
        /// Gets the ios project.
        /// </summary>
        /// <returns>An iOs project.</returns>
        // ReSharper disable once InconsistentNaming
        internal ProjectTemplateInfo GetiOSProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.iOS.GetDescription(),
                ProjectSuffix = this.SettingsService.iOSProjectSuffix,
                TemplateName = ProjectTemplate.iOS.GetDescription(),
                ReferenceCoreProject = true,
                PreSelected = this.SettingsService.AddiOSProject,
                NugetCommands = this.nugetCommandsService.GetNoFrameworksiOSCommands(),
                ItemTemplates = this.GetProjectItems(FrameworkType.NoFramework, ProjectType.iOS)
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
                FriendlyName = ProjectType.WindowsPhone.GetDescription() + " " + this.SettingsService.WindowsPhoneBuildVersion,
                ProjectSuffix = this.SettingsService.WindowsPhoneProjectSuffix,
                TemplateName = ProjectTemplate.WindowsPhone.GetDescription(),
                ReferenceCoreProject = true,
                PreSelected = this.SettingsService.AddWindowsPhoneProject,
                NugetCommands = this.nugetCommandsService.GetNoFrameworksCommands(),
                ItemTemplates = this.GetProjectItems(FrameworkType.NoFramework, ProjectType.WindowsPhone)
            };
        }

        /// <summary>
        /// Gets the windows WPF project.
        /// </summary>
        /// <returns>A ProjectTemplateInfo.</returns>
        internal ProjectTemplateInfo GetWindowsWpfProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.WindowsWpf.GetDescription(),
                ProjectSuffix = this.SettingsService.WpfProjectSuffix,
                TemplateName = ProjectTemplate.Wpf.GetDescription(),
                ReferenceXamarinFormsProject = true,
                ReferenceCoreProject = true,
                PreSelected = this.SettingsService.AddWpfProject,
                ItemTemplates = this.GetProjectItems(FrameworkType.NoFramework, ProjectType.WindowsWpf)
            };
        }

        /// <summary>
        /// Gets the windows universal project.
        /// </summary>
        /// <returns></returns>
        internal ProjectTemplateInfo GetWindowsUniversalProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.WindowsUniversal.GetDescription(),
                ProjectSuffix = this.SettingsService.WindowsUniversalProjectSuffix,
                TemplateName = ProjectTemplate.WindowsUniversal.GetDescription(),
                ReferenceXamarinFormsProject = true,
                ReferenceCoreProject = true,
                PreSelected = this.SettingsService.AddWindowsUniversalProject,
                ItemTemplates = this.GetProjectItems(FrameworkType.NoFramework, ProjectType.WindowsUniversal)
            };
        }
    }
}
