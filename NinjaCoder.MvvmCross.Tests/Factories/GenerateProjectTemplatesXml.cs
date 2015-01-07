// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the GenerateProjectTemplatesXml type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Factories
{
    using Moq;

    using NinjaCoder.MvvmCross.Factories;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using NUnit.Framework;

    /// <summary>
    ///  Defines the TestTestingServiceFactory type.
    /// </summary>
    [TestFixture]
    public class GenerateProjectTemplatesXml
    {
        [Test]
        public void BuildXmlFile()
        {
            Mock<IVisualStudioService> mockVisualStudioService = new Mock<IVisualStudioService>();
            Mock<ISettingsService> mockSettingsServics = new Mock<ISettingsService>();
            Mock<INugetCommandsService> mockNugetCommandService = new Mock<INugetCommandsService>();
            
            NoFrameworkProjectFactory noFrameworkProjectFactory = new NoFrameworkProjectFactory(
                mockVisualStudioService.Object,
                mockSettingsServics.Object,
                mockNugetCommandService.Object);

            var x = noFrameworkProjectFactory.GetAllowedProjects();

        }
    }
}
