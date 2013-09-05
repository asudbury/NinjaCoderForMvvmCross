// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestUpdateVersionService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Services
{
    using System.IO;
    using NinjaCoder.MvvmCross.Services;
    using NUnit.Framework;

    /// <summary>
    /// Defines the TestUpdateVersionService type.
    /// </summary>
    [TestFixture]
    public class TestUpdateVersionService
    {
        /// <summary>
        /// Tests the run.
        /// </summary>
        [Test]
        public void TestRun()
        {
            UpdateVersionService service = new UpdateVersionService();

            string path = Helper.GetTestDataPath("AssemblyInfo.cs");
            const string Version = "1.5.8";

            service.Run(path, Version);

            StreamReader reader = new StreamReader(path);
            string contents = reader.ReadToEnd();

            Assert.IsFalse(contents.Contains(Version) == false);
        }
    }
}
