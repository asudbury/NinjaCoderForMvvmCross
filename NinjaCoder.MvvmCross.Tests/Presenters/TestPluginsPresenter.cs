// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestPluginsPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Presenters
{
    using System.Collections.Generic;

    using Moq;

    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Presenters;
    using NinjaCoder.MvvmCross.Views.Interfaces;
    using NUnit.Framework;

    /// <summary>
    ///  Defines the TestPluginsPresenter type.
    /// </summary>
    [TestFixture]
    public class TestPluginsPresenter
    {
        /// <summary>
        /// The presenter.
        /// </summary>
        private PluginsPresenter presenter;

        /// <summary>
        /// The mock view.
        /// </summary>
        private Mock<IPluginsView> mockView;

        /// <summary>
        /// The mock settings service.
        /// </summary>
        private Mock<ISettingsService> mockSettingsService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockView = new Mock<IPluginsView>();
            this.mockSettingsService = new Mock<ISettingsService>();

            this.presenter = new PluginsPresenter(
                this.mockView.Object,
                this.mockSettingsService.Object);
        }

        /// <summary>
        /// Tests the load.
        /// </summary>
        [Test]
        public void TestLoad()
        {
            List<string> viewModelNames = new List<string> { "ViewModelName" };
            List<Plugin> plugins = new List<Plugin> { new Plugin() };

            this.mockSettingsService.SetupGet(x => x.DisplayLogo).Returns(true);
            this.mockSettingsService.SetupGet(x => x.UseNugetForPlugins).Returns(true);

            this.presenter.Load(viewModelNames, plugins);

            this.mockView.Verify(x => x.AddViewModel(It.IsAny<string>()));
            this.mockView.Verify(x => x.AddCorePlugin(It.IsAny<Plugin>()));
            this.mockView.VerifySet(x => x.DisplayLogo = true);
            this.mockView.VerifySet(x => x.UseNuget = true);
        }

        /// <summary>
        /// Tests the save settings.
        /// </summary>
        [Test]
        public void TestSaveSettings()
        {
            this.mockView.SetupGet(x => x.UseNuget).Returns(true);

            this.presenter.SaveSettings();

            this.mockSettingsService.VerifySet(x => x.UseNugetForPlugins = true);
        }
    }
}
