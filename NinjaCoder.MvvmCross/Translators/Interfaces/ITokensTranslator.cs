// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ITokensTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators.Interfaces
{
    using Scorchio.VisualStudio.Services.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the ITokensTranslator type.
    /// </summary>
    public interface ITokensTranslator
    {
        /// <summary>
        /// Translates the specified project service.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <returns>The Tokens.</returns>
        Dictionary<string, string> Translate(IProjectService projectService);
    }
}
