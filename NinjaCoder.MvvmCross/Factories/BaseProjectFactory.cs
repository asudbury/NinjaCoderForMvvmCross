// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseProjectFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using Entities;
    using Extensions;
    using Scorchio.Infrastructure.Constants;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.VisualStudio.Entities;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Linq;
    using Translators.Interfaces;

    /// <summary>
    /// Defines the BaseProjectFactory type.
    /// </summary>
    public abstract class BaseProjectFactory
    {
        /// <summary>
        /// The dictionary.
        /// </summary>
        private readonly Dictionary<string, IEnumerable<ProjectTemplateInfo>> dictionary;
         
        /// <summary>
        /// The settings service.
        /// </summary>
        protected readonly ISettingsService SettingsService;

        /// <summary>
        /// The translator.
        /// </summary>
        private readonly IProjectTemplatesTranslator translator;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseProjectFactory" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="translator">The translator.</param>
        protected BaseProjectFactory(
            ISettingsService settingsService,
            IProjectTemplatesTranslator translator)
        {
            this.SettingsService = settingsService;
            this.translator = translator;
            this.ProjectTemplateInfos = new List<ProjectTemplateInfo>();
            this.dictionary = new Dictionary<string, IEnumerable<ProjectTemplateInfo>>();
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
            ProjectTemplateInfo projectTemplateInfo = this.GetTestsProject(
                frameworkType, 
                testingFramework, 
                nugetCommands, 
                preSelect,
                projectSuffix, 
                projectType);

            projectTemplateInfo.ReferenceCoreProject = false;
            projectTemplateInfo.ReferencePlatformProject = true;

            return projectTemplateInfo;
        }

        /// <summary>
        /// Gets the project items.
        /// </summary>
        /// <param name="frameworkType">Type of the framework.</param>
        /// <param name="projectType">Type of the project.</param>
        /// <returns>A list of TextTemplateInfos.</returns>
        protected IEnumerable<TextTemplateInfo> GetProjectItems(
            FrameworkType frameworkType,
            ProjectType projectType)
        {
            string uri = string.Empty;

            switch (frameworkType)
            {
                case FrameworkType.NoFramework:
                    uri = this.SettingsService.NoFrameworkProjectsUri;
                    break;

                case FrameworkType.MvvmCross:
                    uri = this.SettingsService.MvvmCrossProjectsUri;
                    break;

                case FrameworkType.XamarinForms:
                    uri = this.SettingsService.XamarinFormsProjectsUri;
                    break;

                case FrameworkType.MvvmCrossAndXamarinForms:
                    uri = this.SettingsService.MvvmCrossAndXamarinFormsProjectsUri;
                    break;
            }

            if (uri != string.Empty)
            {
                IEnumerable<ProjectTemplateInfo> projectTemplateInfos = this.GetPojectTemplateInfos(uri);

                ProjectTemplateInfo projectTemplateInfo = projectTemplateInfos.FirstOrDefault(x => x.Name == projectType.ToString());

                if (projectTemplateInfo != null)
                {
                    return projectTemplateInfo.ItemTemplates;
                }
            }

            return new List<TextTemplateInfo>();
        }

        /// <summary>
        /// Gets the poject template infos.
        /// </summary>
        /// <param name="uri">The URI.</param>
        /// <returns>The project template infos.</returns>
        internal IEnumerable<ProjectTemplateInfo> GetPojectTemplateInfos(string uri)
        {
            IEnumerable<ProjectTemplateInfo> projectTemplateInfos;

            bool hasValue = this.dictionary.TryGetValue(uri, out projectTemplateInfos);

            if (hasValue)
            {
                return projectTemplateInfos;
            }

            projectTemplateInfos = this.translator.Translate(uri);

            this.dictionary.Add(uri, projectTemplateInfos);

            return projectTemplateInfos;
        }
    }
}
