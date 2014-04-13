// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestProjectsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Services
{
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using Moq;

    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.Tests.Mocks;
    using NUnit.Framework;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the TestProjectsService type.
    /// </summary>
    [TestFixture]
    public class TestProjectsService
    {
        /// <summary>
        /// The service.
        /// </summary>
        private ProjectsService service;

        /// <summary>
        /// The mock settings service.
        /// </summary>
        private Mock<ISettingsService> mockSettingsService;

        /// <summary>
        /// The mock file system.
        /// </summary>
        private Mock<IFileSystem> mockFileSystem;

        /// <summary>
        /// The mock visual studio service.
        /// </summary>
        private Mock<IVisualStudioService> mockVisualStudioService;

        /// <summary>
        /// The mock solution service.
        /// </summary>
        private Mock<ISolutionService> mockSolutionService;

        /// <summary>
        /// The mock project service.
        /// </summary>
        private Mock<IProjectService> mockProjectService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockSettingsService = new Mock<ISettingsService>();
            this.mockFileSystem = new Mock<IFileSystem>();
            this.mockVisualStudioService = new Mock<IVisualStudioService>();
            this.mockSolutionService = new Mock<ISolutionService>();
            this.mockProjectService = new Mock<IProjectService>();

            this.mockFileSystem.SetupGet(x => x.Directory).Returns(new MockDirectory());

            this.mockVisualStudioService.SetupGet(x => x.SolutionService).Returns(this.mockSolutionService.Object);
            
            this.mockSolutionService.Setup(x => x.GetProjectService(It.IsAny<string>())).Returns(this.mockProjectService.Object);
            
            this.service = new ProjectsService(
                this.mockSettingsService.Object,
                this.mockFileSystem.Object);
        }

        /// <summary>
        /// Tests the add projects method.
        /// </summary>
        [Test]
        public void TestAddProjects()
        {
            List<ProjectTemplateInfo> infos = new List<ProjectTemplateInfo>
                                                  {
                                                      new ProjectTemplateInfo
                                                          {
                                                              FriendlyName = "FriendlyName",
                                                              Name = "Name"
                                                          }
                                                  };

            this.service.AddProjects(
                this.mockVisualStudioService.Object,
                "path", 
                infos,
                true,
                false);

            //// check we have added the project to the solution.
            this.mockSolutionService.Verify(
                x => x.AddProjectToSolution(
                    It.IsAny<string>(),
                    It.IsAny<string>(), 
                    It.IsAny<string>()));
        }

        /// <summary>
        /// Tests the try to add project.
        /// </summary>
        [Test]
        public void TestTryToAddProject()
        {
            this.service.TryToAddProject("path", true, new ProjectTemplateInfo(), mockProjectService.Object);
        }
    }
}
