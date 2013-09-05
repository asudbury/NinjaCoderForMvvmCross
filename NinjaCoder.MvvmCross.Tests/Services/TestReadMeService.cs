// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestReadMeService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Services
{
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using Moq;
    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Tests.Mocks;
    using NUnit.Framework;

    /// <summary>
    ///  Defines the TestReadMeService type.
    /// </summary>
    [TestFixture]
    public class TestReadMeService
    {
        /// <summary>
        /// The service.
        /// </summary>
        private ReadMeService service;

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
            this.mockFileSystem = new Mock<IFileSystem>();
            this.mockFileSystem.SetupGet(x => x.File).Returns(new MockFile());

            this.service = new ReadMeService(this.mockFileSystem.Object);
        }

        /// <summary>
        /// Tests the add lines.
        /// </summary>
        [Test]
        public void TestAddLines()
        {
            this.service.AddLines("path", "functionName", new List<string>());
        }
    }
}
