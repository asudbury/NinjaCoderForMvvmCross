// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the VisualStudioService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Constants;
    using EnvDTE;
    using EnvDTE80;

    using Interfaces;

    using NinjaCoder.MvvmCross.Entities;

    using Scorchio.Infrastructure.Extensions;
    using Scorchio.VisualStudio;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    using VSLangProj;

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
        /// Gets the droid tests project service.
        /// </summary>
        public IProjectService DroidTestsProjectService
        {
            get
            {
                Project project = this.DroidTestsProject;
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
        /// Gets the ios tests project service.
        /// </summary>
        public IProjectService iOSTestsProjectService
        {
            get
            {
                Project project = this.iOSTestsProject;
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
        /// Gets the windows phone tests project service.
        /// </summary>
        public IProjectService WindowsPhoneTestsProjectService
        {
            get
            {
                Project project = this.WindowsPhoneTestsProject;
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
        /// Gets the windows store tests project service.
        /// </summary>
        public IProjectService WindowsStoreTestsProjectService
        {
            get
            {
                Project project = this.WindowsStoreTestsProject;
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
        /// Gets the WPF tests project service.
        /// </summary>
        public IProjectService WpfTestsProjectService
        {
            get
            {
                Project project = this.WpfTestsProject;
                return project != null ? new ProjectService(project) : null;
            }
        }

        /// <summary>
        /// Gets the xamarin forms project service.
        /// </summary>
        public IProjectService XamarinFormsProjectService
        {
            get
            {
                Project project = this.XamarinFormsProject;
                return project != null ? new ProjectService(project) : null;
            }
        }

        /// <summary>
        /// Gets the xamarin forms tests project service.
        /// </summary>
        public IProjectService XamarinFormsTestsProjectService
        {
            get
            {
                Project project = this.XamarinFormsTestsProject;
                return project != null ? new ProjectService(project) : null;
            }
        }

        /// <summary>
        /// Gets the view models project service.
        /// </summary>
        public IProjectService ViewModelsProjectService
        {
            get
            {
                Project project = this.ViewModelsProject;
                return project != null ? new ProjectService(project) : null;                
            }
        }

        /// <summary>
        /// Gets the plugins project service.
        /// </summary>
        public IProjectService PluginsProjectService
        {
            get
            {
                Project project = this.PluginsProject;
                return project != null ? new ProjectService(project) : null;
            }
        }

        /// <summary>
        /// Gets the core project.
        /// </summary>
        internal Project CoreProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffix.Core.GetDescription())); }
        }

        /// <summary>
        /// Gets the core test project.
        /// </summary>
        internal Project CoreTestsProject
        {
            get {  return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffix.CoreTests.GetDescription())); }
        }

        /// <summary>
        /// Gets the droid project.
        /// </summary>
        internal Project DroidProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffix.Droid.GetDescription())); }
        }

        /// <summary>
        /// Gets the droid tests project.
        /// </summary>
        internal Project DroidTestsProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffix.DroidTests.GetDescription())); }
        }

        /// <summary>
        /// Gets the iOS project.
        /// </summary>
        internal Project iOSProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffix.iOS.GetDescription())); }
        }

        /// <summary>
        /// Gets the iOS tests project.
        /// </summary>
        internal Project iOSTestsProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffix.iOSTests.GetDescription())); }
        }

        /// <summary>
        /// Gets the windows phone project.
        /// </summary>
        internal Project WindowsPhoneProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffix.WindowsPhone.GetDescription())); }
        }

        /// <summary>
        /// Gets the windows phone tests project.
        /// </summary>
        internal Project WindowsPhoneTestsProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffix.WindowsPhoneTests.GetDescription())); }
        }

        /// <summary>
        /// Gets the windows store project.
        /// </summary>
        internal Project WindowsStoreProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffix.WindowsStore.GetDescription())); }
        }

        /// <summary>
        /// Gets the windows store tests project.
        /// </summary>
        internal Project WindowsStoreTestsProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffix.WindowsStoreTests.GetDescription())); }
        }

        /// <summary>
        /// Gets the WPF project.
        /// </summary>
        internal Project WpfProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffix.Wpf.GetDescription())); }
        }

        /// <summary>
        /// Gets the WPF tests project.
        /// </summary>
        internal Project WpfTestsProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffix.WpfTests.GetDescription())); }
        }

        /// <summary>
        /// Gets the xamarin forms project.
        /// </summary>
        internal Project XamarinFormsProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffix.XamarinForms.GetDescription())); }
        }

        /// <summary>
        /// Gets the xamarin forms tests project.
        /// </summary>
        internal Project XamarinFormsTestsProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffix.XamarinFormsTests.GetDescription())); }
        }

        /// <summary>
        /// Gets the view models project.
        /// </summary>
        internal Project ViewModelsProject
        {
            get
            {
                Project coreProject = this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffix.Core.GetDescription()));
                return coreProject;
            }
        }

                /// <summary>
        /// Gets the view models project.
        /// </summary>
        internal Project PluginsProject
        {
            get
            {
                Project pluginProject = this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffix.Core.GetDescription()));
                return pluginProject;
            }
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

                itemTemplateInfos.Add(
                    new ItemTemplateInfo
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
        public void CodeTidyUp(bool removeHeader, bool removeComments)
        {
            TraceService.WriteLine("VisualStudioService::CodeTidyUp");

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

            return projectService != null
                       ? projectService.Name.Replace(ProjectSuffix.Core.GetDescription(), string.Empty)
                       : string.Empty;
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
        /// Gets the project services by suffix.
        /// </summary>
        /// <param name="suffix">The suffix.</param>
        /// <returns>
        /// A collection of project services.
        /// </returns>
        public IEnumerable<IProjectService> GetProjectServicesBySuffix(string suffix)
        {
            IEnumerable<Project> projects = this.Projects.Where(x => x.Name.EndsWith(suffix));

            return projects.Select(project => new ProjectService(project)).ToList();
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

        /// <summary>
        /// Gets the type of the framework.
        /// </summary>
        /// <returns>
        /// The framework type.
        /// </returns>
        public FrameworkType GetFrameworkType()
        {
            int projectType = 0;

            if (this.CoreProjectService == null)
            {
                return FrameworkType.NotSet;
            }

            IEnumerable<Reference> projectReferences = this.CoreProjectService.GetProjectReferences();

            if (projectReferences != null)
            {
                //// the reference will only be there once nuget has run
                if (projectReferences.Any(projectReference => projectReference.Name.Contains("MvvmCross")))
                {
                    projectType = 1;
                }

                //// if nuget has not yet run look for the ViewModelService
                else if (this.CoreProjectService.GetProjectItem("ViewModelService.cs") != null)
                {
                    projectType = 1;
                }
            }

            if (this.XamarinFormsProjectService != null)
            {
                projectType += 2;
            }

            switch (projectType)
            {
                case 0:
                    return FrameworkType.NoFramework;

                case 1:
                    return FrameworkType.MvvmCross;

                case 2:
                    return FrameworkType.XamarinForms;

                case 3:
                    return FrameworkType.MvvmCrossAndXamarinForms;

                default:
                    return FrameworkType.NotSet;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [solution already created].
        /// </summary>
        public bool SolutionAlreadyCreated
        {
            get { return this.SolutionService.GetProjects().Any(); }
        }
    }
}
