// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectItemExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Entities;
    using EnvDTE;
    using EnvDTE80;
    using Services;

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
            TraceService.WriteLine("ProjectItemExtensions::GetSubProjectItems file=" + instance.Name);

            return instance.ProjectItems.Cast<ProjectItem>().ToList();
        }

        /// <summary>
        /// Gets the name space.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The name space.</returns>
        public static CodeNamespace GetNameSpace(this ProjectItem instance)
        {
            TraceService.WriteLine("ProjectItemExtensions::GetNameSpace file=" + instance.Name);

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
            TraceService.WriteLine("ProjectItemExtensions::AddNameSpace file=" + instance.Name);

            return instance.ContainingProject.CodeModel.AddNamespace(nameSpace, instance.Name);
        }

        /// <summary>
        /// Gets the first code element.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The first code element</returns>
        public static CodeElement GetFirstCodeElement(this ProjectItem instance)
        {
            TraceService.WriteLine("ProjectItemExtensions::GetFirstCodeElement projectItem" + instance.Name);

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

            if (!codeClasses.Any())
            {
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
            TraceService.WriteLine("ProjectItemExtensions::GetFirstInterface");

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
            TraceService.WriteLine("ProjectItemExtensions::CreateInterface");

            string interfaceName = string.Format("I{0}", instance.Name);
            string path = instance.FileNames[1].Replace(instance.Name, interfaceName);

            CodeNamespace codeNamespace = instance.GetNameSpace();

            string nameSpace = codeNamespace.FullName;

            object[] bases = { };

            CodeNamespace interfaceNameSpace = instance.ProjectItems.ContainingProject.CodeModel.AddNamespace(
                nameSpace, path);

            CodeInterface codeInterface = interfaceNameSpace.AddInterface(
                interfaceName, 
                -1, 
                bases, 
                vsCMAccess.vsCMAccessPublic);

            CodeClass codeClass = instance.GetFirstClass();

            if (codeClass != null)
            {
                object interfaceClass = interfaceName;
                codeClass.AddImplementedInterface(interfaceClass, 0);
            }
            else
            {
                TraceService.WriteError("ProjectItemExtensions::CreateInterface cannot GetFirstClass");
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
            TraceService.WriteLine("ProjectItemExtensions::AddHeaderComment");
            
            TextSelection selection = (TextSelection)instance.Document.Selection;

            selection.StartOfDocument();
            selection.NewLine();
            selection.LineUp();
            selection.Text = headerComment;
        }

        /// <summary>
        /// Implements the code snippet.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="codeSnippet">The code snippet.</param>
        public static void ImplementCodeSnippet(
            this ProjectItem instance,
            CodeSnippet codeSnippet)
        {
            TraceService.WriteLine("ProjectItemExtensions::ImplementCodeSnippet in file " + instance.Name);

            if (codeSnippet.UsingStatements.Any())
            {
                List<string> statements = instance.GetUsingStatements();

                foreach (string reference in codeSnippet.UsingStatements)
                {
                    string contains = statements.FirstOrDefault(x => x.Contains(reference));

                    if (contains == null)
                    {
                        instance.AddUsingStatement(reference);
                    }
                }
            }

            instance.GetFirstClass().ImplementCodeSnippet(codeSnippet);
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
            TraceService.WriteLine("ProjectItemExtensions::AddUsingStatement in file " + instance.Name + " statement " + usingStatement);

            CodeNamespace codeNamespace = instance.GetFirstNameSpace();

            if (codeNamespace != null)
            {
                FileCodeModel2 fileCodeModel2 = codeNamespace.ProjectItem.FileCodeModel as FileCodeModel2;

                if (fileCodeModel2 != null)
                {
                    fileCodeModel2.AddImport(usingStatement);
                }
            }
            else
            {
                TraceService.WriteError("ProjectItemExtensions::AddUsingStatement cannot find namespace");
            }
        }

        /// <summary>
        /// Gets the using statements.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>A list of using statements.</returns>
        public static List<string> GetUsingStatements(this ProjectItem instance)
        {
            TraceService.WriteLine("ProjectItemExtensions::GetUsingStatements in file " + instance.Name);

            List<string> statements = new List<string>();

            foreach (CodeElement codeElement in instance.FileCodeModel.CodeElements)
            {
                if (codeElement.Kind == vsCMElement.vsCMElementImportStmt)
                {
                    CodeImport import = codeElement as CodeImport;

                    if (import != null)
                    {
                        EditPoint startEditPoint = import.GetStartPoint().CreateEditPoint();
                        TextPoint textPoint = import.GetEndPoint();
                        string text = startEditPoint.GetText(textPoint);
                        statements.Add(text);
                    }
                }

                if (codeElement.Kind == vsCMElement.vsCMElementNamespace)
                {
                    foreach (CodeElement childCodeElement in codeElement.Children)
                    {
                        CodeImport import = childCodeElement as CodeImport;

                        if (import != null)
                        {
                            EditPoint startEditPoint = import.GetStartPoint().CreateEditPoint();
                            TextPoint textPoint = import.GetEndPoint();
                            string text = startEditPoint.GetText(textPoint);
                            statements.Add(text);
                        }
                    }
                }
            }

            return statements;
        }

        /// <summary>
        /// Fixes the using statements.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void FixUsingStatements(this ProjectItem instance)
        {
            instance.MoveUsingStatements();
            instance.SortAndRemoveUsingStatements();
        }

        /// <summary>
        /// Moves the using statements.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void MoveUsingStatements(this ProjectItem instance)
        {
            TraceService.WriteLine("ProjectItemExtensions::MoveUsingStatements in file " + instance.Name);

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

                instance.DeleteNameSpaceUsingStatements();
            }
            catch (Exception exception)
            {
                TraceService.WriteError("MoveUsingStatements " + exception.Message);
            }
        }

        /// <summary>
        /// Deletes the name space using statements.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void DeleteNameSpaceUsingStatements(this ProjectItem instance)
        {
            bool continueLoop = true;

            do
            {
                CodeElement codeElement = instance.FileCodeModel.CodeElements.Item(1);

                if (codeElement != null)
                {
                    if (codeElement.Kind == vsCMElement.vsCMElementImportStmt)
                    {
                        EditPoint editPoint = codeElement.GetStartPoint().CreateEditPoint();
                        TextPoint textPoint = codeElement.GetEndPoint();

                        editPoint.Delete(textPoint);

                        //// should get rid of the blank line!
                        editPoint.Delete(1);
                    }
                    else
                    {
                        continueLoop = false;
                    }
                }
                else
                {
                    continueLoop = false;
                }
            }
            while (continueLoop);
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
                Window window = instance.Open(VSConstants.VsViewKindCode);

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
            TraceService.WriteLine("ProjectItemExtensions::ReplaceText in file " + instance.Name  + " from '" + text + "' to '" + replacementText + "'");

            Window window = instance.Open(VSConstants.VsViewKindCode);

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
        /// Determines whether the project item is a physical file.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>
        ///   <c>true</c> if [is physical file] [the specified instance]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsPhysicalFile(this ProjectItem instance)
        {
            return instance.Kind == VSConstants.VsProjectItemKindPhysicalFile;
        }

        /// <summary>
        /// Determines whether [is C sharp file] [the specified instance].
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>
        ///   <c>true</c> if [is C sharp file] [the specified instance]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsCSharpFile(this ProjectItem instance)
        {
            bool csharpFile = false;

            if (instance.IsPhysicalFile())
            {
                if (instance.Name.EndsWith(".cs"))
                {
                    csharpFile = true;
                }
            }

            return csharpFile;
        }

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The folder.</returns>
        public static string GetFolder(this ProjectItem instance)
        {
            string folder = string.Empty;
            
            string fileName = instance.FileNames[0];

            DirectoryInfo directoryInfo = new DirectoryInfo(fileName);

            if (directoryInfo.Parent != null)
            {
                folder = directoryInfo.Parent.ToString();
            }

            return folder;
        }
   }
}
