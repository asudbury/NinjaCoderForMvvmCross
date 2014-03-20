// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestMockingServiceFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Factories
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;

    using NinjaCoder.MvvmCross.Factories;
    using NinjaCoder.MvvmCross.Infrastructure.Services;

    using NUnit.Framework;

    using Scorchio.Infrastructure.Constants;
    using Scorchio.Infrastructure.Services.Testing;
    using Scorchio.Infrastructure.Services.Testing.Interfaces;

    /// <summary>
    ///  Defines the TestMockingServiceFactory type.
    /// </summary>
    public class TestMockingServiceFactory
    {
        /// <summary>
        /// The mocking service factory.
        /// </summary>
        private MockingServiceFactory factory;

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

            this.factory = new MockingServiceFactory(this.mockSettingsService.Object);
        }

        /// <summary>
        /// Tests the current frame work.
        /// </summary>
        [Test]
        public void TestCurrentFrameWork()
        {
            this.mockSettingsService.SetupGet(x => x.MockingFramework).Returns("mockFrameWork");

            string frameWork = this.factory.CurrentFrameWork;

            Assert.IsTrue(frameWork == "mockFrameWork");
        }

        /// <summary>
        /// Tests the frame works.
        /// </summary>
        [Test]
        public void TestFrameWorks()
        {
            IEnumerable<string> frameWorks = this.factory.FrameWorks;

            Assert.IsTrue(frameWorks.Count() == 3);
        }

        /// <summary>
        /// Tests the get mocking service.
        /// </summary>
        [Test]
        public void TestGetMoqMockingService()
        {
            this.mockSettingsService.SetupGet(x => x.MockingFramework).Returns(TestingConstants.Moq.Name);

            IMockingService mockingService = this.factory.GetMockingService();

            MoqMockingService moqMockingService = (MoqMockingService)mockingService;

            Assert.IsTrue(moqMockingService != null);
        }

        /// <summary>
        /// Tests the get rhino mocks mocking service.
        /// </summary>
        [Test]
        public void TestGetRhinoMocksMockingService()
        {
            this.mockSettingsService.SetupGet(x => x.MockingFramework).Returns(TestingConstants.RhinoMocks.Name);

            IMockingService mockingService = this.factory.GetMockingService();

            RhinoMocksMockingService rhinoMockMockingService = (RhinoMocksMockingService)mockingService;

            Assert.IsTrue(rhinoMockMockingService != null);
        }

        /// <summary>
        /// Tests the get nsubstitute mocking service.
        /// </summary>
        [Test]
        public void TestGetNSubstituteMockingService()
        {
            this.mockSettingsService.SetupGet(x => x.MockingFramework).Returns(TestingConstants.NSubstitute.Name);

            IMockingService mockingService = this.factory.GetMockingService();

            NSubstituteMockingService nSubstituteMockingService = (NSubstituteMockingService)mockingService;

            Assert.IsTrue(nSubstituteMockingService != null);
        }
    }
}
