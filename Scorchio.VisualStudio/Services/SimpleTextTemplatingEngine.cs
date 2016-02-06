// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the SimpleTextTemplatingEngine type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the MockEngine type.
    /// </summary>
    public class SimpleTextTemplatingEngine
    {
        /// <summary>
        /// Processes the template.
        /// </summary>
        /// <param name="sourceText">The source text.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        public TextTransformation ProcessTemplate(
            string sourceText,
            IDictionary<string, string> parameters)
        {
            TraceService.WriteLine("SimpleTextTemplatingEngine::ProcessTemplate");

            TextTransformation textTransformation = new TextTransformation();

           //// first remove all the parameter definitions!

            string[] lines = sourceText.Split('\n');

            //// by default make it a standard csharp source file!
            
            textTransformation.FileExtension = "cs";

            string extensionLine = lines.FirstOrDefault(x => x.StartsWith("<#@ Output Extension="));

            if (string.IsNullOrEmpty(extensionLine) == false)
            {
                ///// <#@ Output Extension="cs" #>

                string[] parts = extensionLine.Split('"');

                if (parts.Length > 1)
                {
                    textTransformation.FileExtension = parts[1];
                }
            }
            
            IEnumerable<string> newLines = lines.Where(x => x.StartsWith("<#@ ") == false);

            string output = string.Empty;

            foreach (string newLine in newLines)
            {
                output += newLine + "\n";
            }
            
            foreach (KeyValuePair<string, string> parameter in parameters)
            {
                string t4Parameter = string.Format("<#= {0} #>", parameter.Key);

                output = output.Replace(t4Parameter, parameter.Value);
            }

            //// sort out single LF and replace with CR LF!
            textTransformation.Output = Regex.Replace(output, @"\r\n|\r|\n", "\r\n");

            return textTransformation;
        }
    }
}
