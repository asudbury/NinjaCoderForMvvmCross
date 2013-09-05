//--------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CodeFunctionExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Extensions
{
    using System;

    using EnvDTE;
    using Services;

    /// <summary>
    /// Defines the CodeFunctionExtensions type.
    /// </summary>
    public static class CodeFunctionExtensions
    {
        /// <summary>
        /// Insert code.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="code">The code.</param>
        /// <param name="atTheStart">if set to <c>true</c> [at the start].</param>
        public static void InsertCode(
            this CodeFunction instance, 
            string code,
            bool atTheStart)
        {
            TraceService.WriteLine("CodeFunctionExtensions::InsertCode codeFunction=" + instance.Name);

            EditPoint editPoint = atTheStart ? 
                instance.GetStartPoint(vsCMPart.vsCMPartBody).CreateEditPoint() : 
                instance.GetEndPoint(vsCMPart.vsCMPartBody).CreateEditPoint();

            if (code.EndsWith(Environment.NewLine) == false)
            {
                code += Environment.NewLine;
            }
            editPoint.Insert(code);
        }

        /// <summary>
        /// Gets the code.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The code.</returns>
        public static string GetCode(this CodeFunction instance)
        {
            TraceService.WriteLine("CodeFunctionExtensions::GetCode codeFunction=" + instance.Name);

            EditPoint startPoint = instance.GetStartPoint(vsCMPart.vsCMPartBody).CreateEditPoint();
            EditPoint endPoint = instance.GetEndPoint(vsCMPart.vsCMPartBody).CreateEditPoint();

            return startPoint.GetText(endPoint);
        }

        /// <summary>
        /// Replaces the code.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="newCode">The new code.</param>
        public static void ReplaceCode(
            this CodeFunction instance, 
            string newCode)
        {
            TraceService.WriteLine("CodeFunctionExtensions::ReplaceCode codeFunction=" + instance.Name);

            EditPoint startPoint = instance.GetStartPoint(vsCMPart.vsCMPartBody).CreateEditPoint();
            EditPoint endPoint = instance.GetEndPoint(vsCMPart.vsCMPartBody).CreateEditPoint();

            startPoint.Delete(endPoint);
            startPoint.Insert(newCode);
        }

        /// <summary>
        /// Removes the comments.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void RemoveComments(this CodeFunction instance)
        {
            TraceService.WriteLine("CodeFunctionExtensions::RemoveComments");

            instance.DocComment = ScorchioConstants.BlankDocComment;
        }
    }
}
