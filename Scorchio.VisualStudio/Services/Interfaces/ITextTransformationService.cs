// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the ITextTransformationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Services.Interfaces
{
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;

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
    }
}
