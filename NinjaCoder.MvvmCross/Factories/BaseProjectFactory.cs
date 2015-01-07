// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseProjectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using NinjaCoder.MvvmCross.Entities;
    using Scorchio.Infrastructure.Constants;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;
    using Scorchio.VisualStudio.Services;

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
            
            if (frameworkType == FrameworkType.MvvmCross ||
                frameworkType == FrameworkType.MvvmCrossAndXamarinForms)
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
        /// <param name="projectSuffix">The project suffix.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Getis the ios project template.
        /// </summary>
        /// <param name="iOSApiVersion">The i os API version.</param>
        /// <returns></returns>
        protected string GetiOSProjectTemplate(string iOSApiVersion)
        {
            TraceService.WriteLine("BaseProjectFactory::GetiOSProjectTemplate");

            if (iOSApiVersion == "Unified")
            {
                return ProjectTemplate.iOSUnified.GetDescription();   
            }
            
            return ProjectTemplate.iOSClassic.GetDescription();
        }
    }
}
