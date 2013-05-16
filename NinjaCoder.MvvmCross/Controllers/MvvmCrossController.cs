// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MvvmCrossController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Constants;
    using EnvDTE;
    using EnvDTE80;
    using NinjaCoder.MvvmCross.Services;
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
        /// Initializes a new instance of the <see cref="MvvmCrossController" /> class.
        /// </summary>
        public MvvmCrossController()
            : this(new VisualStudioService(), new SettingsService())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MvvmCrossController" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        public MvvmCrossController(
            IVisualStudioService visualStudioService, 
            ISettingsService settingsService)
        {
            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Gets or sets the visual studio.
        /// </summary>
        public DTE2 DTE2
        {
            set
            {
                this.visualStudioService.DTE2 = value;
            }
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

                Project project = this.GetProjects().FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.Core));

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
                    Solution2 solution2 = this.visualStudioService.DTE2.GetSolution() as Solution2;

                    solution2.AddProjects(form.Path, form.Presenter.GetRequiredTemplates(), true);

                    //// now collapse the solution!
                    this.visualStudioService.DTE2.CollapseSolution();
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

            List<ItemTemplateInfo> templateInfos = this.visualStudioService.AllowedItemTemplates;

            if (templateInfos.Any())
            {
                ViewModelOptionsView form = new ViewModelOptionsView(templateInfos);

                form.ShowDialog();

                if (form.Continue)
                {
                    Solution2 solution = this.visualStudioService.DTE2.GetSolution() as Solution2;

                    solution.AddItemTemplateToProjects(form.Presenter.GetRequiredItemTemplates());
                }
            }
            else
            {
                MessageBox.Show("This solution is not a MvvmCross solution", Settings.ApplicationName);
            }
        }

        /// <summary>
        /// Adds the converters.
        /// </summary>
        public void AddConverters()
        {
            this.AddTraceHeader("AddConverters");

            List<ItemTemplateInfo> templateInfos = this.visualStudioService.AllowedItemTemplates;

            if (templateInfos.Any())
            {
                string templatesPath = this.settingsService.ConvertersTemplatesPath;

                List<ItemTemplateInfo> itemTemplateInfos = this.visualStudioService.GetFolderTemplateInfos(templatesPath, "Converters");

                ItemTemplatesForm form = new ItemTemplatesForm(itemTemplateInfos);

                form.ShowDialog();

                if (form.Continue)
                {
                    //// big assumption here that this is the right project!!
                    Project project = this.GetProjects().FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.Core));

                    if (project != null)
                    {
                        foreach (ItemTemplateInfo templateInfo in form.RequiredTemplates)
                        {
                            TraceService.WriteLine("MvvmCrossController::AddConverters adding from template path " + templatesPath + " template=" + templateInfo.FileName);

                            project.AddToFolderFromTemplate("Converters", templateInfo.FileName, templateInfo.FriendlyName + ".cs");
                        }
                    }
                    else
                    {
                        TraceService.WriteError("MvvmCrossController::AddConverters Cannot find Core project");
                    }
                }
            }
            else
            {
                MessageBox.Show("This solution is not a MvvmCross solution", Settings.ApplicationName);
            }
        }


        /// <summary>
        /// Adds the plugins.
        /// </summary>
        public void AddPlugins()
        {
            this.AddTraceHeader("AddPlugins");

            List<ItemTemplateInfo> templateInfos = this.visualStudioService.AllowedItemTemplates;

            if (templateInfos.Any())
            {
                string templatesPath = this.settingsService.PluginsTemplatesPath;

                List<ItemTemplateInfo> itemTemplateInfos = this.visualStudioService.GetFolderTemplateInfos(templatesPath, "Bootstrap");

                ItemTemplatesForm form = new ItemTemplatesForm(itemTemplateInfos);

                form.ShowDialog();

                if (form.Continue)
                {
                    //// big assumption here that this is the right project!!
                    Project project = this.GetProjects().FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.Core));

                    if (project != null)
                    {
                        foreach (ItemTemplateInfo templateInfo in form.RequiredTemplates)
                        {
                            TraceService.WriteLine("MvvmCrossController::AddPlugin adding from template path " + templatesPath + " template=" + templateInfo.FileName);

                            project.AddToFolderFromTemplate("Bootstrap", templateInfo.FileName, templateInfo.FriendlyName + ".cs");
                        }
                    }
                    else
                    {
                        TraceService.WriteError("MvvmCrossController::AddPlugIns Cannot find Core project");
                    }
                }
            }
            else
            {
                MessageBox.Show("This solution is not a MvvmCross solution", Settings.ApplicationName);
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
