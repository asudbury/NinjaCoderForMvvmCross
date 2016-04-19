// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvvmCrossAndXamarinFormsProjectFactory type.
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
    ///  Defines the MvvmCrossAndXamarinFormsProjectFactory type. 
    /// </summary>
    public class MvvmCrossAndXamarinFormsProjectFactory : BaseProjectFactory, IMvvmCrossAndXamarinFormsProjectFactory
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
        /// Initializes a new instance of the <see cref="MvvmCrossAndXamarinFormsProjectFactory" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="nugetCommandsService">The nuget commands service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="translator">The translator.</param>
        public MvvmCrossAndXamarinFormsProjectFactory(
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
                this.visualStudioService.XamarinFormsProjectService == null,
                this.GetFormsProject());

            this.AddProjectIf(
                this.visualStudioService.XamarinFormsTestsProjectService == null,
                this.GetFormsTestsProject());

            this.AddProjectIf(
                this.visualStudioService.iOSProjectService == null,
                this.GetiOSProject());

            this.AddProjectIf(
                this.SettingsService.CreatePlatformTestProjects &&
                this.visualStudioService.iOSTestsProjectService == null,
                this.GetPlatformTestsProject(
                    this.nugetCommandsService.GetiOSTestCommands(),
                    true,
                    this.SettingsService.iOSTestsProjectSuffix,
                    ProjectType.iOSTests.GetDescription()));

            this.AddProjectIf(
                this.visualStudioService.DroidProjectService == null,
                this.GetDroidProject());

            this.AddProjectIf(
                this.SettingsService.CreatePlatformTestProjects &&
                this.visualStudioService.DroidTestsProjectService == null,
                this.GetPlatformTestsProject(
                    this.nugetCommandsService.GetAndroidTestCommands(),
                    true,
                    this.SettingsService.DroidTestsProjectSuffix,
                    ProjectType.DroidTests.GetDescription()));

            this.AddProjectIf(
                this.visualStudioService.WindowsPhoneProjectService == null,
                this.GetWindowsPhoneProject());

            this.AddProjectIf(
                this.SettingsService.CreatePlatformTestProjects &&
                this.visualStudioService.WindowsPhoneTestsProjectService == null,
                this.GetPlatformTestsProject(
                    this.nugetCommandsService.GetTestCommands(),
                    true,
                    this.SettingsService.WindowsPhoneTestsProjectSuffix,
                    ProjectType.WindowsPhoneTests.GetDescription()));

            if (this.SettingsService.BetaTesting)
            {
                this.AddProjectIf(
                    this.visualStudioService.WindowsUniversalProjectService == null,
                    this.GetWindowsUniversalProject());

                this.AddProjectIf(
                    this.SettingsService.CreatePlatformTestProjects
                    && this.visualStudioService.WindowsUniversalTestsProjectService == null,
                    this.GetPlatformTestsProject(
                        this.nugetCommandsService.GetTestCommands(),
                        true,
                        this.SettingsService.WindowsUniversalTestsProjectSuffix,
                        ProjectType.WindowsUniversalTests.GetDescription()));
            }

            return this.ProjectTemplateInfos; 
        }

        /// <summary>
        /// Gets the core project.
        /// </summary>
        internal ProjectTemplateInfo GetCoreProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.Core.GetDescription() + " (Profile " + this.SettingsService.PCLProfile + ")",
                ProjectSuffix = this.SettingsService.CoreProjectSuffix,
                TemplateName = ProjectTemplate.Core.GetDescription(),
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossXamarinFormsCoreCommands(),
                ItemTemplates = this.GetProjectItems(FrameworkType.MvvmCrossAndXamarinForms, ProjectType.Core)
            };
        }

        /// <summary>
        /// Gets the core tests project.
        /// </summary>
        /// <returns>A unit tests project.</returns>
        internal ProjectTemplateInfo GetCoreTestsProject()
        {
            return this.GetTestsProject(
                FrameworkType.MvvmCrossAndXamarinForms,
                this.SettingsService.TestingFramework,
                this.nugetCommandsService.GetMvvmCrossTestsCommands(),
                this.SettingsService.AddCoreTestsProject,
                this.SettingsService.CoreTestsProjectSuffix,
                ProjectType.CoreTests.GetDescription());
        }

        /// <summary>
        /// Gets the xamarin forms project.
        /// </summary>
        /// <returns>A ProjectTemplateInfo.</returns>
        internal ProjectTemplateInfo GetFormsProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.XamarinForms.GetDescription() + " (Profile " + this.SettingsService.PCLProfile + ")",
                ProjectSuffix = this.SettingsService.XamarinFormsProjectSuffix,
                TemplateName = ProjectTemplate.XamarinForms.GetDescription(),
                PreSelected = true,
                ReferenceCoreProject = true,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossXamarinFormsCommands(),
                ItemTemplates = this.GetProjectItems(FrameworkType.MvvmCrossAndXamarinForms, ProjectType.XamarinForms)
            };
        }
        
        /// <summary>
        /// Gets the forms tests project.
        /// </summary>
        /// <returns>A ProjectTemplateInfo.</returns>
        internal ProjectTemplateInfo GetFormsTestsProject()
        {
            ProjectTemplateInfo projectTemplateInfo = this.GetTestsProject(
                FrameworkType.XamarinForms,
                this.SettingsService.TestingFramework,
                this.nugetCommandsService.GetTestCommands(),
                this.SettingsService.AddXamarinFormsTestsProject,
                this.SettingsService.XamarinFormsTestsProjectSuffix,
                ProjectType.XamarinFormsTests.GetDescription());

            //// TODO : lets do this properly :-)
            projectTemplateInfo.ReferenceCoreProject = false;
            projectTemplateInfo.ReferenceXamarinFormsProject = true;

            if (this.SettingsService.CreatePlatformTestProjects == false)
            {
                projectTemplateInfo.PreSelected = false;
            }

            return projectTemplateInfo;
        }

        /// <summary>
        /// Gets the ios project.
        /// </summary>
        /// <returns>A ProjectTemplateInfo.</returns>
        // ReSharper disable once InconsistentNaming
        internal ProjectTemplateInfo GetiOSProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.iOS.GetDescription(),
                ProjectSuffix = this.SettingsService.iOSProjectSuffix,
                TemplateName = ProjectTemplate.iOS.GetDescription(),
                PreSelected = this.SettingsService.AddiOSProject,
                ReferenceCoreProject = true,
                ReferenceXamarinFormsProject = true,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossXamarinFormsiOSCommands(),
                ItemTemplates = this.GetProjectItems(FrameworkType.MvvmCrossAndXamarinForms, ProjectType.iOS)
            };
        }

        /// <summary>
        /// Gets the droid project.
        /// </summary>
        /// <returns>A ProjectTemplateInfo.</returns>
        internal ProjectTemplateInfo GetDroidProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = "Android",
                ProjectSuffix = this.SettingsService.DroidProjectSuffix,
                TemplateName = ProjectTemplate.Droid.GetDescription(),
                ReferenceCoreProject = true,
                ReferenceXamarinFormsProject = true,
                PreSelected = this.SettingsService.AddAndroidProject,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossXamarinFormDroidCommands(),
                ItemTemplates = this.GetProjectItems(FrameworkType.MvvmCrossAndXamarinForms, ProjectType.Droid)
            };
        }

        /// <summary>
        /// Gets the windows phone project.
        /// </summary>
        /// <returns>A ProjectTemplateInfo.</returns>
        internal ProjectTemplateInfo GetWindowsPhoneProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.WindowsPhone.GetDescription() + " " + this.SettingsService.WindowsPhoneBuildVersion,
                ProjectSuffix = this.SettingsService.WindowsPhoneProjectSuffix,
                TemplateName = ProjectTemplate.WindowsPhone.GetDescription(),
                ReferenceCoreProject = true,
                ReferenceXamarinFormsProject = true,
                PreSelected = this.SettingsService.AddWindowsPhoneProject,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossXamarinFormsWindowsPhoneCommands(),
                ItemTemplates = this.GetProjectItems(FrameworkType.MvvmCrossAndXamarinForms, ProjectType.WindowsPhone)
            };
        }

        /// <summary>
        /// Gets the windows universal project.
        /// </summary>
        /// <returns>A ProjectTemplateInfo.</returns>
        internal ProjectTemplateInfo GetWindowsUniversalProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.WindowsUniversal.GetDescription(),
                ProjectSuffix = this.SettingsService.WindowsUniversalProjectSuffix,
                TemplateName = ProjectTemplate.WindowsUniversal.GetDescription(),
                ReferenceCoreProject = true,
                ReferenceXamarinFormsProject = true,
                PreSelected = this.SettingsService.AddWindowsUniversalProject,
                ItemTemplates = this.GetProjectItems(FrameworkType.MvvmCrossAndXamarinForms, ProjectType.WindowsUniversal)
            };
        }

        /// <summary>
        /// Gets the core tests project.
        /// </summary>
        /// <param name="nugetCommands">The nuget commands.</param>
        /// <param name="preSelect">if set to <c>true</c> [pre select].</param>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <returns>A ProjectTemplateInfo.</returns>
        internal ProjectTemplateInfo GetPlatformTestsProject(
            IEnumerable<string> nugetCommands,
            bool preSelect,
            string projectSuffix,
            string projectType)
        {
            ProjectTemplateInfo projectTemplateInfo = this.GetPlatFormTestsProject(
                FrameworkType.MvvmCrossAndXamarinForms,
                this.SettingsService.TestingFramework,
                nugetCommands,
                preSelect,
                projectSuffix,
                projectType);

            return projectTemplateInfo;
        }
    }
}
