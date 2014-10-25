// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MainViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.UI.ViewModels
{
    using System.Collections.Generic;
    using System.Windows.Input;

    using EnvDTE;

    using Controllers;

    using Scorchio.Infrastructure.Wpf;
    using Scorchio.Infrastructure.Wpf.ViewModels;

    /// <summary>
    ///  Defines the MainViewModel type.
    /// </summary>
    internal class MainViewModel : BaseDialogViewModel
    {
        /// <summary>
        /// The projects.
        /// </summary>
        private IEnumerable<Project> projects;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel" /> class.
        /// </summary>
        public MainViewModel()
        {
            NinjaController.Startup();

            try
            {
                this.projects = NinjaController.GetProjects();
            }

            catch
            {
            }
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        public IEnumerable<Project> Projects
        {
            get { return this.projects; }
            set { this.SetProperty(ref this.projects, value); }
        }

        /// <summary>
        /// Gets the add projects command.
        /// </summary>
        public ICommand AddProjectsCommand
        {
            get { return new RelayCommand(this.AddProjects); }
        }

        /// <summary>
        /// Gets the add view model and view command.
        /// </summary>
        public ICommand AddViewModelAndViewCommand
        {
            get { return new RelayCommand(this.AddViewModelAndView); }
        }

        /// <summary>
        /// Gets the add plugins command.
        /// </summary>
        public ICommand AddPluginsCommand
        {
            get { return new RelayCommand(this.AddPlugins); }
        }

        /// <summary>
        /// Gets the view log command.
        /// </summary>
        public ICommand ViewLogCommand
        {
            get { return new RelayCommand(this.ViewLog); }
        }

        /// <summary>
        /// Gets the clear log command.
        /// </summary>
        public ICommand ClearLogCommand
        {
            get { return new RelayCommand(this.ClearLog); }
        }

        /// <summary>
        /// Gets the options command.
        /// </summary>
        public ICommand OptionsCommand
        {
            get { return new RelayCommand(this.Options); }
        }

        /// <summary>
        /// Gets the about command.
        /// </summary>
        public ICommand AboutCommand
        {
            get { return new RelayCommand(this.About); }
        }

        /// <summary>
        /// Gets the exit command.
        /// </summary>
        public ICommand ExitCommand
        {
            get { return new RelayCommand(this.Exit); }
        }

        /// <summary>
        /// Adds the projects.
        /// </summary>
        internal void AddProjects()
        {
            NinjaController.RunProjectsController();
            this.Projects = NinjaController.GetProjects();
        }

        /// <summary>
        /// Adds the view model and view.
        /// </summary>
        internal void AddViewModelAndView()
        {
            NinjaController.RunViewModelViewsController();
        }

        /// <summary>
        /// Adds the plugins.
        /// </summary>
        internal void AddPlugins()
        {
            NinjaController.RunPluginsController();
        }

        /// <summary>
        /// Views the log.
        /// </summary>
        internal void ViewLog()
        {
            NinjaController.ViewLogFile();
        }

        /// <summary>
        /// Clears the log.
        /// </summary>
        internal void ClearLog()
        {
            NinjaController.ClearLogFile();
        }

        /// <summary>
        /// Options this instance.
        /// </summary>
        internal void Options()
        {
            NinjaController.ShowOptions();
        }

        /// <summary>
        /// Abouts this instance.
        /// </summary>
        internal void About()
        {
            NinjaController.ShowAboutBox();
        }

        /// <summary>
        /// Exits this instance.
        /// </summary>
        internal void Exit()
        {
            this.OnOk();
        }
    }
}
