// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the ITextTransformationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Services.Interfaces
{
    using System.Collections.Generic;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the ITextTransformationService type.
    /// </summary>
    public interface ITextTransformationService
    {
        /// <summary>
        /// Transforms the specified source file.
        /// </summary>
        /// <param name="sourceFile">The source file.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        TextTransformation Transform(
            string sourceFile,
            IDictionary<string, string> parameters);
    }
}
