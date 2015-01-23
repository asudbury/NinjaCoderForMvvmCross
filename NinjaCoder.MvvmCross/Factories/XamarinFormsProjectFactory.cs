// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the XamarinFormsProjectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Factories
{
    using System;

    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.Infrastructure.Extensions;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the XamarinFormsProjectFactory type. 
    /// </summary>
    public class XamarinFormsProjectFactory : BaseProjectFactory, IXamarinFormsProjectFactory
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
        public XamarinFormsProjectFactory(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            INugetCommandsService nugetCommandsService)
        {
            TraceService.WriteLine("XamarinFormsProjectFactory::Constructor");

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
            TraceService.WriteLine("XamarinFormsProjectFactory::GetAllowedProjects");

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
                this.visualStudioService.DroidProjectService == null,
                this.GetDroidProject());

            this.AddProjectIf(
                (this.settingsService.CreatePlatformTestProjects &&
                this.visualStudioService.DroidTestsProjectService == null),
                this.GetPlatformTestsProject(
                true,
                    ProjectSuffix.DroidTests.GetDescription(),
                    ProjectType.DroidTests.GetDescription()));

            this.AddProjectIf(
                this.visualStudioService.iOSProjectService == null,
                this.GetiOSProject());

            this.AddProjectIf(
                (this.settingsService.CreatePlatformTestProjects &&
                this.visualStudioService.iOSTestsProjectService == null),
                this.GetPlatformTestsProject(
                    true,
                    ProjectSuffix.iOSTests.GetDescription(),
                    ProjectType.iOSTests.GetDescription()));

            this.AddProjectIf(
                this.visualStudioService.WindowsPhoneProjectService == null,
                this.GetWindowsPhoneProject());

            this.AddProjectIf(
                (this.settingsService.CreatePlatformTestProjects &&
                this.visualStudioService.WindowsPhoneTestsProjectService == null),
                this.GetPlatformTestsProject(
                    true,
                    ProjectSuffix.WindowsPhoneTests.GetDescription(),
                    ProjectType.WindowsPhoneTests.GetDescription()));

            if (this.settingsService.BetaTesting)
            { 
                this.AddProjectIf(
                    this.visualStudioService.WpfProjectService == null,
                    this.GetWindowsWpfProject());

                this.AddProjectIf(
                    (this.settingsService.CreatePlatformTestProjects &&
                    this.visualStudioService.WpfTestsProjectService == null),
                    this.GetPlatformTestsProject(
                        false,
                        ProjectSuffix.WpfTests.GetDescription(),
                        ProjectType.WindowsWpfTests.GetDescription()));
            }

            return this.ProjectTemplateInfos;
        }

        /// <summary>
        /// Gets the xamarim forms core project.
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
                NugetCommands = this.nugetCommandsService.GetXamarinFormsCoreCommands()
            };
        }

        /// <summary>
        /// Gets the core tests project.
        /// </summary>
        /// <returns>A unit tests project.</returns>
        internal ProjectTemplateInfo GetCoreTestsProject()
        {
            return this.GetTestsProject(
                FrameworkType.XamarinForms,
                this.settingsService.TestingFramework,
                this.nugetCommandsService.GetTestCommands(),
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
                NugetCommands = this.nugetCommandsService.GetXamarinFormsCommands()
            };
        }

        /// <summary>
        /// Gets the forms tests project.
        /// </summary>
        /// <returns></returns>
        internal ProjectTemplateInfo GetFormsTestsProject()
        {
            ProjectTemplateInfo projectTemplateInfo =  this.GetTestsProject(
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
        /// Gets the droid project.
        /// </summary>
        /// <returns>A android project.</returns>
        internal ProjectTemplateInfo GetDroidProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = "Android",
                ProjectSuffix = ProjectSuffix.Droid.GetDescription(),
                TemplateName = ProjectTemplate.Droid.GetDescription(),
                PreSelected = true,
                ReferenceXamarinFormsProject = true,
                ReferenceCoreProject = true,
                NugetCommands = this.nugetCommandsService.GetXamarinFormsCommands()
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
                PreSelected = true,
                ReferenceXamarinFormsProject = true,
                ReferenceCoreProject = true,
                NugetCommands = this.nugetCommandsService.GetXamarinFormsiOSCommands()
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
                ReferenceXamarinFormsProject = true,
                ReferenceCoreProject = true,
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetXamarinFormsCommands()
            };
        }

        /// <summary>
        /// Gets the windows WPF project.
        /// </summary>
        /// <returns></returns>
        internal ProjectTemplateInfo GetWindowsWpfProject()
        {
            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.WindowsWpf.GetDescription(),
                ProjectSuffix = ProjectSuffix.Wpf.GetDescription(),
                TemplateName = ProjectTemplate.Wpf.GetDescription(),
                ReferenceXamarinFormsProject = true,
                ReferenceCoreProject = true,
                PreSelected = false,
                NugetCommands = this.nugetCommandsService.GetXamarinFormsWpfCommands()
            };
        }

        /// <summary>
        /// Gets the core tests project.
        /// </summary>
        /// <param name="preSelect">if set to <c>true</c> [pre select].</param>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <returns>
        /// A unit tests project.
        /// </returns>
        internal ProjectTemplateInfo GetPlatformTestsProject(
            bool preSelect,
            string projectSuffix,
            string projectType)
        {
            ProjectTemplateInfo projectTemplateInfo = this.GetPlatFormTestsProject(
                FrameworkType.XamarinForms,
                this.settingsService.TestingFramework,
                this.nugetCommandsService.GetTestCommands(),
                preSelect,
                projectSuffix,
                projectType);

            return projectTemplateInfo;
        }
    }
}

