// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Extensions
{
    using EnvDTE;
    using EnvDTE80;
    using Services;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
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
            ////TraceService.WriteLine("ProjectExtensions::GetProjectItems project=" + instance.Name);

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

                    foreach (ProjectItem subItem in subProjectItems
                        .Where(subItem => subItem.Name.StartsWith(fileName)))
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

            try
            {
                List<ProjectItem> projectItems = instance.ProjectItems != null ? instance.ProjectItems.Cast<ProjectItem>().ToList() : null;

                if (projectItems != null)
                {
                    return projectItems
                        .Where(projectItem => projectItem.Kind == VSConstants.VsProjectItemKindPhysicalFolder)
                        .FirstOrDefault(projectItem => projectItem.Name == folderName);
                }

                return null;
            }
            catch (Exception exception)
            {
                TraceService.WriteError("exception=" + exception.Message);
                return null;
            }
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

            foreach (Reference reference in references
                .Where(reference => reference.Name == referenceName))
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

            instance.GetProjectReferences()
                .Where(x => x.Name == referenceName)
                .ToList()
                .ForEach(y => y.Remove());
        }

        /// <summary>
        /// Removes the reference.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="value">The value.</param>
        public static void RemoveReferences(
            this Project instance,
            string value)
        {
            TraceService.WriteLine("ProjectExtensions::RemoveReference project=" + instance.Name);

            instance.GetProjectReferences()
                .Where(x => x.Name.Contains(value))
                .ToList()
                .ForEach(y => y.Remove());
        }

        /// <summary>
        /// Adds the item to folder from template.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>True or False.</returns>
        public static bool AddItemToFolderFromTemplate(
            this Project instance,
            string templateName,
            string fileName)
        {
            TraceService.WriteLine("ProjectExtensions::AddItemToFolderFromTemplate project=" + instance.Name + " templateName=" + templateName + " fileName=" + fileName);

            ProjectItems projectItems = instance.ProjectItems;

            Solution2 solution = instance.DTE.Solution as Solution2;

            string templatePath = solution.GetProjectItemTemplate(templateName);

            if (templatePath != null)
            {
                projectItems.AddFromTemplate(templatePath, fileName);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds the project to folder from template.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="templateName">Name of the template.</param>
        /// <param name="path">The path.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>True or False.</returns>
        public static bool AddProjectToFolderFromTemplate(
            this Project instance, 
            string templateName, 
            string path,
            string fileName)
        {
            TraceService.WriteLine("ProjectExtensions::AddItemToFolderFromTemplate project=" + instance.Name + " templateName=" + templateName + " fileName=" + fileName);

            SolutionFolder solutionFolder = (SolutionFolder)instance.Object;

            Solution2 solution = instance.DTE.Solution as Solution2;

            string templatePath = solution.GetProjectTemplate(templateName);

            if (templatePath != null)
            {
                solutionFolder.AddFromTemplate(templatePath, path, fileName);

                return true;
            }

            return false;
        }

        /// <summary>
        /// Adds to folder from file.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns> True or False.</returns>
        public static ProjectItem AddToFolderFromFile(
            this Project instance,
            string folderName,
            string fileName)
        {
            TraceService.WriteLine("ProjectExtensions::AddToFolderFromFile folderName=" + folderName + " fileName=" + fileName);

            ProjectItem folderProjectItem;

            string[] folders = folderName.Split('\\');

            if (folders.Length > 1)
            {
                //// we need to handle multipe subfolders (Resources\Layout for Android for example!)

                folderProjectItem = instance.ProjectItems
                    .Cast<ProjectItem>()
                    .FirstOrDefault(projectItem => projectItem.Name == folders[0]);

                ProjectItem subFolderProjectItem = folderProjectItem.GetFolder(folders[1]);

                if (subFolderProjectItem == null)
                {
                    subFolderProjectItem = folderProjectItem.ProjectItems.AddFolder(folders[1]);
                }

                if (subFolderProjectItem != null)
                {
                    folderProjectItem = subFolderProjectItem;
                }
            }

            else
            {
                folderProjectItem = instance.ProjectItems
                    .Cast<ProjectItem>()
                    .FirstOrDefault(projectItem => projectItem.Name == folderName);
            }
            
            //// if the folder doesn't exist create it.
            if (folderProjectItem == null)
            {
                folderProjectItem = instance.ProjectItems.AddFolder(folderName);
            }

            return folderProjectItem.ProjectItems.AddFromFile(fileName);
        }

        /// <summary>
        /// Adds the text template.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="projectFolder">The project folder.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="textOutput">The text output.</param>
        /// <param name="outputTextTemplateContentToTraceFile">if set to <c>true</c> [output text template content to trace file].</param>
        /// <returns></returns>
        public static string AddTextTemplate(
            this Project instance,
            string projectFolder, 
            string fileName, 
            string textOutput,
            bool outputTextTemplateContentToTraceFile)
        {
            TraceService.WriteLine("ProjectExtensions::AddTextTemplate projectFolder=" + projectFolder + " fileName=" + fileName);

            if (outputTextTemplateContentToTraceFile)
            {
                TraceService.WriteLine("textOutput=" + textOutput);
            }

            string projectPath = instance.GetProjectPath();

            TraceService.WriteLine("projectPath=" + projectPath);

            string directory = projectPath + "\\" + projectFolder;

            TraceService.WriteLine("directory=" + directory);

            if (Directory.Exists(directory) == false)
            {
                TraceService.WriteLine("creating directory=" + directory);
                Directory.CreateDirectory(directory);
            }

            string path = directory + "\\" + fileName;

            TraceService.WriteLine("path=" + path);

            //// save to a file
            File.WriteAllText(path, textOutput);

            TraceService.WriteLine("addToFolderFromFile");

            instance.AddToFolderFromFile(projectFolder, path);

            return fileName + " added to project " + instance.Name;
        }

        /// <summary>
        /// Adds the reference.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="destinationFolder">The destination folder.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="source">The source.</param>
        /// <returns>
        /// The Reference.
        /// </returns>
        public static Reference AddReference(
            this Project instance,
            string destinationFolder,
            string destination,
            string source)
        {
            TraceService.WriteLine("ProjectExtensions::AddReference project=" + instance.Name);
            TraceService.WriteLine("Source=" + source);

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

                //// reference the source and dont copy the file!
                reference = instance.AddReference(source);
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
            TraceService.WriteLine("ProjectExtensions::AddReference project=" + instance.Name);
            TraceService.WriteLine("Path=" + path);

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
                foreach (ProjectItem projectItem in projectItems
                    .Where(projectItem => projectItem.Kind == VSConstants.VsProjectItemKindPhysicalFolder)
                    .Where(projectItem => string.Equals(projectItem.Name, folderName, StringComparison.CurrentCultureIgnoreCase)))
                {
                    removedProjectItem = projectItem;
                    projectItem.Remove();
                    break;
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
            TraceService.WriteLine("ProjectExtensions::RemoveFileHeaders project=" + instance.Name);

            instance.GetProjectItems()
                .ToList()
                .ForEach(x => x.RemoveHeader());
        }
        
        /// <summary>
        /// Removes the comments.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void RemoveComments(this Project instance)
        {
            TraceService.WriteLine("ProjectExtensions::RemoveComments project=" + instance.Name);

            instance.GetProjectItems()
                .ToList()
                .ForEach(x => x.RemoveComments());
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
            TraceService.WriteLine("ProjectExtensions::GetFolderItems project=" + instance.Name + " Folder=" + folderName);

            List<string> files = new List<string>();
            
            ProjectItem projectItem = instance.GetFolder(folderName);

            if (projectItem != null)
            {
                IEnumerable<ProjectItem> projectItems = projectItem.GetSubProjectItems();

                files.AddRange(
                    withFileExtensions
                        ? projectItems.Select(item => item.Name)
                        : projectItems.Select(item => Path.GetFileNameWithoutExtension(item.Name)));
            }

            return files;
        }

        /// <summary>
        /// Adds the assemblies.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="assembliesPath">The assemblies path.</param>
        /// <param name="assemblies">The assemblies.</param>
        public static void AddAssemblies(
            this Project instance, 
            string assembliesPath, 
            IEnumerable<string> assemblies)
        {
            string[] enumerable = assemblies as string[] ?? assemblies.ToArray();

            TraceService.WriteLine("ProjectExtensions::AddAssemblies project=" + instance.Name + " assembly count=" + enumerable.Count());

            foreach (string path in enumerable
                .Select(assembly => string.Format(
                "{0}{1}.dll",
                assembliesPath,
                assembly)))
            {
                if (File.Exists(path))
                {
                    instance.AddReference(path);
                }
            }
        }

        /// <summary>
        /// Determines whether [has nuget packages] [the specified instance].
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>True or false.</returns>
        public static bool HasNugetPackages(this Project instance)
        {
            TraceService.WriteLine("ProjectExtensions::HasNugetPackages");

            return instance.GetProjectItems().FirstOrDefault(x => x.Name == "packages.config") != null;
        }
        
        /// <summary>
        /// Gets the sub projects.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static IEnumerable<Project> GetSubProjects(this Project instance)
        {
            TraceService.WriteLine("ProjectExtensions::GetSubProjects");

            List<Project> subProjects = new List<Project>();

            if (instance.ProjectItems != null)
            {
                subProjects.AddRange(from ProjectItem projectItem in instance.ProjectItems 
                                     where projectItem.SubProject != null 
                                     select projectItem.SubProject);
            }

            return subProjects.OrderBy(x => x.Name);
        }

        /// <summary>
        /// Gets the sub folders.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="folderName">Name of the folder.</param>
        public static IEnumerable<ProjectItem> GetSubFolders(
            this Project instance,
            string folderName)
        {
            TraceService.WriteLine("ProjectExtensions::GetSubFolders folderName=" + folderName);
            
            List<ProjectItem> folders = new List<ProjectItem>();

            ProjectItem folderItem = instance.GetFolder(folderName);

            if (folderItem != null)
            {
                folders.AddRange(folderItem.ProjectItems.Cast<ProjectItem>()
                    .Where(projectItem => projectItem.Kind == VSConstants.VsProjectItemKindPhysicalFolder));
            }

            return folders;
        }

        /// <summary>
        /// Gets the folder project items.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static IEnumerable<ProjectItem> GetFolderProjectItems(this Project instance)
        {
            TraceService.WriteLine("ProjectExtensions::GetFolderProjectItems");

            return instance.ProjectItems.Cast<ProjectItem>()
                .Where(x => x.Kind == VSConstants.VsProjectItemKindPhysicalFolder);
        }

        /// <summary>
        /// Gets the folder or create.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="folderName">Name of the folder.</param>
        public static ProjectItem GetFolderOrCreate(this Project instance, string folderName)
        {
            TraceService.WriteLine("ProjectExtensions::GetFolderOrCreate folderName=" + folderName);

            ProjectItem projectItem = GetFolder(instance, folderName) ?? instance.ProjectItems.AddFolder(folderName);

            return projectItem;
        }

        /// <summary>
        /// Gets the solution folder projects.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static IEnumerable<Project> GetSolutionFolderProjects(this Project instance)
        {
            List<Project> list = new List<Project>();

            for (int i = 1; i <= instance.ProjectItems.Count; i++)
            {
                Project subProject = instance.ProjectItems.Item(i).SubProject;

                if (subProject == null)
                {
                    continue;
                }

                //// If this is another solution folder, do a recursive call, otherwise add
                if (subProject.Kind == VSConstants.VsProjectKindSolutionItems)
                {
                    list.AddRange(subProject.GetSolutionFolderProjects());
                }
                else
                {
                    list.Add(subProject);
                }
            }

            return list;
        }
    }
}
