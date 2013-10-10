// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CodeClassExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards.Extensions
{
    using System.Linq;
    using EnvDTE;
    using Services;

    /// <summary>
    /// Defines the CodeClassExtensions type.
    /// </summary>
    public static class CodeClassExtensions
    {
        /// <summary>
        /// Gets the function.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="functionName">Name of the function.</param>
        /// <returns>The function.</returns>
        public static CodeFunction GetFunction(
            this CodeClass instance,
            string functionName)
        {
            TraceService.WriteLine("CodeClassExtensions::GetFunction file=" + instance.Name + " function" + functionName);

            return instance.Members.OfType<CodeFunction>().FirstOrDefault(x => x.FullName.Contains(functionName));
        }
    }
}
