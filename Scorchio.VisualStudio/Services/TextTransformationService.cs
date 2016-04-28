// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TextTransformationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Services
{
    using Entities;
    using Extensions;
    using Interfaces;
    using System.Collections.Generic;
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
        /// <returns>The Text Transformation.</returns>
        public TextTransformation Transform(TextTransformationRequest textTransformationRequest)
        {
            TraceService.WriteLine("TextTransformationService::Transform sourceFile=" + textTransformationRequest.SourceFile);

            string sourceText = this.GetText(textTransformationRequest.SourceFile);

            TraceService.WriteDebugLine("sourceText=" + sourceText);

            SimpleTextTemplatingEngine engine = new SimpleTextTemplatingEngine();

            TraceService.WriteLine("Before processing template via SimpleTextTemplatingEngine");

            TextTransformation textTransformation = engine.ProcessTemplate(
                                                            sourceText, 
                                                            textTransformationRequest.Parameters, 
                                                            textTransformationRequest.RemoveFileHeaders, 
                                                            textTransformationRequest.RemoveXmlComments,
                                                            textTransformationRequest.RemoveThisPointer);

            TraceService.WriteLine("After processing template via SimpleTextTemplatingEngine = SUCCESS!");

            TraceService.WriteDebugLine("output=" + textTransformation.Output);

            return textTransformation;
        }

        /// <summary>
        /// Gets the text output.
        /// </summary>
        /// <param name="sourceText">The source text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <param name="removeFileHeaders">if set to <c>true</c> [remove file headers].</param>
        /// <param name="removeXmlComments">if set to <c>true</c> [remove XML comments].</param>
        /// <param name="removeThisPointer">if set to <c>true</c> [remove this pointer].</param>
        /// <returns></returns>
        public string GetTextOutput(
            string sourceText,
            IDictionary<string, string> parameters,
            bool removeFileHeaders,
            bool removeXmlComments,
            bool removeThisPointer)
        {
            SimpleTextTemplatingEngine engine = new SimpleTextTemplatingEngine();

            TextTransformation textTransformation = engine.ProcessTemplate(sourceText, parameters, removeFileHeaders, removeXmlComments, removeThisPointer);

            return textTransformation.Output;
        }

        /// <summary>
        /// Gets the text.
        /// </summary>
        /// <param name="sourceFile">The source file.</param>
        /// <returns>The text of the source file.</returns>
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
