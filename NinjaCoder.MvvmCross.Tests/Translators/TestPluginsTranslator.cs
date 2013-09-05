// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestPluginsTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Tests.Translators
{
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Services;
    using NinjaCoder.MvvmCross.Translators;
    using NUnit.Framework;

    /// <summary>
    ///  Defines the TestPluginsTranslator type.
    /// </summary>
    [TestFixture]
    public class TestPluginsTranslator
    {
        /// <summary>
        /// Tests the translator.
        /// </summary>
        [Test]
        public void TestTranslator()
        {
            PluginsTranslator translator = new PluginsTranslator();

            SettingsService settingsService = new SettingsService();
            string path = settingsService.CorePluginsPath;

            Plugins plugins = translator.Translate(path);

            Assert.IsTrue(plugins.Items.Count > 0);
        }

        /// <summary>
        /// Tests the translator directory does not exist.
        /// </summary>
        [Test]
        public void TestTranslatorDirectoryDoesNotExist()
        {
            PluginsTranslator translator = new PluginsTranslator();

            Plugins plugins = translator.Translate("path");

            Assert.IsTrue(plugins == null);
        }

        /// <summary>
        /// Tests the get plug in file does not exist.
        /// </summary>
        [Test]
        public void TestGetPlugInFileDoesNotExist()
        {
            PluginsTranslator translator = new PluginsTranslator();

            Plugin plugin = translator.GetPlugin("path");
            
            Assert.IsTrue(plugin == null);
        }
    }
}
