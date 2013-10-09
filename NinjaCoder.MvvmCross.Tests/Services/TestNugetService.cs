// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestNugetService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Services
{
    using System.Collections.Generic;

    using Moq;
    using MvvmCross.Services;
    using MvvmCross.Services.Interfaces;
    using NUnit.Framework;

    using Scorchio.VisualStudio;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the TestNugetService type.
    /// </summary>
    [TestFixture]
    public class TestNugetService
    {
        /// <summary>
        /// The service.
        /// </summary>
        private NugetService service;

        /// <summary>
        /// The mock visual studio service.
        /// </summary>
        private Mock<IVisualStudioService> mockVisualStudioService;
        
        /// <summary>
        /// The mock DTE service
        /// </summary>
        private Mock<IDTEService> mockDTEService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockVisualStudioService = new Mock<IVisualStudioService>();
            this.mockDTEService = new Mock<IDTEService>();

            this.mockVisualStudioService.SetupGet(x => x.DTEService).Returns(this.mockDTEService.Object);

            this.service = new NugetService();
        }

        /// <summary>
        /// Tests the get nuget commands.
        /// </summary>
        [Test]
        public void TestGetNugetCommands()
        {
            List<ProjectTemplateInfo> templateInfos = new List<ProjectTemplateInfo>
            {
                new ProjectTemplateInfo
                    {
                        NugetCommands = new List<string> { "1", "2" }
                    },
                 new ProjectTemplateInfo
                    {
                        NugetCommands = new List<string> { "3", "4" }
                    },
            };

            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();

            this.mockVisualStudioService.Setup(x => x.GetProjectServiceBySuffix(It.IsAny<string>())).Returns(mockProjectService.Object);

            string commands = this.service.GetNugetCommands(this.mockVisualStudioService.Object, templateInfos);

            Assert.IsTrue(commands == "1\r\n2\r\n3\r\n4\r\n");
        }

        /// <summary>
        /// Tests the open nuget window.
        /// </summary>
        [Test]
        public void TestOpenNugetWindow()
        {
            this.service.OpenNugetWindow(this.mockVisualStudioService.Object);

            this.mockDTEService.Verify(x => x.ExecuteNugetCommand(It.IsAny<string>()));
        }

        /// <summary>
        /// Tests the execute.
        /// </summary>
        [Test]
        public void TestExecute()
        {
            this.service.Execute(
                this.mockVisualStudioService.Object,
                string.Empty,
                string.Empty, 
                false,
                false);

            this.mockDTEService.Verify(x => x.ExecuteNugetCommand(It.IsAny<string>()));
        }

        /// <summary>
        /// Completed the nuget updates.
        /// </summary>
        [Test]
        public void TestNugetCompleted()
        {
            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();

            this.mockVisualStudioService.SetupGet(x => x.CoreTestsProjectService).Returns(mockProjectService.Object);

            Mock<IProjectItemService> mockProjectItemService = new Mock<IProjectItemService>();
            mockProjectItemService.SetupGet(x => x.Kind).Returns(VSConstants.VsProjectItemKindPhysicalFolder);
            this.service.AddProjectItemToDeleteList(mockProjectItemService.Object);

            Mock<IProjectItemService> mockProjectItemService2 = new Mock<IProjectItemService>();
            mockProjectItemService.SetupGet(x => x.Kind).Returns(string.Empty);
            this.service.AddProjectItemToDeleteList(mockProjectItemService2.Object);
            
            this.service.VisualStudioService = this.mockVisualStudioService.Object;

            this.service.NugetCompleted();
        }

        /// <summary>
        /// Tests the fix test project.
        /// </summary>
        [Test]
        public void TestFixTestProject()
        {
            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();

            this.mockVisualStudioService.SetupGet(x => x.CoreTestsProjectService).Returns(mockProjectService.Object);

            this.service.VisualStudioService = this.mockVisualStudioService.Object;

            this.service.FixTestProject();

            mockProjectService.Verify(x => x.RemoveReference(It.IsAny<string>()));
        }
    }
}
