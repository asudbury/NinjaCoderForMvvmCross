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

    using NinjaCoder.MvvmCross.Infrastructure.Services;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the ProjectsService type.
    /// </summary>
    internal class ProjectsService : BaseService, IProjectsService
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

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
        /// <param name="settingsService">The settings service.</param>
        /// <param name="fileSystem">The file system.</param>
        public ProjectsService(
            ISettingsService settingsService,
            IFileSystem fileSystem)
        {
            this.settingsService = settingsService;
            this.FileSystem = fileSystem;
        }

        /// <summary>
        /// Adds the projects.
        /// </summary>
        /// <param name="visualStudioServiceInstance">The visual studio service.</param>
        /// <param name="path">The path.</param>
        /// <param name="projectsInfos">The projects infos.</param>
        /// <param name="referenceFirstProject">if set to <c>true</c> [reference first project].</param>
        /// <param name="solutionAlreadyCreated">if set to <c>true</c> [solution already created].</param>
        /// <returns>
        /// The messages.
        /// </returns>
        public IEnumerable<string> AddProjects(
            IVisualStudioService visualStudioServiceInstance,
            string path, 
            IEnumerable<ProjectTemplateInfo> projectsInfos, 
            bool referenceFirstProject,
            bool solutionAlreadyCreated)
        {
            IEnumerable<ProjectTemplateInfo> projectTemplateInfos = projectsInfos as ProjectTemplateInfo[] ?? projectsInfos.ToArray();

            string message = string.Format("ProjectsService::AddProjects project count={0} path={1}", projectTemplateInfos.Count(), path);
            
            TraceService.WriteLine(message);

            //// reset the messages.
            this.Messages = new List<string>();

            this.visualStudioService = visualStudioServiceInstance;

            IProjectService firstProjectService = null;

            if (solutionAlreadyCreated)
            {
                firstProjectService = visualStudioServiceInstance.CoreProjectService;
            }

            foreach (ProjectTemplateInfo projectInfo in projectTemplateInfos)
            {
                //// add in the nuget messages.
                if (firstProjectService == null && 
                    projectInfo.UseNuget)
                {
                    this.Messages.Add(NinjaMessages.MvxViaNuget);
                    this.Messages.Add(NinjaMessages.PmConsole);
                    this.Messages.Add(string.Empty);
                }

                this.settingsService.ActiveProject = projectInfo.FriendlyName;

                IProjectService projectService = this.TryToAddProject(
                    path, 
                    referenceFirstProject, 
                    projectInfo, 
                    firstProjectService);

                if (solutionAlreadyCreated == false)
                {
                    firstProjectService = projectService;
                }
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
            TraceService.WriteLine("ProjectsService::TryToAddProject  project=" + projectInfo.Name);

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
            TraceService.WriteLine("ProjectsService::AddProject project=" + projectInfo.Name + " templateName=" + projectInfo.TemplateName);

            try
            {
                string template = this.visualStudioService.SolutionService.GetProjectTemplate(projectInfo.TemplateName);

                TraceService.WriteLine("Template=" + template);

                this.visualStudioService.SolutionService.AddProjectToSolution(projectPath, template, projectInfo.Name);

                this.Messages.Add(projectInfo.Name + " project successfully added.");
            }
            catch (Exception exception)
            {
                TraceService.WriteError("error adding project " + projectPath + " exception=" + exception.Message + " templateName=" + projectInfo.TemplateName);
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
            TraceService.WriteLine("ProjectsService::ReferenceFirstProject project=" + projectInfo.Name);

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
            TraceService.WriteLine("ProjectsService::DeleteLibFolder project=" + projectInfo.Name);

            IProjectService projectService = this.visualStudioService.SolutionService.GetProjectService(projectInfo.Name);

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
