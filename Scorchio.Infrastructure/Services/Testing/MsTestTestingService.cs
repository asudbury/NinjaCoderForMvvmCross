// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MsTestTestingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Services.Testing
{
    using Scorchio.Infrastructure.Constants;
    using Scorchio.Infrastructure.Services.Testing.Interfaces;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    /// Defines the MsTestTestingService type.
    /// </summary>
    public class MsTestTestingService : BaseTestingService, ITestingService
    {
        /// <summary>
        /// Updates the test class attribute.
        /// </summary>
        /// <param name="projectItemService">The project item service.</param>
        public void UpdateTestClassAttribute(IProjectItemService projectItemService)
        {
            TraceService.WriteLine("MsTestTestingService::UpdateTestClassAttribute");

            projectItemService.ReplaceText(
                TestingConstants.NUnit.ClassAttribute,
                TestingConstants.MsTest.ClassAttribute);
        }

        /// <summary>
        /// Updates the test method attribute.
        /// </summary>
        /// <param name="projectItemService">The project item service.</param>
        public void UpdateTestMethodAttribute(IProjectItemService projectItemService)
        {
            TraceService.WriteLine("MsTestTestingService::UpdateTestMethodAttribute");

            projectItemService.ReplaceText(
                TestingConstants.NUnit.MethodAttribute,
                TestingConstants.MsTest.MethodAttribute);
        }
    }
}
