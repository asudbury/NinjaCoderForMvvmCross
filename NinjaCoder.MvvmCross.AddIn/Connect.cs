// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Connect type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.AddIn
{
    using System.Windows.Forms;
    using Controllers;
    using Microsoft.VisualStudio.CommandBars;

    using Scorchio.VisualStudio;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Services;

    /// <summary>
    ///    Defines the Connect type.
    /// </summary>
    public class Connect : CommandManager
    {
        /// <summary>
        /// The command position
        /// </summary>
        private int commandPosition = 1;

        /// <summary>
        /// Initializes a new instance of the <see cref="Connect"/> class.
        /// </summary>
        public Connect()
        {
            TraceService.WriteLine("Connect::Constructor Version=" + this.ApplicationVersion);  
        }

        /// <summary>
        /// Gets the application version.
        /// </summary>
        private string ApplicationVersion
        {
            get
            {
                SettingsService settingsService = new SettingsService();

                return settingsService.ApplicationVersion;
            }
        }

        /// <summary>
        /// Starts up.
        /// </summary>
        public override void StartUp()
        {
            TraceService.WriteLine("Connect::StartUp Version=" + this.ApplicationVersion);
        }

        /// <summary>
        /// UIs the setup.
        /// </summary>
        public override void UISetup()
        {
            TraceService.WriteLine("Connect::UISetup Version=" + this.ApplicationVersion);
        }

        /// <summary>
        /// After the start up.
        /// </summary>
        public override void AfterStartUp()
        {
            TraceService.WriteLine("Connect::AfterStartUp Version=" + this.ApplicationVersion);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            TraceService.WriteLine("Connect::Initialize Version=" + this.ApplicationVersion);

            NinjaController.RunConfigurationController(this.VSInstance.ApplicationObject);

            TraceService.WriteLine("Connect::Initialize Post ConfigurationController::Run");

            this.AddCommands();
        }

        /// <summary>
        /// Adds the commands.
        /// </summary>
        internal void AddCommands()
        {
            TraceService.WriteLine("Connect::AddCommands Version=" + this.ApplicationVersion);

            CommandBar commandBar = this.AddCommandBar("Ninja Coder for MvvmCross");

            VSCommandInfo commandInfo = new VSCommandInfo
            {
                AddIn = this.AddInInstance,
                Name = "NinjaCoderforMvvmCrossAddProjects",
                ButtonText = "Add Projects",
                Tooltip = "Ninja Coder for MvvmCross Add Projects",
                Action = this.BuildProjects,
                ParentCommand = commandBar,
                BitmapResourceId = 183,
            };

            this.AddMenuItem(commandInfo);

            commandInfo = new VSCommandInfo
            {
                AddIn = this.AddInInstance,
                Name = "NinjaCoderforMvvmCrossAddViewModel",
                ButtonText = "Add ViewModel and Views",
                Tooltip = "Ninja Coder for MvvmCross Add ViewModel and Views",
                Action = this.AddViewModelAndViews,
                ParentCommand = commandBar,
                BitmapResourceId = 303,
            };

            this.AddMenuItem(commandInfo);

            commandInfo = new VSCommandInfo
            {
                AddIn = this.AddInInstance,
                Name = "NinjaCoderforMvvmCrossAddPlugins",
                ButtonText = "Add Plugins",
                Tooltip = "Ninja Coder for MvvmCross Add Plugins",
                Action = this.AddPlugins,
                ParentCommand = commandBar,
                BitmapResourceId = 450,
            };

            this.AddMenuItem(commandInfo);

            /*commandInfo = new VSCommandInfo
            {
                AddIn = this.AddInInstance,
                Name = "NinjaCoderforMvvmCrossAddServices",
                ButtonText = "Add Services",
                Tooltip = "Ninja Coder for MvvmCross Add Services",
                Action = this.AddServices,
                ParentCommand = commandBar,
                BitmapResourceId = 450,
            };

            this.AddMenuItem(commandInfo);*/
            
            commandInfo = new VSCommandInfo
            {
                AddIn = this.AddInInstance,
                Name = "NinjaCoderforMvvmCrossAddConverters",
                ButtonText = "Add Converters",
                Tooltip = "Ninja Coder for MvvmCross Add Converters",
                Action = this.AddConverters,
                ParentCommand = commandBar,
                BitmapResourceId = 450,
            };

            this.AddMenuItem(commandInfo);

            commandInfo = new VSCommandInfo
            {
                AddIn = this.AddInInstance,
                Name = "NinjaCoderforMvvmCrossOptions",
                ButtonText = "Options",
                Tooltip = "Ninja Coder for MvvmCross Options",
                Action = this.ShowOptions,
                ParentCommand = commandBar,
                BitmapResourceId = 642
            };

            this.AddMenuItem(commandInfo);

            commandInfo = new VSCommandInfo
            {
                AddIn = this.AddInInstance,
                Name = "NinjaCoderforMvvmCrossHelp",
                ButtonText = "Help",
                Tooltip = "Ninja Coder for MvvmCross Help",
                Action = this.ShowHelp,
                ParentCommand = commandBar,
                BitmapResourceId = 1954,
                BeginGroup = true
            };

            this.AddMenuItem(commandInfo);
            
            commandInfo = new VSCommandInfo
            {
                AddIn = this.AddInInstance,
                Name = "NinjaCoderforMvvmCrossAbout",
                ButtonText = "About",
                Tooltip = "Ninja Coder for MvvmCross About",
                Action = this.ShowAbout,
                ParentCommand = commandBar,
                BitmapResourceId = 0
            };

            this.AddMenuItem(commandInfo);

            TraceService.WriteLine("Connect::AddCommands Ended");
        }

        /// <summary>
        /// Builds Projects.
        /// </summary>
        internal void BuildProjects()
        {
            TraceService.WriteLine("Connect::BuildProjects Version=" + this.ApplicationVersion);

            NinjaController.RunProjectsController(this.VSInstance.ApplicationObject);
        }

        /// <summary>
        /// Adds the view model and views.
        /// </summary>
        internal void AddViewModelAndViews()
        {
            TraceService.WriteLine("Connect::AddViewModelAndViews Version=" + this.ApplicationVersion);

            NinjaController.RunViewModelViewsController(this.VSInstance.ApplicationObject);
        }

        /// <summary>
        /// Add the plugins.
        /// </summary>
        internal void AddPlugins()
        {
            TraceService.WriteLine("Connect::AddPlugins Version=" + this.ApplicationVersion);

            NinjaController.RunPluginsController(this.VSInstance.ApplicationObject);
        }

        /// <summary>
        /// Adds the services.
        /// </summary>
        internal void AddServices()
        {
            TraceService.WriteLine("Connect::AddServices Version=" + this.ApplicationVersion);

            NinjaController.RunServicesController(this.VSInstance.ApplicationObject);
        }

        /// <summary>
        /// Adds the converters.
        /// </summary>
        internal void AddConverters()
        {
            TraceService.WriteLine("Connect::AddConverters Version=" + this.ApplicationVersion);

            NinjaController.RunConvertersController(this.VSInstance.ApplicationObject);
        }

        /// <summary>
        /// Shows the options.
        /// </summary>
        internal void ShowOptions()
        {
            TraceService.WriteLine("Connect::ShowOptions Version=" + this.ApplicationVersion);

            NinjaController.ShowOptions(this.VSInstance.ApplicationObject);
        }

        /// <summary>
        /// Shows help.
        /// </summary>
        internal void ShowHelp()
        {
            TraceService.WriteLine("Connect::ShowHelp Version=" + this.ApplicationVersion);
            MessageBox.Show(this.ApplicationVersion);
        }

        /// <summary>
        /// Shows about.
        /// </summary>
        internal void ShowAbout()
        {
            TraceService.WriteLine("Connect::ShowAbout Version=" + this.ApplicationVersion);

            NinjaController.ShowAboutBox(this.VSInstance.ApplicationObject);
        }

        /// <summary>
        /// Add item to the menu.
        /// </summary>
        /// <param name="vsCommandInfo">The command info.</param>
        protected override void AddMenuItem(VSCommandInfo vsCommandInfo)
        {
            vsCommandInfo.Position = this.commandPosition;
            base.AddMenuItem(vsCommandInfo);
            this.commandPosition++;
        }
    }
}