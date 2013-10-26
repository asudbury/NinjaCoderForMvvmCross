// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestPluginsTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Translators
{
    using System.IO;
    using Entities;
    using MvvmCross.Translators;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
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
            string path = settingsService.InstalledDirectory + @"Plugins\Core";

            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            Plugins plugins = translator.Translate(directoryInfo);

            Assert.IsTrue(plugins.Items.Count > 0);
        }
    }
}
