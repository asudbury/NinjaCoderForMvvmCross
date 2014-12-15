// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the XamarinFormsProjectFactory type.
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

            if (this.visualStudioService.XamarinFormsProjectService == null)
            {
                projectInfos.Add(this.GetFormsProject());
            }

            if (this.visualStudioService.XamarinFormsTestsProjectService == null)
            {
                projectInfos.Add(this.GetFormsTestsProject());
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

            return projectInfos;
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
                FriendlyName = ProjectType.iOS.GetDescription() + " (" + this.settingsService.iOSApiVersion + ")",
                ProjectSuffix = ProjectSuffix.iOS.GetDescription(),
                TemplateName = this.GetiOSProjectTemplate(this.settingsService.iOSApiVersion),
                PreSelected = true,
                ReferenceXamarinFormsProject = true,
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
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetXamarinFormsCommands()
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
                FrameworkType.XamarinForms,
                this.settingsService.TestingFramework,
                this.nugetCommandsService.GetTestCommands(),
                projectSuffix,
                projectType);
        }
    }
}

