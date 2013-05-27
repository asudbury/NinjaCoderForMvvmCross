// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvvmCrossController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Windows.Forms;
    using Constants;
    using EnvDTE;
    using EnvDTE80;
    using Services;
    using Services.Interfaces;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;

    using Views;

    /// <summary>
    ///  Defines the MvvmCrossController type.
    /// </summary>
    public class MvvmCrossController
    {
        /// <summary>
        /// The visual studio service
        /// </summary>
        private readonly IVisualStudioService visualStudioService;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The plugins service.
        /// </summary>
        private readonly IPluginsService pluginsService;

        /// <summary>
        /// The configuration service.
        /// </summary>
        private readonly IConfigurationService configurationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MvvmCrossController" /> class.
        /// </summary>
        public MvvmCrossController()
            : this(new VisualStudioService(), 
            new SettingsService(), 
            new PluginsService(),
            new ConfigurationService())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MvvmCrossController" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="pluginsService">The plugins service.</param>
        /// <param name="configurationService">The configuration service.</param>
        public MvvmCrossController(
            IVisualStudioService visualStudioService, 
            ISettingsService settingsService,
            IPluginsService pluginsService,
            IConfigurationService configurationService)
        {
            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
            this.pluginsService = pluginsService;
            this.configurationService = configurationService;
        }

        /// <summary>
        /// Gets or sets the visual studio.
        /// </summary>
        public DTE2 DTE2
        {
            set { this.visualStudioService.DTE2 = value; }
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Project> GetProjects()
        {
            Solution2 solution = this.visualStudioService.DTE2.GetSolution() as Solution2;

            return solution.GetProjects();
        }

        /// <summary>
        /// Initializes Ninja Coder for MvvmCross.
        /// </summary>
        public void Initialize()
        {
            this.configurationService.CreateUserDirectories();
        }

        /// <summary>
        /// Builds the projects.
        /// </summary>
        public void BuildProjects()
        {
            this.AddTraceHeader("BuildProjects");

            string defaultLocation;
            string defaultProjectName = string.Empty;

            //// if this fails it will almost certainly be the COM integration with VStudio.
            try
            {
                defaultLocation = this.visualStudioService.DTE2.GetDefaultProjectsLocation();

                Project project = this.visualStudioService.CoreProject;

                if (project != null)
                {
                    TraceService.WriteLine("MvvmCrossController::BuildProjects defaultProject=" + project.Name);
                    
                    defaultProjectName = project.Name.Replace(ProjectSuffixes.Core, string.Empty);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(@"Error Communicating with Visual Studio Error:-" + exception.Message);
                return;
            }

            List<ProjectTemplateInfo> projectTemplateInfos = this.visualStudioService.AllowedProjectTemplates;

            if (projectTemplateInfos.Count > 0)
            {
                SolutionOptionsForm form = new SolutionOptionsForm(defaultLocation, defaultProjectName, projectTemplateInfos);

                form.ShowDialog();

                if (form.Continue)
                {
                    this.visualStudioService.DTE2.WriteStatusBarMessage("Ninja Coder is running....");

                    Solution2 solution2 = this.visualStudioService.DTE2.GetSolution() as Solution2;

                    solution2.AddProjects(form.Path, form.Presenter.GetRequiredTemplates(), true);

                    //// now collapse the solution!
                    this.visualStudioService.DTE2.CollapseSolution();

                    this.visualStudioService.DTE2.WriteStatusBarMessage("Ninja Coder has completed the build of the MvvmCross solution.");
                }
            }
            else
            {
                MessageBox.Show(@"This solution has already been setup with the MvvmCross projects", Settings.ApplicationName);
               
            }
        }
        
        /// <summary>
        /// Adds the view model and views.
        /// </summary>
        public void AddViewModelAndViews()
        {
            this.AddTraceHeader("AddViewModelAndViews");

            if (this.visualStudioService.IsMvvmCrossSolution)
            {
                List<ItemTemplateInfo> templateInfos = this.visualStudioService.AllowedItemTemplates;
                ViewModelOptionsView form = new ViewModelOptionsView(templateInfos);

                form.ShowDialog();

                if (form.Continue)
                {
                    this.visualStudioService.DTE2.WriteStatusBarMessage("Ninja Coder is running....");

                    Solution2 solution = this.visualStudioService.DTE2.GetSolution() as Solution2;

                    solution.AddItemTemplateToProjects(form.Presenter.GetRequiredItemTemplates());

                    //// now collapse the solution!
                    this.visualStudioService.DTE2.CollapseSolution();

                    this.visualStudioService.DTE2.WriteStatusBarMessage("Ninja Coder has completed the adding of the viewmodel and views.");
                }
            }
            else
            {
                MessageBox.Show("This solution is not a MvvmCross solution.", Settings.ApplicationName);
            }
        }

        /// <summary>
        /// Adds the converters.
        /// </summary>
        public void AddConverters()
        {
            this.AddTraceHeader("AddConverters");

            if (this.visualStudioService.IsMvvmCrossSolution)
            {
                string templatesPath = this.settingsService.ConvertersTemplatesPath;

                List<ItemTemplateInfo> itemTemplateInfos = this.visualStudioService.GetFolderTemplateInfos(templatesPath, "Converters");

                ItemTemplatesForm form = new ItemTemplatesForm(itemTemplateInfos);

                form.ShowDialog();

                if (form.Continue)
                {
                    this.visualStudioService.DTE2.WriteStatusBarMessage("Ninja Coder is running....");

                    Project project = this.visualStudioService.CoreProject;

                    if (project != null)
                    {
                        foreach (ItemTemplateInfo templateInfo in form.RequiredTemplates)
                        {
                            TraceService.WriteLine("MvvmCrossController::AddConverters adding from template path " + templatesPath + " template=" + templateInfo.FileName);

                            project.AddToFolderFromTemplate("Converters", templateInfo.FileName, templateInfo.FriendlyName + ".cs");
                        }
                        
                        //// now collapse the solution!
                        this.visualStudioService.DTE2.CollapseSolution();

                        this.visualStudioService.DTE2.WriteStatusBarMessage("Ninja Coder has completed the adding of the converters.");
                    }
                    else
                    {
                        TraceService.WriteError("MvvmCrossController::AddConverters Cannot find Core project");
                    }
                }
            }
            else
            {
                MessageBox.Show("This solution is not a MvvmCross solution.", Settings.ApplicationName);
            }
        }
        
        /// <summary>
        /// Adds the plugins.
        /// </summary>
        public void AddPlugins()
        {
            this.AddTraceHeader("AddPlugins");

            if (this.visualStudioService.IsMvvmCrossSolution)
            {
                List<string> viewModelNames = new List<string>();

                Project coreProject = this.visualStudioService.CoreProject;

                //// look for the current view models in the project.
                if (coreProject != null)
                {
                    ProjectItem projectItem = coreProject.GetFolder("ViewModels");

                    IEnumerable<ProjectItem> projectItems = projectItem.GetSubProjectItems();

                    viewModelNames.AddRange(projectItems.Select(item => Path.GetFileNameWithoutExtension(item.Name)));
                }

                PluginsForm form = new PluginsForm(viewModelNames);

                form.ShowDialog();

                if (form.Continue)
                {
                    this.visualStudioService.DTE2.WriteStatusBarMessage("Ninja Coder is running....");

                    pluginsService.AddPlugins(
                        this.visualStudioService, 
                        form.RequiredPlugins, 
                        form.ImplementInViewModel,
                        this.settingsService.CodeSnippetsPath);

                    //// now collapse the solution!
                    this.visualStudioService.DTE2.CollapseSolution();

                    this.visualStudioService.DTE2.WriteStatusBarMessage("Ninja Coder has completed the adding of the plugins.");
                }
            }
            else
            {
                MessageBox.Show("This solution is not a MvvmCross solution.", Settings.ApplicationName);
            }
        }
        
        /// <summary>
        /// Shows the options form.
        /// </summary>
        public void ShowOptions()
        {
            this.AddTraceHeader("ShowOptions");

            OptionsForm form = new OptionsForm();
            
            form.ShowDialog();
        }

        /// <summary>
        /// Shows the stack over flow.
        /// </summary>
        public void ShowStackOverFlow()
        {
            this.AddTraceHeader("ShowStackOverFlow");

            System.Diagnostics.Process.Start(Links.StackOverFlowUrl);
            ////this.VisualStudio.NavigateTo(Links.StackOverFlowUrl);
        }

        /// <summary>
        /// Shows the jabbr room.
        /// </summary>
        public void ShowJabbrRoom()
        {
            this.AddTraceHeader("ShowJabbrRoom");

            System.Diagnostics.Process.Start(Links.JabbrRoomUrl);
            ////this.VisualStudio.NavigateTo(Links.JabbrRoomUrl);
        }

        /// <summary>
        /// Shows the git hub.
        /// </summary>
        public void ShowGitHub()
        {
            this.AddTraceHeader("ShowGitHub");

            System.Diagnostics.Process.Start(Links.GitHubUrl);
            ////this.VisualStudio.NavigateTo(Links.GitHubUrl);
        }

        /// <summary>
        /// Adds the trace header.
        /// </summary>
        /// <param name="methodName">Name of the method.</param>
        internal void AddTraceHeader(string methodName)
        {
            TraceService.WriteLine("--------------------------------------------------------");
            TraceService.WriteLine("MvvmCrossController::" + methodName);
            TraceService.WriteLine("--------------------------------------------------------");
        }
    }
}
