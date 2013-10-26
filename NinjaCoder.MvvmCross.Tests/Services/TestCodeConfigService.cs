// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestCodeConfigService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Services
{
    using System.Collections.Generic;
    using System.IO.Abstractions;

    using Moq;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Tests.Mocks;
    using NinjaCoder.MvvmCross.Translators;
    using NUnit.Framework;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the TestCodeConfigService type.
    /// </summary>
    [TestFixture]
    public class TestCodeConfigService
    {
        /// <summary>
        /// The service.
        /// </summary>
        private CodeConfigService service;

        /// <summary>
        /// The mock file system.
        /// </summary>
        private Mock<IFileSystem> mockFileSystem;

        /// <summary>
        /// The mock settings service.
        /// </summary>
        private Mock<ISettingsService> mockSettingsService;
        
        /// <summary>
        /// The mock translator.
        /// </summary>
        private Mock<ITranslator<string, CodeConfig>> mockTranslator;

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
            this.mockTranslator = new Mock<ITranslator<string, CodeConfig>>();

            this.mockFileSystem.SetupGet(x => x.File).Returns(this.mockFile);
            this.mockFileSystem.SetupGet(x => x.FileInfo).Returns(this.mockFileInfoFactory.Object);
            this.mockFileInfoFactory.Setup(x => x.FromFileName(It.IsAny<string>())).Returns(this.mockFileInfo);

            this.service = new CodeConfigService(
                this.mockFileSystem.Object,
                this.mockSettingsService.Object,
                this.mockTranslator.Object);
        }

        /// <summary>
        /// Tests the process code config.
        /// </summary>
        [Test]
        public void TestProcessCodeConfig()
        {
            SettingsService settingsService = new SettingsService();

            this.mockSettingsService.SetupGet(x => x.ConfigPath).Returns(settingsService.ConfigPath);

            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();
            mockProjectService.SetupGet(x => x.Name).Returns("Adrian.WindowsPhone");

            CodeConfig codeConfig = new CodeConfig
            {
                References = new List<string> { "test" }
            };

            this.mockTranslator.Setup(x => x.Translate(It.IsAny<string>())).Returns(codeConfig);

            MockPathBase mockPathBase = new MockPathBase();

            this.mockFileSystem.SetupGet(x => x.Path).Returns(mockPathBase);

            this.mockSettingsService.SetupGet(x => x.UseNugetForPlugins).Returns(false);

            this.service.ProcessCodeConfig(mockProjectService.Object, "SQLite", "source", "destination");

            mockProjectService.Verify(
                x => x.AddReference(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()));
        }

        /// <summary>
        /// Tests the use local disk copy when nuget requested.
        /// </summary>
        [Test]
        public void TestUseLocalDiskCopyWhenNugetRequested()
        {
            this.mockSettingsService.SetupGet(x => x.UseNugetForPlugins).Returns(true);

            CodeConfig codeConfig = new CodeConfig();

            bool useLocalCopy = this.service.UseLocalDiskCopy(codeConfig);

            Assert.IsTrue(useLocalCopy);
        }

        /// <summary>
        /// Tests the use local disk copy should be false.
        /// </summary>
        [Test]
        public void TestUseLocalDiskCopyShouldBeFalse()
        {
            this.mockSettingsService.SetupGet(x => x.UseNugetForPlugins).Returns(true);

            CodeConfig codeConfig = new CodeConfig
                                        {
                                            NugetPackage = "nugetPackage"
                                        };

            bool useLocalCopy = this.service.UseLocalDiskCopy(codeConfig);

            Assert.IsTrue(!useLocalCopy);
        }

        /// <summary>
        /// Tests the nuget requested and not supported false.
        /// </summary>
        [Test]
        public void TestNugetRequestedAndNotSupportedFalse()
        {
            this.mockSettingsService.SetupGet(x => x.UseNugetForPlugins).Returns(true);

            CodeConfig codeConfig = new CodeConfig
            {
                NugetPackage = "nugetPackage"
            };

            bool supported = this.service.NugetRequestedAndNotSupported(codeConfig);

            Assert.IsTrue(!supported);
        }

        /// <summary>
        /// Tests the nuget requested and supported true.
        /// </summary>
        [Test]
        public void TestNugetRequestedAndNotSupportedTrue()
        {
            this.mockSettingsService.SetupGet(x => x.UseNugetForPlugins).Returns(true);

            CodeConfig codeConfig = new CodeConfig();

            bool supported = this.service.NugetRequestedAndNotSupported(codeConfig);

            Assert.IsTrue(supported);
        }

        /// <summary>
        /// Tests the get code config from a file.
        /// </summary>
        [Test]
        public void TestGetCodeConfigFromPath()
        {
            this.mockTranslator.SetupGet(x => x.Translate(It.IsAny<string>()));
            
            this.service.GetCodeConfigFromPath("path");

            this.mockTranslator.Verify(x => x.Translate(It.IsAny<string>()));
        }

        /// <summary>
        /// Tests the ge project code config.
        /// </summary>
        [Test]
        public void TestGeProjectCodeConfig()
        {
            this.mockTranslator.SetupGet(x => x.Translate(It.IsAny<string>()));
            
            this.service.GetCodeConfigFromPath("path");

            this.mockTranslator.Verify(x => x.Translate(It.IsAny<string>()));
        }

        
        /// <summary>
        /// Tests the name of the get bootstrap file.
        /// </summary>
        [Test]
        public void TestGetBootstrapFileName()
        {
            string fileName = this.service.GetBootstrapFileName(null, "friendlyName");

            Assert.IsTrue(fileName == "friendlyNamePluginBootstrap.cs");
        }

        /// <summary>
        /// Tests the get bootstrap file name is overidden.
        /// </summary>
        [Test]
        public void TestGetBootstrapFileNameIsOveridden()
        {
            CodeConfig codeConfig = new CodeConfig
            {
                BootstrapFileNameOverride = "OverrideBootstrap.cs"
            };

            string fileName = this.service.GetBootstrapFileName(codeConfig, "friendlyName");

            Assert.IsTrue(fileName == "OverrideBootstrap.cs");
        }
    }
}
