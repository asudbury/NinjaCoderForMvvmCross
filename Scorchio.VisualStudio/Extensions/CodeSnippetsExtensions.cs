// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CodeSnippetsExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the CodeSnippetsExtensions type.
    /// </summary>
    public static class CodeSnippetsExtensions
    {
        /// <summary>
        /// Gets the mock init code.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static string GetMockInitCode(this CodeSnippet instance)
        {
            string code = string.Empty;

            if (instance.MockVariables != null)
            {
                foreach (string[] parts in instance.MockVariables
                    .Select(variable => variable.Split(' ')))
                {
                    code = GetSpacedCodeLine(string.Format("this.{0} = {2}<{1}>();", parts[1], parts[0], instance.MockInitCode));
                }
            }

            return code;
        }

        /// <summary>
        /// Gets the mock constructor code.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The Mock Constructor Code</returns>
        public static string GetMockConstructorCode(this CodeSnippet instance)
        {
            string code = string.Empty;

            if (instance.MockVariables != null)
            {
                foreach (string[] parts in instance.MockVariables
                    .Select(variable => variable.Split(' ')))
                {
                    if (string.IsNullOrEmpty(instance.MockConstructorCode) == false)
                    {
                        code = string.Format("this.{0}{1}", parts[1], instance.MockConstructorCode);
                    }
                    else
                    {
                        code = string.Format("this.{0}", parts[1]);
                    }
                }
            }

            return code;
        }

        /// <summary>
        /// Adds a replacement variable.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="key">The key.</param>
        /// <param name="value">The value.</param>
        public static void AddReplacementVariable(
            this CodeSnippet instance,
            string key, 
            string value)
        {
            if (instance.ReplacementVariables == null)
            {
                instance.ReplacementVariables = new List<KeyValuePair<string, string>>();
            }

            instance.ReplacementVariables.Add(new KeyValuePair<string, string>(key, value));
        }

        /// <summary>
        /// Gets the spaced code line.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <returns>A spaced code line.</returns>
        internal static string GetSpacedCodeLine(string code)
        {
            if (code != null)
            {
                return string.Empty.PadLeft(12) + code;
            }

            return null;
        }
    }
}
