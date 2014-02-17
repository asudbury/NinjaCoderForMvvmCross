// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestCodeConfigService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Services
{
    using System.IO.Abstractions;

    using Moq;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Tests.Mocks;

    using NUnit.Framework;

    /// <summary>
    ///  Defines the TestCodeConfigService type.
    /// </summary>
    [TestFixture]
    public class TestApplicationService
    {
        /// <summary>
        /// The service.
        /// </summary>
        private ApplicationService service;

        /// <summary>
        /// The mock settings service.
        /// </summary>
        private Mock<ISettingsService> mockSettingsService;

        /// <summary>
        /// The mock file system.
        /// </summary>
        private Mock<IFileSystem> mockFileSystem;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockSettingsService = new Mock<ISettingsService>();
            this.mockFileSystem = new Mock<IFileSystem>();

            this.service = new ApplicationService(
                mockSettingsService.Object, 
                this.mockFileSystem.Object);
        }

        /// <summary>
        /// Tests the check for updates.
        /// </summary>
        [Test]
        public void TestCheckForUpdates()
        {
            SettingsService settingsService = new SettingsService();

            this.mockSettingsService.SetupGet(x => x.UpdateCheckerPath).Returns(settingsService.UpdateCheckerPath);

            MockFile mockFile = new MockFile { FileExists = true };

            this.mockFileSystem.SetupGet(x => x.File).Returns(mockFile);
            
            this.service.CheckForUpdates();
        }

        /// <summary>
        /// Tests the is update available - true
        /// </summary>
        [Test]
        public void TestIsUpdateAvailableTrue()
        {
            this.mockSettingsService.SetupGet(x => x.MvvmCrossVersion).Returns("1.0.0");
            this.mockSettingsService.SetupGet(x => x.LatestVersionOnGallery).Returns("1.0.1");

            bool available = this.service.IsUpdateAvailable();

            Assert.IsTrue(available);
        }

        /// <summary>
        /// Tests the is update available - false
        /// </summary>
        [Test]
        public void TestIsUpdateAvailableFalse()
        {
            this.mockSettingsService.SetupGet(x => x.MvvmCrossVersion).Returns("1.0.1");
            this.mockSettingsService.SetupGet(x => x.LatestVersionOnGallery).Returns("1.0.0");

            bool available = this.service.IsUpdateAvailable();

            Assert.IsFalse(available);
        }
    }
}
