// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.Linq;

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
        /// <param name="solutionAlreadyCreated">if set to <c>true</c> [solution already created].</param>
        /// <returns>
        /// The messages.
        /// </returns>
        public IEnumerable<string> AddProjects(
            IVisualStudioService visualStudioServiceInstance,
            string path, 
            IEnumerable<ProjectTemplateInfo> projectsInfos, 
            bool solutionAlreadyCreated)
        {
            IEnumerable<ProjectTemplateInfo> projectTemplateInfos = projectsInfos as ProjectTemplateInfo[] ?? projectsInfos.ToArray();

            string message = string.Format("ProjectsService::AddProjects project count={0} path={1}", projectTemplateInfos.Count(), path);
            
            TraceService.WriteLine(message);

            //// reset the messages.
            this.Messages = new List<string> { this.settingsService.FrameworkType + " framework selected." };

            this.visualStudioService = visualStudioServiceInstance;
            
            foreach (ProjectTemplateInfo projectInfo in projectTemplateInfos)
            {
                this.AddProject(path, projectInfo);
            }

            return this.Messages;
        }

        /// <summary>
        /// Adds the project.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="projectInfo">The project information.</param>
        internal void AddProject(
            string path,
            ProjectTemplateInfo projectInfo)
        {
            string message = string.Format("ProjectsService::AddProject project {0}", projectInfo.Name);

            TraceService.WriteLine(message);
            
            this.settingsService.ActiveProject = projectInfo.FriendlyName;

            this.TryToAddProject(path, projectInfo);

            //// add reference to core project

            IProjectService coreProjectService = this.visualStudioService.CoreProjectService;

            IProjectService projectService = this.visualStudioService.GetProjectServiceBySuffix(projectInfo.ProjectSuffix);

            if (coreProjectService != null && 
                projectService != null && 
                projectInfo.ReferenceCoreProject)
            {
                projectService.AddProjectReference(coreProjectService);
            }

            //// now add references to xamarin forms if required.

            IProjectService formsProjectService = this.visualStudioService.FormsProjectService;

            if (formsProjectService != null && 
                projectService != null && 
                projectInfo.ReferenceXamarinFormsProject)
            {
                projectService.AddProjectReference(formsProjectService);
            }
        }

        /// <summary>
        /// Adds the project.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="projectInfo">The project info.</param>
        internal void TryToAddProject(
            string path,
            ProjectTemplateInfo projectInfo)
        {
            TraceService.WriteLine("ProjectsService::TryToAddProject  project=" + projectInfo.Name);

            //// Project may actually already exist - if so just skip it!

            string projectPath = string.Format(@"{0}\{1}\", path, projectInfo.Name);

            if (this.FileSystem.Directory.Exists(projectPath) == false)
            {
                this.AddProject(projectInfo, projectPath);
            }

            else
            {
                TraceService.WriteError("Directory " + projectPath + " not empty");
            }
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

                this.Messages.Add(projectInfo.Name + " project successfully added.  (template=" + projectInfo.TemplateName + ")");
            }
            catch (Exception exception)
            {
                TraceService.WriteError("error adding project " + projectPath + " exception=" + exception.Message + " templateName=" + projectInfo.TemplateName);
            }
        }
    }
}
