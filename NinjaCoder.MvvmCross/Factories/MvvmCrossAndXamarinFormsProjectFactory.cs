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
                this.visualStudioService.DroidProjectService == null,
                this.GetDroidProject());

            this.AddProjectIf(
                this.visualStudioService.WindowsPhoneProjectService == null,
                this.GetWindowsPhoneProject());

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
                TemplateName = MvvmCrossProjectTemplate.Core.GetDescription(),
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
                true,
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
                true,
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
                TemplateName =  ProjectTemplate.iOS.GetDescription(),
                PreSelected = true,
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
                TemplateName = MvvmCrossProjectTemplate.Droid.GetDescription(),
                ReferenceCoreProject = true,
                ReferenceXamarinFormsProject = true,
                PreSelected = true,
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
                TemplateName = MvvmCrossProjectTemplate.WindowsPhone.GetDescription(),
                ReferenceCoreProject = true,
                ReferenceXamarinFormsProject = true,
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetMvvmCrossXamarinFormsWindowsPhoneCommands()
            };
        }
    }
}


