// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestPluginsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Services
{
    using Constants;
    using Entities;
    using EnvDTE;

    using Mocks;
    using Moq;
    using MvvmCross.Factories.Interfaces;
    using MvvmCross.Services;
    using MvvmCross.Services.Interfaces;
    using NUnit.Framework;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.Linq;

    /// <summary>
    /// Defines the TestPluginsService type.
    /// </summary>
    [TestFixture]
    public class TestPluginsService
    {
        /// <summary>
        /// The service.
        /// </summary>
        private PluginsService service;

        /// <summary>
        /// The mock plugin service.
        /// </summary>
        private Mock<IPluginService> mockPluginService;

        /// <summary>
        /// The mock visual studio service.
        /// </summary>
        private Mock<IVisualStudioService> mockVisualStudioService;

        /// <summary>
        /// The mock file system.
        /// </summary>
        private Mock<IFileSystem> mockFileSystem;

        /// <summary>
        /// The mock settings service.
        /// </summary>
        private Mock<ISettingsService> mockSettingsService;

        /// <summary>
        /// The mock nuget service.
        /// </summary>
        private Mock<INugetService> mockNugetService;

        /// <summary>
        /// The mock file.
        /// </summary>
        private MockFile mockFile;

        /// <summary>
        /// The mock file info.
        /// </summary>
        private Mock<IFileInfoFactory> mockFileInfoFactory;

        /// <summary>
        /// The mock file info.
        /// </summary>
        private MockFileInfo mockFileInfo;

        /// <summary>
        /// The mock code snippet factory.
        /// </summary>
        private Mock<ICodeSnippetFactory> mockCodeSnippetFactory;

        /// <summary>
        /// The mock testing service factory.
        /// </summary>
        private Mock<ITestingServiceFactory> mockTestingServiceFactory;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockPluginService = new Mock<IPluginService>();
            this.mockVisualStudioService = new Mock<IVisualStudioService>();
            this.mockFileSystem = new Mock<IFileSystem>();
            this.mockSettingsService = new Mock<ISettingsService>();
            this.mockNugetService = new Mock<INugetService>();

            this.mockFile = new MockFile();
            this.mockFileInfoFactory = new Mock<IFileInfoFactory>();
            this.mockFileInfo = new MockFileInfo();

            this.mockTestingServiceFactory = new Mock<ITestingServiceFactory>();
            this.mockCodeSnippetFactory = new Mock<ICodeSnippetFactory>();

            this.mockFileSystem.SetupGet(x => x.File).Returns(this.mockFile);
            this.mockFileSystem.SetupGet(x => x.FileInfo).Returns(this.mockFileInfoFactory.Object);
            this.mockFileInfoFactory.Setup(x => x.FromFileName(It.IsAny<string>())).Returns(this.mockFileInfo);

            this.service = new PluginsService(
                this.mockPluginService.Object,
                this.mockSettingsService.Object,
                this.mockNugetService.Object,
                this.mockCodeSnippetFactory.Object,
                this.mockTestingServiceFactory.Object);
        }

        /// <summary>
        /// Tests the add plugins.
        /// </summary>
        [Test]
        public void TestAddPlugins()
        {
            //// arrange
            /*
            List<Plugin> plugins = new List<Plugin>
                                       {
                                           new Plugin
                                               {
                                                   FileName = "fileName",
                                                   FriendlyName = "friendlyName",
                                                   Source = "source"
                                               }
                                       };

            //// core
            Mock<IProjectService> mockCoreProjectService = new Mock<IProjectService>();
            this.mockVisualStudioService.SetupGet(x => x.CoreProjectService).Returns(mockCoreProjectService.Object);

            Mock<IProjectItemService> mockProjectItemService = new Mock<IProjectItemService>();
            mockCoreProjectService.Setup(x => x.GetProjectItem(It.IsAny<string>()))
                                  .Returns(mockProjectItemService.Object);

            Mock<ProjectItem> mockProjectItem = new Mock<ProjectItem>();
            mockProjectItemService.SetupGet(x => x.ProjectItem).Returns(mockProjectItem.Object);

            //// tests 
            Mock<IProjectService> mockTestsProjectService = new Mock<IProjectService>();
            this.mockVisualStudioService.SetupGet(x => x.CoreTestsProjectService).Returns(mockTestsProjectService.Object);
           
            //// droid
            Mock<IProjectService> mockDroidProjectService = new Mock<IProjectService>();
            this.mockVisualStudioService.SetupGet(x => x.DroidProjectService).Returns(mockDroidProjectService.Object);

            //// ios
            Mock<IProjectService> mockiOSProjectService = new Mock<IProjectService>();
            this.mockVisualStudioService.SetupGet(x => x.iOSProjectService).Returns(mockiOSProjectService.Object);

            //// windows phone
            Mock<IProjectService> mockWindowsPhoneProjectService = new Mock<IProjectService>();
            this.mockVisualStudioService.SetupGet(x => x.WindowsPhoneProjectService)
                .Returns(mockWindowsPhoneProjectService.Object);

            //// windows store
            Mock<IProjectService> mockWindowsStoreProjectService = new Mock<IProjectService>();
            this.mockVisualStudioService.SetupGet(x => x.WindowsStoreProjectService)
                .Returns(mockWindowsStoreProjectService.Object);

            //// wpf
            Mock<IProjectService> mockWpfProjectService = new Mock<IProjectService>();
            this.mockVisualStudioService.SetupGet(x => x.WpfProjectService).Returns(mockWpfProjectService.Object);

            this.mockFile.FileExists = true;

            ////this.mockSettingsService.SetupGet(x => x.UseNugetForPlugins).Returns(true);
            
            //// act
            /*IEnumerable<string> messages  = this.service.AddPlugins(
                this.mockVisualStudioService.Object, 
                plugins, 
                null, 
                true, 
                false);*/

            //// assert*/
        }

        /// <summary>
        /// Tests the add project plugins.
        /// </summary>
        [Test]
        public void TestAddProjectPlugins()
        {
            //// arrange

            List<Plugin> plugins = new List<Plugin> { new Plugin() };
            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();

            Mock<Project> mockProject = new Mock<Project>();
            mockProjectService.SetupGet(x => x.Project).Returns(mockProject.Object);

            //// act
            this.service.AddProjectPlugins(
               mockProjectService.Object, 
               plugins, 
               true);

            //// assert
            this.mockPluginService.Verify(x => x.AddProjectPlugin(
                It.IsAny<IProjectService>(), 
                It.IsAny<Plugin>()));
        }
    }
}
