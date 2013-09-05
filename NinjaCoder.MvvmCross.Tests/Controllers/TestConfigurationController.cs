// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestConfigurationController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Controllers
{
    using Moq;
    using NinjaCoder.MvvmCross.Controllers;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NUnit.Framework;

    /// <summary>
    ///  Defines the TestConfigurationController type.
    /// </summary>
    [TestFixture]
    public class TestConfigurationController
    {
        /// <summary>
        /// The controller.
        /// </summary>
        private ConfigurationController controller;

        /// <summary>
        /// The mock configuration service.
        /// </summary>
        private Mock<IConfigurationService> mockConfigurationService;

        /// <summary>
        /// The mock visual studio service.
        /// </summary>
        private Mock<IVisualStudioService> mockVisualStudioService;

        /// <summary>
        /// The mock read me service.
        /// </summary>
        private Mock<IReadMeService> mockReadMeService;

        /// <summary>
        /// The mock settings service.
        /// </summary>
        private Mock<ISettingsService> mockSettingsService;

        /// <summary>
        /// The mock message box service.
        /// </summary>
        private Mock<IMessageBoxService> mockMessageBoxService;

        /// <summary>
        /// The mock dialog service.
        /// </summary>
        private Mock<IDialogService> mockDialogService;

        /// <summary>
        /// The mock forms service.
        /// </summary>
        private Mock<IFormsService> mockFormsService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockConfigurationService = new Mock<IConfigurationService>();
            this.mockVisualStudioService = new Mock<IVisualStudioService>();
            this.mockReadMeService = new Mock<IReadMeService>();
            this.mockSettingsService = new Mock<ISettingsService>();
            this.mockMessageBoxService = new Mock<IMessageBoxService>();
            this.mockDialogService = new Mock<IDialogService>();
            this.mockFormsService = new Mock<IFormsService>();

            this.controller = new ConfigurationController(
                this.mockConfigurationService.Object,
                this.mockVisualStudioService.Object,
                this.mockReadMeService.Object,
                this.mockSettingsService.Object,
                this.mockMessageBoxService.Object,
                this.mockDialogService.Object,
                this.mockFormsService.Object);
        }

        /// <summary>
        /// Tests the run.
        /// </summary>
        [Test]
        public void TestRun()
        {
            this.controller.Run();

            this.mockConfigurationService.Verify(x => x.CreateUserDirectories());
        }
    }
}
