// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestPluginsTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Translators
{
    using System.IO;
    using Entities;
    using MvvmCross.Services;
    using MvvmCross.Translators;
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
            PluginsTranslator translator = new PluginsTranslator(new PluginTranslator());

            SettingsService settingsService = new SettingsService();
            string path = settingsService.CorePluginsPath;

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            Plugins plugins = translator.Translate(directoryInfo);

            Assert.IsTrue(plugins.Items.Count > 0);
        }
    }
}
