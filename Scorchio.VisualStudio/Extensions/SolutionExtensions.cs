// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SolutionExtensions type.
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
    using Project = EnvDTE.Project;

    /// <summary>
    ///  Defines the SolutionExtensions type.
    /// </summary>
    public static class SolutionExtensions
    {
        /// <summary>
        /// Creates the empty solution.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="solutionPath">The solution path.</param>
        /// <param name="solutionName">Name of the solution.</param>
        public static void CreateEmptySolution(
            this Solution2 instance,
            string solutionPath,
            string solutionName)
        {
            TraceService.WriteLine("SolutionExtensions::CreateEmptySolution");

            if (Directory.Exists(solutionPath) == false)
            {
                Directory.CreateDirectory(solutionPath);
            }

            instance.Create(solutionPath, solutionName);
        }

        /// <summary>
        /// Gets the directory name.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The Directory path.</returns>
        public static string GetDirectoryName(this Solution2 instance)
        {
            TraceService.WriteLine("SolutionExtensions::GetDirectoryName");

            string directoryName = string.Empty;

            if (instance.FileName != string.Empty)
            {
                string path = Path.GetDirectoryName(instance.FullName);

                if (string.IsNullOrEmpty(path) == false)
                {
                    if (path.EndsWith(@"\") == false)
                    {
                        directoryName = path + @"\";
                    }
                }
            }
            
            if (directoryName == string.Empty)
            {
                Project project = instance.GetProjects().FirstOrDefault();

                if (project != null)
                {
                    string projectPath = Path.GetDirectoryName(project.FullName);

                    if (projectPath != null)
                    {
                        string path = projectPath.Replace(project.Name, string.Empty);

                        if (path.EndsWith(@"\") == false)
                        {
                            path += @"\";
                        }

                        directoryName = path;
                    }
                } 
            }

            TraceService.WriteLine("SolutionExtensions::GetDirectoryName directoryName=" + directoryName);
           
            return directoryName;
        }

        /// <summary>
        /// Gets the project template.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="templateName">ProjectName of the template.</param>
        /// <returns>The path of the template.</returns>
        public static string GetProjectTemplate(
            this Solution2 instance,
            string templateName)
        {
            TraceService.WriteLine("SolutionExtensions::GetProjectTemplate templateName=" + templateName);

            return instance.GetProjectTemplate(templateName, "CSharp");
        }

        /// <summary>
        /// Gets the item template.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="templateName">ProjectName of the template.</param>
        /// <returns>The path of the template.</returns>
        public static string GetProjectItemTemplate(
            this Solution2 instance,
            string templateName)
        {
            TraceService.WriteLine("SolutionExtensions::GetProjectItemTemplate templateName=" + templateName);

            string templatePath = instance.GetProjectItemTemplate(templateName, "CSharp");

            TraceService.WriteLine("TemplatePath=" + templatePath);

            return templatePath;
        }

        /// <summary>
        /// Adds the item.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="solutionFolder">The solution folder.</param>
        /// <param name="path">The path.</param>
        /// <returns>
        /// The project item.
        /// </returns>
        public static ProjectItem AddSolutionItem(
            this Solution2 instance,
            string solutionFolder,
            string path)
        {
            TraceService.WriteLine("SolutionExtensions::AddSolutionItem path=" + path);

            Project project = instance.GetProject(solutionFolder);

            if (project == null)
            {
                TraceService.WriteLine("project is null");

                project = instance.AddSolutionFolder(solutionFolder);
            }

            return project.ProjectItems.AddFromFile(path);
        }

        /// <summary>
        /// Adds the project to solution.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="path">The path.</param>
        /// <param name="templatePath">The template path.</param>
        /// <param name="projectName">ProjectName of the project.</param>
        public static void AddProjectToSolution(
            this Solution instance,
            string path,
            string templatePath,
            string projectName)
        {
            TraceService.WriteLine("SolutionExtensions::AddProjectToSolution path=" + path);

            instance.AddFromTemplate(templatePath, path, projectName);
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The projects.</returns>
        public static IEnumerable<Project> GetProjects(this Solution2 instance)
        {
            ////TraceService.WriteLine("SolutionExtensions::GetProjects");

            List<Project> projects = instance.Projects.Cast<Project>().ToList();

            List<Project> list = new List<Project>();

            List<Project>.Enumerator item = projects.GetEnumerator();

            while (item.MoveNext())
            {
                Project project = item.Current;

                if (project == null)
                {
                    continue;
                }

                if (project.Kind == VSConstants.VsProjectKindSolutionItems)
                {
                    list.AddRange(project.GetSolutionFolderProjects());
                }
                else
                {
                    list.Add(project);
                }
            }

            return list;
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="projectName">Name of the project.</param>
        /// <returns>The project.</returns>
        public static Project GetProject(
            this Solution2 instance,
            string projectName)
        {
            ////TraceService.WriteLine("SolutionExtensions::GetProject name=" + projectName);

            IEnumerable<Project> projects = instance.GetProjects();

            ////TraceService.WriteLine("return project name=" + projectName);

            return projects.FirstOrDefault(project => project.Name == projectName);
        }

        /// <summary>
        /// Adds the item template to project.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="templateInfos">The template infos.</param>
        /// <returns>The messages.</returns>
        public static IEnumerable<string> AddItemTemplateToProjects(
            this Solution2 instance,
            IEnumerable<ItemTemplateInfo> templateInfos) 
        {
            TraceService.WriteLine("SolutionExtensions::AddItemTemplateToProjects");

            List<string> messages = new List<string>();

            IEnumerable<Project> projects = instance.GetProjects();

            IEnumerable<Project> projectItems = projects as Project[] ?? projects.ToArray();

            foreach (ItemTemplateInfo info in templateInfos)
            {
                Project project = projectItems.FirstOrDefault(x => x.Name.EndsWith(info.ProjectSuffix));

                if (project != null)
                {
                    string context = project.Name + "  (template=" + info.TemplateName + ") fileName=" + info.FileName;

                    TraceService.WriteLine("AddItemTemplateToProjects"  + context);

                    try
                    {
                        if (project.AddItemToFolderFromTemplate(info.TemplateName, info.FileName))
                        {
                            messages.Add(info.FileName + " added to " + project.Name + " project (template=" + info.TemplateName + ")");
                        }
                    }
                    catch (Exception exception)
                    {
                        TraceService.WriteError(context + " exception=" + exception.Message);
                        messages.Add(context + " exception=" + exception.Message);
                    }
                }
                else
                {
                    TraceService.WriteError("AddItemTemplateToProjects cannot find project " + info.ProjectSuffix);

                    foreach (string projectName in projectItems
                        .Select(projectItem => projectItem.Name))
                    {
                        TraceService.WriteError("AddItemTemplateToProjects project " + projectName);
                        messages.Add(info.FileName + " added to " + projectName + " project (template=" + info.TemplateName + ")");
                    }
                }
            }

            return messages;
        }

        /// <summary>
        /// Removes the folder.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="folderName">Name of the folder.</param>
        public static void RemoveFolder(
            this Solution2 instance, 
            string folderName)
        {
            TraceService.WriteLine("SolutionExtensions::RemoveFolder folder=" + folderName);

            instance.Projects.Cast<Project>()
                .ToList()
                .ForEach(x => x.RemoveFolder(folderName));
        }

        /// <summary>
        /// Removes the comments.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void RemoveFileHeaders(this Solution2 instance)
        {
            TraceService.WriteLine("SolutionExtensions::RemoveFileHeaders");

            instance.Projects.Cast<Project>()
                .ToList()
                .ForEach(x => x.RemoveFileHeaders());
        }

        /// <summary>
        /// Removes the comments.
        /// </summary>
        /// <param name="instance">The instance.</param>
        public static void RemoveComments(this Solution2 instance)
        {
            TraceService.WriteLine("SolutionExtensions::RemoveComments");

            instance.Projects.Cast<Project>()
                .ToList()
                .ForEach(x => x.RemoveComments());
        }

        /// <summary>
        /// Gets the solution item path.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="file">The file.</param>
        /// <returns>
        /// The solution item path.
        /// </returns>
        public static string GetSolutionItemPath(
            this Solution2 instance,
            string file)
        {
            TraceService.WriteLine("SolutionExtensions::GetSolutionItemPath file=" + file);

            return instance.GetDirectoryName() + file;
        }

        /// <summary>
        /// Creates the file.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="file">The file.</param>
        /// <param name="contents">The contents.</param>
        public static void CreateFile(
            this Solution2 instance,
            string file,
            string contents)
        {
            TraceService.WriteLine("SolutionExtensions::CreateFile file=" + file);

            string path = instance.GetSolutionItemPath(file);
            instance.WriteFile(path, contents);
        }

        /// <summary>
        /// Writes the file.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="path">The path.</param>
        /// <param name="contents">The contents.</param>
        public static void WriteFile(
            this Solution2 instance,
            string path,
            string contents)
        {
            TraceService.WriteLine("SolutionExtensions::WriteFile path=" + path);

            using (StreamWriter sw = new StreamWriter(path, false))
            {
                sw.Write(contents);
                sw.Close();
            }
        }
    }
}
