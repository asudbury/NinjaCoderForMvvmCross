// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestSnippetService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Services
{
    using System.IO.Abstractions;
    using Moq;
    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.Tests.Mocks;
    using NinjaCoder.MvvmCross.Translators;
    using NUnit.Framework;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the TestSnippetService type.
    /// </summary>
    [TestFixture]
    public class TestSnippetService
    {
        /// <summary>
        /// The service.
        /// </summary>
        private SnippetService service;

        /// <summary>
        /// The mock settings service.
        /// </summary>
        private Mock<ISettingsService> mockSettingsService;

        /// <summary>
        /// The mock file system.
        /// </summary>
        private Mock<IFileSystem> mockFileSystem;

        /// <summary>
        /// The mock translator.
        /// </summary>
        private Mock<ITranslator<string, CodeSnippet>> mockTranslator;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockSettingsService = new Mock<ISettingsService>();
            this.mockFileSystem = new Mock<IFileSystem>();
            this.mockTranslator = new Mock<ITranslator<string, CodeSnippet>>();
            
            MockFile mockFile = new MockFile { FileExists = true };

            this.mockFileSystem.SetupGet(x => x.File).Returns(mockFile);

            Mock<IFileInfoFactory> mockFileInfoFactory = new Mock<IFileInfoFactory>();

            MockFileInfoBase mockFileInfoBase = new MockFileInfoBase { LengthValue = 22 };

            mockFileInfoFactory.Setup(x => x.FromFileName(It.IsAny<string>())).Returns(mockFileInfoBase);
            this.mockFileSystem.SetupGet(x => x.FileInfo).Returns(mockFileInfoFactory.Object);

            this.service = new SnippetService(
                this.mockSettingsService.Object, this.mockFileSystem.Object, this.mockTranslator.Object);
        }

        /// <summary>
        /// Tests the get snippet.
        /// </summary>
        [Test]
        public void TestGetSnippet()
        {
            CodeSnippet codeSnippet = new CodeSnippet { MockInitCode = "hello" };
            this.mockTranslator.Setup(x => x.Translate(It.IsAny<string>())).Returns(codeSnippet);

            CodeSnippet codeSnippetReturn = this.service.GetSnippet("path");

            Assert.IsTrue(codeSnippetReturn.MockInitCode == codeSnippet.MockInitCode);
        }

        /// <summary>
        /// Tests the get unit testing snippet.
        /// </summary>
        [Test]
        public void TestGetUnitTestingSnippet()
        {
            CodeSnippet codeSnippet = new CodeSnippet { MockInitCode = "hello" };
            this.mockTranslator.Setup(x => x.Translate(It.IsAny<string>())).Returns(codeSnippet);

            SettingsService settingsService = new SettingsService();

            this.mockSettingsService.SetupGet(x => x.UnitTestingAssemblies)
                .Returns(settingsService.UnitTestingAssemblies);

            this.mockSettingsService.SetupGet(x => x.UnitTestingInitMethod)
                .Returns(settingsService.UnitTestingInitMethod);
            
            CodeSnippet codeSnippetReturn = this.service.GetUnitTestingSnippet("path");

            Assert.IsTrue(codeSnippetReturn.MockInitCode == codeSnippet.MockInitCode);
            Assert.IsTrue(codeSnippetReturn.UsingStatements.Count == 2);
            Assert.IsTrue(codeSnippetReturn.TestInitMethod == settingsService.UnitTestingInitMethod);
        }
    }
}
