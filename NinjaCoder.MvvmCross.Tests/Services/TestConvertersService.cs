// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestConvertersService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Services
{
    using System.Collections.Generic;
    using Moq;
    using NinjaCoder.MvvmCross.Services;
    using NUnit.Framework;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the TestConvertersService type.
    /// </summary>
    [TestFixture]
    public class TestConvertersService
    {
        /// <summary>
        /// The service.
        /// </summary>
        private ConvertersService service;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.service = new ConvertersService();
        }
        
        /// <summary>
        /// Tests the add converters.
        /// </summary>
        [Test]
        public void TestAddConverters()
        {
            //// arrange.
            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();
            Mock<IProjectItemService> mockProjectItemService = new Mock<IProjectItemService>();

            mockProjectService.Setup(x => x.GetProjectItem(It.IsAny<string>())).Returns(mockProjectItemService.Object);

            //// act.
            IEnumerable<string> message = this.service.AddConverters(
                mockProjectService.Object, 
                "templatePath", 
                new List<ItemTemplateInfo>{ new ItemTemplateInfo()});

            //// assert.
            Assert.IsTrue(message != null);
            
            mockProjectItemService.Verify(x => x.Save());
        }

        /// <summary>
        /// Tests the add converter.
        /// </summary>
        [Test]
        public void TestAddConverter()
        {
            //// arrange.
            Mock<IProjectService> mockProjectService = new Mock<IProjectService>();
            Mock<IProjectItemService> mockProjectItemService = new Mock<IProjectItemService>();

            mockProjectService.Setup(x => x.GetProjectItem(It.IsAny<string>())).Returns(mockProjectItemService.Object);
            
            List<string> messages = new List<string>();

            //// act.
            this.service.AddConverter(
                mockProjectService.Object,
                "templatesPath", 
                messages, 
                new ItemTemplateInfo());

            //// assert.
            mockProjectItemService.Verify(x => x.ReplacePattern(It.IsAny<string>(), It.IsAny<string>()));
            mockProjectItemService.Verify(x => x.Save());
        }
    }
}
