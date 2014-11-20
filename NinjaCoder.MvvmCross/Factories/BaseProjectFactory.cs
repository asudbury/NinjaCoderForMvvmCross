// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseProjectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using System.Collections.Generic;

    using NinjaCoder.MvvmCross.Entities;

    using Scorchio.Infrastructure.Constants;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    /// Defines the BaseProjectFactory type.
    /// </summary>
    public abstract class BaseProjectFactory
    {
        /// <summary>
        /// Gets the core tests project.
        /// </summary>
        /// <param name="frameworkType">Type of the framework.</param>
        /// <param name="testingFramework">The testing framework.</param>
        /// <param name="mockingFramework">The mocking framework.</param>
        /// <param name="nugetCommands">The nuget commands.</param>
        /// <returns>
        /// A unit tests project.
        /// </returns>
        protected ProjectTemplateInfo GetCoreTestsProject(
            FrameworkType frameworkType,
            string testingFramework,
            string mockingFramework,
            IEnumerable<string> nugetCommands)
        {
            string friendlyName = ProjectType.CoreTests.GetDescription() + " (" + testingFramework + " and " + mockingFramework + ")";

            string templateName = ProjectTemplate.NUnitTests.GetDescription();

            if (testingFramework == TestingConstants.MsTest.Name)
            {
                templateName = ProjectTemplate.MsTestTests.GetDescription();   
            }
            
            if (frameworkType == FrameworkType.MvvmCross ||
                frameworkType == FrameworkType.MvvmCrossAndXamarinForms)
            {
                templateName = MvvmCrossProjectTemplate.NUnitTests.GetDescription();

                if (testingFramework == TestingConstants.MsTest.Name)
                {
                    templateName = MvvmCrossProjectTemplate.MsTestTests.GetDescription();
                }
            }

            return new ProjectTemplateInfo
            {
                FriendlyName = friendlyName,
                ProjectSuffix = ProjectSuffix.CoreTests.GetDescription(),
                TemplateName = templateName,
                PreSelected = true,
                ReferenceCoreProject = true,
                NugetCommands = nugetCommands
            };
        }
    }
}
