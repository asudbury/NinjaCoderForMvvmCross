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
    using MvvmCross.Factories;
    using MvvmCross.Services.Interfaces;
    using NUnit.Framework;
    
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
    }
}
