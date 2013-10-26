// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestItemTemplatesPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Presenters
{
    using System.Collections.Generic;

    using Moq;

    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Presenters;
    using NinjaCoder.MvvmCross.Views.Interfaces;
    using NUnit.Framework;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the TestItemTemplatesPresenter type.
    /// </summary>
    [TestFixture]
    public class TestItemTemplatesPresenter
    {
        /// <summary>
        /// The presenter.
        /// </summary>
        private ItemTemplatesPresenter presenter;

        /// <summary>
        /// The view.
        /// </summary>
        private Mock<IItemTemplatesView> mockView;

        /// <summary>
        /// The settings service.
        /// </summary>
        private Mock<ISettingsService> mockSettingsService;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockView = new Mock<IItemTemplatesView>();
            this.mockSettingsService = new Mock<ISettingsService>();

            this.presenter = new ItemTemplatesPresenter(
                this.mockView.Object,
                this.mockSettingsService.Object);
        }

        /// <summary>
        /// Tests the load.
        /// </summary>
        [Test]
        public void TestLoad()
        {
            this.presenter.Load(new List<ItemTemplateInfo> { new ItemTemplateInfo() });

            this.mockView.Verify(x => x.AddTemplate(It.IsAny<ItemTemplateInfo>()));
        }

        /// <summary>
        /// Tests the get required item templates.
        /// </summary>
        [Test]
        public void TestGetRequiredItemTemplates()
        {
            this.presenter.GetRequiredItemTemplates();

            this.mockView.Verify(x => x.RequiredTemplates);
        }
    }
}
