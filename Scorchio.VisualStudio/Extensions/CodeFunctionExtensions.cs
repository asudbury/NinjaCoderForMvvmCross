//--------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CodeFunctionExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Extensions
{
    using System;
    using System.Text.RegularExpressions;

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

        /// <summary>
        /// Formats the parameters.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void FormatParameters(this CodeFunction instance)
        {
            TraceService.WriteLine("CodeFunctionExtensions::FormatParameters");

            int count = instance.Parameters.Count;

            if (count > 1)
            {
                string parameters = string.Empty;

                EditPoint firstParameterEditPoint = null;
                EditPoint lastParameterEditPoint = null;

                foreach (CodeParameter codeParameter in instance.Parameters)
                {
                    if (firstParameterEditPoint == null)
                    {
                        firstParameterEditPoint = codeParameter.StartPoint.CreateEditPoint();
                    }

                    //// note the 12 spaces at the start!
                    string parameter = string.Format(
                        "{0}            {1} {2},",
                        Environment.NewLine,
                        codeParameter.GetParameterType(), 
                        codeParameter.Name);

                    parameters += parameter;

                    lastParameterEditPoint = codeParameter.EndPoint.CreateEditPoint();
                }

                //// remove the last comma
                parameters = parameters.Substring(0, parameters.Length - 1);
                
                if (firstParameterEditPoint != null)
                {
                    firstParameterEditPoint.ReplaceText(lastParameterEditPoint, string.Empty, 0);
                    firstParameterEditPoint.Insert(parameters);
                }
            }
        }
    }
}
