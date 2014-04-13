// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectsViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO.Abstractions;
    using System.Linq;
    using System.Windows.Input;

    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.Infrastructure.Extensions;
    using Scorchio.Infrastructure.Services;
    using Scorchio.Infrastructure.Wpf;
    using Scorchio.Infrastructure.Wpf.ViewModels;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the ProjectsViewModel type.
    /// </summary>
    internal class ProjectsViewModel : BaseViewModel
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The file system.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// The message box service.
        /// </summary>
        private readonly IMessageBoxService messageBoxService;

        /// <summary>
        /// The folder browser dialog service.
        /// </summary>
        private readonly IFolderBrowserDialogService folderBrowserDialogService;

        /// <summary>
        /// The projects.
        /// </summary>
        private readonly ObservableCollection<SelectableItemViewModel<ProjectTemplateInfo>> projects;

        /// <summary>
        /// The solution already created.
        /// </summary>
        private readonly bool solutionAlreadyCreated;

        /// <summary>
        /// The path.
        /// </summary>
        private string path;

        /// <summary>
        /// The project.
        /// </summary>
        private string project;

        /// <summary>
        /// The use nuget.
        /// </summary>
        private bool useNuget;

        /// <summary>
        /// The project is focused.
        /// </summary>
        private bool projectIsFocused;

        /// <summary>
        /// The view types.
        /// </summary>
        private readonly IEnumerable<string> viewTypes;

        /// <summary>
        /// The selected view type.
        /// </summary>
        private string selectedViewType;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProjectsViewModel" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="projectFactory">The project factory.</param>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="folderBrowserDialogService">The folder browser dialog service.</param>
        /// <param name="viewModelAndViewsFactory">The view model and views factory.</param>
        public ProjectsViewModel(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            ////ISolutionService solutionService,
            IProjectFactory projectFactory,
            IFileSystem fileSystem,
            IMessageBoxService messageBoxService,
            IFolderBrowserDialogService folderBrowserDialogService,
            IViewModelAndViewsFactory viewModelAndViewsFactory)
            : base(settingsService)
        {
            TraceService.WriteLine("ProjectsViewModel::Constructor Start");

            this.settingsService = settingsService;
            this.fileSystem = fileSystem;
            this.messageBoxService = messageBoxService;
            this.folderBrowserDialogService = folderBrowserDialogService;

            this.projects = new ObservableCollection<SelectableItemViewModel<ProjectTemplateInfo>>();

            IEnumerable<ProjectTemplateInfo> projectTemplateInfos = projectFactory.GetAllowedProjects();

            foreach (SelectableItemViewModel<ProjectTemplateInfo> viewModel in projectTemplateInfos
                .Select(projectTemplateInfo => new SelectableItemViewModel<ProjectTemplateInfo>(projectTemplateInfo, projectTemplateInfo.PreSelected)))
            {
                this.projects.Add(viewModel);
            }

            //// set the defaults!
            this.Project = visualStudioService.GetDefaultProjectName();

            string defaultPath = this.settingsService.DefaultProjectsPath;

            //// if we are already in the solution disable project name and path.
            this.solutionAlreadyCreated = this.Project.Length > 0;

            if (this.solutionAlreadyCreated)
            {
                this.Path = visualStudioService.SolutionService.GetParentDirectoryName();
            }

            else
            {
                this.Path = string.IsNullOrEmpty(defaultPath) == false
                                ? defaultPath
                                : visualStudioService.DTEService.GetDefaultProjectsLocation();
            }

            this.useNuget = this.settingsService.UseNugetForProjectTemplates;

            this.viewTypes = viewModelAndViewsFactory.GetAvailableViewTypes();
            this.selectedViewType = this.settingsService.SelectedViewType;

            TraceService.WriteLine("ProjectsViewModel::Constructor End");
        }

        /// <summary>
        /// Gets or sets the Path.
        /// </summary>
        public string Path
        {
            get
            {
                if (string.IsNullOrEmpty(this.path) == false &&
                    this.path.EndsWith(@"\") == false)
                {
                    this.path = string.Format(@"{0}\", this.path);
                }

                return this.path;
            }

            set
            {
                this.SetProperty(ref this.path, value);
                this.OnNotify("IsOKCommandEnabled");
            }
        }

        /// <summary>
        /// Gets or sets the Project.
        /// </summary>
        public string Project
        {
            get
            {
                return this.project.CaptialiseFirstCharacter();
            }

            set
            {
                this.SetProperty(ref this.project, value);
                this.OnNotify("IsOKCommandEnabled");
            }
        }

        /// <summary>
        /// Gets a value indicating whether [solution already created].
        /// </summary>
        public bool SolutionAlreadyCreated
        {
            get { return this.solutionAlreadyCreated; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether to use nuget.
        /// </summary>
        public bool UseNuget
        { 
            get { return this.useNuget; }
            set { this.SetProperty(ref this.useNuget, value); }
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        public ObservableCollection<SelectableItemViewModel<ProjectTemplateInfo>> Projects
        {
            get { return this.projects; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [project is focused].
        /// </summary>
        public bool ProjectIsFocused
        {
            get { return this.projectIsFocused; }
            set { this.SetProperty(ref this.projectIsFocused, value);}
        }

        /// <summary>
        /// Gets or sets the type of the selected view.
        /// </summary>
        public string SelectedViewType
        {
            get { return this.selectedViewType; }
            set { this.SetProperty(ref this.selectedViewType, value); }
        }

        /// <summary>
        /// Gets or sets the type of the view.
        /// </summary>
        public IEnumerable<string> ViewTypes
        {
            get { return this.viewTypes; }
        }

        /// <summary>
        /// Gets a value indicating whether [is ok command enabled].
        /// </summary>
        public bool IsOKCommandEnabled
        {
            get
            {
                return this.Project.Length > 0 && 
                       this.Path.Length > 2;
            }
        }

        /// <summary>
        /// Gets the browser folders command.
        /// </summary>
        public ICommand BrowserFoldersCommand
        {
            get { return new RelayCommand(this.BrowserFolders); }
        }

        /// <summary>
        /// Browsers the folders.
        /// </summary>
        public void BrowserFolders()
        {
            bool? returnValue = this.folderBrowserDialogService.ShowDialog();

            if (returnValue.HasValue && 
                returnValue == true)
            {
                this.Path = this.folderBrowserDialogService.SelectedPath;
                this.OnNotify("Path");
            }
        }

        /// <summary>
        /// Gets the solution path.
        /// </summary>
        /// <returns>The solution path.</returns>
        public string GetSolutionPath()
        {
            return string.Format(@"{0}{1}", this.Path, this.Project);
        }

        /// <summary>
        /// Gets the formatted required templates.
        /// </summary>
        /// <returns>Formatted required templates</returns>
        public IEnumerable<ProjectTemplateInfo> GetFormattedRequiredTemplates()
        {
            List<ProjectTemplateInfo> templateInfos = new List<ProjectTemplateInfo>();

            foreach (SelectableItemViewModel<ProjectTemplateInfo> templateInfo in this.projects
                .Where(x => x.IsSelected))
            {
                this.GetRequiredTemplate(templateInfo.Item);
                templateInfos.Add(templateInfo.Item);
            }

            return templateInfos;
        }

        /// <summary>
        /// Called when ok button pressed.
        /// </summary>
        public override void OnOk()
        {
            if (this.DoesDirectoryAlreadyExist() && 
                this.solutionAlreadyCreated == false)
            {
                this.messageBoxService.Show(
                    Constants.Settings.DirectoryExists, 
                    Constants.Settings.ApplicationName,
                    this.settingsService.BetaTesting,
                    this.CurrentTheme,
                    this.settingsService.ThemeColor);

                this.ProjectIsFocused = true;
            }
            else
            {
                this.settingsService.UseNugetForProjectTemplates = this.useNuget;

                if (this.solutionAlreadyCreated == false)
                {
                    this.settingsService.DefaultProjectsPath = this.Path;
                }

                this.settingsService.SelectedViewType = this.selectedViewType ?? "SampleData";
                this.settingsService.SelectedViewPrefix = "First";

                base.OnOk();
            }
        }

        /// <summary>
        /// Gets the required template.
        /// </summary>
        /// <param name="projectInfo">The project info.</param>
        internal void GetRequiredTemplate(ProjectTemplateInfo projectInfo)
        {
            projectInfo.Name = this.Project + projectInfo.ProjectSuffix;
            projectInfo.UseNuget = this.UseNuget;

            if (projectInfo.NugetCommands != null)
            {
                List<string> newCommands =
                    projectInfo.NugetCommands.Select(
                        nugetCommand => string.Format("{0} {1}", nugetCommand, projectInfo.Name)).ToList();

                projectInfo.NugetCommands = newCommands;
            }
        }

        /// <summary>
        /// Does the directory already exist.
        /// </summary>
        /// <returns>True or false.</returns>
        internal bool DoesDirectoryAlreadyExist()
        {
            string solutionsPath = this.GetSolutionPath();

            return this.fileSystem.Directory.Exists(solutionsPath);
        }
    }
}
