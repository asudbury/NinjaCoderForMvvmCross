// ---------- ----------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CodeClassExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using Entities;
    using EnvDTE;
    using EnvDTE80;
    using Services;

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
            TraceService.WriteLine("CodeClassExtensions::GetConstructors file=" + instance.Name);

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
            TraceService.WriteLine("CodeClassExtensions::AddDefaultConstructor file=" + instance.Name);

            CodeFunction codeFunction = instance.AddFunction(
                instance.Name,
                vsCMFunction.vsCMFunctionConstructor,
                vsCMTypeRef.vsCMTypeRefVoid,
                0,
                vsCMAccess.vsCMAccessPublic,
                null);

            codeFunction.GetEndPoint().CreateEditPoint().InsertNewLine();
            string comment = "<doc><summary>\r\nInitializes a new instance of the " + instance.Name + " class.\r\n</summary></doc>\r\n";

            codeFunction.DocComment = comment;
            
            return codeFunction;
        }

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
        
        /// <summary>
        /// Implements the code snippet.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="codeSnippet">The code snippet.</param>
        public static void ImplementCodeSnippet(
            this CodeClass instance,
            CodeSnippet codeSnippet)
        {
            TraceService.WriteLine("CodeClassExtensions::ImplementCodeSnippet file" + instance.Name);

            if (string.IsNullOrEmpty(codeSnippet.Code) == false)
            {
                instance.ImplementFunction(codeSnippet);
            }

            if (codeSnippet.Interfaces.Count > 0)
            {
                IEnumerable<CodeFunction> constructors = instance.GetConstructors();

                CodeFunction constructor = constructors.FirstOrDefault() ?? instance.AddDefaultConstructor();

                foreach (string variable in codeSnippet.Interfaces)
                {
                    instance.ImplementInterface(constructor, variable);
                }
            }

            foreach (string variable in codeSnippet.Variables)
            {
                //// split the variable string
                string[] parts = variable.Split(' ');

                //// variable could already exist!
                try
                {
                    instance.ImplementVariable(parts[1], parts[0], false);
                }
                catch (Exception exception)
                {
                    TraceService.WriteError("Error adding variable exception=" + exception.Message + " variable=" + parts[1]);
                }
            }

            foreach (string variable in codeSnippet.MockVariables)
            {
                //// split the variable string
                string[] parts = variable.Split(' ');

                //// variable could already exist!
                try
                {
                    instance.ImplementMockVariable(parts[1], parts[0]);
                }
                catch (Exception exception)
                {
                    TraceService.WriteError("Error adding mock variable exception=" + exception.Message + " variable=" + parts[1]);
                }
            }

            if (string.IsNullOrEmpty(codeSnippet.MockInitCode) == false ||
                string.IsNullOrEmpty(codeSnippet.MockConstructorCode) == false)
            {
                instance.ImplementMockCode(codeSnippet);
            }
       }

        /// <summary>
        /// Implements the mock code.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="codeSnippet">The code snippet.</param>
        public static void ImplementMockCode(
            this CodeClass instance, 
            CodeSnippet codeSnippet)
        {
            TraceService.WriteLine("CodeClassExtensions::ImplementMockCode file=" + instance.Name);

            CodeFunction codeFunction = instance.GetFunction(codeSnippet.TestInitMethod);

            if (codeFunction != null)
            {
                if (string.IsNullOrEmpty(codeSnippet.MockInitCode) == false)
                {
                    codeFunction.InsertCode(codeSnippet.MockInitCode, true);
                }

                if (string.IsNullOrEmpty(codeSnippet.MockConstructorCode) == false)
                {
                    string code = codeFunction.GetCode();

                    string testFileName = instance.Name;

                    //// remove Test at the start - need a better way of doing this!
                    string fileName = testFileName.Replace("Test", string.Empty);

                    //// TODO : this wont always work if the function spans multiple lines!
                    int index = code.IndexOf(fileName + "(", StringComparison.Ordinal);

                    if (index != 0)
                    {
                        string seperator = string.Empty;

                        //// here we are looking for the closing bracket
                        //// if we dont have a closing bracket then we already have something
                        //// on the constructor and therefore need a ',' to make it work!
                        if (code.Substring(index + fileName.Length + 1, 1) != ")")
                        {
                            //// TODO : do we want to create a new line too!
                            seperator = ", ";
                        }

                        StringBuilder sb = new StringBuilder();
                        sb.Append(code.Substring(0, index + fileName.Length + 1));
                        sb.Append(codeSnippet.MockConstructorCode + seperator);
                        sb.Append(code.Substring(index + fileName.Length + 1));

                        codeFunction.ReplaceCode(sb.ToString());
                    }
                }
            }
        }

        /// <summary>
        /// Implements the function.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="codeSnippet">The code snippet.</param>
        public static void ImplementFunction(
            this CodeClass instance, 
            CodeSnippet codeSnippet)
        {
            TraceService.WriteLine("CodeClassExtensions::ImplementFunction file=" + instance.Name);

            CodeFunction codeFunction = instance.AddFunction(
                "temp",
                vsCMFunction.vsCMFunctionFunction,
                vsCMTypeRef.vsCMTypeRefVoid,
                -1,
                vsCMAccess.vsCMAccessPublic,
                null);

            TextPoint startPoint = codeFunction.StartPoint;

            EditPoint editPoint = startPoint.CreateEditPoint();

            instance.RemoveMember(codeFunction);

            editPoint.Insert(codeSnippet.Code);
        }

        /// <summary>
        /// Implements the interface.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="constructor">The constructor.</param>
        /// <param name="variable">The variable.</param>
        public static void ImplementInterface(
            this CodeClass instance, 
            CodeFunction constructor, 
            string variable)
        {
            TraceService.WriteLine("CodeClassExtensions::ImplementInterface file=" + variable);

            //// now add in the interfaces!

            //// split the variable string
            string[] parts = variable.Split(' ');

            constructor.AddParameter(parts[1], parts[0]);

            //// we need to add the variable.
            //// variable could already exist!
            try
            {
                instance.ImplementVariable(parts[1], parts[0], true);
            }
            catch (Exception)
            {
                TraceService.WriteError("variable already exists " + parts[1]);
            }

            //// now do the wiring up of the interface and variable!
            EditPoint editPoint = constructor.GetEndPoint(vsCMPart.vsCMPartBody).CreateEditPoint();

            string code = string.Format("this.{0} = {1};", parts[1], parts[1]);
            editPoint.InsertCodeLine(code);

            //// now update the constructor document comments.
            string paramComment = string.Format("<param name=\"{0}\">{0}.</param>\r\n", parts[1]);
            string currentComment = constructor.DocComment;

            int index = currentComment.LastIndexOf("</summary>", StringComparison.Ordinal);

            StringBuilder sb = new StringBuilder();

            sb.Append(currentComment.Substring(0, index + 10));
            sb.Append(paramComment);
            sb.Append(currentComment.Substring(index + 10));

            constructor.DocComment = sb.ToString();
        }

        /// <summary>
        /// Implements the variable.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="isReadOnly">if set to <c>true</c> [is read only].</param>
        /// <returns>The Code Variable. </returns>
        public static CodeVariable ImplementVariable(
            this CodeClass instance,
            string name,
            string type,
            bool isReadOnly)
        {
            TraceService.WriteLine("CodeClassExtensions::ImplementVariable name=" + name + " type=" + type);

            CodeVariable codeVariable = instance.AddVariable(name, type, 0, vsCMAccess.vsCMAccessPrivate, 0);

            codeVariable.DocComment = "<doc><summary>\r\nBacking field for " + name + ".\r\n</summary></doc>";
            codeVariable.GetEndPoint().CreateEditPoint().InsertNewLine();

            if (isReadOnly)
            {
                CodeVariable2 codeVariable2 = codeVariable as CodeVariable2;

                if (codeVariable2 != null)
                {
                    codeVariable2.ConstKind = vsCMConstKind.vsCMConstKindReadOnly;
                }
            }

            return codeVariable;
        }

        /// <summary>
        /// Implements the mock variable.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <returns>The code variable.</returns>
        public static CodeVariable ImplementMockVariable(
            this CodeClass instance,
            string name,
            string type)
        {
            TraceService.WriteLine("CodeClassExtensions::ImplementMockVariable file=" + instance.Name);

            CodeVariable codeVariable = instance.AddVariable(name, type, 0, vsCMAccess.vsCMAccessPrivate, 0);

            string typeDescriptor = name.Substring(4,1).ToUpper() + name.Substring(5);

            codeVariable.DocComment = "<doc><summary>\r\nMock " + typeDescriptor + ".\r\n</summary></doc>";
            

            EditPoint startPoint = codeVariable.StartPoint.CreateEditPoint();
            EditPoint endPoint = codeVariable.EndPoint.CreateEditPoint();

            string text = startPoint.GetText(endPoint);
            string newText = text.Replace(type, "Mock<" + type + ">");
            startPoint.ReplaceText(endPoint, newText, 0);

            // get the new endpoint before inserting new line.
            endPoint = codeVariable.EndPoint.CreateEditPoint();
            endPoint.InsertNewLine();

            return codeVariable;
        }
    }
}
