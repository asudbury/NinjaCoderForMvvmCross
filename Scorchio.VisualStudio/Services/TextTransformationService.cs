// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TextTransformationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Services
{
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System.IO;
    using System.Net;

    /// <summary>
    ///  Defines the TextTransformationService type.
    /// </summary>
    public class TextTransformationService : ITextTransformationService
    {
        /// <summary>
        /// The cache.
        /// </summary>
        private readonly T4Cache cache;

        /// <summary>
        /// Initializes a new instance of the <see cref="TextTransformationService" /> class.
        /// </summary>
        public TextTransformationService()
        {
            TraceService.WriteLine("TextTransformationService::Constructor");

            this.cache = new T4Cache();

            TraceService.WriteLine("TextTransformationService::Constructor END");
        }

        /// <summary>
        /// Transforms the specified source file.
        /// </summary>
        /// <param name="textTransformationRequest">The text transformation request.</param>
        /// <returns></returns>
        public TextTransformation Transform(TextTransformationRequest textTransformationRequest)
        {
            TraceService.WriteLine("TextTransformationService::Transform sourceFile=" + textTransformationRequest.SourceFile);

            string sourceText = this.GetText(textTransformationRequest.SourceFile);

            SimpleTextTemplatingEngine engine = new SimpleTextTemplatingEngine();

            TraceService.WriteLine("Before processing template via SimpleTextTemplatingEngine");

            TextTransformation textTransformation = engine.ProcessTemplate(
                                                            sourceText, 
                                                            textTransformationRequest.Parameters, 
                                                            textTransformationRequest.RemoveFileHeaders, 
                                                            textTransformationRequest.RemoveXmlComments,
                                                            textTransformationRequest.RemoveThisPointer);

            TraceService.WriteLine("After processing template via SimpleTextTemplatingEngine = SUCCESS!");

            return textTransformation;
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <param name="sourceFile">The source file.</param>
        /// <returns></returns>
        internal string GetText(string sourceFile)
        {
            if (this.cache.Files.ContainsKey(sourceFile))
            {
                string cachedFile = this.cache.Files[sourceFile];

                TraceService.WriteLine("Using cached version of " + sourceFile);
                return cachedFile;
            }

            if (sourceFile.Contains("http") == false)
            {
                return File.ReadAllText(sourceFile);
            }

            WebClient client = new WebClient();
            Stream stream = client.OpenRead(sourceFile);

            if (stream != null)
            {
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }

            return string.Empty;
        }
    }
}
