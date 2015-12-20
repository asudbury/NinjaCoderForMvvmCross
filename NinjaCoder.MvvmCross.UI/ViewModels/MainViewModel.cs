// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MainViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.UI.ViewModels
{
    using Controllers;
    using EnvDTE;
    using Microsoft.Win32;
    using Scorchio.Infrastructure.Wpf;
    using Scorchio.Infrastructure.Wpf.ViewModels;
    using System.Collections.Generic;
    using System.Windows.Input;

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

                string workingDirectory = this.GetWorkingDirectory();

                NinjaController.SetWorkingDirectory(workingDirectory);
                NinjaController.UseSimpleTextTemplatingEngine(true);
            }
            catch
            {
                // ignored
            }
        }

        /// <summary>
        /// Gets or sets the projects.
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
        /// Gets the add nuget packages command.
        /// </summary>
        public ICommand AddNugetPackagesCommand
        {
            get { return new RelayCommand(this.AddNugetPackages); }
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

        public ICommand OpenWorkingDirectoryCommand
        {
            get { return new RelayCommand(this.OpenWorkingDirectory); }
        }

        /// <summary>
        /// Gets the exit command.
        /// </summary>
        public ICommand ExitCommand
        {
            get { return new RelayCommand(this.Exit); }
        }

        /// <summary>
        /// Gets the add forms dependency service command.
        /// </summary>
        /// <value>
        /// The add forms dependency service command.
        /// </value>
        public ICommand AddFormsDependencyServiceCommand
        {
            get { return new RelayCommand(this.FormsDependencyService); }
        }

        /// <summary>
        /// Gets the add forms custom renderer command.
        /// </summary>
        public ICommand AddFormsCustomRendererCommand
        {
            get { return new RelayCommand(this.FormsCustomRenderer); }
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
        /// Adds the nuget packages.
        /// </summary>
        internal void AddNugetPackages()
        {
            NinjaController.RunNugetPackagesController();
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
        
        /// <summary>
        /// Formses the dependency service.
        /// </summary>
        internal void FormsDependencyService()
        {
            NinjaController.RunDependencyServicesController();
        }

        /// <summary>
        /// Formses the custom renderer.
        /// </summary>
        internal void FormsCustomRenderer()
        {
            NinjaController.RunCustomerRendererController();
        }

        /// <summary>
        /// Opens the working directory.
        /// </summary>
        internal void OpenWorkingDirectory()
        {
            System.Diagnostics.Process.Start(this.GetWorkingDirectory());
        }

        /// <summary>
        /// Gets the working directory.
        /// </summary>
        /// <returns></returns>
        internal string GetWorkingDirectory()
        {
            RegistryKey softwareKey = Registry.CurrentUser.OpenSubKey("Software");

            if (softwareKey != null)
            {
                RegistryKey microsoftKey = softwareKey.OpenSubKey("Microsoft");

                if (microsoftKey != null)
                {
                    RegistryKey vsKey = microsoftKey.OpenSubKey("VisualStudio");

                    if (vsKey != null)
                    {
                        RegistryKey versionKey = vsKey.OpenSubKey("14.0");

                        if (versionKey != null)
                        {
                            RegistryKey extensionManagerKey = versionKey.OpenSubKey("ExtensionManager");

                            if (extensionManagerKey != null)
                            {
                                RegistryKey enabledExtensionsKey = extensionManagerKey.OpenSubKey("EnabledExtensions");

                                if (enabledExtensionsKey != null)
                                {
                                    string[] valueNames = enabledExtensionsKey.GetValueNames();

                                    foreach (string valueName in valueNames)
                                    {
                                        if (valueName.StartsWith("NinjaCoderMvvmCross.vsix"))
                                        {
                                            return enabledExtensionsKey.GetValue(valueName) as string;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return string.Empty;
        }
    }
}
