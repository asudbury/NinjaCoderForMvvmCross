// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectItemExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using EnvDTE;
    using Services;

    /// <summary>
    ///  Defines the ProjectItemExtensions type.
    /// </summary>
    public static class ProjectItemExtensions
    {
        /// <summary>
        /// The vs view kind code.
        /// </summary>
        private const string VsViewKindCode = "{7651A701-06E5-11D1-8EBD-00A0C90F26EA}";

        /// <summary>
        /// Replaces the text.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="text">The text.</param>
        /// <param name="replacementText">The replacement text.</param>
        public static void ReplaceText(
            this ProjectItem instance,
            string text,
            string replacementText)
        {
            TraceService.WriteLine("ProjectItemExtensions::ReplaceText in file " + instance.Name + " from '" + text + "' to '" + replacementText + "'");

            Window window = instance.Open(VsViewKindCode);

            if (window != null)
            {
                window.Activate();

                TextSelection textSelection = instance.DTE.ActiveDocument.Selection;
                textSelection.SelectAll();
                textSelection.ReplacePattern(text, replacementText, (int)vsFindOptions.vsFindOptionsMatchCase);
                instance.Save();
            }
        }

        /// <summary>
        /// Gets the first name space.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The first namespace.</returns>
        public static CodeNamespace GetFirstNameSpace(this ProjectItem instance)
        {
            TraceService.WriteLine("ProjectItemExtensions::GetFirstNameSpace file=" + instance.Name);

            IEnumerable<CodeNamespace> codeNamespaces = instance.FileCodeModel.CodeElements.OfType<CodeNamespace>();

            return codeNamespaces.FirstOrDefault();
        }

        /// <summary>
        /// Gets the first class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The first class.</returns>
        public static CodeClass GetFirstClass(this ProjectItem instance)
        {
            TraceService.WriteLine("ProjectItemExtensions::GetFirstClass file=" + instance.Name);

            IEnumerable<CodeClass> codeClasses = instance.FileCodeModel.CodeElements.OfType<CodeClass>();

            CodeClass codeClass = codeClasses.FirstOrDefault();

            if (codeClass != null)
            {
                return codeClass;
            }

            CodeNamespace codeNamespace = instance.GetFirstNameSpace();

            if (codeNamespace != null)
            {
                foreach (CodeElement codeElement in codeNamespace.Children)
                {
                    if (codeElement.Kind == vsCMElement.vsCMElementClass)
                    {
                        return codeElement as CodeClass;
                    }
                }
            }
            else
            {
                TraceService.WriteError("ProjectItemExtensions::GetFirstClass cannot find namespace");
            }

            return null;
        }

        /// <summary>
        /// Sorts the and remove using statements.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void SortAndRemoveUsingStatements(this ProjectItem instance)
        {
            TraceService.WriteLine("ProjectItemExtensions::SortAndRemoveUsingStatements in file " + instance.Name);

            try
            {
                Window window = instance.Open(VsViewKindCode);

                window.Activate();

                instance.DTE.ExecuteCommand("Edit.RemoveAndSort");
            }
            catch (Exception exception)
            {
                TraceService.WriteError("SortAndRemoveUsingStatements " + exception.Message);
            }
        }
    }
}
