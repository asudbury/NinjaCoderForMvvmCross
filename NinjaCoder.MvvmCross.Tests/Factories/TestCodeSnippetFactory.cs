// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestCodeSnippetFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Factories
{
    using System.IO.Abstractions;

    using Moq;

    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.Tests.Mocks;

    using NUnit.Framework;

    using Scorchio.Infrastructure.Services.Testing.Interfaces;
    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the TestCodeSnippetFactory type.
    /// </summary>
    [TestFixture]
    public class TestCodeSnippetFactory
    {
        /// <summary>
        /// The code snippet factory.
        /// </summary>
        private CodeSnippetFactory factory;

        /// <summary>
        /// The mock code snippet service.
        /// </summary>
        private Mock<ICodeSnippetService> mockCodeSnippetService;

        /// <summary>
        /// The mock file system.
        /// </summary>
        private Mock<IFileSystem> mockFileSystem;

        /// <summary>
        /// The mock settings service,
        /// </summary>
        private Mock<ISettingsService> mockSettingsService;
        
        /// <summary>
        /// The mock translator.
        /// </summary>
        private Mock<ITranslator<string, CodeSnippet>> mockTranslator;

        /// <summary>
        /// The mock mocking service factory.
        /// </summary>
        private Mock<IMockingServiceFactory> mockMockingServiceFactory;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockCodeSnippetService = new Mock<ICodeSnippetService>();
            this.mockFileSystem = new Mock<IFileSystem>();
            this.mockSettingsService = new Mock<ISettingsService>();
            this.mockTranslator = new Mock<ITranslator<string, CodeSnippet>>();
            this.mockMockingServiceFactory = new Mock<IMockingServiceFactory>();

            Mock<IMockingService> mockingService = new Mock<IMockingService>();
            mockingService.SetupGet(x => x.MockingAssemblyReference).Returns("MvxAssembly");

            this.mockMockingServiceFactory.Setup(x => x.GetMockingService()).Returns(mockingService.Object);
            
            this.factory = new CodeSnippetFactory(
                this.mockCodeSnippetService.Object,
                this.mockFileSystem.Object,
                this.mockSettingsService.Object,
                this.mockTranslator.Object,
                this.mockMockingServiceFactory.Object);
        }

        /// <summary>
        /// Tests the get code snippet service.
        /// </summary>
        [Test]
        public void TestGetCodeSnippetService()
        {
            ICodeSnippetService codeSnippetService = this.factory.GetCodeSnippetService();

            Assert.IsTrue(codeSnippetService == this.mockCodeSnippetService.Object);
        }

        /// <summary>
        /// Tests the get snippet.
        /// </summary>
        [Test]
        public void TestGetSnippet()
        {
            MockFile mockFile = new MockFile { FileExists = true };
            this.mockFileSystem.SetupGet(x => x.File).Returns(mockFile);

            this.factory.GetSnippet("path");

            this.mockTranslator.Verify(x => x.Translate(It.IsAny<string>()));
        }

        /// <summary>
        /// Tests the get plugin snippet.
        /// </summary>
        [Test]
        public void TestGetPluginSnippet()
        {
            Plugin plugin = new Plugin();

            MockFile mockFile = new MockFile { FileExists = true };
            this.mockFileSystem.SetupGet(x => x.File).Returns(mockFile);

            SettingsService settingsService = new SettingsService();

            this.mockSettingsService.SetupGet(x => x.PluginsCodeSnippetsPath).Returns(settingsService.PluginsCodeSnippetsPath);
            this.mockSettingsService.SetupGet(x => x.UserCodeSnippetsPluginsPath).Returns(settingsService.UserCodeSnippetsPluginsPath);

            this.mockTranslator.Setup(x => x.Translate(It.IsAny<string>())).Returns(new CodeSnippet());
            
            this.factory.GetPluginSnippet(plugin);

            this.mockTranslator.Verify(x => x.Translate(It.IsAny<string>()));
        }

        /// <summary>
        /// Tests the get plugin test snippet.
        /// </summary>
        [Test]
        public void TestGetPluginTestSnippet()
        {
            Plugin plugin = new Plugin();

            MockFile mockFile = new MockFile { FileExists = true };
            this.mockFileSystem.SetupGet(x => x.File).Returns(mockFile);

            SettingsService settingsService = new SettingsService();

            this.mockSettingsService.SetupGet(x => x.PluginsCodeSnippetsPath).Returns(settingsService.PluginsCodeSnippetsPath);
            this.mockSettingsService.SetupGet(x => x.UserCodeSnippetsPluginsPath).Returns(settingsService.UserCodeSnippetsPluginsPath);

            this.mockTranslator.Setup(x => x.Translate(It.IsAny<string>())).Returns(new CodeSnippet());
            
            this.factory.GetPluginTestSnippet(plugin);

            this.mockTranslator.Verify(x => x.Translate(It.IsAny<string>()));
        }

        /// <summary>
        /// Tests the get service snippet.
        /// </summary>
        [Test]
        public void TestGetServiceSnippet()
        {
            MockFile mockFile = new MockFile { FileExists = true };
            this.mockFileSystem.SetupGet(x => x.File).Returns(mockFile);

            this.factory.GetServiceSnippet("friendlyName");

            this.mockTranslator.Verify(x => x.Translate(It.IsAny<string>()));
        }

        /// <summary>
        /// Tests the get service test snippet.
        /// </summary>
        [Test]
        public void TestGetServiceTestSnippet()
        {
            MockFile mockFile = new MockFile { FileExists = true };
            this.mockFileSystem.SetupGet(x => x.File).Returns(mockFile);

            this.factory.GetServiceTestSnippet("friendlyName");
            
            this.mockTranslator.Verify(x => x.Translate(It.IsAny<string>()));
        }

        /// <summary>
        /// Tests the build testing snippet.
        /// </summary>
        [Test]
        public void TestBuildTestingSnippet()
        {
            CodeSnippet codeSnippet = new CodeSnippet();

            this.mockSettingsService.SetupGet(x => x.UnitTestingAssemblies).Returns("unittestassembly");

            this.mockSettingsService.SetupGet(x => x.UnitTestingInitMethod).Returns("initmethod");

            this.factory.BuildTestingSnippet(codeSnippet);

            Assert.IsTrue(codeSnippet.UsingStatements.Count == 2);
            Assert.IsTrue(codeSnippet.TestInitMethod == "initmethod");
        }

        /// <summary>
        /// Tests the get snippet core.
        /// </summary>
        [Test]
        public void TestGetSnippetCore()
        {
            MockFile mockFile = new MockFile { FileExists = false };
            this.mockFileSystem.SetupGet(x => x.File).Returns(mockFile);

            this.factory.GetSnippet("coreDirectory", "userDirectory", "FileName");

            this.mockTranslator.Verify(x => x.Translate("userDirectoryFileName"), Times.Never());
        }

        /// <summary>
        /// Tests the get snippet user.
        /// </summary>
        [Test]
        public void TestGetSnippetUser()
        {
            MockFile mockFile = new MockFile { FileExists = true };
            this.mockFileSystem.SetupGet(x => x.File).Returns(mockFile);

            this.factory.GetSnippet("coreDirectory", "userDirectory", "FileName");

            this.mockTranslator.Verify(x => x.Translate("userDirectoryFileName"));
        }
    }
}
