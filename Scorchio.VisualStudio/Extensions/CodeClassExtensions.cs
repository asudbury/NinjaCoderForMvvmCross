// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CodeClassExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using EnvDTE;

    /// <summary>
    ///  Defines the CodeClassExtensions type.
    /// </summary>
    public static class CodeClassExtensions
    {
        /// <summary>
        /// Gets the constructors.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The constructors.</returns>
        public static IEnumerable<CodeFunction> GetConstructors(this CodeClass instance)
        {
            return instance.Members.OfType<CodeFunction>()
                            .Where(x => x.FunctionKind == vsCMFunction.vsCMFunctionConstructor);
        }

        /// <summary>
        /// add a default constructor.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>
        /// The constructor.
        /// </returns>
        public static CodeFunction AddDefaultConstructor(this CodeClass instance)
        {
            return instance.AddFunction(
                instance.Name,
                vsCMFunction.vsCMFunctionConstructor,
                vsCMTypeRef.vsCMTypeRefVoid,
                0,
                vsCMAccess.vsCMAccessPublic,
                null);
        }

        /// <summary>
        /// Adds the constructor.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The constructor.</returns>
        public static CodeFunction AddConstructorFromDataMembers(this CodeClass instance)
        {
            CodeFunction codeFunction = instance.AddDefaultConstructor();

            IEnumerable<CodeVariable> codeVariables = instance.Members.OfType<CodeVariable>();

            foreach (CodeVariable codeVariable in codeVariables)
            {
                ////codeFunction.AddParameter()  
            }

            return codeFunction;
        }

        /// <summary>
        /// Adds the variable.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="comment">The comment.</param>
        /// <returns>
        /// The Code Variable.
        /// </returns>
        public static CodeVariable AddVariable(
            this CodeClass instance,
            string name,
            string type,
            string comment)
        {
            CodeVariable codeVariable = instance.AddVariable(name, type, 0, vsCMAccess.vsCMAccessPrivate);

            codeVariable.Comment = comment;

            return codeVariable;
        }
    }
}
