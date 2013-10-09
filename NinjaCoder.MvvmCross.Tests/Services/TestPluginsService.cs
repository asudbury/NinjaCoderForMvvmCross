// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestPluginsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Services
{
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using EnvDTE;
    using Moq;
    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.Tests.Mocks;
    using NinjaCoder.MvvmCross.Translators;
    using NUnit.Framework;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;

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
        private Mock<ITranslator<string, CodeConfig>> mockTranslator;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockVisualStudioService = new Mock<IVisualStudioService>();
            this.mockFileSystem = new Mock<IFileSystem>();
            this.mockSettingsService = new Mock<ISettingsService>();
            this.mockSnippetsService = new Mock<ISnippetService>();
            this.mockFile = new MockFile();
            this.mockFileInfoFactory = new Mock<IFileInfoFactory>();
            this.mockFileInfo = new MockFileInfo();
            this.mockTranslator = new Mock<ITranslator<string, CodeConfig>>();

            this.mockFileSystem.SetupGet(x => x.File).Returns(this.mockFile);
            this.mockFileSystem.SetupGet(x => x.FileInfo).Returns(this.mockFileInfoFactory.Object);
            this.mockFileInfoFactory.Setup(x => x.FromFileName(It.IsAny<string>())).Returns(this.mockFileInfo);

            this.service = new PluginsService(
                this.mockFileSystem.Object, 
                this.mockSettingsService.Object,
                this.mockSnippetsService.Object,
                this.mockTranslator.Object);
        }

        /// <summary>
        /// Tests the add plugins.
        /// </summary>
        [Test]
        public void TestAddPlugins()
        {
            //// arrange

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

            this.mockSettingsService.SetupGet(x => x.UseNugetForPlugins).Returns(true);


            CodeSnippet codeSnippet = new CodeSnippet
                                          {
                                              UsingStatements = new List<string>()
                                          };

            this.mockSnippetsService.Setup(x => x.GetSnippet(It.IsAny<string>())).Returns(codeSnippet);

            //// act
            this.service.AddPlugins(this.mockVisualStudioService.Object, plugins, "viewModelName", true);

            //// assert
            mockProjectItemService.Verify(x => x.ImplementCodeSnippet(It.IsAny<CodeSnippet>(), It.IsAny<bool>()));
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
               string.Empty, 
               Settings.Core);

            //// assert
            mockProjectService.Verify(x => x.WriteStatusBarMessage(It.IsAny<string>()));
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
                It.IsAny<bool>()));
        }

        /// <summary>
        /// Tests the add plugin to non core.
        /// </summary>
        [Test]
        public void TestAddPluginToNonCore()
        {
            //// arrange

            Plugin plugin = new Plugin { Source = "Here I am" };

            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();

            Mock<Project> mockProject = new Mock<Project>();
            mockProjectService.SetupGet(x => x.Project).Returns(mockProject.Object);

            this.mockFile.FileExists = true;

            //// act
            this.service.AddPlugin(
               mockProjectService.Object,
               plugin,
               "Test.Droid",
               Settings.Droid);

            //// assert
            mockProjectService.Verify(
                x => x.AddReference(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<bool>()), 
                Times.Never());
        }

        /// <summary>
        /// Tests the build source file.
        /// </summary>
        [Test]
        public void TestBuildSourceFile()
        {
            //// arrange
            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();
            mockProjectService.SetupGet(x => x.Name).Returns("Ninja");

            //// Mock the Project Service GetProjectItem.
            Mock<IProjectItemService> mockProjectItemService = new Mock<IProjectItemService>();
            mockProjectService.Setup(x => x.GetProjectItem(It.IsAny<string>())).Returns(mockProjectItemService.Object);

            //// Mock the ProjectItem Service Get ProjectItem.
            Mock<ProjectItem> mockProjectItem = new Mock<ProjectItem>();
            mockProjectItemService.SetupGet(x => x.ProjectItem).Returns(mockProjectItem.Object);

            //// Mock the Replace Text Function.
            mockProjectItemService.Setup(x => x.ReplaceText(It.IsAny<string>(), It.IsAny<string>()));

            //// act
            this.service.BuildSourceFile(
                mockProjectService.Object,
                "extensionSource",
                "extensionDestination",
                "friendlyName");

            //// assert
            mockProjectItemService.Verify(x => x.ReplaceText(It.IsAny<string>(), It.IsAny<string>()));
        }
    }
}
