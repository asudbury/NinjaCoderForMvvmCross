// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestCodeConfigTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Translators
{
    using NinjaCoder.MvvmCross.Translators;
    using NUnit.Framework;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the TestCodeConfigTranslator type.
    /// </summary>
    [TestFixture]
    public class TestCodeConfigTranslator
    {
        /// <summary>
        /// Tests the translator.
        /// </summary>
        [Test]
        public void TestTranslator()
        {
            CodeConfigTranslator translator = new CodeConfigTranslator();

            CodeConfig codeConfig = translator.Translate(Helper.GetTestDataPath("CodeConfig.xml"));

            Assert.IsTrue(codeConfig.NugetPackage != string.Empty);
        }

        /// <summary>
        /// Tests the translator file does not exist.
        /// </summary>
        [Test]
        public void TestTranslatorFileDoesNotExist()
        {
            CodeConfigTranslator translator = new CodeConfigTranslator();

            const string FileName = @"C";

            CodeConfig codeConfig = translator.Translate(FileName);

            Assert.IsTrue(codeConfig == null);
        }
    }
}
