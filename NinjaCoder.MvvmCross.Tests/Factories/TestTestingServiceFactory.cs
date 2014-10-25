// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestTestingServiceFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Factories
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;

    using NinjaCoder.MvvmCross.Factories;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using NUnit.Framework;

    using Scorchio.Infrastructure.Constants;
    using Scorchio.Infrastructure.Services.Testing;
    using Scorchio.Infrastructure.Services.Testing.Interfaces;

    /// <summary>
    ///  Defines the TestTestingServiceFactory type.
    /// </summary>
    [TestFixture]
    public class TestTestingServiceFactory
    {
        /// <summary>
        /// The testing service factory.
        /// </summary>
        private TestingServiceFactory factory;

        /// <summary>
        /// The mock settings service.
        /// </summary>
        private Mock<ISettingsService> mockSettingsService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockSettingsService = new Mock<ISettingsService>();

            this.factory = new TestingServiceFactory(this.mockSettingsService.Object);
        }

        /// <summary>
        /// Tests the current frame work.
        /// </summary>
        [Test]
        public void TestCurrentFrameWork()
        {
            this.mockSettingsService.SetupGet(x => x.TestingFramework).Returns("testingFrameWork");

            string frameWork = this.factory.CurrentFrameWork;

            Assert.IsTrue(frameWork == "testingFrameWork");
        }

        /// <summary>
        /// Tests the frame works.
        /// </summary>
        [Test]
        public void TestFrameWorks()
        {
            IEnumerable<string> frameWorks = this.factory.FrameWorks;

            Assert.IsTrue(frameWorks.Count() == 2);
        }

        /// <summary>
        /// Tests the get nunit testing service.
        /// </summary>
        [Test]
        public void TestGetNUnitTestingService()
        {
            this.mockSettingsService.SetupGet(x => x.MockingFramework).Returns(TestingConstants.NUnit.Name);

            ITestingService testingService = this.factory.GetTestingService();

            NUnitTestingService nUnitTestingService = (NUnitTestingService)testingService;

            Assert.IsTrue(nUnitTestingService != null);
        }

        /// <summary>
        /// Tests the get ms test testing service.
        /// </summary>
        [Test]
        public void TestGetMsTestTestingService()
        {
            this.mockSettingsService.SetupGet(x => x.MockingFramework).Returns(TestingConstants.MsTest.Name);

            ITestingService testingService = this.factory.GetTestingService();
            
            MsTestTestingService msTestTestingService = (MsTestTestingService)testingService;

            Assert.IsTrue(msTestTestingService != null);
        }
    }
}
