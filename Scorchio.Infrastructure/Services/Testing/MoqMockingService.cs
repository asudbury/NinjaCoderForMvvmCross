// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MoqMockingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Services.Testing
{
    using Constants;
    using Interfaces;
    using VisualStudio.Entities;
    using VisualStudio.Services;

    /// <summary>
    ///  Defines the MoqMockingService type.
    /// </summary>
    public class MoqMockingService : IMockingService
    {
        /// <summary>
        /// The moq.
        /// </summary>
        private const string Moq = "Moq";

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name
        {
            get { return Moq; }
        }

        /// <summary>
        /// Gets the mocking assembly reference.
        /// </summary>
        public string MockingAssemblyReference
        {
            get { return Moq; }
        }

        /// <summary>
        /// Injects the mocking details.
        /// </summary>
        /// <param name="codeSnippet">The code snippet.</param>
        public void InjectMockingDetails(CodeSnippet codeSnippet)
        {
            TraceService.WriteLine("MoqMockingService::InjectMockingDetails");

            codeSnippet.MockingVariableDeclaration = TestingConstants.Moq.MockingVariableDeclaration;

            codeSnippet.MockConstructorCode = TestingConstants.Moq.MockConstructorCode;

            codeSnippet.MockInitCode = TestingConstants.Moq.MockInitCode;
        }
    }
}
