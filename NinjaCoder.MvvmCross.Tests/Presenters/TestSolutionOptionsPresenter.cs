// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestSolutionOptionsPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Presenters
{
    using System.Collections.Generic;

    using Moq;

    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Presenters;
    using NinjaCoder.MvvmCross.Views.Interfaces;
    using NUnit.Framework;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the TestSolutionOptionsPresenter type.
    /// </summary>
    [TestFixture]
    public class TestSolutionOptionsPresenter
    {
        /// <summary>
        /// The presenter.
        /// </summary>
        private ProjectsPresenter presenter;

        /// <summary>
        /// The mock view.
        /// </summary>
        private Mock<IProjectsView> mockView;

        /// <summary>
        /// The mock settings service.
        /// </summary>
        private Mock<ISettingsService> mockSettingsService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockView = new Mock<IProjectsView>();
            this.mockSettingsService = new Mock<ISettingsService>();

            this.presenter = new ProjectsPresenter(
                this.mockView.Object,
                this.mockSettingsService.Object);
        }

        /// <summary>
        /// Tests the get solution path.
        /// </summary>
        [Test]
        public void TestGetSolutionPath()
        {
            this.mockView.SetupGet(x => x.Path).Returns("path");
            this.mockView.SetupGet(x => x.ProjectName).Returns("projectName");

            string path = this.presenter.GetSolutionPath();

            Assert.IsTrue(path == "pathprojectName");
        }

        /// <summary>
        /// Tests the load.
        /// </summary>
        [Test]
        public void TestLoad()
        {
            List<ProjectTemplateInfo> templateInfos = new List<ProjectTemplateInfo>();

            this.presenter.Load("defaultProjectsLocation", "defaultProjectName", templateInfos);
        }

        /// <summary>
        /// Tests the get required templates.
        /// </summary>
        [Test]
        public void TestGetRequiredTemplates()
        {
            List<ProjectTemplateInfo> templateInfos = new List<ProjectTemplateInfo>();

            this.mockView.SetupGet(x => x.RequiredProjects).Returns(templateInfos);

            this.presenter.GetRequiredTemplates();
        }

        /// <summary>
        /// Tests the get required template.
        /// </summary>
        [Test]
        public void TestGetRequiredTemplate()
        {
            ProjectTemplateInfo templateInfo = new ProjectTemplateInfo();
            templateInfo.NugetCommands = new List<string> { "testCommand" };

            this.presenter.GetRequiredTemplate("projectName", templateInfo);

            string command = templateInfo.NugetCommands[0];

            Assert.IsTrue(command == "testCommand projectName");
        }
    }
}
