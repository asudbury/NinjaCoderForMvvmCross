// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the EditPointExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Extensions
{
    using System;

    using EnvDTE;

    /// <summary>
    /// Defines the EditPointExtensions type.
    /// </summary>
    public static class EditPointExtensions
    {
        /// <summary>
        /// Inserts the new line.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void InsertNewLine(this EditPoint instance)
        {
            instance.Insert(Environment.NewLine);
        }

        /// <summary>
        /// Inserts the code line.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="code">The code.</param>
        public static void InsertCodeLine(
            this EditPoint instance,
            string code)
        {
            string insertCode = string.Format("            {0}{1}", code, Environment.NewLine);
            instance.Insert(insertCode);
        }
    }
}
