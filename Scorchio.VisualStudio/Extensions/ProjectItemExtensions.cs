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
        /// Gets the c# project items.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The project items.</returns>
        public static IEnumerable<ProjectItem> GetCSharpProjectItems(this ProjectItem instance)
        {
            TraceService.WriteLine("ProjectExtensions::GetCSharpProjectItems project=" + instance.Name);

            return instance.ProjectItems.Cast<ProjectItem>().Where(x => x.Name.EndsWith(".cs")).ToList();
        }

        /// <summary>
        /// Gets the c# project items.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The project items.</returns>
        public static IEnumerable<ProjectItem> GetXamlProjectItems(this ProjectItem instance)
        {
            TraceService.WriteLine("ProjectExtensions::GetCSharpProjectItems project=" + instance.Name);

            return instance.ProjectItems.Cast<ProjectItem>().Where(x => x.Name.EndsWith(".xaml")).ToList();
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

            return instance.FileCodeModel.CodeElements.OfType<CodeNamespace>().FirstOrDefault();
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
                return codeNamespace.Children.OfType<CodeClass>().FirstOrDefault();
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

            return instance.FileCodeModel.CodeElements.OfType<CodeInterface>().FirstOrDefault();
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
        /// <param name="formatFunctionParameters">if set to <c>true</c> [format function parameters].</param>
        public static void ImplementCodeSnippet(
            this ProjectItem instance,
            CodeSnippet codeSnippet,
            bool formatFunctionParameters)
        {
            TraceService.WriteLine("ProjectItemExtensions::ImplementCodeSnippet in file " + instance.Name);

            if (codeSnippet.UsingStatements != null &&
                codeSnippet.UsingStatements.Any())
            {
                IEnumerable<string> statements = instance.GetUsingStatements();

                foreach (string reference in codeSnippet.UsingStatements)
                {
                    string contains = statements.FirstOrDefault(x => x.Contains(reference));

                    if (contains == null)
                    {
                        instance.AddUsingStatement(reference);

                        TraceService.WriteLine("Using statement added " + reference);
                    }
                }
            }

            instance.GetFirstClass().ImplementCodeSnippet(
                codeSnippet,
                formatFunctionParameters);

            //// now apply any variable substitutions

            if (codeSnippet.ReplacementVariables != null)
            {
                foreach (KeyValuePair<string, string> item in codeSnippet.ReplacementVariables)
                {
                    instance.ReplaceText(item.Key, item.Value);
                }
            }
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
        public static IEnumerable<string> GetUsingStatements(this ProjectItem instance)
        {
            TraceService.WriteLine("ProjectItemExtensions::GetUsingStatements in file " + instance.Name);

            List<string> statements = new List<string>();

            foreach (CodeElement codeElement in instance.FileCodeModel.CodeElements)
            {
                CodeImport import = codeElement as CodeImport;

                switch (codeElement.Kind)
                {
                    case vsCMElement.vsCMElementImportStmt:

                        if (import != null)
                        {
                            EditPoint startEditPoint = import.GetStartPoint().CreateEditPoint();
                            TextPoint textPoint = import.GetEndPoint();
                            string text = startEditPoint.GetText(textPoint);
                            statements.Add(text);
                        }

                        break;

                    case vsCMElement.vsCMElementNamespace:

                        foreach (CodeElement childCodeElement in codeElement.Children)
                        {
                            import = childCodeElement as CodeImport;

                            if (import != null)
                            {
                                EditPoint startEditPoint = import.GetStartPoint().CreateEditPoint();
                                TextPoint textPoint = import.GetEndPoint();
                                string text = startEditPoint.GetText(textPoint);
                                statements.Add(text);
                            }
                        }

                        break;
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
            TraceService.WriteLine("ProjectItemExtensions::FixUsingStatements in file " + instance.Name);

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
                    switch (codeElement.Kind)
                    {
                        case vsCMElement.vsCMElementImportStmt:

                            TextPoint startPoint = codeElement.StartPoint;
                            TextPoint endPoint = codeElement.EndPoint;

                            string text = startPoint.CreateEditPoint().GetText(endPoint);
                            usingStatements.Add(text);

                            break;

                        case vsCMElement.vsCMElementNamespace:
                            
                            TextPoint nameSpacePoint = codeElement.GetStartPoint(vsCMPart.vsCMPartBody);
                            EditPoint editPoint = nameSpacePoint.CreateEditPoint();

                            foreach (string statement in usingStatements)
                            {
                                editPoint.Insert("\t" + statement + "\n");
                            }

                            break;
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
            TraceService.WriteLine("ProjectItemExtensions::DeleteNameSpaceUsingStatements in file " + instance.Name);

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
        /// <param name="findOptions">The find options.</param>
        public static void ReplaceText(
            this ProjectItem instance,
            string text,
            string replacementText,
            int findOptions = (int)vsFindOptions.vsFindOptionsMatchCase)
        {
            TraceService.WriteLine("ProjectItemExtensions::ReplaceText in file " + instance.Name  + " from '" + text + "' to '" + replacementText + "'");

            Window window = instance.Open(VSConstants.VsViewKindCode);

            if (window != null)
            {
                window.Activate();

                TextSelection textSelection = instance.DTE.ActiveDocument.Selection;
                textSelection.SelectAll();

                bool replaced = textSelection.ReplacePattern(text, replacementText, findOptions);
               
                if (replaced)
                {
                    TraceService.WriteLine("Replaced");
                }

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
        /// Determines whether [is xaml file] [the specified instance].
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>
        ///   <c>true</c> if [is xaml file] [the specified instance]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsXamlFile(this ProjectItem instance)
        {
            bool xamlFile = false;

            if (instance.IsPhysicalFile())
            {
                if (instance.Name.EndsWith(".xaml"))
                {
                    xamlFile = true;
                }
            }

            return xamlFile;
        }

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The folder.</returns>
        public static string GetFolder(this ProjectItem instance)
        {
            TraceService.WriteLine("ProjectItemExtensions::GetFolder");

            string folder = string.Empty;
            
            string fileName = instance.FileNames[0];

            DirectoryInfo directoryInfo = new DirectoryInfo(fileName);

            if (directoryInfo.Parent != null)
            {
                folder = directoryInfo.Parent.ToString();
            }

            return folder;
        }

        /// <summary>
        /// Deletes the folder.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="excludeFiles">The exclude files.</param>
        public static void DeleteFolder(
            this ProjectItem instance,
            IEnumerable<string> excludeFiles = null)
        {
            TraceService.WriteLine("ProjectItemExtensions::DeleteFolder");

            bool deleteDirectory = true;

            string directoryName = instance.FileNames[0];

            DirectoryInfo directoryInfo = new DirectoryInfo(directoryName);

            foreach (FileInfo fileInfo in directoryInfo.GetFiles())
            {
                if (excludeFiles != null)
                {
                    if (excludeFiles.Any(excludeFile => fileInfo.FullName.ToLower().Contains(excludeFile.ToLower())))
                    {
                        deleteDirectory = false;
                    }
                    else
                    {
                       fileInfo.Delete();
                    }
                }
                else
                {
                    fileInfo.Delete();
                }
            }

            if (deleteDirectory)
            {
                directoryInfo.Delete();
            }
        }

        /// <summary>
        /// Removes the comments.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void RemoveComments(this ProjectItem instance)
        {
            TraceService.WriteLine("ProjectExtensions::RemoveComments");

            if (instance.IsCSharpFile())
            {
                CodeClass codeClass = instance.GetFirstClass();

                if (codeClass != null)
                {
                    instance.GetFirstClass().RemoveComments();
                }
            }

            //// don't forget sub items.

            if (instance.ProjectItems != null)
            {
                instance.GetCSharpProjectItems()
                    .ToList()
                    .ForEach(x => x.RemoveComments());

                instance.GetXamlProjectItems()
                    .ToList()
                    .ForEach(x => x.RemoveComments());

            }
        }
        
        /// <summary>
        /// Removes the header.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void RemoveHeader(this ProjectItem instance)
        {
            TraceService.WriteLine("ProjectExtensions::RemoveHeader");

            if (instance.IsCSharpFile())
            {
                Window window = instance.Open(VSConstants.VsViewKindCode);
                
                if (window != null)
                {
                    window.Activate();

                    TextSelection selection = (TextSelection)instance.Document.Selection;

                    bool continueLoop = true;
                    int loopCounter = 0;

                    do
                    {
                        //// just in case we get infinity loop problem!
                        if (loopCounter > 100)
                        {
                            continueLoop = false;
                        }

                        selection.GotoLine(1, true);
                        selection.SelectLine();

                        if (selection.Text.StartsWith("//"))
                        {
                            selection.Delete();
                            loopCounter++;
                        }
                        else
                        {
                            continueLoop = false;
                        }
                    }
                    while (continueLoop);
                }
            }

            //// don't forget sub items.

            if (instance.ProjectItems != null)
            {
                IEnumerable<ProjectItem> subProjectItems = instance.GetCSharpProjectItems();

                foreach (ProjectItem subProjectItem in subProjectItems)
                {
                    subProjectItem.RemoveHeader();
                }
            }
        }

        /// <summary>
        /// Removes the and delete.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void RemoveAndDelete(this ProjectItem instance)
        {
            TraceService.WriteLine("ProjectExtensions::RemoveAndDelete");

            instance.Remove();

            string fileName = instance.FileNames[0];

            if (File.Exists(fileName))
            {
                TraceService.WriteLine("ProjectExtensions::RemoveAndDelete fileName=" + fileName);
                File.Delete(fileName);
            }
        }

        /// <summary>
        /// Removes the double blank lines.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void RemoveDoubleBlankLines(this ProjectItem instance)
        {
            TraceService.WriteLine("ProjectExtensions::RemoveDoubleBlankLines");
            
            const string RegEx = @"^(?([^\r\n])\s)*\r?$\r?\n^(?([^\r\n])\s)*\r?$\r?\n";

            instance.ReplaceText(RegEx, string.Empty, (int)vsFindOptions.vsFindOptionsRegularExpression);
        }

        /// <summary>
        /// Delete the file contents.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void DeleteFileContents(this ProjectItem instance)
        {
            TraceService.WriteLine("ProjectExtensions::DeleteFileContents");

            Document document = instance.Document;

            TextDocument textDoc = (TextDocument)document.Object("TextDocument");

            EditPoint editPoint = textDoc.StartPoint.CreateEditPoint();
            EditPoint endPoint = textDoc.EndPoint.CreateEditPoint();
            editPoint.Delete(endPoint);
        }
    }
}
