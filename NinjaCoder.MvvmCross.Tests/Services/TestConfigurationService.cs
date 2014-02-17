// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestConfigurationService type.
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
    ///  Defines the TestConfigurationService type.
    /// </summary>
    [TestFixture]
    public class TestConfigurationService
    {
        /// <summary>
        /// The service.
        /// </summary>
        private ConfigurationService service;

        /// <summary>
        /// The mock file system.
        /// </summary>
        private Mock<IFileSystem> mockFileSystem;

        /// <summary>
        /// The mock setting service.
        /// </summary>
        private Mock<ISettingsService> mockSettingService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockFileSystem = new Mock<IFileSystem>();
            this.mockSettingService = new Mock<ISettingsService>();

            MockDirectory mockDirectory = new MockDirectory { DirectoryExists = true };

            this.mockFileSystem.SetupGet(x => x.Directory).Returns(mockDirectory);

            this.service = new ConfigurationService(
                this.mockFileSystem.Object,
                this.mockSettingService.Object);
        }

        /// <summary>
        /// Tests the create user directories.
        /// </summary>
        [Test]
        public void TestCreateUserDirectories()
        {
            this.service.CreateUserDirectories();
        }
    }
}
