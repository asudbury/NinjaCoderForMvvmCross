// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestPluginsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.Runtime.InteropServices;
    using Castle.DynamicProxy.Generators;
    using Moq;
    using NinjaCoder.MvvmCross.Controllers;
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.Tests.Mocks;
    using NUnit.Framework;

    using Scorchio.Infrastructure.Services;
    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the TestPluginsController type.
    /// </summary>
    [TestFixture]
    public class TestPluginsController
    {
        /// <summary>
        /// The controller.
        /// </summary>
        private PluginsController controller;

        /// <summary>
        /// The mock file system.
        /// </summary>
        private Mock<IFileSystem> mockFileSystem;

        /// <summary>
        /// The mock plugins service.
        /// </summary>
        private Mock<IPluginsService> mockPluginsService;

        /// <summary>
        /// The mock nuget service.
        /// </summary>
        private Mock<INugetService> mockNugetService;

        /// <summary>
        /// The mock visual studio service.
        /// </summary>
        private Mock<IVisualStudioService> mockVisualStudioService;

        /// <summary>
        /// The mock settings service.
        /// </summary>
        private Mock<ISettingsService> mockSettingsService;

        /// <summary>
        /// The mock message box service.
        /// </summary>
        private Mock<IMessageBoxService> mockMessageBoxService;

        /// <summary>
        /// The mock translator.
        /// </summary>
        private Mock<ITranslator<Tuple<DirectoryInfoBase, DirectoryInfoBase>, Plugins>> mockTranslator;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            AttributesToAvoidReplicating.Add<TypeIdentifierAttribute>();

            this.mockFileSystem = new Mock<IFileSystem>();
            this.mockPluginsService = new Mock<IPluginsService>();
            this.mockNugetService = new Mock<INugetService>();
            this.mockVisualStudioService = new Mock<IVisualStudioService>();
            this.mockSettingsService = new Mock<ISettingsService>();
            this.mockMessageBoxService = new Mock<IMessageBoxService>();
            this.mockTranslator = new Mock<ITranslator<Tuple<DirectoryInfoBase, DirectoryInfoBase>, Plugins>>();
            
            /*this.controller = new PluginsController(
                this.mockFileSystem.Object,
                this.mockPluginsService.Object,
                this.mockNugetService.Object,
                this.mockVisualStudioService.Object,
                this.mockReadMeService.Object,
                this.mockSettingsService.Object,
                this.mockMessageBoxService.Object,
                this.mockTranslator.Object);*/
        }

        /// <summary>
        /// Tests the run.
        /// </summary>
        [Test]
        public void TestRun()
        {
            this.mockVisualStudioService.SetupGet(x => x.IsMvvmCrossSolution).Returns(true);

            /*PluginsForm pluginsForm = new PluginsForm(new SettingsService(), new List<string>(),  new Plugins());
            */
            /*
            this.mockFormsService.Setup(x => x.GetPluginsForm(
                It.IsAny<ISettingsService>(),
                It.IsAny<IEnumerable<string>>(), 
                It.IsAny<Plugins>())).Returns(pluginsForm);

            this.mockDialogService.Setup(x => x.ShowDialog(It.IsAny<Form>())).Returns(DialogResult.OK);
            */

            MockDirectoryInfoFactory mockDirectoryInfoFactory = new MockDirectoryInfoFactory();
            this.mockFileSystem.SetupGet(x => x.DirectoryInfo).Returns(mockDirectoryInfoFactory);

            this.mockSettingsService.SetupGet(x => x.InstalledDirectory).Returns("path");

            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();

            this.mockVisualStudioService.SetupGet(x => x.CoreProjectService).Returns(mockProjectService.Object);

            this.controller.Run();
        }

        /// <summary>
        /// Tests the run.
        /// </summary>
        [Test]
        public void TestRunNotMvvmCrossSolution()
        {
            this.mockVisualStudioService.SetupGet(x => x.IsMvvmCrossSolution).Returns(false);

            this.controller.Run();
        }
    }
}
