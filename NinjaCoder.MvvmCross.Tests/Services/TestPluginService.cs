// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestPluginService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Services
{
    using System.IO.Abstractions;
    using EnvDTE;
    using Moq;
    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.Tests.Mocks;
    using NUnit.Framework;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    /// Defines the TestPluginService type.
    /// </summary>
    [TestFixture]
    public class TestPluginService
    {
        /// <summary>
        /// The service.
        /// </summary>
        private PluginService service;

        /// <summary>
        /// The mock file system.
        /// </summary>
        private Mock<IFileSystem> mockFileSystem;

        /// <summary>
        /// The mock settings service.
        /// </summary>
        private Mock<ISettingsService> mockSettingsService;

        /// <summary>
        /// The mock snippets service.
        /// </summary>
        private Mock<ISnippetService> mockSnippetsService;

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
        /// The mock translator.
        /// </summary>
        private Mock<ICodeConfigService> mockCodeConfigService;
        
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockFileSystem = new Mock<IFileSystem>();
            this.mockSettingsService = new Mock<ISettingsService>();
            this.mockSnippetsService = new Mock<ISnippetService>();
            this.mockFile = new MockFile();
            this.mockFileInfoFactory = new Mock<IFileInfoFactory>();
            this.mockFileInfo = new MockFileInfo();
            this.mockCodeConfigService = new Mock<ICodeConfigService>();

            this.mockFileSystem.SetupGet(x => x.File).Returns(this.mockFile);
            this.mockFileSystem.SetupGet(x => x.FileInfo).Returns(this.mockFileInfoFactory.Object);
            this.mockFileInfoFactory.Setup(x => x.FromFileName(It.IsAny<string>())).Returns(this.mockFileInfo);

            this.service = new PluginService(
                this.mockFileSystem.Object, 
                this.mockSettingsService.Object,
                this.mockCodeConfigService.Object);
        }

        /// <summary>
        /// Tests the add plugin to core.
        /// </summary>
        [Test]
        public void TestAddPluginToCore()
        {
            //// arrange

            Plugin plugin = new Plugin();
            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();

            Mock<Project> mockProject = new Mock<Project>();
            mockProjectService.SetupGet(x => x.Project).Returns(mockProject.Object);

            //// act
            this.service.AddPlugin(
               mockProjectService.Object,
               plugin, 
               string.Empty,
               Settings.Core);

            //// assert
            mockProjectService.Verify(x => x.AddReference(
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<bool>(),
                It.IsAny<bool>()));
        }

        /// <summary>
        /// Tests the add UI plugin.
        /// </summary>
        [Test]
        public void TestAddUIPlugin()
        {
            //// arrange

            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();

            Mock<Project> mockProject = new Mock<Project>();
            mockProjectService.SetupGet(x => x.Project).Returns(mockProject.Object);

            this.mockFile.FileExists = false;

            mockProjectService.SetupGet(x => x.Name).Returns("core");

            //// act
            this.service.AddUIPlugin(
                mockProjectService.Object,
                "friendlyName",
                "source",
                "destination",
                "extensionSource",
                "extensionDestination",
                true);

            //// assert
            mockProjectService.Verify(
                x => x.AddReference(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()));
        }

        /// <summary>
        /// Tests the get plugin path.
        /// </summary>
        [Test]
        public void TestGetPluginPath()
        {
            string path = this.service.GetPluginPath("WindowsPhone", "plugin.dll");

            Assert.IsTrue(path == "plugin.WindowsPhone.dll");
        }
    }
}
