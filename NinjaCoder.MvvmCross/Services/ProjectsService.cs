// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Entities;
    using Extensions;
    using Interfaces;
    using Scorchio.Infrastructure.Extensions;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.Linq;
    using Translators.Interfaces;

    /// <summary>
    ///  Defines the ProjectsService type.
    /// </summary>
    public class ProjectsService : BaseService, IProjectsService
    {
        /// <summary>
        /// The text templating service.
        /// </summary>
        private readonly ITextTemplatingService textTemplatingService;

        /// <summary>
        /// The tokens translator.
        /// </summary>
        private readonly ITokensTranslator tokensTranslator;

        /// <summary>
        /// The visual studio service.
        /// </summary>
        private IVisualStudioService visualStudioService;

        /// <summary>
        /// The file system.
        /// </summary>
        protected readonly IFileSystem FileSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsService" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="textTemplatingService">The text templating service.</param>
        /// <param name="tokensTranslator">The tokens translator.</param>
        public ProjectsService(
            ISettingsService settingsService,
            IFileSystem fileSystem,
            ITextTemplatingService textTemplatingService,
            ITokensTranslator tokensTranslator)
            :base(settingsService)
        {
            TraceService.WriteLine("ProjectsService::constructor");

            this.FileSystem = fileSystem;
            this.textTemplatingService = textTemplatingService;
            this.tokensTranslator = tokensTranslator;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is new solution.
        /// </summary>
        public bool IsNewSolution { get; set; }

        /// <summary>
        /// Adds the projects.
        /// </summary>
        /// <param name="visualStudioServiceInstance">The visual studio service.</param>
        /// <param name="path">The path.</param>
        /// <param name="projectsInfos">The projects infos.</param>
        /// <returns>
        /// The messages.
        /// </returns>
        public IEnumerable<string> AddProjects(
            IVisualStudioService visualStudioServiceInstance,
            string path, 
            IEnumerable<ProjectTemplateInfo> projectsInfos)
        {
            TraceService.WriteLine("ProjectsService::AddProjects");

            //// reset the messages.
            this.Messages = new List<string>();

            IEnumerable<ProjectTemplateInfo> projectTemplateInfos = projectsInfos as ProjectTemplateInfo[] ?? projectsInfos.ToArray();

            string message = $"ProjectsService::AddProjects project count={projectTemplateInfos.Count()} path={path}";
            
            TraceService.WriteLine(message);

            this.visualStudioService = visualStudioServiceInstance;
            
            foreach (ProjectTemplateInfo projectInfo in projectTemplateInfos)
            {
                this.AddProject(path, projectInfo);
            }

            this.CreateMessages();

            return this.Messages;
        }

        /// <summary>
        /// Sets the start up project.
        /// </summary>
        public void SetStartUpProject()
        {
            if (this.IsNewSolution)
            {
                return;
            }

            string startUpProject = this.SettingsService.StartUpProject;

            TraceService.WriteLine("StartUpProject=" + startUpProject);

            if (startUpProject != string.Empty)
            {
                IProjectService projectService = this.visualStudioService.GetProjectServiceBySuffix(startUpProject);

                if (projectService != null)
                {
                    this.visualStudioService.SolutionService.SetStartUpProject(projectService.Name);
                }
                else
                {
                    TraceService.WriteError("Cannot find StartUpProject=" + startUpProject);
                }
            }
        }

        /// <summary>
        /// Creates the messages.
        /// </summary>
        internal void CreateMessages()
        {
            if (this.IsNewSolution)
            {
                return;
            }

            //// reset the messages.
            this.Messages.AddRange(new List<string>
            {
                string.Empty,
                "----------------------------------------------------------------------------------------------------",
                "Options",
                "----------------------------------------------------------------------------------------------------",
                this.SettingsService.FrameworkType.GetDescription() + " framework selected.", 
                this.SettingsService.TestingFramework + " testing framework selected.", 
                this.SettingsService.MockingFramework + " mocking framework selected.",
            });

            if (this.SettingsService.BetaTesting)
            {
                this.Messages.Add("BETA Testing is set to on!");
            }

            if (this.SettingsService.UseLocalUris)
            {
                this.Messages.Add("Use Local Config files selected.");
            }

            if (this.SettingsService.UseLocalTextTemplates)
            {
                this.Messages.Add("Use Local Text Templates selected.");
            }

            if (this.SettingsService.UseLocalNuget)
            {
                this.Messages.Add("Use Local Nuget selected.");
            }

            if (this.SettingsService.StartUpProject != string.Empty)
            {
                this.Messages.Add("StartUp Project set to " + this.SettingsService.StartUpProject);
            }

            if (this.SettingsService.FrameworkType.IsMvvmCrossSolutionType() &&
                this.SettingsService.AddiOSProject)
            {
                this.Messages.Add("iOS View Type " + this.SettingsService.SelectedMvvmCrossiOSViewType + " selected.");
            }

            if (this.SettingsService.CreatePlatformTestProjects)
            {
                this.Messages.Add("Create Test Projects selected.");
            }

            if (this.SettingsService.UseXamarinTestCloud)
            {
                this.Messages.Add("Use Xamarin Test Cloud selected.");
            }

            if (this.SettingsService.UseXamarinInsights)
            {
                this.Messages.Add("Use Xamarin Insights selected.");
            }

            if (this.SettingsService.FrameworkType.IsXamarinFormsSolutionType())
            {
                string type = this.SettingsService.UseXamarinFormsXamlCompilation ? 
                    XamarinFormsCompileOption.Compile.GetDescription() : 
                    XamarinFormsCompileOption.Skip.GetDescription();

                this.Messages.Add("Xamarin Forms Compile Option=" + type);
            }

            if (this.SettingsService.UsePreReleaseMvvmCrossNugetPackages &&
                this.SettingsService.FrameworkType.IsMvvmCrossSolutionType())
            {
                this.Messages.Add("Pre Release MvvmCross Nuget Packages selected.");
            }

            if (this.SettingsService.UsePreReleaseMvvmCrossNugetPackages &&
               this.SettingsService.FrameworkType.IsXamarinFormsSolutionType())
            {
                this.Messages.Add("Pre Release Xamarin Forms Nuget Packages selected.");
            }

            if (this.SettingsService.UsePreReleaseNinjaNugetPackages)
            {
                this.Messages.Add("Pre Release Ninja Nuget Packages selected.");
            }

            this.Messages.Add(string.Empty);
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
            string message = $"ProjectsService::AddProjectIf project {projectInfo.Name}";

            TraceService.WriteLine(message);
            
            this.SettingsService.ActiveProject = projectInfo.FriendlyName;

            this.TryToAddProject(path, projectInfo);

            //// add reference to core project

            IProjectService coreProjectService = this.visualStudioService.CoreProjectService;

            IProjectService projectService = this.visualStudioService.GetProjectServiceBySuffix(projectInfo.ProjectSuffix);

            if (coreProjectService != null && 
                projectService != null && 
                projectInfo.ReferenceCoreProject)
            {
                if (projectService.Name != coreProjectService.Name)
                {
                    projectService.AddProjectReference(coreProjectService);
                }
                else
                {
                    TraceService.WriteError("Attemped to reference project to its self project=" + projectService.Name);
                }
            }

            //// now add references to xamarin forms if required.

            if (projectInfo.ReferenceXamarinFormsProject)
            {
                IProjectService formsProjectService = this.visualStudioService.XamarinFormsProjectService;

                if (formsProjectService != null && 
                    projectService != null)
                {
                    if (projectService.Name != formsProjectService.Name)
                    {
                        projectService.AddProjectReference(formsProjectService);
                    }
                    else
                    {
                        TraceService.WriteError("Attemped to reference project to its self project=" + projectService.Name);
                    }
                }
            }

            //// now add reference to the plaform project from the test platform project

            if (projectInfo.ReferencePlatformProject)
            {
                //// TODO : tidy this up a little bit!
                IProjectService platformProjectService = this.visualStudioService.GetProjectServiceBySuffix(
                    projectInfo.ProjectSuffix.Replace(".Tests", string.Empty));

                if (platformProjectService != null && 
                    projectService != null)
                {
                    //// for some reason cant add project reference to windows store project !!
                    //// as no one is developing mvvmcross windows store apps dont worry about it :-)
                    try
                    {
                        projectService.AddProjectReference(platformProjectService);
                    }
                    catch (Exception exception)
                    {
                        TraceService.WriteError("Unable to add project reference to " + platformProjectService.Name + " exception=" + exception.Message);
                    }
                }
            }

            if (projectInfo.ItemTemplates != null && 
                projectInfo.ItemTemplates.Any() &&
                projectService != null)
            {
                foreach (TextTemplateInfo textTemplateInfo in projectInfo.ItemTemplates)
                {
                    textTemplateInfo.Tokens = this.tokensTranslator.Translate(projectService);
                }

                this.textTemplatingService.AddTextTemplates(
                        "Adding items to project " + projectService.Name,
                        projectInfo.ItemTemplates);
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

            string projectPath = $@"{path}\{projectInfo.Name}\";

            if (this.FileSystem.Directory.Exists(projectPath) == false)
            {
                TraceService.WriteDebugLine(projectInfo.Name + " " + projectPath + " added to the solution.");
                
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
            TraceService.WriteLine("ProjectsService::AddProject projectPath=" + projectPath + "templateName = " + projectInfo.TemplateName);

            try
            {
                string template = this.visualStudioService.SolutionService.GetProjectTemplate(projectInfo.TemplateName);

                TraceService.WriteLine("Template=" + template);

                //// add to TestProjects subfolder if applicable,
                if (this.SettingsService.CreateTestProjectsSolutionFolder && projectInfo.Name.EndsWith("Tests"))
                { 
                    this.visualStudioService.SolutionService.AddProjectToSubFolder(
                        this.SettingsService.TestProjectsSolutionFolderName, 
                        projectPath, 
                        template, 
                        projectInfo.Name);
                }
                else
                {
                    this.visualStudioService.SolutionService.AddProjectToSolution(projectPath, template, projectInfo.Name);
                }

                this.Messages.Add(projectInfo.Name + " project successfully added. (template " + projectInfo.TemplateName + ")");
            }
            catch (Exception exception)
            {
                TraceService.WriteError("error adding project exception=" + exception.Message);
                TraceService.WriteError("projectPath=" + projectPath);
                TraceService.WriteError("projectInfo.TemplateName=" + projectInfo.TemplateName);
               
                this.Messages.Add("ERROR " + projectInfo.Name + " not added. exception " + exception.Message + " (template " + projectInfo.TemplateName + ")");
            }
        }
    }
}
