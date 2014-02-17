// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the VisualStudioService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using Constants;
    using EnvDTE;
    using EnvDTE80;
    using Interfaces;

    using NinjaCoder.MvvmCross.Infrastructure.Services;

    using Scorchio.VisualStudio;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    /// Defines the VisualStudioService type.
    /// </summary>
    internal class VisualStudioService : BaseService, IVisualStudioService
    {
        /// <summary>
        /// The dte2.
        /// </summary>
        private DTE2 dte2;

        /// <summary>
        /// Initializes a new instance of the <see cref="VisualStudioService" /> class.
        /// </summary>
        public VisualStudioService()
        {
            TraceService.WriteLine("VisualStudioService::Constructor");
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        public IEnumerable<Project> Projects
        {
            get { return this.Solution.GetProjects(); }
        }

        /// <summary>
        /// Gets or sets the visual studio.
        /// </summary>
        public DTE2 DTE2
        {
            get
            {
                if (this.dte2 == null)
                {
                    TraceService.WriteHeader("VisualStudioService Activating Visual Studio Link");

                    SettingsService settingsService = new SettingsService();

                    string objectName = ScorchioConstants.VisualStudio + "." + settingsService.VisualStudioVersion;

                    this.DTE2 = VSActivatorService.Activate(objectName);
                }

                return this.dte2;
            }

            set
            {
                this.dte2 = value;
            }
        }

        /// <summary>
        /// Gets the DTE service.
        /// </summary>
        public IDTEService DTEService
        {
            get { return new DTEService(this.DTE2); }
        }

        /// <summary>
        /// Gets the solution service.
        /// </summary>
        public ISolutionService SolutionService
        {
            get { return new SolutionService(this.DTE2.Solution); }
        }

        /// <summary>
        /// Gets the solution.
        /// </summary>
        public Solution2 Solution 
        {
            get { return this.DTE2.Solution as Solution2; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is MVVM cross solution.
        /// </summary>
        public bool IsMvvmCrossSolution 
        {
            get { return this.CoreProject != null; }
        }

        /// <summary>
        /// Gets the core project service.
        /// </summary>
        public IProjectService CoreProjectService 
        { 
            get
            {
                Project project = this.CoreProject;
                return project != null ? new ProjectService(project) : null;
            }
        }

        /// <summary>
        /// Gets the core tests project service.
        /// </summary>
        public IProjectService CoreTestsProjectService
        {
            get
            {
                Project project = this.CoreTestsProject;
                return project != null ? new ProjectService(project) : null;
            }
        }

        /// <summary>
        /// Gets the droid project service.
        /// </summary>
        public IProjectService DroidProjectService
        {
            get
            {
                Project project = this.DroidProject;
                return project != null ? new ProjectService(project) : null;
            }
        }

        /// <summary>
        /// Gets the iOS project service.
        /// </summary>
        public IProjectService iOSProjectService 
        {
            get
            {
                Project project = this.iOSProject;
                return project != null ? new ProjectService(project) : null;
            }
        }

        /// <summary>
        /// Gets the windows phone project service.
        /// </summary>
        public IProjectService WindowsPhoneProjectService 
        {
            get
            {
                Project project = this.WindowsPhoneProject;
                return project != null ? new ProjectService(project) : null;
            }
        }

        /// <summary>
        /// Gets the windows store project service.
        /// </summary>
        public IProjectService WindowsStoreProjectService 
        {
            get
            {
                Project project = this.WindowsStoreProject;
                return project != null ? new ProjectService(project) : null;
            }
        }

        /// <summary>
        /// Gets the WPF project service.
        /// </summary>
        public IProjectService WpfProjectService
        {
            get
            {
                Project project = this.WpfProject;
                return project != null ? new ProjectService(project) : null;
            }
        }

        /// <summary>
        /// Gets the core project.
        /// </summary>
        internal Project CoreProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.Core)); }
        }

        /// <summary>
        /// Gets the core test project.
        /// </summary>
        internal Project CoreTestsProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.CoreTests)); }
        }

        /// <summary>
        /// Gets the droid project.
        /// </summary>
        internal Project DroidProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.Droid)); }
        }

        /// <summary>
        /// Gets the iOS project.
        /// </summary>
        internal Project iOSProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.iOS)); }
        }

        /// <summary>
        /// Gets the windows phone project.
        /// </summary>
        internal Project WindowsPhoneProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.WindowsPhone)); }
        }

        /// <summary>
        /// Gets the windows store project.
        /// </summary>
        internal Project WindowsStoreProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.WindowsStore)); }
        }

        /// <summary>
        /// Gets the WPF project.
        /// </summary>
        internal Project WpfProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.WindowsWpf)); }
        }

 
        /// <summary>
        /// Gets the allowed converters.
        /// </summary>
        /// <param name="templatesPath">The templates path.</param>
        /// <returns>The allowed converters.</returns>
        public IEnumerable<ItemTemplateInfo> GetAllowedConverters(string templatesPath)
        {
            List<ItemTemplateInfo> templateInfos = this.GetFolderTemplateInfos(templatesPath);

            //// now check to see if currently in the project!!

            IProjectService coreProjectService = this.CoreProjectService;

            if (coreProjectService != null)
            {
                IEnumerable<string> folderItems = coreProjectService.GetFolderItems("Converters", false);

                foreach (ItemTemplateInfo item in folderItems
                    .Select(folderItem => templateInfos
                    .FirstOrDefault(x => x.FriendlyName.Contains(folderItem)))
                    .Where(item => item != null))
                {
                    //// remove if already in the project!
                    templateInfos.Remove(item);
                }
            }

            return templateInfos;
        }

        /// <summary>
        /// Gets the folder template infos.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The template Infos.</returns>
        public List<ItemTemplateInfo> GetFolderTemplateInfos(string path)
        {
            List<ItemTemplateInfo> itemTemplateInfos = new List<ItemTemplateInfo>();

            string[] paths = Directory.GetFiles(path);

            foreach (string filePath in paths)
            {
                FileInfo fileInfo = new FileInfo(filePath);

                string name = Path.GetFileNameWithoutExtension(fileInfo.Name);

                itemTemplateInfos.Add(new ItemTemplateInfo
                {
                    FriendlyName = name.Replace("MvvmCross.", string.Empty),
                    FileName = fileInfo.Name,
                });
            }

            return itemTemplateInfos;
        }

        /// <summary>
        /// Writes the status bar message.
        /// </summary>
        /// <param name="message">The message.</param>
        public void WriteStatusBarMessage(string message)
        {
            this.DTE2.WriteStatusBarMessage(message);
        }

        /// <summary>
        /// Tidy the code up.
        /// </summary>
        /// <param name="removeHeader">if set to <c>true</c> [remove header].</param>
        /// <param name="removeComments">if set to <c>true</c> [remove comments].</param>
        public void CodeTidyUp(
            bool removeHeader,
            bool removeComments)
        {
            if (removeHeader)
            {
                this.SolutionService.RemoveFileHeaders();
            }

            if (removeComments)
            {
                this.SolutionService.RemoveComments();
            }

            this.DTEService.SaveAll();
        }

        /// <summary>
        /// Gets the default name of the project.
        /// </summary>
        /// <returns>The default project name.</returns>
        public string GetDefaultProjectName()
        {
            IProjectService projectService = this.CoreProjectService;

            return projectService != null ? projectService.Name.Replace(ProjectSuffixes.Core, string.Empty) : string.Empty;
        }
        
        /// <summary>
        /// Gets the project service by suffix.
        /// </summary>
        /// <param name="suffix">The suffix.</param>
        /// <returns>The project service.</returns>
        public IProjectService GetProjectServiceBySuffix(string suffix)
        {
            Project project = this.Projects.FirstOrDefault(x => x.Name.EndsWith(suffix));

            return project != null ? new ProjectService(project) : null;
        }

        /// <summary>
        /// Gets the public view model names.
        /// </summary>
        /// <returns>The public view model names.</returns>
        public IEnumerable<string> GetPublicViewModelNames()
        {
            if (this.CoreProjectService != null)
            {
                IEnumerable<string> viewModelNames = this.CoreProjectService.GetFolderItems("ViewModels", false);

                //// exclude the base view model.
                return viewModelNames.ToList().Except(new List<string> { Settings.BaseViewModelName });
            }

            return null;
        }
    }
}
