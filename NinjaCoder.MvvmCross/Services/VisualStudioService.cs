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
                    TraceService.WriteLine("*****VisualStudioService Activating Visual Studio Link*****");
                    this.dte2 = VSActivatorService.Activate();
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
            get { return new DTEService(this.dte2); }
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
        /// Gets the allowed project templates.
        /// </summary>
        public IEnumerable<ProjectTemplateInfo> AllowedProjectTemplates
        {
            get
            {
                List<ProjectTemplateInfo> projectInfos = new List<ProjectTemplateInfo>();

                if (this.CoreProject == null)
                {
                    projectInfos.Add(new ProjectTemplateInfo
                    {
                        FriendlyName = FriendlyNames.Core,
                        ProjectSuffix = ProjectSuffixes.Core,
                        TemplateName = ProjectTemplates.Core,
                        PreSelected = true,
                        NugetCommands = new List<string> 
                            {
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMvvmCrossPackage),
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMessengerPackage)
                            }
                    });
                }

                if (this.CoreTestsProject == null)
                {
                    projectInfos.Add(new ProjectTemplateInfo
                    {
                        FriendlyName = FriendlyNames.CoreTests,
                        ProjectSuffix = ProjectSuffixes.CoreTests,
                        TemplateName = ProjectTemplates.Tests,
                        PreSelected = true,
                        NugetCommands = new List<string> 
                            {
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetUnitTestsPackage),
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMvvmCrossPackage)
                            },
                        NonMvxAssemblies = new List<string> { "Moq", "NUnit" }
                    });
                }

                if (this.DroidProject == null)
                {
                    projectInfos.Add(new ProjectTemplateInfo
                    {
                        FriendlyName = FriendlyNames.Droid,
                        ProjectSuffix = ProjectSuffixes.Droid,
                        TemplateName = ProjectTemplates.Droid,
                        NugetCommands = new List<string> 
                            {
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMvvmCrossPackage),
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMessengerPackage)
                            }
                    });
                }

                if (this.iOSProject == null)
                {
                    projectInfos.Add(new ProjectTemplateInfo
                    {
                        FriendlyName = FriendlyNames.iOS,
                        ProjectSuffix = ProjectSuffixes.iOS,
                        TemplateName = ProjectTemplates.IOS,
                        NugetCommands = new List<string> 
                            {
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMvvmCrossPackage),
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMessengerPackage)
                            }
                    });
                }

                if (this.WindowsPhoneProject == null)
                {
                    projectInfos.Add(new ProjectTemplateInfo
                    {
                        FriendlyName = FriendlyNames.WindowsPhone,
                        ProjectSuffix = ProjectSuffixes.WindowsPhone,
                        TemplateName = ProjectTemplates.WindowsPhone,
                        NugetCommands = new List<string> 
                            {
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMvvmCrossPackage),
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMessengerPackage)
                            }
                    });
                }

                if (this.WindowsStoreProject == null)
                {
                    projectInfos.Add(new ProjectTemplateInfo
                    {
                        FriendlyName = FriendlyNames.WindowsStore,
                        ProjectSuffix = ProjectSuffixes.WindowsStore,
                        TemplateName = ProjectTemplates.WindowsStore,
                        NugetCommands = new List<string> 
                            {
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMvvmCrossPackage),
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMessengerPackage)
                            }
                    });
                }

                if (this.WpfProject == null)
                {
                    projectInfos.Add(new ProjectTemplateInfo
                    {
                        FriendlyName = FriendlyNames.WindowsWpf,
                        ProjectSuffix = ProjectSuffixes.WindowsWpf,
                        TemplateName = ProjectTemplates.WindowsWPF,
                        NugetCommands = new List<string> 
                            {
                                Settings.NugetInstallPackage.Replace("%s", Settings.NugetMvvmCrossPackage)
                            }
                    });
                }

                return projectInfos;
            }
        }

        /// <summary>
        /// Gets the allowed item templates.
        /// </summary>
        public List<ItemTemplateInfo> AllowedItemTemplates
        {
            get
            {
                const string FolderName = "Views";

                List<ItemTemplateInfo> itemTemplateInfos = new List<ItemTemplateInfo>();

                if (this.iOSProject != null)
                {
                    itemTemplateInfos.Add(new ItemTemplateInfo
                    {
                        FriendlyName = FriendlyNames.iOS,
                        ProjectSuffix = ProjectSuffixes.iOS,
                        FolderName = FolderName,
                        TemplateName = ItemTemplates.Views.IOS,
                        PreSelected = true
                    });
                }

                if (this.DroidProject != null)
                {
                    itemTemplateInfos.Add(new ItemTemplateInfo
                    {
                        FriendlyName = FriendlyNames.Droid,
                        ProjectSuffix = ProjectSuffixes.Droid,
                        FolderName = FolderName,
                        TemplateName = ItemTemplates.Views.Droid,
                        PreSelected = true
                    });
                }

                if (this.WindowsPhoneProject != null)
                {
                    itemTemplateInfos.Add(new ItemTemplateInfo
                    {
                        FriendlyName = FriendlyNames.WindowsPhone,
                        ProjectSuffix = ProjectSuffixes.WindowsPhone,
                        FolderName = FolderName,
                        TemplateName = ItemTemplates.Views.WindowsPhone,
                        PreSelected = true
                    });
                }

                if (this.WindowsStoreProject != null)
                {
                    itemTemplateInfos.Add(new ItemTemplateInfo
                    {
                        FriendlyName = FriendlyNames.WindowsStore,
                        ProjectSuffix = ProjectSuffixes.WindowsStore,
                        FolderName = FolderName,
                        TemplateName = ItemTemplates.Views.WindowsStore,
                        PreSelected = true
                    });
                }

                if (this.WpfProject != null)
                {
                    itemTemplateInfos.Add(new ItemTemplateInfo
                    {
                        FriendlyName = FriendlyNames.WindowsWpf,
                        ProjectSuffix = ProjectSuffixes.WindowsWpf,
                        FolderName = FolderName,
                        TemplateName = ItemTemplates.Views.WindowsWPF,
                        PreSelected = true
                    });
                }

                return itemTemplateInfos;
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
        /// Gets the folder template infos.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="destinationFolder">The destination folder.</param>
        /// <returns>The template Infos.</returns>
        public List<ItemTemplateInfo> GetFolderTemplateInfos(
            string path,
            string destinationFolder)
        {
            List<ItemTemplateInfo> itemTemplateInfos = new List<ItemTemplateInfo>();

            string[] paths = Directory.GetFiles(path);

            ////paths.ToList().ForEach();

            foreach (string filePath in paths)
            {
                FileInfo fileInfo = new FileInfo(filePath);

                string name = Path.GetFileNameWithoutExtension(fileInfo.Name);

                itemTemplateInfos.Add(new ItemTemplateInfo
                {
                    FriendlyName = name.Replace("MvvmCross.", string.Empty),
                    FileName = fileInfo.Name,
                    FolderName = destinationFolder
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
    }
}
