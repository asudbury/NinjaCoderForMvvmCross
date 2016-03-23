// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TextTransformationRequest type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Extensions
{
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the TextTransformation type.
    /// </summary>
    public class TextTransformationRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TextTransformationRequest"/> class.
        /// </summary>
        public TextTransformationRequest()
        {
            this.Parameters = new Dictionary<string, string>(); 
        }

        /// <summary>
        /// Gets or sets the source file.
        /// </summary>
        public string SourceFile { get; set; }

        /// <summary>
        /// Gets or sets the parameters.
        /// </summary>
        public IDictionary<string, string> Parameters { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remove file headers].
        /// </summary>
        public bool RemoveFileHeaders { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remove XML comments].
        /// </summary>
        public bool RemoveXmlComments { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [remove this pointer].
        /// </summary>
        public bool RemoveThisPointer { get; set; }
    }
}
