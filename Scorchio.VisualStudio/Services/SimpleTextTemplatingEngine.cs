// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the SimpleTextTemplatingEngine type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Services
{
    using System.Collections.Generic;
    using System.Linq;

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
        public string ProcessTemplate(
            string sourceText,
            IDictionary<string, string> parameters)
        {
            TraceService.WriteLine("SimpleTextTemplatingEngine::ProcessTemplate");

           //// first remove all the parameter definitions!

            string[] lines = sourceText.Split('\n');

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

            return output;
        }
    }
}
