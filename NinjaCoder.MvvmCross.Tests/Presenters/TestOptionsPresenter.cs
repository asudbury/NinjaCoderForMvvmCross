// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestOptionsPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Presenters
{
    using Moq;
    using NinjaCoder.MvvmCross.Presenters;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.Views.Interfaces;
    using NUnit.Framework;

    /// <summary>
    ///  Defines the TestOptionsPresenter type.
    /// </summary>
    [TestFixture]
    public class TestOptionsPresenter
    {
        /// <summary>
        /// The presenter.
        /// </summary>
        private OptionsPresenter presenter;

        /// <summary>
        /// The mock view.
        /// </summary>
        private Mock<IOptionsView> mockView;

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
            this.mockView = new Mock<IOptionsView>();
            this.mockSettingsService = new Mock<ISettingsService>();

            this.presenter = new OptionsPresenter(
                this.mockView.Object,
                this.mockSettingsService.Object);
        }

        /// <summary>
        /// Tests the load settings.
        /// </summary>
        [Test]
        public void TestLoadSettings()
        {
            //// setup
            this.mockSettingsService.SetupGet(x => x.LogToTrace).Returns(true);
            this.mockSettingsService.SetupGet(x => x.LogToFile).Returns(true);
            this.mockSettingsService.SetupGet(x => x.LogFilePath).Returns(@"path");
            this.mockSettingsService.SetupGet(x => x.IncludeLibFolderInProjects).Returns(true);
            this.mockSettingsService.SetupGet(x => x.DisplayErrors).Returns(true);
            this.mockSettingsService.SetupGet(x => x.RemoveDefaultComments).Returns(true);
            this.mockSettingsService.SetupGet(x => x.RemoveDefaultFileHeaders).Returns(true);
            this.mockSettingsService.SetupGet(x => x.UseNugetForProjectTemplates).Returns(true);
            this.mockSettingsService.SetupGet(x => x.UseNugetForPlugins).Returns(true);
            this.mockSettingsService.SetupGet(x => x.SuspendReSharperDuringBuild).Returns(true);

            this.presenter.LoadSettings();

            this.mockView.VerifySet(x => x.TraceOutputEnabled = true);
            this.mockView.VerifySet(x => x.LogToFile = true);
            this.mockView.VerifySet(x => x.LogFilePath = "path");
            this.mockView.VerifySet(x => x.IncludeLibFolderInProjects = true);
            this.mockView.VerifySet(x => x.DisplayErrors = true);
            this.mockView.VerifySet(x => x.RemoveDefaultComments = true);
            this.mockView.VerifySet(x => x.RemoveDefaultFileHeaders = true);
            this.mockView.VerifySet(x => x.UseNugetForProjectTemplates = true);
            this.mockView.VerifySet(x => x.UseNugetForPlugins = true);
            this.mockView.VerifySet(x => x.SuspendReSharperDuringBuild = true);
        }

        /// <summary>
        /// Tests the save settings.
        /// </summary>
        [Test]
        public void TestSaveSettings()
        {
            this.mockView.SetupGet(x => x.TraceOutputEnabled).Returns(true);
            this.mockView.SetupGet(x => x.LogToFile).Returns(true);
            this.mockView.SetupGet(x => x.LogFilePath).Returns(@"path");
            this.mockView.SetupGet(x => x.IncludeLibFolderInProjects).Returns(true);
            this.mockView.SetupGet(x => x.DisplayErrors).Returns(true);
            this.mockView.SetupGet(x => x.RemoveDefaultComments).Returns(true);
            this.mockView.SetupGet(x => x.RemoveDefaultFileHeaders).Returns(true);
            this.mockView.SetupGet(x => x.UseNugetForProjectTemplates).Returns(true);
            this.mockView.SetupGet(x => x.UseNugetForPlugins).Returns(true);
            this.mockView.SetupGet(x => x.SuspendReSharperDuringBuild).Returns(true);

            this.presenter.SaveSettings();

            this.mockSettingsService.VerifySet(x => x.LogToTrace = true);
            this.mockSettingsService.VerifySet(x => x.LogToFile = true);
            this.mockSettingsService.VerifySet(x => x.LogFilePath = "path");
            this.mockSettingsService.VerifySet(x => x.IncludeLibFolderInProjects = true);
            this.mockSettingsService.VerifySet(x => x.DisplayErrors = true);
            this.mockSettingsService.VerifySet(x => x.RemoveDefaultComments = true);
            this.mockSettingsService.VerifySet(x => x.RemoveDefaultFileHeaders = true);
            this.mockSettingsService.VerifySet(x => x.UseNugetForProjectTemplates = true);
            this.mockSettingsService.VerifySet(x => x.UseNugetForPlugins = true);
            this.mockSettingsService.VerifySet(x => x.SuspendReSharperDuringBuild = true);
        }
    }
}
