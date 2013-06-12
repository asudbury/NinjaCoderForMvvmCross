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

    using EnvDTE;
    using EnvDTE80;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;

    using Project = EnvDTE.Project;

    /// <summary>
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
            if (instance.FileName != string.Empty)
            {
                return Path.GetDirectoryName(instance.FullName);
            }

            return string.Empty;
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
            string projectPath = string.Format("{0}{1}", path, projectName);

            instance.AddFromTemplate(templatePath, projectPath, projectName);
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <returns>The projects.</returns>
        public static IEnumerable<Project> GetProjects(this Solution2 instance)
        {
            List<Project> projects = instance.Projects.Cast<Project>().ToList();

            List<Project> allProjects = new List<Project>(projects);

            foreach (Project project in projects)
            {
                IEnumerable<ProjectItem> projectItems = project.GetProjectItems();

                if (projectItems != null)
                {
                    foreach (ProjectItem projectItem in projectItems)
                    {
                        ////if (projectItem.Kind == ProjectKinds.vsProjectKindSolutionFolder)
                        if (projectItem.Kind == "{66A26722-8FB5-11D2-AA7E-00C04F688DDE}")
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
        /// <returns>The messages.</returns>
        public static IEnumerable<string> AddProjects(
            this Solution2 instance,
            string path,
            IEnumerable<ProjectTemplateInfo> projectsInfos, 
            bool referenceFirstProject)
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
        public static void RemoveFolders(
            this Solution2 instance, 
            string folderName)
        {
            List<Project> projects = instance.Projects.Cast<Project>().ToList();

            foreach (Project project in projects)
            {
                IEnumerable<ProjectItem> projectItems = project.GetProjectItems();

                if (projectItems != null)
                {
                    foreach (ProjectItem projectItem in projectItems)
                    {
                        ////if (projectItem.Kind == ProjectKinds.vsProjectKindPhysicalFolder)
                        if (projectItem.Kind == "{6BB5F8EF-4483-11D3-8BCF-00C04F8EC28C}")
                        {
                            if (projectItem.Name.ToLower() == folderName.ToLower())
                            {
                                projectItem.Remove();
                                break;
                            }
                        }
                    }
                }
            }
        }
    }
}
