// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the ITextTransformationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Services.Interfaces
{
    using Entities;
    using Extensions;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the ITextTransformationService type.
    /// </summary>
    public interface ITextTransformationService
    {
        /// <summary>
        /// Transforms the specified source file.
        /// </summary>
        /// <param name="textTransformationRequest">The text transformation request.</param>
        /// <returns>A Text Transformation.</returns>
        TextTransformation Transform(TextTransformationRequest textTransformationRequest);

        /// <summary>
        /// Gets the text output.
        /// </summary>
        /// <param name="sourceText">The source text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="removeFileHeaders">if set to <c>true</c> [remove file headers].</param>
        /// <param name="removeXmlComments">if set to <c>true</c> [remove XML comments].</param>
        /// <param name="removeThisPointer">if set to <c>true</c> [remove this pointer].</param>
        /// <returns></returns>
        string GetTextOutput(
            string sourceText,
            IDictionary<string, string> parameters,
            bool removeFileHeaders,
            bool removeXmlComments,
            bool removeThisPointer);
    }
}
