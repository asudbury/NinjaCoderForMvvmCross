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
        /// Initializes a new instance of the <see cref="MvvmCrossController" /> class.
        /// </summary>
        public MvvmCrossController()
            : this(new VisualStudioService())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="MvvmCrossController" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        public MvvmCrossController(IVisualStudioService visualStudioService)
        {
            this.visualStudioService = visualStudioService;
        }

        /// <summary>
        /// Visual Studio Instance.
        /// </summary>
        private DTE2 dte2;

        /// <summary>
        /// Gets or sets the visual studio.
        /// </summary>
        public DTE2 VisualStudio
        {
            get
            {
                if (this.dte2 == null)
                {
                    TraceService.WriteLine("MvvmCrossController Activating Visual Studio Link");
                    this.dte2 = VSActivatorService.Activate();
                }

                return this.dte2;
            }

            set
            {
                this.dte2 = value;
                this.visualStudioService.DTE2 = value;
            }
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Project> GetProjects()
        {
            Solution2 solution = this.VisualStudio.GetSolution() as Solution2;

            return solution.GetProjects();
        }

        /// <summary>
        /// Builds the projects.
        /// </summary>
        public void BuildProjects()
        {
            TraceService.WriteLine("MvvmCrossController::BuildProjects");

            this.visualStudioService.DTE2 = VisualStudio;

            string defaultLocation;
            string defaultProjectName = string.Empty;

            //// if this fails it will almost certainly be the COM integration with VStudio.
            try
            {
                defaultLocation = this.VisualStudio.GetDefaultProjectsLocation();

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
                    Solution2 solution2 = this.VisualStudio.GetSolution() as Solution2;

                    solution2.AddProjects(form.Path, form.Presenter.GetRequiredTemplates(), true);

                    //// now collapse the solution!
                    this.dte2.CollapseSolution();
                }
            }
            else
            {
                MessageBox.Show(@"This solution has already been setup with the MvvmCross projects");
               
            }
        }
        
        /// <summary>
        /// Adds the view model and views.
        /// </summary>
        public void AddViewModelAndViews()
        {
            TraceService.WriteLine("MvvmCrossController::AddViewModelAndViews");

            VisualStudioService service = new VisualStudioService
                                              {
                                                  DTE2 = this.VisualStudio
                                              };

            List<ItemTemplateInfo> templateInfos = service.AllowedItemTemplates;

            if (templateInfos.Any())
            {
                ViewModelOptionsView form = new ViewModelOptionsView(templateInfos);

                form.ShowDialog();

                if (form.Continue)
                {
                    Solution2 solution = this.VisualStudio.GetSolution() as Solution2;

                    solution.AddItemTemplateToProjects(form.Presenter.GetRequiredItemTemplates());
                }
            }
            else
            {
                MessageBox.Show("This solution is not a MvvmCross solution", "Ninja Coder for MvvmCross");
            }
        }

        /// <summary>
        /// Adds the converters.
        /// </summary>
        public void AddConverters()
        {
            TraceService.WriteLine("MvvmCrossController::AddConverters");

            string templatesPath  = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Microsoft Visual Studio 11.0\Common7\IDE\ItemTemplates\CSharp\MvvmCross\Converters";

            List<ItemTemplateInfo> itemTemplateInfos = this.visualStudioService.GetFolderTemplateInfos(templatesPath, "Converters");

            ConvertersForm form = new ConvertersForm(itemTemplateInfos);

            form.ShowDialog();

            if (form.Continue)
            {
                //// big assumption here that this is the right project!!
                Project project = this.GetProjects().FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.Core));

                if (project != null)
                {
                    foreach (ItemTemplateInfo templateInfo in form.RequiredTemplates)
                    {
                        TraceService.WriteLine("MvvmCrossController::AddConverters adding from template path " + templatesPath);

                        project.AddToFolderFromTemplate("Converters", templateInfo.FileName, templateInfo.FriendlyName + ".cs");
                    }
                }
                else
                {
                    TraceService.WriteError("MvvmCrossController::AddConverters Cannot find Core project");
                }
            }
        }

        /// <summary>
        /// Shows the options form.
        /// </summary>
        public void ShowOptions()
        {
            TraceService.WriteLine("MvvmCrossController::ShowOptions");

            OptionsForm form = new OptionsForm();
            
            form.ShowDialog();
        }

        /// <summary>
        /// Shows the stack over flow.
        /// </summary>
        public void ShowStackOverFlow()
        {
            System.Diagnostics.Process.Start(Links.StackOverFlowUrl);
            ////this.VisualStudio.NavigateTo(Links.StackOverFlowUrl);
        }

        /// <summary>
        /// Shows the jabbr room.
        /// </summary>
        public void ShowJabbrRoom()
        {
            System.Diagnostics.Process.Start(Links.JabbrRoomUrl);
            ////this.VisualStudio.NavigateTo(Links.JabbrRoomUrl);
        }

        /// <summary>
        /// Shows the git hub.
        /// </summary>
        public void ShowGitHub()
        {
            System.Diagnostics.Process.Start(Links.GitHubUrl);
            ////this.VisualStudio.NavigateTo(Links.GitHubUrl);
        }
    }
}
