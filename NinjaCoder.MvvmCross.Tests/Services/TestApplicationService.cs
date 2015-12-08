// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestCodeConfigService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Services
{
    using System.IO.Abstractions;
    using Moq;
    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;
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

            /*this.service = new ApplicationService(
                
                mockSettingsService.Object, 
                this.mockFileSystem.Object);*/
        }
    }
}
