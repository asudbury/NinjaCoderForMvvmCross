// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NSubstituteMockingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Services.Testing
{
    using Constants;
    using Interfaces;
    using VisualStudio.Entities;
    using VisualStudio.Services;

    /// <summary>
    ///  Defines the NSubstituteMockingService type.
    /// </summary>
    public class NSubstituteMockingService : IMockingService
    {
        /// <summary>
        /// The nsubstitute
        /// </summary>
        private const string NSubstitute = "NSubstitute";

        /// <summary>
        /// Gets the name.
        /// </summary>
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
