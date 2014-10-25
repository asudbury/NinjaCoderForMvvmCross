// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestSnippetService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Services
{
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using Moq;

    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.Tests.Mocks;
    using NUnit.Framework;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the TestSnippetService type.
    /// </summary>
    [TestFixture]
    public class TestSnippetService
    {
        /// <summary>
        /// The service.
        /// </summary>
        private CodeSnippetService service;

        /// <summary>
        /// The mock settings service.
        /// </summary>
        private Mock<ISettingsService> mockSettingsService;

        /// <summary>
        /// The mocking service factory.
        /// </summary>
        private Mock<IMockingServiceFactory> mockingServiceFactory;
             
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockSettingsService = new Mock<ISettingsService>();

            Mock<IFileInfoFactory> mockFileInfoFactory = new Mock<IFileInfoFactory>();

            MockFileInfoBase mockFileInfoBase = new MockFileInfoBase { LengthValue = 22 };

            mockFileInfoFactory.Setup(x => x.FromFileName(It.IsAny<string>())).Returns(mockFileInfoBase);

            this.mockingServiceFactory = new Mock<IMockingServiceFactory>();

            this.service = new CodeSnippetService(
                this.mockSettingsService.Object, 
                this.mockingServiceFactory.Object);
        }

        /// <summary>
        /// Tests the get snippet.
        /// </summary>
        [Test]
        public void TestGetSnippet()
        {
            /*CodeSnippet codeSnippet = new CodeSnippet {  = "hello" };
            this.mockTranslator.Setup(x => x.Translate(It.IsAny<string>())).Returns(codeSnippet);

            CodeSnippet codeSnippetReturn = this.service.GetSnippet("path");

            ////Assert.IsTrue(codeSnippetReturn.MockInitCode == codeSnippet.MockInitCode);*/
        }

        /// <summary>
        /// Tests the get unit testing snippet.
        /// </summary>
        [Test]
        public void TestGetUnitTestingSnippet()
        {
            /*
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
            Assert.IsTrue(codeSnippetReturn.TestInitMethod == settingsService.UnitTestingInitMethod);*/
        }

        /// <summary>
        /// Tests the apply globals.
        /// </summary>
        [Test]
        public void TestApplyGlobals()
        {
            Mock<ISolutionService> mockSolutionService = new Mock<ISolutionService>();
            mockSolutionService.Setup(x => x.HasGlobals).Returns(true);

            Dictionary<string, string> dictionary = new Dictionary<string, string>
            {
                { "key", "value" }
            };

            mockSolutionService.Setup(x => x.GetGlobalVariables()).Returns(dictionary);

            Mock<IDTEService> mockDTEService = new Mock<IDTEService>();
            mockDTEService.SetupGet(x => x.SolutionService).Returns(mockSolutionService.Object);

            Mock<IVisualStudioService> mockVisualStudioService = new Mock<IVisualStudioService>();
            
            mockVisualStudioService.SetupGet(x => x.DTEService).Returns(mockDTEService.Object);

            CodeSnippet codeSnippet = new CodeSnippet();

            this.service.ApplyGlobals(
                mockVisualStudioService.Object,
                codeSnippet);

            Assert.IsTrue(codeSnippet.ReplacementVariables.Count == 1);
        }

        /// <summary>
        /// Tests the create unit tests.
        /// </summary>
        [Test]
        public void TestCreateUnitTests()
        {
            /*
            Mock<IProjectItemService> mockProjectItemService = new Mock<IProjectItemService>();

            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();
            mockProjectService.Setup(x => x.GetProjectItem(It.IsAny<string>())).Returns(mockProjectItemService.Object);

            CodeSnippet codeSnippet = new CodeSnippet();

            Mock<ISnippetService> mockSnippetService = new Mock<ISnippetService>();

            mockSnippetService.Setup(x => x.GetUnitTestingSnippet(It.IsAny<string>())).Returns(codeSnippet);

            Mock<IVisualStudioService> mockVisualStudioService = new Mock<IVisualStudioService>();

            this.service.CreateUnitTests(
                mockVisualStudioService.Object,
                mockProjectService.Object,
                new CodeSnippet(), 
                "viewModelName",
                "friendlyName",
                "usingStatement");

            mockProjectItemService.Verify(x => x.ImplementUnitTestingCodeSnippet(
                It.IsAny<CodeSnippet>(),
                It.IsAny<string>(),
                It.IsAny<bool>(),
                It.IsAny<bool>(),
                It.IsAny<bool>()));*/
        }
    }
}
