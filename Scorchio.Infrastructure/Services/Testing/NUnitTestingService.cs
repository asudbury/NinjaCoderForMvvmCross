// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NUnitTestingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Services.Testing
{
    using Scorchio.Infrastructure.Services.Testing.Interfaces;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    /// Defines the NUnitTestingService type.
    /// </summary>
    public class NUnitTestingService : BaseTestingService, ITestingService
    {
        /// <summary>
        /// Updates the test class attribute.
        /// </summary>
        /// <param name="projectItemService">The project item service.</param>
        public void UpdateTestClassAttribute(IProjectItemService projectItemService)
        {
            TraceService.WriteLine("NUnitTestingService::UpdateTestClassAttribute");

            //// Currently we dont do anything as the snippets
            //// are written in nunit format - this may change at some point
            //// in the future.
        }

        /// <summary>
        /// Updates the test method attribute.
        /// </summary>
        /// <param name="projectItemService">The project item service.</param>
        public void UpdateTestMethodAttribute(IProjectItemService projectItemService)
        {
            TraceService.WriteLine("NUnitTestingService::UpdateTestMethodAttribute");

            //// Currently we dont do anything as the snippets
            //// are written in nunit format - this may change at some point
            //// in the future.
        }
    }
}
