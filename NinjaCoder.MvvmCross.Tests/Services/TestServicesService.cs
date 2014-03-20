// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestServicesService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Services
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;

    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using NUnit.Framework;

    using Scorchio.Infrastructure.Services;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    /// Defines the TestServicesService type.
    /// </summary>
    [TestFixture]
    public class TestServicesService
    {
        /// <summary>
        /// The service.
        /// </summary>
        private ServicesService service;
        
        /// <summary>
        /// The mock code config factory.
        /// </summary>
        private Mock<ICodeConfigFactory> mockCodeConfigFactory;

        /// <summary>
        /// The mock code snippet factory.
        /// </summary>
        private Mock<ICodeSnippetFactory> mockCodeSnippetFactory;

        /// <summary>
        /// The mock settings service.
        /// </summary>
        private Mock<ISettingsService> mockSettingsService;

        /// <summary>
        /// The mock message box service.
        /// </summary>
        private Mock<IMessageBoxService> mockMessageBoxService;

        /// <summary>
        /// The mock nuget service.
        /// </summary>
        private Mock<INugetService> mockNugetService;

        /// <summary>
        /// The mock plugin factory.
        /// </summary>
        private Mock<IPluginFactory> mockPluginFactory;
        
        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockCodeConfigFactory = new Mock<ICodeConfigFactory>();
            this.mockCodeSnippetFactory = new Mock<ICodeSnippetFactory>();
            this.mockSettingsService = new Mock<ISettingsService>();
            this.mockMessageBoxService = new Mock<IMessageBoxService>();
            this.mockNugetService = new Mock<INugetService>();
            this.mockPluginFactory = new Mock<IPluginFactory>();

            //// make sure factories setup correctly!
            Mock<IPluginsService> mockPluginsService = new Mock<IPluginsService>();
            this.mockPluginFactory.Setup(x => x.GetPluginsService())
                .Returns(mockPluginsService.Object);

            Mock<IProjectItemService> mockProjectItemService = new Mock<IProjectItemService>();

            Mock<ICodeSnippetService> mockCodeSnippetService = new Mock<ICodeSnippetService>();
            mockCodeSnippetService.Setup(
                x =>
                    x.CreateUnitTests(
                        It.IsAny<IVisualStudioService>(),
                        It.IsAny<IProjectService>(),
                        It.IsAny<CodeSnippet>(),
                        It.IsAny<string>(),
                        It.IsAny<string>(),
                        It.IsAny<string>()))
                        .Returns(mockProjectItemService.Object);
            
            this.mockCodeSnippetFactory.Setup(x => x.GetCodeSnippetService())
                .Returns(mockCodeSnippetService.Object);

            Mock<ICodeConfigService> mockCodeConfigService = new Mock<ICodeConfigService>();
            this.mockCodeConfigFactory.Setup(x => x.GetCodeConfigService())
                .Returns(mockCodeConfigService.Object);

            this.service = new ServicesService(
                this.mockCodeConfigFactory.Object,
                this.mockCodeSnippetFactory.Object,
                this.mockSettingsService.Object,
                this.mockMessageBoxService.Object,
                this.mockNugetService.Object,
                this.mockPluginFactory.Object);
        }

        /// <summary>
        /// Tests the constructor.
        /// </summary>
        [Test]
        public void TestInit()
        {
            this.service.Init();

            this.mockPluginFactory.Verify(x => x.GetPluginsService());
            this.mockCodeSnippetFactory.Verify(x => x.GetCodeSnippetService());
            this.mockCodeConfigFactory.Verify(x => x.GetCodeConfigService());

            IPluginsService pluginsService = this.mockPluginFactory.Object.GetPluginsService();

            Assert.IsTrue(pluginsService != null);

            ICodeSnippetService codeSnippetService = this.mockCodeSnippetFactory.Object.GetCodeSnippetService();

            Assert.IsTrue(codeSnippetService != null);
            
            ICodeConfigService codeConfigService = this.mockCodeConfigFactory.Object.GetCodeConfigService();

            Assert.IsTrue(codeConfigService != null);
        }

        /// <summary>
        /// Tests the add services.
        /// </summary>
        [Test]
        public void TestAddServices()
        {
            Mock<IVisualStudioService> mockVisualStudioService = new Mock<IVisualStudioService>();

            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();

            mockVisualStudioService.SetupGet(x => x.CoreProjectService)
                .Returns(mockProjectService.Object);

            Mock<IDTEService> mockDTEService = new Mock<IDTEService>();

            mockVisualStudioService.SetupGet(x => x.DTEService)
                .Returns(mockDTEService.Object);

            List<ItemTemplateInfo> templateInfos = new List<ItemTemplateInfo> { new ItemTemplateInfo()};
            const string ViewModelName = null;

            //// we are testing messages are reset.
            service.Messages.Add("notrequiredmessage");

            service.AddServices(
                mockVisualStudioService.Object, 
                templateInfos, 
                ViewModelName, 
                true);

            Assert.IsTrue(!this.service.Messages.Any());
        }

        /// <summary>
        /// Tests the add service.
        /// </summary>
        [Test]
        public void TestAddService()
        {
            Mock<IVisualStudioService> mockVisualStudioService = new Mock<IVisualStudioService>();

            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();

            mockVisualStudioService.SetupGet(x => x.CoreProjectService).Returns(mockProjectService.Object);

            ItemTemplateInfo itemTemplateInfo = new ItemTemplateInfo();

            service.AddService(
                mockVisualStudioService.Object, 
                mockProjectService.Object, 
                itemTemplateInfo);

            mockProjectService.Verify(x => x.AddToFolderFromTemplate(
                It.IsAny<string>(), 
                It.IsAny<string>()));

            this.mockCodeConfigFactory.Verify(x => x.GetServiceConfig(
                It.IsAny<string>()));
        }

        /// <summary>
        /// Processes the config.
        /// </summary>
        [Test]
        public void TestProcessConfig()
        {
            Mock<IVisualStudioService> mockVisualStudioService = new Mock<IVisualStudioService>();

            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();

            mockVisualStudioService.SetupGet(x => x.CoreProjectService)
                .Returns(mockProjectService.Object);

            CodeConfig codeConfig = new CodeConfig();

            service.ProcessConfig(
                mockVisualStudioService.Object,
                mockProjectService.Object,
                codeConfig);
        }

        /// <summary>
        /// Tests the add dependant plugin.
        /// </summary>
        [Test]
        public void TestAddDependantPlugin()
        {
            Mock<IVisualStudioService> mockVisualStudioService = new Mock<IVisualStudioService>();

            this.mockPluginFactory.Setup(x => x.GetPluginByName(
                It.IsAny<string>()))
                .Returns(new Plugin());

            this.service.AddDependantPlugin(
                mockVisualStudioService.Object,
                "dependantPlugin");
        }

        /// <summary>
        /// Tests the on file added to project.
        /// </summary>
        [Test]
        public void TestOnFileAddedToProject()
        {
            Mock<IProjectItemService> mockProjectItemService = new Mock<IProjectItemService>();

            mockProjectItemService.Setup(x => x.IsCSharpFile())
                .Returns(true);

            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();

            mockProjectItemService.SetupGet(x => x.ContainingProjectService)
                .Returns(mockProjectService.Object);

            this.mockSettingsService.SetupGet(x => x.RemoveDefaultComments)
                .Returns(true);

            this.mockSettingsService.SetupGet(x => x.RemoveDefaultFileHeaders)
                .Returns(true);

            this.service.OnFileAddedToProject(mockProjectItemService.Object);

            mockProjectItemService.Verify(x => x.RemoveComments());
            mockProjectItemService.Verify(x => x.RemoveHeader());
        }

        /// <summary>
        /// Tests the add services to view model.
        /// </summary>
        [Test]
        public void TestAddServicesToViewModel()
        {
            Mock<IVisualStudioService> mockVisualStudioService = new Mock<IVisualStudioService>();

            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();

            mockVisualStudioService.SetupGet(x => x.CoreProjectService)
                .Returns(mockProjectService.Object);

            List<ItemTemplateInfo> templateInfos = new List<ItemTemplateInfo>();

            Mock<IProjectItemService> mockProjectItemService = new Mock<IProjectItemService>();

            mockProjectService.Setup(x => x.GetProjectItem(
                It.IsAny<string>()))
                .Returns(mockProjectItemService.Object);

           service.AddServicesToViewModel(
               mockVisualStudioService.Object,
               templateInfos,
               "ViewModelName",
               true,
               mockProjectService.Object); 
        }

        /// <summary>
        /// Tests the implement code snippet.
        /// </summary>
        [Test]
        public void TestImplementCodeSnippet()
        {
            Mock<IVisualStudioService> mockVisualStudioService = new Mock<IVisualStudioService>();

            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();

            mockVisualStudioService.SetupGet(x => x.CoreProjectService)
                .Returns(mockProjectService.Object);

            Mock<IProjectItemService> mockProjectItemService = new Mock<IProjectItemService>();

            ItemTemplateInfo itemTemplateInfo = new ItemTemplateInfo();

            this.mockCodeSnippetFactory
                .Setup(x => x.GetServiceSnippet(
                    It.IsAny<string>()))
                    .Returns(new CodeSnippet());

            service.ImplementCodeSnippet(
                mockVisualStudioService.Object,
                "viewModelName",
                mockProjectService.Object,
                mockProjectItemService.Object,
                itemTemplateInfo);

            mockProjectItemService
                .Verify(x => x.ImplementCodeSnippet(
                    It.IsAny<CodeSnippet>(), 
                    It.IsAny<bool>()));
        }

        /// <summary>
        /// Tests the implement test code snippet.
        /// </summary>
        [Test]
        public void TestImplementTestCodeSnippet()
        {
            Mock<IVisualStudioService> mockVisualStudioService = new Mock<IVisualStudioService>();

            ItemTemplateInfo itemTemplateInfo = new ItemTemplateInfo();

            this.service.ImplementTestCodeSnippet(
                mockVisualStudioService.Object,
                "viewModelName",
                itemTemplateInfo);
        }
    }
}
