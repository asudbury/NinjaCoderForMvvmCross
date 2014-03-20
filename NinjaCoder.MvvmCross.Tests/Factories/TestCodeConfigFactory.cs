// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestCodeConfigFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Factories
{
    using System.IO.Abstractions;

    using Moq;

    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.Tests.Mocks;

    using NUnit.Framework;

    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the TestCodeConfigFactory type.
    /// </summary>
    [TestFixture]
    public class TestCodeConfigFactory
    {
        /// <summary>
        /// The code config factory.
        /// </summary>
        private CodeConfigFactory factory;

        /// <summary>
        /// The mock code config service.
        /// </summary>
        private Mock<ICodeConfigService> mockCodeConfigService;
        
        /// <summary>
        /// The mock file system.
        /// </summary>
        private Mock<IFileSystem> mockFileSystem;

        /// <summary>
        /// The mock settings service.
        /// </summary>
        private Mock<ISettingsService> mockSettingsService;

        /// <summary>
        /// The translator.
        /// </summary>
        private Mock<ITranslator<string, CodeConfig>> mockTranslator;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockCodeConfigService = new Mock<ICodeConfigService>();
            this.mockFileSystem = new Mock<IFileSystem>();
            this.mockSettingsService = new Mock<ISettingsService>();
            this.mockTranslator = new Mock<ITranslator<string, CodeConfig>>();

            this.factory = new CodeConfigFactory(
                this.mockCodeConfigService.Object,
                this.mockFileSystem.Object,
                this.mockSettingsService.Object,
                this.mockTranslator.Object);
        }

        /// <summary>
        /// Tests the get code config service.
        /// </summary>
        [Test]
        public void TestGetCodeConfigService()
        {
            ICodeConfigService codeConfigService = this.factory.GetCodeConfigService();

            Assert.IsTrue(codeConfigService == this.mockCodeConfigService.Object);
        }

        /// <summary>
        /// Tests the get plugin config.
        /// </summary>
        [Test]
        public void TestGetPluginConfig()
        {
            Plugin plugin = new Plugin { FriendlyName = "File" };

            MockFile mockFile = new MockFile();
            this.mockFileSystem.SetupGet(x => x.File).Returns(mockFile);

            SettingsService settingsService = new SettingsService();

            this.mockSettingsService.SetupGet(x => x.PluginsConfigPath).Returns(settingsService.PluginsConfigPath);
            this.mockSettingsService.SetupGet(x => x.UserCodeConfigPluginsPath).Returns(settingsService.UserCodeConfigPluginsPath);

            this.mockTranslator.Setup(x => x.Translate(It.IsAny<string>())).Returns(new CodeConfig());

            this.factory.GetPluginConfig(plugin);

            this.mockTranslator.Verify(x => x.Translate(It.IsAny<string>()));
        }

        /// <summary>
        /// Tests the get service config.
        /// </summary>
        [Test]
        public void  TestGetServiceConfig()
        {
            MockFile mockFile = new MockFile();
            this.mockFileSystem.SetupGet(x => x.File).Returns(mockFile);

            SettingsService settingsService = new SettingsService();

            this.mockSettingsService.SetupGet(x => x.ServicesConfigPath).Returns(settingsService.ServicesConfigPath);
            this.mockSettingsService.SetupGet(x => x.UserCodeConfigServicesPath).Returns(settingsService.UserCodeConfigServicesPath);

            this.mockTranslator.Setup(x => x.Translate(It.IsAny<string>())).Returns(new CodeConfig());

            this.factory.GetServiceConfig("friendlyName");

            this.mockTranslator.Verify(x => x.Translate(It.IsAny<string>()));
        }

        /// <summary>
        /// Tests the get config core.
        /// </summary>
        [Test]
        public void TestGetConfigCore()
        {
            MockFile mockFile = new MockFile { FileExists = false };

            this.mockFileSystem.SetupGet(x => x.File).Returns(mockFile);

            this.factory.GetConfig("coreDirectory", "userDirectory", "fileName");

            this.mockTranslator.Verify(x => x.Translate("coreDirectoryfileName"));
        }

        /// <summary>
        /// Tests the get config user.
        /// </summary>
        [Test]
        public void TestGetConfigUser()
        {
            MockFile mockFile = new MockFile { FileExists = true };
            this.mockFileSystem.SetupGet(x => x.File).Returns(mockFile);

            this.factory.GetConfig("coreDirectory", "userDirectory", "fileName");

            this.mockTranslator.Verify(x => x.Translate("userDirectoryfileName"));
        }

        /// <summary>
        /// Tests the get code config from path.
        /// </summary>
        [Test]
        public void TestGetCodeConfigFromPath()
        {
            this.factory.GetCodeConfigFromPath("path");

            this.mockTranslator.Verify(x => x.Translate(It.IsAny<string>()));
        }
    }
}
