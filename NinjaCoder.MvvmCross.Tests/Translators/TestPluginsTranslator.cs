// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestPluginsTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Translators
{
    using Entities;
    using MvvmCross.Translators;

    using NinjaCoder.MvvmCross.Services;

    using NUnit.Framework;
    using System.Collections.Generic;
    using System.IO.Abstractions;

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

            List<DirectoryInfoBase> directories = new List<DirectoryInfoBase>();

            Plugins plugins = translator.Translate(directories);

            ////Assert.IsTrue(plugins.Items.Count > 0);
        }
    }
}
