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

    using EnvDTE;
    using EnvDTE80;

    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Views;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the MvvmCrossController type.
    /// </summary>
    public class MvvmCrossController
    {
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
            }
        }

        /// <summary>
        /// Builds the projects.
        /// </summary>
        public void BuildProjects()
        {
            TraceService.WriteLine("MvvmCrossController::BuildProjects");

            List<ProjectInfo> projectInfos = new List<ProjectInfo>
            {
               new ProjectInfo
                    {
                        FriendlyName = FriendlyNames.Core,
                        ProjectSuffix = ProjectSuffixes.Core,
                        TemplateName = ProjectTemplates.Core,
                    },
              new ProjectInfo
                    {
                        FriendlyName = FriendlyNames.CoreTests,
                        ProjectSuffix = ProjectSuffixes.Tests,
                        TemplateName = ProjectTemplates.Tests,
                    },
                new ProjectInfo
                    {
                        FriendlyName = FriendlyNames.iOS,
                        ProjectSuffix = ProjectSuffixes.iOS,
                        TemplateName = ProjectTemplates.IOS,
                    },
                new ProjectInfo
                    {
                        FriendlyName = FriendlyNames.Droid,
                        ProjectSuffix = ProjectSuffixes.Droid,
                        TemplateName = ProjectTemplates.Droid,
                    },
                new ProjectInfo
                    {
                        FriendlyName = FriendlyNames.WindowsPhone,
                        ProjectSuffix = ProjectSuffixes.WindowsPhone,
                        TemplateName = ProjectTemplates.WindowsPhone,
                    },
                new ProjectInfo
                    {
                        FriendlyName = FriendlyNames.WindowsStore,
                        ProjectSuffix = ProjectSuffixes.WindowsStore,
                        TemplateName = ProjectTemplates.WindowsStore,
                    },
                new ProjectInfo
                    {
                        FriendlyName = FriendlyNames.WindowsWpf,
                        ProjectSuffix = ProjectSuffixes.WindowsWpf,
                        TemplateName = ProjectTemplates.WindowsWPF,
                    },
            };

            string defaultLocation;
            string defaultProjectName = string.Empty;

            //// if this fails it will almost certainly be the COM integration with VStudio.
            try
            {
                defaultLocation = this.VisualStudio.GetDefaultProjectsLocation();

                Solution2 solution = this.VisualStudio.GetSolution() as Solution2;

                IEnumerable<Project> projects = solution.GetProjects();

                Project project = projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.Core));

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

            SolutionOptionsForm form = new SolutionOptionsForm(defaultLocation, defaultProjectName, projectInfos);

            form.ShowDialog();

            if (form.Continue)
            {
                Solution2 solution2 = this.VisualStudio.GetSolution() as Solution2;
                
                solution2.AddProjects(form.Path, form.Presenter.GetRequiredTemplates(), true);

                //// now collapse the solution!
                this.dte2.CollapseSolution();
            }
        }
        
        /// <summary>
        /// Adds the view model and views.
        /// </summary>
        public void AddViewModelAndViews()
        {
            TraceService.WriteLine("MvvmCrossController::AddViewModelAndViews");

            List<ItemTemplateInfo> itemTemplateInfos = new List<ItemTemplateInfo>
            {
                new ItemTemplateInfo
                    {
                        FriendlyName = FriendlyNames.iOS,
                        ProjectSuffix = ProjectSuffixes.iOS,
                        FolderName = "Views",
                        TemplateName = ItemTemplates.Views.IOS,
                    },
                new ItemTemplateInfo
                    {
                        FriendlyName = FriendlyNames.Droid,
                        ProjectSuffix = ProjectSuffixes.Droid,
                        FolderName = "Views",
                        TemplateName = ItemTemplates.Views.Droid,
                    },
                new ItemTemplateInfo
                    {
                        FriendlyName = FriendlyNames.WindowsPhone,
                        ProjectSuffix = ProjectSuffixes.WindowsPhone,
                        FolderName = "Views",
                        TemplateName = ItemTemplates.Views.WindowsPhone,
                    },
                new ItemTemplateInfo
                    {
                        FriendlyName = FriendlyNames.WindowsStore,
                        ProjectSuffix = ProjectSuffixes.WindowsStore,
                        FolderName = "Views",
                        TemplateName = ItemTemplates.Views.WindowsStore,
                    },
                new ItemTemplateInfo
                    {
                        FriendlyName = FriendlyNames.WindowsWpf,
                        ProjectSuffix = ProjectSuffixes.WindowsWpf,
                        FolderName = "Views",
                        TemplateName = ItemTemplates.Views.WindowsWPF,
                    },
            };


            //// set all platforms required for now.
            ViewModelOptionsView form = new ViewModelOptionsView(itemTemplateInfos);

            form.ShowDialog();

            if (form.Continue)
            {
                Solution2 solution = this.VisualStudio.GetSolution() as Solution2;

                solution.AddItemTemplateToProjects(form.Presenter.GetRequiredItemTemplates());
            }
        }

        /// <summary>
        /// Adds the converters.
        /// </summary>
        public void AddConverters()
        {
            TraceService.WriteLine("MvvmCrossController::AddConverters");

             string templatesPath  = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Microsoft Visual Studio 11.0\Common7\IDE\ItemTemplates\CSharp\MvvmCross\Converters";

            ConvertersForm form = new ConvertersForm(templatesPath);

            form.ShowDialog();

            if (form.Continue)
            {
                Solution2 solution = this.VisualStudio.GetSolution() as Solution2;

                IEnumerable<Project> projects = solution.GetProjects();

                //// big assumption here that this is the right project!!
                Project project = projects.FirstOrDefault(x => x.Name.EndsWith(ProjectSuffixes.Core));

                if (project != null)
                {
                    List<string> converters = form.RequiredConverters;

                    foreach (string converter in converters)
                    {
                        string templatePath = form.Presenter.GetPathFromFileName(converter);

                        TraceService.WriteError("MvvmCrossController::AddConverters adding from template path " + templatesPath);

                        project.AddToFolderFromTemplate("Converters", templatePath, converter + ".cs");
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
