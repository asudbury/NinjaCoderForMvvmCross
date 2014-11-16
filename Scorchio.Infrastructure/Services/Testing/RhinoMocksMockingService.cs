// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the RhinoMocksMockingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Services.Testing
{
    using Scorchio.Infrastructure.Constants;
    using Scorchio.Infrastructure.Services.Testing.Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the RhinoMocksMockingService type.
    /// </summary>
    public class RhinoMocksMockingService : IMockingService
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return "RhinoMocks"; }
        }

        /// <summary>
        /// Gets the mocking assembly reference.
        /// </summary>
        public string MockingAssemblyReference
        {
            get { return "Rhino.Mocks"; }
        }

        /// <summary>
        /// Injects the mocking details.
        /// </summary>
        /// <param name="codeSnippet">The code snippet.</param>
        public void InjectMockingDetails(CodeSnippet codeSnippet)
        {
            TraceService.WriteLine("RhinoMocksMockingService::InjectMockingDetails");

            codeSnippet.MockInitCode = TestingConstants.RhinoMocks.MockInitCode;
        }
    }
}
