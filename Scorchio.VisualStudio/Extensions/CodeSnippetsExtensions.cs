// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CodeSnippetsExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the CodeSnippetsExtensions type.
    /// </summary>
    public static class CodeSnippetsExtensions
    {
        /// <summary>
        /// Gets the mock init code.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The Mock Init Code</returns>
        public static string GetMockInitCode(this CodeSnippet instance)
        {
            string code = string.Empty;

            if (instance.MockVariables != null)
            {
                foreach (string[] parts in instance.MockVariables
                    .Select(variable => variable.Split(' ')))
                {
                    try
                    {
                        code = GetSpacedCodeLine(string.Format("this.{0} = new Mock<{1}>();", parts[1], parts[0]));
                    }
                    catch (Exception exception)
                    {
                        TraceService.WriteError("Error GetMockInitCode exception=" + exception.Message + " variable=" + parts[1]);
                    }
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
                    try
                    {
                        code = string.Format("this.{0}.Object", parts[1]);
                    }
                    catch (Exception exception)
                    {
                        TraceService.WriteError("Error GetMockConstructorCode exception=" + exception.Message + " variable=" + parts[1]);
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
