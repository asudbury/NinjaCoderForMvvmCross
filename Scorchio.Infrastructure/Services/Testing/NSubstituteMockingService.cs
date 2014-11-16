// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NSubstituteMockingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Services.Testing
{
    using Scorchio.Infrastructure.Constants;
    using Scorchio.Infrastructure.Services.Testing.Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the NSubstituteMockingService type.
    /// </summary>
    public class NSubstituteMockingService : IMockingService
    {
        /// <summary>
        /// The nsubstitute
        /// </summary>
        private const string NSubstitute = "NSubstitute";

        public string Name
        {
            get { return NSubstitute; }
        }

        /// <summary>
        /// Gets the mocking assembly reference.
        /// </summary>
        public string MockingAssemblyReference
        {
            get { return NSubstitute; }
        }

        /// <summary>
        /// Injects the mocking details.
        /// </summary>
        /// <param name="codeSnippet">The code snippet.</param>
        public void InjectMockingDetails(CodeSnippet codeSnippet)
        {
            TraceService.WriteLine("NSubstituteMockingService::InjectMockingDetails");

            codeSnippet.MockInitCode = TestingConstants.NSubstitute.MockInitCode;
        }
    }
}
