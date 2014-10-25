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
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NUnit.Framework;

    using Scorchio.Infrastructure.Services;
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
        /// The mock configuration service.
        /// </summary>
        private Mock<IConfigurationService> mockConfigurationService;

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
        /// The mocking service factory.
        /// </summary>
        private Mock<IMockingServiceFactory> mockingServiceFactory;

        /// <summary>
        /// The mock message box service.
        /// </summary>
        private Mock<IMessageBoxService> mockMessageBoxService;

        /// <summary>
        /// The mock resolver service.
        /// </summary>
        private Mock<IResolverService> mockResolverService;

        /// <summary>
        /// The mock view model views service.
        /// </summary>
        private Mock<IViewModelViewsService> mockViewModelViewsService;

        /// <summary>
        /// The mock read me service.
        /// </summary>
        private Mock<IReadMeService> mockReadMeService;

        /// <summary>
        /// The mock view model and views factory.
        /// </summary>
        private Mock<IViewModelAndViewsFactory> mockViewModelAndViewsFactory;
        
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            AttributesToAvoidReplicating.Add<TypeIdentifierAttribute>();

            this.mockConfigurationService = new Mock<IConfigurationService>();
            this.mockProjectsService = new Mock<IProjectsService>();
            this.mockNugetService = new Mock<INugetService>();
            this.mockVisualStudioService = new Mock<IVisualStudioService>();
            this.mockSettingsService = new Mock<ISettingsService>();
            this.mockMessageBoxService = new Mock<IMessageBoxService>();
            this.mockingServiceFactory = new Mock<IMockingServiceFactory>();
            this.mockViewModelViewsService = new Mock<IViewModelViewsService>();
            this.mockViewModelAndViewsFactory = new Mock<IViewModelAndViewsFactory>();

            this.mockResolverService = new Mock<IResolverService>();

            this.mockReadMeService = new Mock<IReadMeService>();
            this.controller = new ProjectsController(
                this.mockConfigurationService.Object,
                this.mockProjectsService.Object,
                this.mockNugetService.Object,
                this.mockVisualStudioService.Object,
                this.mockSettingsService.Object,
                this.mockMessageBoxService.Object,
                this.mockingServiceFactory.Object,
                this.mockResolverService.Object,
                this.mockViewModelViewsService.Object,
                this.mockViewModelAndViewsFactory.Object,
                this.mockReadMeService.Object);

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

        /// <summary>
        /// Tests the fix info plist.
        /// </summary>
        [Test]
        public void TestFixInfoPlist()
        {
            Mock<IProjectItemService> mockProjectItemService = new Mock<IProjectItemService>();
            mockProjectItemService.Setup(x => x.FileName).Returns(@"B:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\NinjaCoder.MvvmCross.Tests\TestData\info.plist");
            
            ////this.controller.FixInfoPlist(mockProjectItemService.Object);
        }
    }
}
