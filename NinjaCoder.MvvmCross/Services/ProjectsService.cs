// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.Linq;
    using Constants;
    using Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the ProjectsService type.
    /// </summary>
    internal class ProjectsService : BaseService, IProjectsService
    {
        /// <summary>
        /// The file system.
        /// </summary>
        protected readonly IFileSystem FileSystem;

        /// <summary>
        /// The visual studio service.
        /// </summary>
        private IVisualStudioService visualStudioService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsService" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public ProjectsService(IFileSystem fileSystem)
        {
            this.FileSystem = fileSystem;
        }

        /// <summary>
        /// Adds the projects.
        /// </summary>
        /// <param name="visualStudioServiceInstance">The visual studio service.</param>
        /// <param name="path">The path.</param>
        /// <param name="projectsInfos">The projects infos.</param>
        /// <param name="referenceFirstProject">if set to <c>true</c> [reference first project].</param>
        /// <param name="includeLibFolderInProjects">if set to <c>true</c> [include lib folder in projects].</param>
        /// <returns>The messages.</returns>
        public IEnumerable<string> AddProjects(
            IVisualStudioService visualStudioServiceInstance,
            string path, 
            IEnumerable<ProjectTemplateInfo> projectsInfos, 
            bool referenceFirstProject, 
            bool includeLibFolderInProjects)
        {
            string message = string.Format("ProjectsService::AddProjects project count={0} path={1}", projectsInfos.Count(), path);
            
            TraceService.WriteLine(message);

            //// reset the messages.
            this.Messages = new List<string>();

            this.visualStudioService = visualStudioServiceInstance;

            IProjectService firstProjectService = null;
            
            foreach (ProjectTemplateInfo projectInfo in projectsInfos)
            {
                //// add in the nuget messages.
                if (firstProjectService == null && 
                    projectInfo.UseNuget)
                {
                    this.Messages.Add(NinjaMessages.MvxViaNuget);
                    this.Messages.Add(NinjaMessages.PmConsole);
                    this.Messages.Add(string.Empty);
                }

                firstProjectService = this.TryToAddProject(
                    path, 
                    referenceFirstProject, 
                    projectInfo, 
                    firstProjectService);
            }

            return this.Messages;
        }

        /// <summary>
        /// Adds the project.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="referenceFirstProject">if set to <c>true</c> [reference first project].</param>
        /// <param name="projectInfo">The project info.</param>
        /// <param name="firstProjectService">The first project service.</param>
        /// <returns>The project service.</returns>
        internal IProjectService TryToAddProject(
            string path,
            bool referenceFirstProject,
            ProjectTemplateInfo projectInfo,
            IProjectService firstProjectService)
        {
            TraceService.WriteLine("ProjectsService::TryToAddProject");

            //// Project may actually already exist - if so just skip it!

            string projectPath = string.Format(@"{0}\{1}\", path, projectInfo.Name);

            if (this.FileSystem.Directory.Exists(projectPath) == false)
            {
                this.AddProject(projectInfo, projectPath);
            }

            if (referenceFirstProject)
            {
                firstProjectService = this.ReferenceFirstProject(projectInfo, firstProjectService);
            }

            return firstProjectService;
        }

        /// <summary>
        /// Adds the project.
        /// </summary>
        /// <param name="projectInfo">The project info.</param>
        /// <param name="projectPath">The project path.</param>
        internal void AddProject(
            ProjectTemplateInfo projectInfo,
            string projectPath)
        {
            TraceService.WriteLine("ProjectsService::AddProject");

            try
            {
                string template = this.visualStudioService.SolutionService.GetProjectTemplate(projectInfo.TemplateName);

                this.visualStudioService.SolutionService.AddProjectToSolution(projectPath, template, projectInfo.Name);

                this.Messages.Add(projectInfo.Name + " project successfully added.");
            }
            catch (Exception exception)
            {
                TraceService.WriteError("error adding project " + projectPath + " exception=" + exception.Message);
            }
        }

        /// <summary>
        /// References the first project.
        /// </summary>
        /// <param name="projectInfo">The project info.</param>
        /// <param name="firstProjectService">The first project service.</param>
        /// <returns>The project.</returns>
        internal IProjectService ReferenceFirstProject(
            ProjectTemplateInfo projectInfo, 
            IProjectService firstProjectService)
        {
            TraceService.WriteLine("ProjectsService::ReferenceFirstProject");

            IProjectService projectService = this.visualStudioService.SolutionService.GetProjectService(projectInfo.Name);

            if (projectService != null)
            {
                if (firstProjectService == null)
                {
                    firstProjectService = projectService;
                }
                else
                {
                    projectService.AddProjectReference(firstProjectService);
                }
            }

            return firstProjectService;
        }

        /// <summary>
        /// Deletes the lib folder.
        /// </summary>
        /// <param name="projectInfo">The project info.</param>
        internal void DeleteLibFolder(ProjectTemplateInfo projectInfo)
        {
            TraceService.WriteLine("ProjectsService::DeleteLibFolder");

            IProjectService projectService = this.visualStudioService.SolutionService.GetProjectService((projectInfo.Name));

            if (projectService != null)
            {
                IProjectItemService projectItemService = projectService.RemoveFolder("Lib");

                //// remove the local files if we are going to use nuget.
                if (projectInfo.UseNuget)
                {
                    projectItemService.DeleteFolder(projectInfo.NonMvxAssemblies);
                }
            }
        }
    }
}
