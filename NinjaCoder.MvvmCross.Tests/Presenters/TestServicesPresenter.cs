// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestServicesPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Presenters
{
    using System.Collections.Generic;

    using Moq;
    using NinjaCoder.MvvmCross.Presenters;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.Views.Interfaces;
    using NUnit.Framework;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the TestServicesPresenter type.
    /// </summary>
    [TestFixture]
    public class TestServicesPresenter
    {
        /// <summary>
        /// The presenter.
        /// </summary>
        private ServicesPresenter presenter;

        /// <summary>
        /// The mock options view.
        /// </summary>
        private Mock<IServicesView> mockView;

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
            this.mockView = new Mock<IServicesView>();
            this.mockSettingsService = new Mock<ISettingsService>();

            this.presenter = new ServicesPresenter(
                this.mockView.Object,
                this.mockSettingsService.Object);
        }

        /// <summary>
        /// Tests the load.
        /// </summary>
        [Test]
        public void TestLoad()
        {
            List<string> viewModelNames = new List<string> { "hello" };
            List<ItemTemplateInfo> templateInfos = new List<ItemTemplateInfo> { new ItemTemplateInfo()};

            this.mockSettingsService.SetupGet(x => x.DisplayLogo).Returns(true);

            this.presenter.Load(viewModelNames, templateInfos);

            this.mockView.VerifySet(x => x.DisplayLogo = true);
            this.mockView.Verify(x => x.AddTemplate(It.IsAny<ItemTemplateInfo>()));
            this.mockView.Verify(x => x.AddViewModel(It.IsAny<string>()));
        }
    }
}
