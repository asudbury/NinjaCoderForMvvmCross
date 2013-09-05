namespace NinjaCoder.MvvmCross.Tests.Infrastructure.IoC
{
    using NUnit.Framework;
    using TinyIoC;

    /// <summary>
    /// Defines the TestTinyIoC type.
    /// </summary>
    [TestFixture]
    public class TestTinyIoC
    {
        /// <summary>
        /// Tests the auto register.
        /// </summary>
        [Test]
        public void TestAutoRegister()
        {
            TinyIoCContainer container = TinyIoCContainer.Current;
            container.AutoRegister();
        }
    }
}
