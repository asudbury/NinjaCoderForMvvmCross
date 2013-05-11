// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectItemExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Scorchio.VisualStudio.Extensions
{
    using System.Collections.Generic;
    using System.Linq;

    using EnvDTE;

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
        /// Gets the first class.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The first class.</returns>
        public static CodeClass GetFirstClass(this ProjectItem instance)
        {
            CodeClass codeClass = null;

            CodeElement codeElement = instance.GetFirstCodeElement();

            if (codeElement != null)
            {
                if (codeElement.Kind == vsCMElement.vsCMElementClass)
                {
                    codeClass = codeElement as CodeClass;
                }
            }

            return codeClass;
        }

        /// <summary>
        /// Gets the first interface.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The first interface.</returns>
        public static CodeInterface GetFirstInterface(this ProjectItem instance)
        {
            CodeInterface codeInterface = null;

            CodeElement codeElement = instance.GetFirstCodeElement();

            if (codeElement != null)
            {
                if (codeElement.Kind == vsCMElement.vsCMElementInterface)
                {
                    codeInterface = codeElement as CodeInterface;
                }
            }

            return codeInterface;
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
        public static void AddHeaderComment(this ProjectItem instance, string headerComment)
        {
            TextSelection selection = (TextSelection)instance.Document.Selection;

            selection.StartOfDocument();
            selection.NewLine();
            selection.LineUp();
            selection.Text = headerComment;
        }

        /// <summary>
        /// Gets the icon.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The associated icon.</returns>
        /*public static ImageSource GetIcon(this ProjectItem instance)
        {
            IconService iconService = new IconService();

            return iconService.GetImageSource(instance.Document.FullName, false) ?? 
                   iconService.GetFolderImageSource(false);
        }*/
    }
}
