// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseTestingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Services.Testing
{
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the BaseTestingService type.
    /// </summary>
    public class BaseTestingService
    {
        /// <summary>
        /// Updates the file.
        /// </summary>
        /// <param name="projectItemService">The project item service.</param>
        /// <param name="replacementVariables">The replacement variables.</param>
        public void UpdateFile(
            IProjectItemService projectItemService,
            IEnumerable<KeyValuePair<string, string>> replacementVariables)
        {
            TraceService.WriteLine("BaseTestingService::UpdateFile");

            foreach (KeyValuePair<string, string> replacementVariable in replacementVariables)
            {
                TraceService.WriteLine("Key=" + replacementVariable.Key + " Value=" + replacementVariable.Value);

                projectItemService.ReplaceText(replacementVariable.Key, replacementVariable.Value);
            }
        }
    }
}