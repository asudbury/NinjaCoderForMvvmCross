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
    public class VisualStudioService : BaseService, IVisualStudioService
    {
        /// <summary>
        /// The projects
        /// </summary>
        private IEnumerable<Project> projects;

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
            get { return this.projects ?? (this.projects = this.Solution.GetProjects()); }
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
                    TraceService.WriteLine("*****MvvmCrossController Activating Visual Studio Link*****");
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
        /// Gets the solution.
        /// </summary>
        public Solution2 Solution 
        {
            get { return this.DTE2.Solution as Solution2; }
        }
        
        /// <summary>
        /// Gets a value indicating whether [allow droid project].
        /// </summary>
        public bool AllowDroidProject
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether [allowi OS project].
        /// </summary>
        public bool AllowiOSProject
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether [allow windows phone project].
        /// </summary>
        public bool AllowWindowsPhoneProject
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether [allow windows store project].
        /// </summary>
        public bool AllowWindowsStoreProject
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether [allow WPF project].
        /// </summary>
        public bool AllowWpfProject
        {
            get { return true; }
        }

        /// <summary>
        /// Gets a value indicating whether this instance is MVVM cross solution.
        /// </summary>
        public bool IsMvvmCrossSolution 
        {
            get { return this.CoreProject != null; }
        }

        /// <summary>
        /// Gets the core project.
        /// </summary>
        public Project CoreProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.Core)); }
        }

        /// <summary>
        /// Gets the core project service.
        /// </summary>
        public IProjectService CoreProjectService 
        { 
            get { return new ProjectService(this.CoreProject); }
        }

        /// <summary>
        /// Gets the core test project.
        /// </summary>
        public Project CoreTestsProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.CoreTests)); }
        }

        /// <summary>
        /// Gets the core tests project service.
        /// </summary>
        public IProjectService CoreTestsProjectService
        {
            get { return new ProjectService(this.CoreTestsProject); }
        }

        /// <summary>
        /// Gets the droid project.
        /// </summary>
        public Project DroidProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.Droid)); }
        }

        /// <summary>
        /// Gets the droid project service.
        /// </summary>
        public IProjectService DroidProjectService
        {
            get { return new ProjectService(this.DroidProject); }
        }

        /// <summary>
        /// Gets the i OS project.
        /// </summary>
        public Project iOSProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.iOS)); }
        }

        /// <summary>
        /// Gets the iOS project service.
        /// </summary>
        public IProjectService iOSProjectService 
        { 
            get { return new ProjectService(this.iOSProject); }
        }

        /// <summary>
        /// Gets the windows phone project.
        /// </summary>
        public Project WindowsPhoneProject 
        { 
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.WindowsPhone)); } 
        }

        /// <summary>
        /// Gets the windows phone project service.
        /// </summary>
        public IProjectService WindowsPhoneProjectService 
        {
            get { return new ProjectService(this.WindowsPhoneProject); } 
        }

        /// <summary>
        /// Gets the windows store project.
        /// </summary>
        public Project WindowsStoreProject
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.WindowsStore)); }
        }

        /// <summary>
        /// Gets the windows store project service.
        /// </summary>
        public IProjectService WindowsStoreProjectService 
        { 
            get { return new ProjectService(this.WindowsStoreProject); }
        }

        /// <summary>
        /// Gets the WPF project.
        /// </summary>
        public Project WpfProject 
        {
            get { return this.Projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.WindowsWpf)); }
        }

        /// <summary>
        /// Gets the WPF project service.
        /// </summary>
        public IProjectService WpfProjectService
        {
            get { return new ProjectService(this.WpfProject); }
        }
        
        /// <summary>
        /// Gets the allowed project templates.
        /// </summary>
        public List<ProjectTemplateInfo> AllowedProjectTemplates
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
                            NugetCommand = Settings.NugetCommand
                        });
                }

                if (this.CoreTestsProject == null)
                {
                    projectInfos.Add(new ProjectTemplateInfo
                        {
                            FriendlyName = FriendlyNames.CoreTests,
                            ProjectSuffix = ProjectSuffixes.CoreTests,
                            TemplateName = ProjectTemplates.Tests,
                            PreSelected = true
                        });
                }

                if (this.DroidProject == null)
                {
                    projectInfos.Add(new ProjectTemplateInfo
                        {
                            FriendlyName = FriendlyNames.Droid,
                            ProjectSuffix = ProjectSuffixes.Droid,
                            TemplateName = ProjectTemplates.Droid,
                            NugetCommand = Settings.NugetCommand
                        });
                }

                if (this.iOSProject == null)
                {
                    projectInfos.Add( new ProjectTemplateInfo
                        {
                            FriendlyName = FriendlyNames.iOS,
                            ProjectSuffix = ProjectSuffixes.iOS,
                            TemplateName = ProjectTemplates.IOS,
                            NugetCommand = Settings.NugetCommand
                        });
                }

                if (this.WindowsPhoneProject == null)
                {
                    projectInfos.Add(new ProjectTemplateInfo
                        {
                            FriendlyName = FriendlyNames.WindowsPhone,
                            ProjectSuffix = ProjectSuffixes.WindowsPhone,
                            TemplateName = ProjectTemplates.WindowsPhone,
                            NugetCommand = Settings.NugetCommand
                        });
                }

                if (this.WindowsStoreProject == null)
                {
                    projectInfos.Add(new ProjectTemplateInfo
                        {
                            FriendlyName = FriendlyNames.WindowsStore,
                            ProjectSuffix = ProjectSuffixes.WindowsStore,
                            TemplateName = ProjectTemplates.WindowsStore,
                            NugetCommand = Settings.NugetCommand
                        });
                }

                if (this.WpfProject == null)
                {
                    projectInfos.Add( new ProjectTemplateInfo
                        {
                            FriendlyName = FriendlyNames.WindowsWpf,
                            ProjectSuffix = ProjectSuffixes.WindowsWpf,
                            TemplateName = ProjectTemplates.WindowsWPF,
                            NugetCommand = Settings.NugetCommand
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
        /// Gets or sets the dte2.
        /// </summary>
        private DTE2 dte2 { get; set; }

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
    }
}
