// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestPluginsTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Translators
{
    using System;
    using System.IO;
    using System.IO.Abstractions;

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
            PluginsTranslator translator = new PluginsTranslator(new PluginTranslator(new SettingsService()));

            SettingsService settingsService = new SettingsService();

            DirectoryInfoBase directoryInfo1 = new DirectoryInfo(settingsService.MvvmCrossAssembliesPath);
            DirectoryInfoBase directoryInfo2 = new DirectoryInfo(settingsService.MvvmCrossAssembliesOverrideDirectory);

            Tuple<DirectoryInfoBase, DirectoryInfoBase> directories = new Tuple<DirectoryInfoBase, DirectoryInfoBase>(directoryInfo1, directoryInfo2);

            Plugins plugins = translator.Translate(directories);

            Assert.IsTrue(plugins.Items.Count > 0);
        }
    }
}
