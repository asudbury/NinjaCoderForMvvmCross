// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestViewModelOptionsPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Presenters
{
    using System.Collections.Generic;
    using System.Linq;

    using Moq;

    using NinjaCoder.MvvmCross.Presenters;
    using NinjaCoder.MvvmCross.Views.Interfaces;
    using NUnit.Framework;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the TestViewModelOptionsPresenter type.
    /// </summary>
    [TestFixture]
    public class TestViewModelOptionsPresenter
    {
        /// <summary>
        /// The presenter.
        /// </summary>
        private ViewModelViewsPresenter presenter;

        /// <summary>
        /// The mock view.
        /// </summary>
        private Mock<IViewModelViewsView> mockView;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockView = new Mock<IViewModelViewsView>();

            this.presenter = new ViewModelViewsPresenter(this.mockView.Object);
        }

        /// <summary>
        /// Tests the load.
        /// </summary>
        [Test]
        public void TestLoad()
        {
            List<ItemTemplateInfo> itemTemplateInfos = new List<ItemTemplateInfo> { new ItemTemplateInfo()};

            this.presenter.Load(itemTemplateInfos);

            this.mockView.Verify(x => x.AddTemplate(It.IsAny<ItemTemplateInfo>()));
        }

        /// <summary>
        /// Tests the get required item templates.
        /// </summary>
        [Test]
        public void TestGetRequiredItemTemplates()
        {
            this.mockView.SetupGet(x => x.ViewModelName).Returns("ViewModelName");
            this.mockView.Setup(x => x.IncludeUnitTests).Returns(true);

            List<ItemTemplateInfo> requiredTemplates = new List<ItemTemplateInfo> { new ItemTemplateInfo() };

            this.mockView.Setup(x => x.RequiredTemplates).Returns(requiredTemplates);

            IEnumerable<ItemTemplateInfo> templateInfos = this.presenter.GetRequiredItemTemplates();

            Assert.IsTrue(templateInfos.Count() == 3);
        }
    }
}
