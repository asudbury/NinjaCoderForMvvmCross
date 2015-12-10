// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TextTemplatingHostService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Scorchio.VisualStudio.Services
{
    using System;
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    using Microsoft.VisualStudio.TextTemplating;

    /// <summary>
    /// Defines the TextTemplatingHostService type.
    /// </summary>
    public class TextTemplatingHostService : ITextTemplatingEngineHost
    {
        /// <summary>
        /// The file extension value.
        /// </summary>
        private string fileExtensionValue = ".txt";

        /// <summary>
        /// The file encoding value.
        /// </summary>
        private Encoding fileEncodingValue = Encoding.UTF8;

        /// <summary>
        /// The errors value.
        /// </summary>
        private CompilerErrorCollection errorsValue;

        /// <summary>
        /// The template file value.
        /// </summary>
        internal string TemplateFileValue;

        /// <summary>
        /// Acquires the text that corresponds to a request to include a partial text template file.
        /// </summary>
        /// <param name="requestFileName">The name of the partial text template file to acquire.</param>
        /// <param name="content">A <see cref="T:System.String" /> that contains the acquired text or <see cref="F:System.String.Empty" /> if the file could not be found.</param>
        /// <param name="location">A <see cref="T:System.String" /> that contains the location of the acquired text. If the host searches the registry for the location of include files or if the host searches multiple locations by default, the host can return the final path of the include file in this parameter. The host can set the <paramref name="location" /> to <see cref="F:System.String.Empty" /> if the file could not be found or if the host is not file-system based.</param>
        /// <returns>
        /// true to indicate that the host was able to acquire the text; otherwise, false.
        /// </returns>
        public bool LoadIncludeText(
            string requestFileName,
            out string content,
            out string location)
        {
            TraceService.WriteLine("TextTransformationService::LoadIncludeText");

            content = string.Empty;
            location = string.Empty;

            if (File.Exists(requestFileName))
            {
                content = File.ReadAllText(requestFileName);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Allows a host to provide additional information about the location of an assembly.
        /// </summary>
        /// <param name="assemblyReference">The assembly to resolve.</param>
        /// <returns>
        /// A <see cref="T:System.String" /> that contains the specified assembly reference or the specified assembly reference with additional information.
        /// </returns>
        public string ResolveAssemblyReference(string assemblyReference)
        {
            TraceService.WriteLine("TextTransformationService::ResolveAssemblyReference assemblyReference=" + assemblyReference);

            if (File.Exists(assemblyReference))
            {
                return assemblyReference;
            }

            string candidate = Path.Combine(Path.GetDirectoryName(this.TemplateFile), assemblyReference);

            if (File.Exists(candidate))
            {
                return candidate;
            }

            return string.Empty;
        }

        /// <summary>
        /// Returns the type of a directive processor, given its friendly name.
        /// </summary>
        /// <param name="processorName">The name of the directive processor to be resolved.</param>
        /// <returns>
        /// The <see cref="T:System.Type" /> of the directive processor.
        /// </returns>
        public Type ResolveDirectiveProcessor(string processorName)
        {
            TraceService.WriteLine("TextTransformationService::ResolveDirectiveProcessor processorName=" + processorName);

            if (string.Compare(processorName, "XYZ", StringComparison.OrdinalIgnoreCase) == 0)
            {
                //return typeof();
            }

            throw new Exception("Directive Processor not found");
        }

        /// <summary>
        /// Allows a host to provide a complete path, given a file name or relative path.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>
        /// A <see cref="T:System.String" /> that contains an absolute path.
        /// </returns>
        /// <exception cref="ArgumentNullException">the file name cannot be null</exception>
        public string ResolvePath(string fileName)
        {
            TraceService.WriteLine("TextTransformationService::ResolvePath path=" + fileName);

            if (fileName == null)
            {
                throw new ArgumentNullException("fileName");
            }

            if (File.Exists(fileName))
            {
                return fileName;
            }

            string candidate = Path.Combine(Path.GetDirectoryName(this.TemplateFile), fileName);

            if (File.Exists(candidate))
            {
                return candidate;
            }

            return fileName;
        }

        /// <summary>
        /// Resolves the value of a parameter for a directive processor if the parameter is not specified in the template text.
        /// </summary>
        /// <param name="directiveId">The ID of the directive call to which the parameter belongs. This ID disambiguates repeated calls to the same directive from the same text template.</param>
        /// <param name="processorName">The name of the directive processor to which the directive belongs.</param>
        /// <param name="parameterName">The name of the parameter to be resolved.</param>
        /// <returns>
        /// A <see cref="T:System.String" /> that represents the resolved parameter value.
        /// </returns>
        public string ResolveParameterValue(
            string directiveId,
            string processorName,
            string parameterName)
        {
            TraceService.WriteLine("TextTransformationService::ResolveParameterValue directiveId=" + directiveId + 
                " processorName=" + processorName + 
                " parameterName=" + parameterName);

            if (directiveId == null)
            {
                throw new ArgumentNullException("directiveId");
            }

            if (processorName == null)
            {
                throw new ArgumentNullException("processorName");
            }

            if (parameterName == null)
            {
                throw new ArgumentNullException("parameterName");
            }

            return string.Empty;
        }

        /// <summary>
        /// Provides an application domain to run the generated transformation class.
        /// </summary>
        /// <param name="content">The contents of the text template file to be processed.</param>
        /// <returns>
        /// An <see cref="T:System.AppDomain" /> that compiles and executes the generated transformation class.
        /// </returns>
        public AppDomain ProvideTemplatingAppDomain(string content)
        {
            TraceService.WriteLine("TextTransformationService::ProvideTemplatingAppDomain");

            return AppDomain.CreateDomain("Generation App Domain");
        }

        /// <summary>
        /// Tells the host the file name extension that is expected for the generated text output.
        /// </summary>
        /// <param name="extension">The file name extension for the generated text output.</param>
        public void SetFileExtension(string extension)
        {
            TraceService.WriteLine("TextTransformationService::SetFileExtension");

            this.fileExtensionValue = extension;
        }

        /// <summary>
        /// Called by the Engine to ask for the value of a specified option. Return null if you do not know.
        /// </summary>
        /// <param name="optionName">The name of an option.</param>
        /// <returns>
        /// Null to select the default value for this option. Otherwise, an appropriate value for the option.
        /// </returns>
        public object GetHostOption(string optionName)
        {
            TraceService.WriteLine("TextTransformationService::GetHostOption");

            object returnObject;

            switch (optionName)
            {
                case "CacheAssemblies":
                    returnObject = true;
                    break;
                default:
                    returnObject = null;
                    break;
            }

            return returnObject;
        }

        /// <summary>
        /// Gets a list of assembly references.
        /// </summary>
        public IList<string> StandardAssemblyReferences
        {
            get { return new[] { typeof(Uri).Assembly.Location }; }
        }

        /// <summary>
        /// Gets a list of namespaces.
        /// </summary>
        public IList<string> StandardImports
        {
            get { return new[] { "System" }; }
        }

        /// <summary>
        /// Gets the path and file name of the text template that is being processed.
        /// </summary>
        public string TemplateFile { get; private set; }

        /// <summary>
        /// Gets the errors.
        /// </summary>
        public CompilerErrorCollection Errors
        {
            get { return this.errorsValue; }
        }

        /// <summary>
        /// Tells the host the encoding that is expected for the generated text output.
        /// </summary>
        /// <param name="encoding">The encoding for the generated text output.</param>
        /// <param name="fromOutputDirective">true to indicate that the user specified the encoding in the encoding parameter of the output directive.</param>
        public void SetOutputEncoding(
            Encoding encoding,
            bool fromOutputDirective)
        {
            TraceService.WriteLine("TextTransformationService::SetOutputEncoding");

            this.fileEncodingValue = encoding;
        }

        /// <summary>
        /// Receives a collection of errors and warnings from the transformation engine.
        /// </summary>
        /// <param name="errors">The <see cref="T:System.CodeDom.Compiler.CompilerErrorCollection" />  being passed to the host from the engine.</param>
        public void LogErrors(CompilerErrorCollection errors)
        {
            TraceService.WriteLine("TextTransformationService::LogErrors");

            this.errorsValue = errors;
        }
    }
}
