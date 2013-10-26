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
            Assert.IsTrue(codeConfig.BootstrapFileNameOverride == "CommunitySqlitePluginBootstrap.cs");

            Assert.IsTrue(codeConfig.References.Count == 2);
            Assert.IsTrue(codeConfig.References[0] == "R1");
            Assert.IsTrue(codeConfig.References[1] == "R2");

            Assert.IsTrue(codeConfig.NugetInstallationMandatory == "Y");

            Assert.IsTrue(codeConfig.DependentPlugins.Count == 2);
            Assert.IsTrue(codeConfig.DependentPlugins[0] == "Plugin1");
            Assert.IsTrue(codeConfig.DependentPlugins[1] == "Plugin2");

            Assert.IsTrue(codeConfig.CodeDependencies.Count == 1);
            Assert.IsTrue(codeConfig.CodeDependencies[0].Class == "Class1");
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
