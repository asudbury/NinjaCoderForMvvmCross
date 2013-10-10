// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CodeSnippetsExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Extensions
{
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the CodeSnippetsExtensions type.
    /// </summary>
    public static class CodeSnippetsExtensions
    {
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
    }
}
