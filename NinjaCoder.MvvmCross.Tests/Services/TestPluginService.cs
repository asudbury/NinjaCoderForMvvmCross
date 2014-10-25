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
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
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
        /// The mock code config factory.
        /// </summary>
        private Mock<ICodeConfigFactory> mockCodeConfigFactory;
        
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockFileSystem = new Mock<IFileSystem>();
            this.mockSettingsService = new Mock<ISettingsService>();
            this.mockFile = new MockFile();
            this.mockFileInfoFactory = new Mock<IFileInfoFactory>();
            this.mockFileInfo = new MockFileInfo();
            this.mockCodeConfigFactory = new Mock<ICodeConfigFactory>();

            this.mockFileSystem.SetupGet(x => x.File).Returns(this.mockFile);
            this.mockFileSystem.SetupGet(x => x.FileInfo).Returns(this.mockFileInfoFactory.Object);
            this.mockFileInfoFactory.Setup(x => x.FromFileName(It.IsAny<string>())).Returns(this.mockFileInfo);

            this.service = new PluginService(this.mockSettingsService.Object);
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

            //// assert
            mockProjectService.Verify(x => x.AddReference(
                It.IsAny<string>(), 
                It.IsAny<string>(), 
                It.IsAny<string>()));
        }
    }
}
