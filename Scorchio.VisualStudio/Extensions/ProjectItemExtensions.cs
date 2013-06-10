// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectItemExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    using EnvDTE;

    using EnvDTE80;

    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the ProjectItemExtensions type.
    /// </summary>
    public static class ProjectItemExtensions
    {
        /// <summary>
        /// Gets the sub project items.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The sub project items.</returns>
        public static IEnumerable<ProjectItem> GetSubProjectItems(this ProjectItem instance)
        {
            return instance.ProjectItems.Cast<ProjectItem>().ToList();
        }

        /// <summary>
        /// Gets the name space.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The name space.</returns>
        public static CodeNamespace GetNameSpace(this ProjectItem instance)
        {
            return
                instance.FileCodeModel.CodeElements.Cast<CodeElement>()
                        .Where(codeElement => codeElement.Kind == vsCMElement.vsCMElementNamespace)
                        .Cast<CodeNamespace>()
                        .FirstOrDefault();
        }

        /// <summary>
        /// Adds the name space.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="nameSpace">The name space.</param>
        /// <returns>
        /// The created name space.
        /// </returns>
        public static CodeNamespace AddNameSpace(this ProjectItem instance, string nameSpace)
        {
            return instance.ContainingProject.CodeModel.AddNamespace(nameSpace, instance.Name);
        }

        /// <summary>
        /// Gets the first code element.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The first code element</returns>
        public static CodeElement GetFirstCodeElement(this ProjectItem instance)
        {
            if (instance.FileCodeModel.CodeElements.Count > 0)
            {
                CodeElement nameSpaceCodeElement = instance.FileCodeModel.CodeElements.Item(1);

                if (nameSpaceCodeElement.Kind == vsCMElement.vsCMElementNamespace)
                {
                    CodeNamespace codeNamespace = nameSpaceCodeElement as CodeNamespace;

                    if (codeNamespace != null)
                    {
                        if (nameSpaceCodeElement.Children.Count > 0)
                        {
                            return nameSpaceCodeElement.Children.Item(1);
                        }
                    }
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the first name space.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The first namespace.</returns>
        public static CodeNamespace GetFirstNameSpace(this ProjectItem instance)
        {
            IEnumerable<CodeNamespace> codeNameespaces = instance.FileCodeModel.CodeElements.OfType<CodeNamespace>();
            
            return codeNameespaces.FirstOrDefault();
        }

        /// <summary>
        /// Gets the first class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The first class.</returns>
        public static CodeClass GetFirstClass(this ProjectItem instance)
        {
            IEnumerable<CodeClass> codeClasses = instance.FileCodeModel.CodeElements.OfType<CodeClass>();

            if (!codeClasses.Any())
            {
                CodeNamespace codeNamespace = instance.GetFirstNameSpace();

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
                return codeClasses.FirstOrDefault();
            }

            return null;
        }

        /// <summary>
        /// Gets the first interface.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The first interface.</returns>
        public static CodeInterface GetFirstInterface(this ProjectItem instance)
        {
            IEnumerable<CodeInterface> codeInterfaces = instance.FileCodeModel.CodeElements.OfType<CodeInterface>();

            return codeInterfaces.FirstOrDefault();
        }

        /// <summary>
        /// Creates the interface.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The interface.</returns>
        public static CodeInterface CreateInterface(this ProjectItem instance)
        {
            string interfaceName = string.Format("I{0}", instance.Name);
            string path = instance.FileNames[1].Replace(instance.Name, interfaceName);

            CodeNamespace codeNamespace = instance.GetNameSpace();

            string nameSpace = codeNamespace.FullName;

            object[] bases = { };

            CodeNamespace interfaceNameSpace = instance.ProjectItems.ContainingProject.CodeModel.AddNamespace(
                nameSpace, path);

            CodeInterface codeInterface = interfaceNameSpace.AddInterface(
                interfaceName, -1, bases, vsCMAccess.vsCMAccessPublic);

            CodeClass codeClass = instance.GetFirstClass();

            if (codeClass != null)
            {
                object interfaceClass = interfaceName;
                codeClass.AddImplementedInterface(interfaceClass, 0);
            }

            return codeInterface;
        }

        /// <summary>
        /// Adds the header comment.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="headerComment">The header comment.</param>
        public static void AddHeaderComment(
            this ProjectItem instance, 
            string headerComment)
        {
            TextSelection selection = (TextSelection)instance.Document.Selection;

            selection.StartOfDocument();
            selection.NewLine();
            selection.LineUp();
            selection.Text = headerComment;
        }

        /// <summary>
        /// Adds the using statement.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="usingStatement">The using statement.</param>
        public static void AddUsingStatement(
            this ProjectItem instance,
            string usingStatement)
        {
            FileCodeModel2 fileCodeModel2 = instance.GetFirstNameSpace().ProjectItem.FileCodeModel as FileCodeModel2;

            if (fileCodeModel2 != null)
            {
                foreach (CodeElement codeElement in fileCodeModel2.CodeElements)
                {
                    if (codeElement.Kind == vsCMElement.vsCMElementImportStmt)
                    {
                        CodeImport codeImport = codeElement as CodeImport;

                        if (codeImport.Namespace == usingStatement)
                        {
                            return;
                        }
                    }
                }

                fileCodeModel2.AddImport(usingStatement);
            }
        }

        /// <summary>
        /// Inserts the method at end of class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="snippetPath">The snippet path.</param>
        /// <param name="updateUsingStatements">if set to <c>true</c> [update using statements].</param>
        public static void InsertMethod(
            this ProjectItem instance,
            string snippetPath,
            bool updateUsingStatements,
            bool closeWindow)
        {
            CodeClass codeClass = instance.GetFirstClass();

            if (codeClass != null)
            {
                CodeFunction codeFunction = codeClass.AddFunction(
                                                    "temp",
                                                    vsCMFunction.vsCMFunctionFunction,
                                                    vsCMTypeRef.vsCMTypeRefVoid,
                                                    -1,
                                                    vsCMAccess.vsCMAccessPublic,
                                                    null);

                TextPoint startPoint = codeFunction.StartPoint;

                EditPoint editPoint = startPoint.CreateEditPoint();

                codeClass.RemoveMember(codeFunction);

                editPoint.Insert("\r\n\r\n");
                editPoint.InsertFromFile(snippetPath);

                if (updateUsingStatements)
                {
                    //// tidy up the using statements.
                    instance.Save();
                    instance.MoveUsingStatements();
                    instance.Save();
                    instance.SortAndRemoveUsingStatements();
                    instance.Save();
                }

                if (closeWindow)
                {
                    if (instance.Document != null)
                    {
                        instance.Document.ActiveWindow.Close();
                    }
                }
            }
        }

        /// <summary>
        /// Gets the using statements.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>A list of using statements.</returns>
        public static List<string> GetUsingStatements(this ProjectItem instance)
        {
            List<string> statements = new List<string>();

            foreach (CodeElement codeElement in instance.FileCodeModel.CodeElements)
            {
                if (codeElement.Kind == vsCMElement.vsCMElementImportStmt)
                {
                    statements.Add(codeElement.FullName);
                }
            }

            return statements;
        }

        /// <summary>
        /// Moves the using statements.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void MoveUsingStatements(this ProjectItem instance)
        {
            List<string> usingStatements = new List<string>();

            try
            {
                foreach (CodeElement codeElement in instance.FileCodeModel.CodeElements)
                {
                    //// assume the using statements come before the required namespace.
                    if (codeElement.Kind == vsCMElement.vsCMElementImportStmt)
                    {
                        TextPoint startPoint = codeElement.StartPoint;
                        TextPoint endPoint = codeElement.EndPoint;

                        string text = startPoint.CreateEditPoint().GetText(endPoint);
                        usingStatements.Add(text);
                    }
                    else if (codeElement.Kind == vsCMElement.vsCMElementNamespace)
                    {
                        TextPoint nameSpacePoint = codeElement.GetStartPoint(vsCMPart.vsCMPartBody);
                        EditPoint editPoint = nameSpacePoint.CreateEditPoint();

                        foreach (string statement in usingStatements)
                        {
                            editPoint.Insert("\t" + statement + "\n");
                        }
                    }
                }

                //// now perform the deletes.
                for (int i = 1; i <= instance.FileCodeModel.CodeElements.Count; i++)
                {
                    CodeElement codeElement = instance.FileCodeModel.CodeElements.Item(i);

                    if (codeElement.Kind == vsCMElement.vsCMElementImportStmt)
                    {
                        EditPoint editPoint = codeElement.GetStartPoint().CreateEditPoint();
                        TextPoint textPoint = codeElement.GetEndPoint();

                        editPoint.Delete(textPoint);
                        
                        //// should get rid of the blank line!
                        editPoint.Delete(1);
                    }
                }
            }
            catch (Exception exception)
            {
                TraceService.WriteError("MoveUsingStatements " + exception.Message);
            }
        }

        /// <summary>
        /// Sorts the and remove using statements.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void SortAndRemoveUsingStatements(this ProjectItem instance)
        {
            try
            {
                const string ConstantsvsViewKindCode = "{7651A701-06E5-11D1-8EBD-00A0C90F26EA}";
                Window window = instance.Open(ConstantsvsViewKindCode);

                window.Activate();

                instance.DTE.ExecuteCommand("Edit.RemoveAndSort");
            }
            catch (Exception exception)
            {
                TraceService.WriteError("SortAndRemoveUsingStatements " + exception.Message);
            }
        }

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
            TextSelection textSelection = instance.DTE.ActiveDocument.Selection;
            textSelection.SelectAll();
            textSelection.ReplacePattern(text, replacementText);
            instance.Save();
        }

        /*public static CodeVariable  AddVariable(
            this ProjectItem instance,
            string text)
        {
            CodeClass codeClass = instance.GetFirstClass();

            if (codeClass != null)
            {
                codeClass.AddVariable()
            }
        }*/
    }
}
