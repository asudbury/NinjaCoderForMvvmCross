// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ITestingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Services.Testing.Interfaces
{
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    /// Defines the ITestingService.
    /// </summary>
    public interface ITestingService
    {
        /// <summary>
        /// Updates the file.
        /// </summary>
        /// <param name="projectItemService">The project item service.</param>
        /// <param name="replacementVariables">The replacement variables.</param>
        void UpdateFile(
            IProjectItemService projectItemService,
            IEnumerable<KeyValuePair<string, string>> replacementVariables);

        /// <summary>
        /// Updates the test class attribute.
        /// </summary>
        /// <param name="projectItemService">The project item service.</param>
        void UpdateTestClassAttribute(IProjectItemService projectItemService);
        
        /// <summary>
        /// Updates the test method attribute.
        /// </summary>
        /// <param name="projectItemService">The project item service.</param>
        void UpdateTestMethodAttribute(IProjectItemService projectItemService);
    }
}
