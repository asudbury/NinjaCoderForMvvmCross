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
        /// Gets the variables.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The variables.</returns>
        public static IEnumerable<CodeVariable> GetVariables(this CodeClass instance)
        {
            TraceService.WriteLine("CodeClassExtensions::GetVariables file=" + instance.Name);

            return instance.Members.OfType<CodeVariable>();
        }

        /// <summary>
        /// add a default constructor.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="moveToCorrectPosition">if set to <c>true</c> [move to correct position].</param>
        /// <returns>The constructor.</returns>
        public static CodeFunction AddDefaultConstructor(
            this CodeClass instance,
            bool moveToCorrectPosition)
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
            
            if (moveToCorrectPosition)
            {
                TraceService.WriteLine("Move to correct position");

                IEnumerable<CodeVariable> variables = instance.GetVariables();

                CodeVariable lastVariable = variables.LastOrDefault();

                if (lastVariable != null)
                {
                    EditPoint startPoint = codeFunction.StartPoint.CreateEditPoint();
                    EditPoint endPoint = codeFunction.EndPoint.CreateEditPoint();
                    string text = startPoint.GetText(endPoint);

                    //// remove current code Function text
                    instance.RemoveMember(codeFunction);

                    if (endPoint.GetText(1) == Environment.NewLine)
                    {
                        TraceService.WriteLine("Delete line");
                        endPoint.Delete(1);
                    }

                    EditPoint editPoint = lastVariable.EndPoint.CreateEditPoint();
                    editPoint.Insert(string.Format("{0}{1}{2}", Environment.NewLine, Environment.NewLine, text));

                    //// we need to re find the function as we have deleted this one!
                    codeFunction = instance.GetFunction(instance.Name);

                    codeFunction.DocComment = comment;
                }
            }

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
        /// <param name="formatFunctionParameters">if set to <c>true</c> [format function parameters].</param>
        public static void ImplementCodeSnippet(
            this CodeClass instance,
            CodeSnippet codeSnippet,
            bool formatFunctionParameters)
        {
            TraceService.WriteLine("CodeClassExtensions::ImplementCodeSnippet file" + instance.Name);

            if (codeSnippet.Variables != null)
            {
                foreach (string[] parts in codeSnippet.Variables
                    .Select(variable => variable.Split(' ')))
                {
                    //// variable could already exist!
                    try
                    {
                        instance.ImplementVariable(parts[1], parts[0], false);
                    }
                    catch (Exception exception)
                    {
                        TraceService.WriteError("Error adding variable exception=" + exception.Message + " variable=" + parts[1]);
                        
                        //// if variable already exists get out - code snippet will already have been applied.
                        return;
                    }
                }
            }

            if (codeSnippet.MockVariables != null)
            {
                foreach (string[] parts in codeSnippet.MockVariables
                    .Select(variable => variable.Split(' ')))
                {
                    //// variable could already exist!
                    try
                    {
                        instance.ImplementMockVariable(parts[1], parts[0], codeSnippet.MockingVariableDeclaration);
                    }
                    catch (Exception exception)
                    {
                        TraceService.WriteError("Error adding mock variable exception=" + exception.Message + " variable=" + parts[1]);

                        //// if variable already exists get out - code snippet will already have been applied.
                        return;
                    }
                }
            }

            if (string.IsNullOrEmpty(codeSnippet.Code) == false)
            {
                instance.ImplementFunction(codeSnippet);
            }

            if (codeSnippet.Interfaces != null &&
                codeSnippet.Interfaces.Count > 0)
            {
                IEnumerable<CodeFunction> constructors = instance.GetConstructors();

                CodeFunction constructor = constructors.FirstOrDefault() ?? instance.AddDefaultConstructor(true);

                foreach (string variable in codeSnippet.Interfaces)
                {
                    instance.ImplementInterface(constructor, variable);
                }

                if (formatFunctionParameters)
                {
                    constructor.FormatParameters();
                }
            }

            if (string.IsNullOrEmpty(codeSnippet.GetMockInitCode()) == false ||
                string.IsNullOrEmpty(codeSnippet.GetMockConstructorCode()) == false)
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
                if (string.IsNullOrEmpty(codeSnippet.GetMockInitCode()) == false)
                {
                    codeFunction.InsertCode(codeSnippet.GetMockInitCode(), true);
                }

                if (string.IsNullOrEmpty(codeSnippet.GetMockConstructorCode()) == false)
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
                        sb.Append(codeSnippet.GetMockConstructorCode() + seperator);
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
            string paramComment = string.Format("<param name=\"{0}\">The {0}.</param>{1}", parts[1], Environment.NewLine);
            string currentComment = constructor.DocComment;

            int index = currentComment.LastIndexOf("</summary>", StringComparison.Ordinal);

            StringBuilder sb = new StringBuilder();

            if (index != -1)
            {
                sb.Append(currentComment.Substring(0, index + 10));
                sb.Append(paramComment);
                sb.Append(currentComment.Substring(index + 10));

                TraceService.WriteLine("comment added=" + paramComment);
            }

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

            string variableName = name;

            bool placeHolderVariable = false;
            
            //// are we a place holder variable (used in snippets!)
            if (variableName.Contains("%"))
            {
                variableName = variableName.Replace("%", string.Empty);
                placeHolderVariable = true;
            }

            CodeVariable codeVariable = instance.AddVariable(variableName, type, 0, vsCMAccess.vsCMAccessPrivate, 0);

            codeVariable.DocComment = "<doc><summary>" + "\r\nBacking field for " + name + ".\r\n</summary></doc>";
            codeVariable.GetEndPoint().CreateEditPoint().InsertNewLine();

            if (isReadOnly)
            {
                CodeVariable2 codeVariable2 = codeVariable as CodeVariable2;

                if (codeVariable2 != null)
                {
                    codeVariable2.ConstKind = vsCMConstKind.vsCMConstKindReadOnly;
                }
            }

            if (placeHolderVariable)
            {
                EditPoint startPoint = codeVariable.StartPoint.CreateEditPoint();
                EditPoint endPoint = codeVariable.EndPoint.CreateEditPoint();
                string text = startPoint.GetText(endPoint);
                string newText = text.Replace(variableName, name);
                startPoint.ReplaceText(endPoint, newText, 0);
            }

            return codeVariable;
        }

        /// <summary>
        /// Implements the mock variable.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        /// <param name="mockVariableDeclaration">The mock variable declaration.</param>
        /// <returns>The code variable.</returns>
        public static CodeVariable ImplementMockVariable(
            this CodeClass instance,
            string name,
            string type,
            string mockVariableDeclaration)
        {
            TraceService.WriteLine("CodeClassExtensions::ImplementMockVariable file=" + instance.Name);

            CodeVariable codeVariable = instance.AddVariable(name, type, 0, vsCMAccess.vsCMAccessPrivate, 0);

            string typeDescriptor = name.Substring(4, 1).ToUpper() + name.Substring(5);

            codeVariable.DocComment = "<doc><summary>\r\nMock " + typeDescriptor + ".\r\n</summary></doc>";
            
            EditPoint startPoint = codeVariable.StartPoint.CreateEditPoint();
            EditPoint endPoint = codeVariable.EndPoint.CreateEditPoint();

            //// if we are Moq then we change the variable declaration.
            if (string.IsNullOrEmpty(mockVariableDeclaration) == false)
            {
                string substitution = mockVariableDeclaration.Replace("%TYPE%", type);

                string text = startPoint.GetText(endPoint);
                string newText = text.Replace("private " + type, "private " + substitution);
                startPoint.ReplaceText(endPoint, newText, 0);
            }

            // get the new endpoint before inserting new line.
            endPoint = codeVariable.EndPoint.CreateEditPoint();
            endPoint.InsertNewLine();

            return codeVariable;
        }

        /// <summary>
        /// Removes the comments.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void RemoveComments(this CodeClass instance)
        {
            TraceService.WriteLine("CodeClassExtensions::RemoveComments");

            instance.DocComment = ScorchioConstants.BlankDocComment;

            //// remove function comments.
            instance.Members.OfType<CodeFunction>()
                .ToList()
                .ForEach(x => x.RemoveComments());

            //// remove variable comments.
            instance.Members.OfType<CodeVariable>()
                .ToList()
                .ForEach(x => x.DocComment = ScorchioConstants.BlankDocComment);

            instance.Members.OfType<CodeProperty>()
                .ToList()
                .ForEach(x => x.DocComment = ScorchioConstants.BlankDocComment);
        }
    }
}
