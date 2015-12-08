// ---------- ----------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CodeParameterExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Extensions
{
    using System;
    using EnvDTE;

    /// <summary>
    /// Defines the CodeParameterExtensions type.
    /// </summary>
    public static class CodeParameterExtensions
    {
        /// <summary>
        /// Gets the type of the parameter.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The parameter type.</returns>
        public static string GetParameterType(this CodeParameter instance)
        {
            string type = instance.Type.AsString;

            int index = type.LastIndexOf(".", StringComparison.Ordinal);

            if (index != -1)
            {
                type = type.Substring(index + 1);
            }

            return type;
        }
    }
}
