// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseProjectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Extensions;
    using Scorchio.Infrastructure.Constants;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the BaseProjectFactory type.
    /// </summary>
    public abstract class BaseProjectFactory
    {
        /// <summary>
        /// Prevents a default instance of the <see cref="BaseProjectFactory"/> class from being created.
        /// </summary>
        protected BaseProjectFactory()
        {
            this.ProjectTemplateInfos = new List<ProjectTemplateInfo>();
        }

        /// <summary>
        /// Gets or sets the project template infos.
        /// </summary>
        protected List<ProjectTemplateInfo> ProjectTemplateInfos { get; set; }

        /// <summary>
        /// Adds the project if.
        /// </summary>
        /// <param name="condition">if set to <c>true</c> [condition].</param>
        /// <param name="projectTemplateInfo">The project template information.</param>
        protected void AddProjectIf(
            bool condition,
            ProjectTemplateInfo projectTemplateInfo)
        {
            if (condition)
            {
                this.ProjectTemplateInfos.Add(projectTemplateInfo);
            }
        }

        /// <summary>
        /// Gets the core tests project.
        /// </summary>
        /// <param name="frameworkType">Type of the framework.</param>
        /// <param name="testingFramework">The testing framework.</param>
        /// <param name="nugetCommands">The nuget commands.</param>
        /// <param name="preSelect">if set to <c>true</c> [pre select].</param>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <returns>
        /// A unit tests project.
        /// </returns>
        protected ProjectTemplateInfo GetTestsProject(
            FrameworkType frameworkType,
            string testingFramework,
            IEnumerable<string> nugetCommands,
            bool preSelect,
            string projectSuffix,
            string projectType)
        {
            TraceService.WriteLine("BaseProjectFactory::GetTestsProject");

            string friendlyName = projectType;

            string templateName = ProjectTemplate.NUnitTests.GetDescription();

            if (testingFramework == TestingConstants.MsTest.Name)
            {
                templateName = ProjectTemplate.MsTestTests.GetDescription();
            }

            if (frameworkType.IsMvvmCrossSolutionType())
            {
                templateName = ProjectTemplate.NUnitTests.GetDescription();

                if (testingFramework == TestingConstants.MsTest.Name)
                {
                    templateName = ProjectTemplate.MsTestTests.GetDescription();
                }
            }

            TraceService.WriteLine("BaseProjectFactory::GetTestsProject End");

            return new ProjectTemplateInfo
            {
                FriendlyName = friendlyName,
                ProjectSuffix = projectSuffix,
                TemplateName = templateName,
                PreSelected = preSelect,
                ReferenceCoreProject = true,
                NugetCommands = nugetCommands
            };
        }

        /// <summary>
        /// Gets the platform tests project.
        /// </summary>
        /// <param name="frameworkType">Type of the framework.</param>
        /// <param name="testingFramework">The testing framework.</param>
        /// <param name="nugetCommands">The nuget commands.</param>
        /// <param name="preSelect">if set to <c>true</c> [pre select].</param>
        /// <param name="projectSuffix">The project suffix.</param>
        /// <param name="projectType">Type of the project.</param>
        protected ProjectTemplateInfo GetPlatFormTestsProject(
            FrameworkType frameworkType,
            string testingFramework,
            IEnumerable<string> nugetCommands,
            bool preSelect,
            string projectSuffix,
            string projectType)
        {
            TraceService.WriteLine("BaseProjectFactory::GetPlatFormTestsProject");

            ProjectTemplateInfo projectTemplateInfo = this.GetTestsProject(
                frameworkType, 
                testingFramework, 
                nugetCommands, 
                preSelect,
                projectSuffix, 
                projectType);

            projectTemplateInfo.ReferenceCoreProject = false;
            projectTemplateInfo.ReferencePlatformProject = true;

            TraceService.WriteLine("BaseProjectFactory::GetPlatFormTestsProject End");

            return projectTemplateInfo;
        }
    }
}
