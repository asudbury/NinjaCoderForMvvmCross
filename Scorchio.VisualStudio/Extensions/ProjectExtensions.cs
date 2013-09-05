// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Extensions
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using EnvDTE;
    using EnvDTE80;
    using Services;
    using VSLangProj;

    /// <summary>
    ///  Defines the ProjectExtensions type.
    /// </summary>
    public static class ProjectExtensions
    {
        /// <summary>
        /// Gets the project path.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The project path.</returns>
        public static string GetProjectPath(this Project instance)
        {
            string path = string.Empty;

            try
            {
                path = Path.GetDirectoryName(instance.FullName);
            }
            catch (Exception exception)
            {
                TraceService.WriteError("ProjectExtensions::GetProjectPath exception=" + exception.Message);
            }

            return path;
        }

        /// <summary>
        /// Gets the project items.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The project items.</returns>
        public static IEnumerable<ProjectItem> GetProjectItems(this Project instance)
        {
            TraceService.WriteLine("ProjectExtensions::GetProjectItems project=" + instance.Name);

            return instance.ProjectItems != null ? instance.ProjectItems.Cast<ProjectItem>().ToList() : null;
        }

        /// <summary>
        /// Gets the c# project items.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The project items.</returns>
        public static IEnumerable<ProjectItem> GetCSharpProjectItems(this Project instance)
        {
            TraceService.WriteLine("ProjectExtensions::GetCSharpProjectItems project=" + instance.Name);

            return instance.ProjectItems.Cast<ProjectItem>().Where(x => x.Name.EndsWith(".cs")).ToList();
        }

        /// <summary>
        /// Gets the project item.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The project item.</returns>
        public static ProjectItem GetProjectItem(
            this Project instance,
            string fileName)
        {
            TraceService.WriteLine("ProjectExtensions::GetProjectItem project=" + instance.Name + " fileName=" + fileName);

            ProjectItem projectItem = instance.GetCSharpProjectItems().FirstOrDefault(x => x.Name.StartsWith(fileName));

            //// try all the sub-folders!
            if (projectItem == null)
            {
                IEnumerable<ProjectItem> projectItems = instance.GetProjectItems();

                foreach (ProjectItem item in projectItems)
                {
                    if (item.Name.StartsWith(fileName))
                    {
                        return item;
                    }

                    IEnumerable<ProjectItem> subProjectItems = item.GetSubProjectItems();

                    foreach (ProjectItem subItem in subProjectItems.Where(subItem => subItem.Name.StartsWith(fileName)))
                    {
                        return subItem;
                    }
                }
            }

            return projectItem;
        }

        /// <summary>
        /// Gets the folder.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns>The folder.</returns>
        public static ProjectItem GetFolder(
            this Project instance,
            string folderName)
        {
            TraceService.WriteLine("ProjectExtensions::GetFolder project=" + instance.Name + " folderName=" + folderName);

            List<ProjectItem> projectItems = instance.ProjectItems != null ? instance.ProjectItems.Cast<ProjectItem>().ToList() : null;

            if (projectItems != null)
            {
                return projectItems.Where(projectItem => projectItem.Kind == VSConstants.VsProjectItemKindPhysicalFolder)
                    .FirstOrDefault(projectItem => projectItem.Name == folderName);
            }

            return null;
        }

        /// <summary>
        /// Gets the project references.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The project references.</returns>
        public static IEnumerable<Reference> GetProjectReferences(this Project instance)
        {
            TraceService.WriteLine("ProjectExtensions::GetProjectReferences project=" + instance.Name);

            VSProject project = instance.Object as VSProject;

            return project != null ? project.References.Cast<Reference>() : null;
        }

        /// <summary>
        /// Gets the reference path.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="referenceName">Name of the reference.</param>
        /// <returns>The path of the reference.</returns>
        public static string GetReferencePath(
            this Project instance,
            string referenceName)
        {
            TraceService.WriteLine("ProjectExtensions::GetReferencePath project=" + instance.Name);

            IEnumerable<Reference> references = instance.GetProjectReferences();

            foreach (Reference reference in references.Where(reference => reference.Name == referenceName))
            {
                return reference.Path;
            }

            return string.Empty;
        }

        /// <summary>
        /// Adds the project reference.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="referencedProject">The referenced project.</param>
        /// <returns>The reference.</returns>
        public static Reference AddProjectReference(
            this Project instance, 
            Project referencedProject)
        {
            TraceService.WriteLine("ProjectExtensions::AddProjectReference project=" + instance.Name);

            VSProject project = (VSProject)instance.Object;

            return project.References.AddProject(referencedProject);
        }

        /// <summary>
        /// Removes the reference.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="referenceName">Name of the reference.</param>
        public static void RemoveReference(
            this Project instance,
            string referenceName)
        {
            TraceService.WriteLine("ProjectExtensions::RemoveReference project=" + instance.Name);

            IEnumerable<Reference> references = instance.GetProjectReferences();

            foreach (Reference reference in references
                .Where(reference => reference.Name == referenceName))
            {
                reference.Remove();
            }    
        }

        /// <summary>
        /// Adds to folder from template.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="createFolder">if set to <c>true</c> [create folder].</param>
        /// <returns>True or False.</returns>
        public static bool AddToFolderFromTemplate(
            this Project instance,
            string folderName,
            string templateName,
            string fileName,
            bool createFolder)
        {
            TraceService.WriteLine("ProjectExtensions::AddToFolderFromTemplate project=" + instance.Name);

            string path = instance.Properties.Item("FullPath").Value;
            ProjectItems projectItems = instance.ProjectItems;

            //// this supports passing of folder name - currently used by viewmodels and views.
            //// may not be required if we refactor the item templates to embed the directory.
            if (createFolder)
            {
                ProjectItem folderProjectItem = instance.ProjectItems
                    .Cast<ProjectItem>()
                    .FirstOrDefault(projectItem => projectItem.Name == folderName);

                //// if the folder doesn't exist create it.
                if (folderProjectItem == null)
                {
                    folderProjectItem = instance.ProjectItems.AddFolder(folderName);
                }

                projectItems = folderProjectItem.ProjectItems;

                path = folderProjectItem.Properties.Item("FullPath").Value;
            }

            Solution2 solution = instance.DTE.Solution as Solution2;

            string templatePath = solution.GetProjectItemTemplate(templateName);

            if (templatePath != null)
            {
                string filePath = string.Format(@"{0}\{1}\{2}", path, folderName, fileName);

                if (File.Exists(filePath) == false)
                {
                    projectItems.AddFromTemplate(templatePath, fileName);
                }

                return true;
            }

            TraceService.WriteError("ProjectExtensions::AddToFolderFromTemplate Cannot find template " + templateName);

            return false;
        }

        /// <summary>
        /// Adds to folder from file.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns> True or False.</returns>
        public static bool AddToFolderFromFile(
            this Project instance,
            string folderName,
            string fileName)
        {
            TraceService.WriteLine("ProjectExtensions::AddToFolderFromFile folderName=" + folderName + " fileName=" + fileName);

            ProjectItem folderProjectItem = instance.ProjectItems
                .Cast<ProjectItem>()
                .FirstOrDefault(projectItem => projectItem.Name == folderName);

            //// if the folder doesn't exist create it.
            if (folderProjectItem == null)
            {
                folderProjectItem = instance.ProjectItems.AddFolder(folderName);
            }

            folderProjectItem.ProjectItems.AddFromFile(fileName);
            return true;
        }

        /// <summary>
        /// Adds the reference.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="destinationFolder">The destination folder.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="source">The source.</param>
        /// <param name="addFileToFolder">if set to <c>true</c> [add file to folder].</param>
        /// <returns>The Reference.</returns>
        public static Reference AddReference(
            this Project instance,
            string destinationFolder,
            string destination,
            string source,
            bool addFileToFolder)
        {
            TraceService.WriteLine("ProjectExtensions::AddReference project=" + instance.Name);

            Reference reference = null;

            if (destination.EndsWith(";"))
            {
                destination = destination.TrimEnd(';');
            }

            if (destination.EndsWith(".dll") == false)
            {
                destination += ".dll";
            }

            if (source.EndsWith(";"))
            {
                source = source.TrimEnd(';');
            }

            if (source.EndsWith(".dll") == false)
            {
                source += ".dll";
            }

            //// only do if destination file doesn't exist
            if (File.Exists(destination) == false)
            {
                //// the directory may not exist - if so create it!
                string directory = Path.GetDirectoryName(destination);

                if (directory != null)
                {
                    if (Directory.Exists(directory) == false)
                    {
                        Directory.CreateDirectory(directory);
                    }
                }

                File.Copy(source, destination, true);

                if (addFileToFolder)
                {
                    instance.AddToFolderFromFile(destinationFolder, destination);
                }

                reference = instance.AddReference(destination);
            }

            return reference;
        }

        /// <summary>
        /// Adds the reference.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="path">The path.</param>
        /// <returns>The reference.</returns>
        public static Reference AddReference(
            this Project instance,
            string path)
        {
            Reference reference = null;

            //// now add a reference to the file
            VSProject studioProject = instance.Object as VSProject;

            if (studioProject != null)
            {
                reference = studioProject.References.Add(path);
            }

            return reference;
        }

        /// <summary>
        /// Removes the folder.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <returns>The removed folder.</returns>
        public static ProjectItem RemoveFolder(
            this Project instance, 
            string folderName)
        {
            TraceService.WriteLine("ProjectExtensions::RemoveFolder project=" + instance.Name + " folder=" + folderName);

            ProjectItem removedProjectItem = null;

            IEnumerable<ProjectItem> projectItems = instance.GetProjectItems();

            if (projectItems != null)
            {
                foreach (ProjectItem projectItem in projectItems)
                {
                    if (projectItem.Kind == VSConstants.VsProjectItemKindPhysicalFolder)
                    {
                        if (projectItem.Name.ToLower() == folderName.ToLower())
                        {
                            removedProjectItem = projectItem;
                            projectItem.Remove();
                            break;
                        }
                    }
                }
            }

            return removedProjectItem;
        }

        /// <summary>
        /// Removes the file headers.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void RemoveFileHeaders(this Project instance)
        {
            TraceService.WriteLine("ProjectExtensions::RemoveFileHeaders");

            IEnumerable<ProjectItem> projectItems = instance.GetProjectItems();

            if (projectItems != null)
            {
                foreach (ProjectItem projectItem in projectItems)
                {
                    projectItem.RemoveHeader();
                }
            }
        }
        
        /// <summary>
        /// Removes the comments.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void RemoveComments(this Project instance)
        {
            TraceService.WriteLine("ProjectExtensions::RemoveComments");

            IEnumerable<ProjectItem> projectItems = instance.GetProjectItems();

            if (projectItems != null)
            {
                foreach (ProjectItem projectItem in projectItems)
                {
                    projectItem.RemoveComments();
                }
            }
        }

        /// <summary>
        /// Gets the folder items.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="withFileExtensions">if set to <c>true</c> [with file extensions].</param>
        /// <returns>The folder items.</returns>
        public static IEnumerable<string> GetFolderItems(
            this Project instance,
            string folderName,
            bool withFileExtensions)
        {
            TraceService.WriteLine("ProjectExtensions::GetFolderItems");

            List<string> files = new List<string>();
            
            ProjectItem projectItem = instance.GetFolder(folderName);

            IEnumerable<ProjectItem> projectItems = projectItem.GetSubProjectItems();

            if (withFileExtensions)
            {
                files.AddRange(projectItems.Select(item => item.Name));
            }
            else
            {
                files.AddRange(projectItems.Select(item => Path.GetFileNameWithoutExtension(item.Name)));
            }

            return files;
        }
    }
}
