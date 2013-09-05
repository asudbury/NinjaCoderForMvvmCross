// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SnippetTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using System;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Xml;
    using System.Xml.Serialization;
    using Constants;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    /// Defines the SnippetTranslator type.
    /// </summary>
    public class CodeSnippetTranslator : ITranslator<string, CodeSnippet>
    {
        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">From object.</param>
        /// <returns>A CodeSnippet.</returns>
        public CodeSnippet Translate(string @from)
        {
            if (File.Exists(from))
            {
                FileStream fileStream = new FileStream(from, FileMode.Open, FileAccess.Read);
                XmlDictionaryReader reader = XmlDictionaryReader.CreateTextReader(fileStream, new XmlDictionaryReaderQuotas());

                XmlSerializer serializer = new XmlSerializer(typeof(CodeSnippet));
                CodeSnippet codeSnippet = (CodeSnippet)serializer.Deserialize(reader);
                return this.CleanedUp(codeSnippet);
            }

            return null;
        }
        
        /// <summary>
        /// Cleaned up.
        /// </summary>
        /// <param name="codeSnippet">The code snippet.</param>
        /// <returns>A cleaned up version of the CodeSnippet</returns>
        internal CodeSnippet CleanedUp(CodeSnippet codeSnippet)
        {
            //// we have problems with CR and LF in the code snippets
            //// here we try and fix them!!!
            
            //// replace all the single \n with a null!
            Regex regEx = new Regex(Settings.ReplaceBlankLinesRegEx);

            string code = regEx.Replace(codeSnippet.Code, Environment.NewLine).TrimEnd();

            //// get rid of tabs
            code = code.Replace("\t", "    ");
            
            codeSnippet.Code = code;

            if (codeSnippet.UsingStatements != null)
            {
                //// sometimes people might put the ; in the statement - cater for it!
                for (int i = 0; i < codeSnippet.UsingStatements.Count; i++)
                {
                    codeSnippet.UsingStatements[i] = this.CleanUsingStatement(codeSnippet.UsingStatements[i]);
                }
            }

            //// tidy up some code items.
            codeSnippet.MockConstructorCode = this.CleanedCode(codeSnippet.MockConstructorCode);
            codeSnippet.MockInitCode = this.GetSpacedCodeLine(this.CleanedCode(codeSnippet.MockInitCode));

            return codeSnippet;
        }

        /// <summary>
        /// Cleans the using statement.
        /// </summary>
        /// <param name="statement">The statement.</param>
        /// <returns>The cleaned using statement.</returns>
        internal string CleanUsingStatement(string statement)
        {
            return statement.Replace(";", string.Empty);
        }

        /// <summary>
        /// Cleans the code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>The cleaned up code.</returns>
        internal string CleanedCode(string code)
        {
            if (code != null)
            {
                code = code.Replace("\n ", string.Empty);
                code = code.Trim();
            }

            return code;
        }

        /// <summary>
        /// Gets the spaced code line.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>A spaced code line.</returns>
        internal string GetSpacedCodeLine(string code)
        {
            if (code != null)
            {
                return "            " + code;
            }

            return null;
        }
    }
}
