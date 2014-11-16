// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IMockingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Services.Testing.Interfaces
{
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the IMockingService type.
    /// </summary>
    public interface IMockingService
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the mocking assembly reference.
        /// </summary>
        string MockingAssemblyReference { get; }

        /// <summary>
        /// Injects the mocking details.
        /// </summary>
        /// <param name="codeSnippet">The code snippet.</param>
        void InjectMockingDetails(CodeSnippet codeSnippet);
    }
}