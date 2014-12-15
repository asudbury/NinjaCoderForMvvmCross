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
    using Scorchio.VisualStudio.Services;
    using System;
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
            TraceService.WriteLine("NoFrameworkProjectFactory::Constructor");

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
            TraceService.WriteLine("NoFrameworkProjectFactory::GetAllowedProjects");

            List<ProjectTemplateInfo> projectInfos = new List<ProjectTemplateInfo>();

            if (this.visualStudioService.CoreProjectService == null)
            {
                projectInfos.Add(this.GetCoreProject());
            }

            if (this.visualStudioService.CoreTestsProjectService == null)
            {
                projectInfos.Add(this.GetTestsProject(
                    ProjectSuffix.CoreTests.GetDescription(),
                    ProjectType.CoreTests.GetDescription()));
            }

            if (this.visualStudioService.DroidProjectService == null)
            {
                projectInfos.Add(this.GetDroidProject());
            }

            if (this.settingsService.CreatePlatformTestProjects &&
                this.visualStudioService.DroidTestsProjectService == null)
            {
                TraceService.WriteLine("get droid test project");

                string projectSuffix = ProjectSuffix.DroidTests.GetDescription();
                string projectType = ProjectType.DroidTests.GetDescription();

                try
                {
                    ProjectTemplateInfo projectTemplateInfo = this.GetPlatFormTestsProject(
                           FrameworkType.NoFramework,
                           this.settingsService.TestingFramework,
                           this.nugetCommandsService.GetTestCommands(),
                           projectSuffix,
                           projectType);

                    projectInfos.Add(projectTemplateInfo);
                }
                catch (Exception exception)
                {
                    TraceService.WriteError("Error adding test project exception=" + exception.Message);
                }
            }

            if (this.visualStudioService.iOSProjectService == null)
            {
                projectInfos.Add(this.GetiOSProject());
            }

            if (this.settingsService.CreatePlatformTestProjects && 
                this.visualStudioService.iOSTestsProjectService == null)
            {
                projectInfos.Add(this.GetPlatFormTestsProject(
                      FrameworkType.NoFramework,
                      this.settingsService.TestingFramework,
                      this.nugetCommandsService.GetTestCommands(),
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
                projectInfos.Add(this.GetPlatFormTestsProject(
                      FrameworkType.NoFramework,
                      this.settingsService.TestingFramework,
                      this.nugetCommandsService.GetTestCommands(),
                      ProjectSuffix.WindowsPhoneTests.GetDescription(),
                      ProjectType.WindowsPhoneTests.GetDescription()));
            }

            TraceService.WriteLine("projectInfos Count=" + projectInfos.Count);
            
            return projectInfos;
        }

        /// <summary>
        /// Gets the core project.
        /// </summary>
        /// <returns>A core project.</returns>
        internal ProjectTemplateInfo GetCoreProject()
        {
            TraceService.WriteLine("NoFrameworkProjectFactory::GetCoreProject");

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
        /// <param name="projectSuffix">The project suffix.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <returns>
        /// A unit tests project.
        /// </returns>
        internal ProjectTemplateInfo GetTestsProject(
            string projectSuffix,
            string projectType)
        {
            TraceService.WriteLine("NoFrameworkProjectFactory::GetTestsProject");

            return this.GetTestsProject(
                FrameworkType.NoFramework,
                this.settingsService.TestingFramework,
                this.nugetCommandsService.GetTestCommands(),
                projectSuffix,
                projectType);
        }

        /// <summary>
        /// Gets the droid project.
        /// </summary>
        /// <returns>An android project.</returns>
        internal ProjectTemplateInfo GetDroidProject()
        {
            TraceService.WriteLine("NoFrameworkProjectFactory::GetDroidProject");

            return new ProjectTemplateInfo
            {
                FriendlyName = "Android",
                ProjectSuffix = ProjectSuffix.Droid.GetDescription(),
                TemplateName = ProjectTemplate.Droid.GetDescription(),
                ReferenceCoreProject = true,
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetNoFrameworksCommands()
            };
        }

        /// <summary>
        /// Gets the ios project.
        /// </summary>
        /// <returns>An iOs project.</returns>
        internal ProjectTemplateInfo GetiOSProject()
        {
            TraceService.WriteLine("NoFrameworkProjectFactory::GetiOSProject");

            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.iOS.GetDescription() + " (" + this.settingsService.iOSApiVersion + ")",
                ProjectSuffix = ProjectSuffix.iOS.GetDescription(),
                TemplateName = this.GetiOSProjectTemplate(this.settingsService.iOSApiVersion),
                ReferenceCoreProject = true,
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetNoFrameworksiOSCommands()
            };
        }

        /// <summary>
        /// Gets the windows phone project.
        /// </summary>
        /// <returns>A windows phone project.</returns>
        internal ProjectTemplateInfo GetWindowsPhoneProject()
        {
            TraceService.WriteLine("NoFrameworkProjectFactory::GetWindowsPhoneProject");

            return new ProjectTemplateInfo
            {
                FriendlyName = ProjectType.WindowsPhone.GetDescription() + " " + this.settingsService.WindowsPhoneBuildVersion,
                ProjectSuffix = ProjectSuffix.WindowsPhone.GetDescription(),
                TemplateName = ProjectTemplate.WindowsPhone.GetDescription(),
                ReferenceCoreProject = true,
                PreSelected = true,
                NugetCommands = this.nugetCommandsService.GetNoFrameworksCommands()
            };
        }
    }
}
