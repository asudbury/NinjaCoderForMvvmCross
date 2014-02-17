// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Connect type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.AddIn
{
    using Controllers;
    using Microsoft.VisualStudio.CommandBars;

    using NinjaCoder.MvvmCross.Infrastructure.Services;

    using Scorchio.VisualStudio;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;

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
            TraceService.WriteLine("Connect::Constructor");
        }
        /// <summary>
        /// Starts up.
        /// </summary>
        public override void StartUp()
        {
            TraceService.WriteLine("Connect::StartUp");
        }

        /// <summary>
        /// UIs the setup.
        /// </summary>
        public override void UISetup()
        {
            TraceService.WriteLine("Connect::UISetup");
        }

        /// <summary>
        /// After the start up.
        /// </summary>
        public override void AfterStartUp()
        {
            TraceService.WriteLine("Connect::AfterStartUp");
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            TraceService.WriteLine("Connect::Initialize");

            this.AddCommands();
        }

        /// <summary>
        /// Adds the commands.
        /// </summary>
        internal void AddCommands()
        {
            TraceService.WriteLine("Connect::AddCommands");

            CommandBar commandBar = this.AddCommandBar("Ninja Coder for MvvmCross");

            VSCommandInfo commandInfo = new VSCommandInfo
            {
                AddIn = this.AddInInstance,
                Name = "NinjaCoderforMvvmCrossAddProjects",
                ButtonText = "Add Projects",
                Tooltip = "Ninja Coder for MvvmCross Add Projects",
                Action = this.BuildProjects,
                ParentCommand = commandBar,
                BitmapResourceId = 0,
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
                BitmapResourceId = 0,
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
                BitmapResourceId = 0,
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
                BitmapResourceId = 0,
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
                BitmapResourceId = 0,
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
                BitmapResourceId = 0
            };

            this.AddMenuItem(commandInfo);

            SettingsService settingsService = new SettingsService();

            if (settingsService.ShowViewLogFileOnVisualStudioMenu)
            {
                commandInfo = new VSCommandInfo
                {
                    AddIn = this.AddInInstance,
                    Name = "NinjaCoderforMvvmCrossViewLogFile",
                    ButtonText = "View Log File",
                    Tooltip = "Ninja Coder for MvvmCross View Log File",
                    Action = this.ViewLogFile,
                    ParentCommand = commandBar,
                    BitmapResourceId = 0
                };

                this.AddMenuItem(commandInfo);
            }

            if (settingsService.ShowClearLogFileOnVisualStudioMenu)
            {
                commandInfo = new VSCommandInfo
                {
                    AddIn = this.AddInInstance,
                    Name = "NinjaCoderforMvvmCrossClearLogFile",
                    ButtonText = "Clear Log File",
                    Tooltip = "Ninja Coder for MvvmCross Clear Log File",
                    Action = this.ClearLogFile,
                    ParentCommand = commandBar,
                    BitmapResourceId = 0
                };

                this.AddMenuItem(commandInfo);
            }

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
            TraceService.WriteLine("Connect::BuildProjects");

            NinjaController.RunProjectsController(this.VSInstance.ApplicationObject);
        }

        /// <summary>
        /// Adds the view model and views.
        /// </summary>
        internal void AddViewModelAndViews()
        {
            TraceService.WriteLine("Connect::AddViewModelAndViews");

            NinjaController.RunViewModelViewsController(this.VSInstance.ApplicationObject);
        }

        /// <summary>
        /// Add the plugins.
        /// </summary>
        internal void AddPlugins()
        {
            TraceService.WriteLine("Connect::AddPlugins");

            NinjaController.RunPluginsController(this.VSInstance.ApplicationObject);
        }

        /// <summary>
        /// Adds the services.
        /// </summary>
        internal void AddServices()
        {
            TraceService.WriteLine("Connect::AddServices");

            NinjaController.RunServicesController(this.VSInstance.ApplicationObject);
        }

        /// <summary>
        /// Adds the converters.
        /// </summary>
        internal void AddConverters()
        {
            TraceService.WriteLine("Connect::AddConverters");

            NinjaController.RunConvertersController(this.VSInstance.ApplicationObject);
        }

        /// <summary>
        /// Shows the options.
        /// </summary>
        internal void ShowOptions()
        {
            TraceService.WriteLine("Connect::ShowOptions");

            NinjaController.ShowOptions(this.VSInstance.ApplicationObject);
        }

        /// <summary>
        /// Shows about.
        /// </summary>
        internal void ShowAbout()
        {
            TraceService.WriteLine("Connect::ShowAbout");

            NinjaController.ShowAboutBox(this.VSInstance.ApplicationObject);
        }

        /// <summary>
        /// Views the log file.
        /// </summary>
        internal void ViewLogFile()
        {
            TraceService.WriteLine("Connect::ViewLogFile");

            NinjaController.ViewLogFile();
        }

        /// <summary>
        /// Clears the log file.
        /// </summary>
        internal void ClearLogFile()
        {
            TraceService.WriteLine("Connect::ClearLogFile");

            NinjaController.ClearLogFile();
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