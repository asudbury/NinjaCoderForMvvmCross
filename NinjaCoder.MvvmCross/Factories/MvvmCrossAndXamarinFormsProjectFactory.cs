// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvvmCrossAndXamarinFormsProjectFactory type.
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
    ///  Defines the MvvmCrossAndXamarinFormsProjectFactory type. 
    /// </summary>
    public class MvvmCrossAndXamarinFormsProjectFactory : BaseProjectFactory, IMvvmCrossAndXamarinFormsProjectFactory
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
        /// Initializes a new instance of the <see cref="XamarinFormsProjectFactory"/> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="nugetCommandsService">The nuget commands service.</param>
        public MvvmCrossAndXamarinFormsProjectFactory(
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
                this.visualStudioService.XamarinFormsProjectService == null,
                this.GetFormsProject());

            this.AddProjectIf(
                this.visualStudioService.XamarinFormsTestsProjectService == null,
                this.GetFormsTestsProject());

            this.AddProjectIf(
                this.visualStudioService.iOSProjectService == null,
                this.GetiOSProject());

            this.AddProjectIf(
                (this.settingsService.CreatePlatformTestProjects &&
                this.visualStudioService.iOSTestsProjectService == null),
                this.GetPlatformTestsProject(
                    this.nugetCommandsService.GetiOSTestCommands(),
                    true,
                    ProjectSuffix.iOSTests.GetDescription(),
                    ProjectType.iOSTests.GetDescription()));

            this.AddProjectIf(
                this.visualStudioService.DroidProjectService == null,
                this.GetDroidProject());

            this.AddProjectIf(
                (this.settingsService.CreatePlatformTestProjects &&
                this.visualStudioService.DroidTestsProjectService == null),
                this.GetPlatformTestsProject(
                    this.nugetCommandsService.GetAndroidTestCommands(),
                    true,
                    ProjectSuffix.DroidTests.GetDescription(),
                    ProjectType.DroidTests.GetDescription()));

            this.AddProjectIf(
                this.visualStudioService.WindowsPhoneProjectService == null,
                this.GetWindowsPhoneProject());

            this.AddProjectIf(
                (this.settingsService.CreatePlatformTestProjects &&
                this.visualStudioService.WindowsPhoneTestsProjectService == null),
                this.GetPlatformTestsProject(
                    this.nugetCommandsService.GetTestCommands(),
                    true,
                    ProjectSuffix.WindowsPhoneTests.GetDescription(),
                    ProjectType.WindowsPhoneTests.GetDescription()));

            return this.ProjectTemplateInfos; 
        }

        /// <summary>
        /// Gets the core project.
        /// </summary>
        internal ProjectTemplateInfo GetCoreProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.Core.GetDescription() + " (Profile " + this.settingsService.PCLProfile + ")",
                ProjectSuffix = ProjectSuffix.Core.GetDescription(),
                TemplateName = ProjectTemplate.Core.GetDescription(),
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossXamarinFormsCoreCommands()
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
                this.settingsService.TestingFramework,
                this.nugetCommandsService.GetMvvmCrossTestsCommands(),
                this.settingsService.AddCoreTestsProject,
                ProjectSuffix.CoreTests.GetDescription(),
                ProjectType.CoreTests.GetDescription());
        }

        /// <summary>
        /// Gets the xamarin forms project.
        /// </summary>
        /// <returns>A xamarin forms project.</returns>
        internal ProjectTemplateInfo GetFormsProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.XamarinForms.GetDescription() + " (Profile " + this.settingsService.PCLProfile + ")",
                ProjectSuffix = ProjectSuffix.XamarinForms.GetDescription(),
                TemplateName = ProjectTemplate.XamarinForms.GetDescription(),
                PreSelected = true,
                ReferenceCoreProject = true,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossXamarinFormsCommands()
            };
        }
        
        /// <summary>
        /// Gets the forms tests project.
        /// </summary>
        /// <returns></returns>
        internal ProjectTemplateInfo GetFormsTestsProject()
        {
            ProjectTemplateInfo projectTemplateInfo = this.GetTestsProject(
                FrameworkType.XamarinForms,
                this.settingsService.TestingFramework,
                this.nugetCommandsService.GetTestCommands(),
                this.settingsService.AddXamarinFormsTestsProject,
                ProjectSuffix.XamarinFormsTests.GetDescription(),
                ProjectType.XamarinFormsTests.GetDescription());

            //// TODO : lets do this properly :-)
            projectTemplateInfo.ReferenceCoreProject = false;
            projectTemplateInfo.ReferenceXamarinFormsProject = true;

            if (this.settingsService.CreatePlatformTestProjects == false)
            {
                projectTemplateInfo.PreSelected = false;
            }

            return projectTemplateInfo;
        }

        /// <summary>
        /// Gets the ios project.
        /// </summary>
        /// <returns>an iOS project.</returns>
        internal ProjectTemplateInfo GetiOSProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.iOS.GetDescription(),
                ProjectSuffix = ProjectSuffix.iOS.GetDescription(),
                TemplateName = ProjectTemplate.iOS.GetDescription(),
                PreSelected = this.settingsService.AddiOSProject,
                ReferenceCoreProject = true,
                ReferenceXamarinFormsProject = true,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossXamarinFormsiOSCommands()
            };
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
                ReferenceXamarinFormsProject = true,
                PreSelected = this.settingsService.AddAndroidProject,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossXamarinFormDroidCommands()
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
                ReferenceXamarinFormsProject = true,
                PreSelected = this.settingsService.AddWindowsPhoneProject,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossXamarinFormsWindowsPhoneCommands()
            };
        }

        /// <summary>
        /// Gets the core tests project.
        /// </summary>
        /// <param name="nugetCommands">The nuget commands.</param>
        /// <param name="preSelect">if set to <c>true</c> [pre select].</param>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <returns>
        /// A unit tests project.
        /// </returns>
        internal ProjectTemplateInfo GetPlatformTestsProject(
            IEnumerable<string> nugetCommands,
            bool preSelect,
            string projectSuffix,
            string projectType)
        {
            ProjectTemplateInfo projectTemplateInfo = this.GetPlatFormTestsProject(
                FrameworkType.MvvmCrossAndXamarinForms,
                this.settingsService.TestingFramework,
                nugetCommands,
                preSelect,
                projectSuffix,
                projectType);

            return projectTemplateInfo;
        }
    }
}
