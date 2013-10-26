// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestProjectsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Controllers
{
    using System.Runtime.InteropServices;

    using Castle.DynamicProxy.Generators;

    using EnvDTE;

    using Moq;
    using NinjaCoder.MvvmCross.Controllers;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NUnit.Framework;

    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the TestProjectsController type.
    /// </summary>
    [TestFixture]
    public class TestProjectsController
    {
        /// <summary>
        /// The controller.
        /// </summary>
        private ProjectsController controller;

        /// <summary>
        /// The mock projects service.
        /// </summary>
        private Mock<IProjectsService> mockProjectsService;

        /// <summary>
        /// The mock nuget service.
        /// </summary>
        private Mock<INugetService> mockNugetService;

        /// <summary>
        /// The mock visual studio service.
        /// </summary>
        private Mock<IVisualStudioService> mockVisualStudioService;

        /// <summary>
        /// The mock read me service.
        /// </summary>
        private Mock<IReadMeService> mockReadMeService;

        /// <summary>
        /// The mock settings service.
        /// </summary>
        private Mock<ISettingsService> mockSettingsService;
        
        /// <summary>
        /// The mock project service.
        /// </summary>
        private Mock<IProjectService> mockProjectService;

        /// <summary>
        /// The mock project,
        /// </summary>
        private Mock<Project> mockProject;

        /// <summary>
        /// The mock message box service.
        /// </summary>
        private Mock<IMessageBoxService> mockMessageBoxService;

        /// <summary>
        /// The mock dialog service.
        /// </summary>
        private Mock<IDialogService> mockDialogService;

        /// <summary>
        /// The mock forms service.
        /// </summary>
        private Mock<IFormsService> mockFormsService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            AttributesToAvoidReplicating.Add<TypeIdentifierAttribute>();

            this.mockProjectsService = new Mock<IProjectsService>();
            this.mockNugetService = new Mock<INugetService>();
            this.mockVisualStudioService = new Mock<IVisualStudioService>();
            this.mockReadMeService = new Mock<IReadMeService>();
            this.mockSettingsService = new Mock<ISettingsService>();
            this.mockMessageBoxService = new Mock<IMessageBoxService>();
            this.mockDialogService = new Mock<IDialogService>();
            this.mockFormsService = new Mock<IFormsService>();

            this.controller = new ProjectsController(
                this.mockProjectsService.Object,
                this.mockNugetService.Object,
                this.mockVisualStudioService.Object,
                this.mockReadMeService.Object,
                this.mockSettingsService.Object,
                this.mockMessageBoxService.Object,
                this.mockDialogService.Object,
                this.mockFormsService.Object);

            //// setup the project service and core project once!
            this.mockProjectService = new Mock<IProjectService>();
            this.mockProject = new Mock<Project>();

            this.mockProjectService.SetupGet(x => x.Project).Returns(this.mockProject.Object);

            this.mockProjectService.SetupGet(x => x.Name).Returns("Hello.Core");

            this.mockVisualStudioService.SetupGet(x => x.CoreProjectService).Returns(this.mockProjectService.Object);
        }

        /// <summary>
        /// Tests the run.
        /// </summary>
        [Test]
        public void TestRun()
        {
            Mock<IDTEService> mockDTEService = new Mock<IDTEService>();
            this.mockVisualStudioService.SetupGet(x => x.DTEService).Returns(mockDTEService.Object);

            mockDTEService.Setup(x => x.GetDefaultProjectsLocation()).Returns(@"c:\temp\");

            this.controller.Run();
        }
    }
}
