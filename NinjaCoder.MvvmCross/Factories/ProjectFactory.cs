// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using System.Collections.Generic;

    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.Infrastructure.Constants;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the ProjectFactory type.
    /// </summary>
    public class ProjectFactory : IProjectFactory
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
        /// Initializes a new instance of the <see cref="ProjectFactory" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        public ProjectFactory(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService)
        {
            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Gets the allowed projects.
        /// </summary>
        /// <returns>The allowed projects.</returns>
        public IEnumerable<ProjectTemplateInfo> GetAllowedProjects()
        {
            List<ProjectTemplateInfo> projectInfos = new List<ProjectTemplateInfo>();

            if (this.visualStudioService.CoreProjectService == null)
            {
                projectInfos.Add(new ProjectTemplateInfo
                {
                    FriendlyName = FriendlyNames.Core + " (Profile " + this.settingsService.PCLProfile + ")",
                    ProjectSuffix = ProjectSuffixes.Core,
                    TemplateName = ProjectTemplates.Core,
                    PreSelected = true,
                    NugetCommands = new List<string> 
                            {
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMvvmCrossPackage),
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMessengerPackage)
                            }
                });
            }

            if (this.visualStudioService.CoreTestsProjectService == null)
            {
                string testingFrameWork = this.settingsService.TestingFramework;
                string mockingFrameWork = this.settingsService.MockingFramework;

                string friendlyName = FriendlyNames.CoreTests + " (" + testingFrameWork + " and " + mockingFrameWork + ")";
                string templateName = ProjectTemplates.NUnitTests;

                if (testingFrameWork == TestingConstants.MsTest.Name)
                {
                    templateName = ProjectTemplates.MsTestTests;
                }

                projectInfos.Add(new ProjectTemplateInfo
                {
                    FriendlyName = friendlyName,
                    ProjectSuffix = ProjectSuffixes.CoreTests,
                    TemplateName = templateName,
                    PreSelected = true,
                    NugetCommands = new List<string> 
                            {
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetUnitTestsPackage),
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMvvmCrossPackage)
                            },
                    NonMvxAssemblies = new List<string> 
                    { 
                        "Moq",
                        "NUnit",
                        "Rhino.Mocks"
                    }
                });
            }

            if (this.visualStudioService.DroidProjectService == null)
            {
                projectInfos.Add(new ProjectTemplateInfo
                {
                    FriendlyName = FriendlyNames.Droid,
                    ProjectSuffix = ProjectSuffixes.Droid,
                    TemplateName = ProjectTemplates.Droid,
                    NugetCommands = new List<string> 
                            {
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMvvmCrossPackage),
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMessengerPackage)
                            }
                });
            }

            if (this.visualStudioService.iOSProjectService == null)
            {
                projectInfos.Add(new ProjectTemplateInfo
                {
                    FriendlyName = FriendlyNames.iOS + this.settingsService.iOSBuildVersion,
                    ProjectSuffix = ProjectSuffixes.iOS,
                    TemplateName = ProjectTemplates.IOS,
                    NugetCommands = new List<string> 
                            {
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMvvmCrossPackage),
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMessengerPackage)
                            }
                });
            }

            if (this.visualStudioService.WindowsPhoneProjectService == null)
            {
                projectInfos.Add(new ProjectTemplateInfo
                {
                    FriendlyName = FriendlyNames.WindowsPhone + this.settingsService.WindowsPhoneBuildVersion,
                    ProjectSuffix = ProjectSuffixes.WindowsPhone,
                    TemplateName = ProjectTemplates.WindowsPhone,
                    NugetCommands = new List<string> 
                            {
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMvvmCrossPackage),
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMessengerPackage)
                            }
                });
            }

            if (this.visualStudioService.WindowsStoreProjectService == null)
            {
                projectInfos.Add(new ProjectTemplateInfo
                {
                    FriendlyName = FriendlyNames.WindowsStore,
                    ProjectSuffix = ProjectSuffixes.WindowsStore,
                    TemplateName = ProjectTemplates.WindowsStore,
                    NugetCommands = new List<string> 
                            {
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMvvmCrossPackage),
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMessengerPackage)
                            }
                });
            }

            if (this.visualStudioService.WpfProjectService == null)
            {
                projectInfos.Add(new ProjectTemplateInfo
                {
                    FriendlyName = FriendlyNames.WindowsWpf,
                    ProjectSuffix = ProjectSuffixes.WindowsWpf,
                    TemplateName = ProjectTemplates.WindowsWPF,
                    NugetCommands = new List<string> 
                            {
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMvvmCrossPackage)
                            }
                });
            }

            return projectInfos;
        }
    }
}
