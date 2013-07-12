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

    /// <summary>L
    ///  Defines the SolutionExtensions type.
    /// </summary>
    public static class SolutionExtensions
    {
        /// <summary>
        /// Gets the directory name.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The Directory path.</returns>
        public static string GetDirectoryName(this Solution2 instance)
        {
            string directoryName = string.Empty;

            TraceService.WriteLine("SolutionExtensions::GetDirectoryName");

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

            return instance.GetProjectItemTemplate(templateName, "CSharp");
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

            Project project = instance.GetProject(solutionFolder) ?? instance.AddSolutionFolder(solutionFolder);

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
            TraceService.WriteLine("SolutionExtensions::GetProjects");

            List<Project> projects = instance.Projects.Cast<Project>().ToList();

            List<Project> allProjects = new List<Project>(projects);

            foreach (Project project in projects)
            {
                IEnumerable<ProjectItem> projectItems = project.GetProjectItems();

                if (projectItems != null)
                {
                    foreach (ProjectItem projectItem in projectItems)
                    {
                        if (projectItem.Kind == VSConstants.VsProjectItemKindSolutionItems)
                        {
                            if (projectItem.SubProject != null)
                            {
                                allProjects.Add(projectItem.SubProject);
                            }
                        }
                    }
                }
            }

            return allProjects;
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
            IEnumerable<Project> projects = instance.GetProjects();

            return projects.FirstOrDefault(project => project.Name == projectName);
        }

        /// <summary>
        /// Adds the projects.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="path">The path.</param>
        /// <param name="projectsInfos">The projects infos.</param>
        /// <param name="referenceFirstProject">if set to <c>true</c> [reference first project].</param>
        /// <param name="includeLibFolderInProjects">if set to <c>true</c> [include lib folder in projects].</param>
        /// <returns> The messages.</returns>
        public static IEnumerable<string> AddProjects(
            this Solution2 instance,
            string path,
            IEnumerable<ProjectTemplateInfo> projectsInfos, 
            bool referenceFirstProject,
            bool includeLibFolderInProjects)
        {
            string message = string.Format(
                "SolutionExtensions::AddProjects project count={0} path={1}", projectsInfos.Count(), path);
            
            TraceService.WriteLine(message);

            Project firstProject = null;

            Solution solution = instance as Solution;

            List<string> messages = new List<string>();

            foreach (ProjectTemplateInfo projectInfo in projectsInfos)
            {
                try
                {
                    //// Project may actually already exist - if so just skip it!

                    string projectPath = string.Format(@"{0}\{1}\", path, projectInfo.Name);

                    if (Directory.Exists(projectPath) == false)
                    {
                        try
                        {
                            string template = instance.GetProjectTemplate(projectInfo.TemplateName);
                            solution.AddProjectToSolution(projectPath, template, projectInfo.Name);
                            
                            //// remove the lib folder if that's what the developer wants to happen.
                            if (includeLibFolderInProjects == false)
                            {
                                Project project = instance.GetProject(projectInfo.Name);

                                if (project != null)
                                {
                                    project.RemoveFolder("Lib");
                                }
                            }

                            messages.Add(projectInfo.Name + " project successfully added.");
                        }
                        catch (Exception exception)
                        {
                            string exceptionMessage = string.Format(
                                "Unsupported project {0} not added to the solution.", projectInfo.Name);

                            TraceService.WriteError(exceptionMessage + " exception=" + exception.Message);
                            
                            messages.Add(exceptionMessage);
                        }
                    }

                    if (referenceFirstProject)
                    {
                        Project project = instance.GetProject(projectInfo.Name);

                        if (project != null)
                        {
                            if (firstProject == null)
                            {
                                firstProject = project;
                            }
                            else
                            {
                                try
                                {
                                    project.AddProjectReference(firstProject);
                                }
                                catch (Exception exception)
                                {
                                    string exceptionMessage = "SolutionExtensions::AddProjects Error=" + exception.Message;

                                    TraceService.WriteError(exceptionMessage);
                                    messages.Add(exceptionMessage);
                                }
                            }
                        }
                    }
                }
                catch (FileNotFoundException exception)
                {
                    //// if template not found just miss it out for now.
                    message = string.Format(
                        "Template not found for {0} Error {1}", projectInfo.TemplateName, exception.Message);

                    TraceService.WriteError(message);
                    messages.Add(message);
                }
             }

            return messages;
        }

        /// <summary>
        /// Adds the item template to project.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="templateInfos">The template infos.</param>
        /// <param name="createFolder">if set to <c>true</c> [create folder].</param>
        /// <returns> The messages. </returns>
        public static List<string> AddItemTemplateToProjects(
             this Solution2 instance,
            IEnumerable<ItemTemplateInfo> templateInfos,
            bool createFolder) 
        {
            List<string> messages = new List<string>();

            IEnumerable<Project> projects = instance.GetProjects();

            TraceService.WriteError("AddItemTemplateToProjects project count=" + projects.Count());

            foreach (ItemTemplateInfo info in templateInfos)
            {
                Project project = projects.FirstOrDefault(x => x.Name.EndsWith(info.ProjectSuffix));

                if (project != null)
                {
                    project.AddToFolderFromTemplate(info.FolderName, info.TemplateName, info.FileName, createFolder);
                    messages.Add(info.FolderName + @"\" + info.FileName + ".cs added to " + project.Name + " project.");
                }
                else
                {
                    TraceService.WriteError("AddItemTemplateToProjects cannot find project " + info.ProjectSuffix);

                    foreach (Project projectItem in projects)
                    {
                        string projectName = projectItem.Name;

                        TraceService.WriteError("AddItemTemplateToProjects project " + projectName);
                        messages.Add(info.FileName + " added to " + projectName + " project.");
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
            List<Project> projects = instance.Projects.Cast<Project>().ToList();

            foreach (Project project in projects)
            {
                project.RemoveFolder(folderName);
            }
        }
    }
}
