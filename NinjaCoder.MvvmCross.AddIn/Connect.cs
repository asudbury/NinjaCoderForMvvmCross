// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Connect type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.AddIn
{
    using System.Windows.Forms;

    using Microsoft.VisualStudio.CommandBars;

    using NinjaCoder.MvvmCross.Controllers;

    using Scorchio.VisualStudio;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///    Defines the Connect type.
    /// </summary>
    public class Connect : CommandManager
    {
        /// <summary>
        /// Version Number.
        /// </summary>
        private const string Version = "Version 1.0.2";

        /// <summary>
        /// Initializes a new instance of the <see cref="Connect"/> class.
        /// </summary>
        public Connect()
        {
            TraceService.WriteLine("Connect::Constructor Version=" + Version);  
        }

        /// <summary>
        /// Starts up.
        /// </summary>
        public override void StartUp()
        {
            TraceService.WriteLine("Connect::StartUp Version=" + Version);
        }

        /// <summary>
        /// UIs the setup.
        /// </summary>
        public override void UISetup()
        {
            TraceService.WriteLine("Connect::UISetup Version=" + Version);
        }

        /// <summary>
        /// After the start up.
        /// </summary>
        public override void AfterStartUp()
        {
            TraceService.WriteLine("Connect::AfterStartUp Version=" + Version);
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public override void Initialize()
        {
            TraceService.WriteLine("Connect::Initialize Version=" + Version);
            this.AddCommands();
        }

        /// <summary>
        /// Adds the commands.
        /// </summary>
        internal void AddCommands()
        {
            TraceService.WriteLine("Connect::AddCommands Version=" + Version);

            CommandBar commandBar = this.AddCommandBar("Ninja Coder for MvvmCross");

            VSCommandInfo commandInfo = new VSCommandInfo
            {
                AddIn = this.AddInInstance,
                Name = "NinjaCoderforMvvmCrossAddProjects",
                ButtonText = "Add Projects",
                Tooltip = "Ninja Coder for MvvmCross Add Projects",
                Action = this.BuildProjects,
                ParentCommand = commandBar,
                Position = 1,
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
                Position = 2,
                BitmapResourceId = 303,
            };

            this.AddMenuItem(commandInfo);

            commandInfo = new VSCommandInfo
            {
                AddIn = this.AddInInstance,
                Name = "NinjaCoderforMvvmCrossAddConverters",
                ButtonText = "Add Converters",
                Tooltip = "Ninja Coder for MvvmCross Add Converters",
                Action = this.AddConverters,
                ParentCommand = commandBar,
                Position = 3,
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
                Position = 4,
                BitmapResourceId = 642
            };

            this.AddMenuItem(commandInfo);

            commandInfo = new VSCommandInfo
            {
                AddIn = this.AddInInstance,
                Name = "NinjaCoderforMvvmCrossStackOverFlow",
                ButtonText = "Stack Overflow",
                Tooltip = "Ninja Coder for MvvmCross Stack Overflow",
                Action = this.ShowStackOverFlow,
                ParentCommand = commandBar,
                Position = 5,
                BitmapResourceId = 1445,
                BeginGroup = true
            };

            this.AddMenuItem(commandInfo);

            commandInfo = new VSCommandInfo
            {
                AddIn = this.AddInInstance,
                Name = "NinjaCoderforMvvmCrossJabbrRoom",
                ButtonText = "JAbbR",
                Tooltip = "Ninja Coder for MvvmCross JAbbR Room",
                Action = this.ShowJabbrRoom,
                ParentCommand = commandBar,
                Position = 6,
                BitmapResourceId = 1445
            };

            this.AddMenuItem(commandInfo);

            commandInfo = new VSCommandInfo
            {
                AddIn = this.AddInInstance,
                Name = "NinjaCoderforMvvmCrossGitHub",
                ButtonText = "GitHub",
                Tooltip = "Ninja Coder for MvvmCross GitHub",
                Action = this.ShowGitHub,
                ParentCommand = commandBar,
                Position = 7,
                BitmapResourceId = 1445
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
                Position = 8,
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
                Position = 9,
                BitmapResourceId = 0
            };

            this.AddMenuItem(commandInfo);
        }

        /// <summary>
        /// Gets the controller.
        /// </summary>
        /// <value>
        /// The controller.
        /// </value>
        internal MvvmCrossController Controller
        {
            get
            {
                return new MvvmCrossController
                {
                    VisualStudio = this.VSInstance.ApplicationObject
                };            
            }
        }

        /// <summary>
        /// Builds Projects.
        /// </summary>
        internal void BuildProjects()
        {
            TraceService.WriteLine("Connect::BuildProjects Version=" + Version);  
            this.Controller.BuildProjects();
        }

        /// <summary>
        /// Adds the view model and views.
        /// </summary>
        internal void AddViewModelAndViews()
        {
            TraceService.WriteLine("Connect::AddViewModelAndViews");
            this.Controller.AddViewModelAndViews();
        }

        /// <summary>
        /// Adds the converters.
        /// </summary>
        internal void AddConverters()
        {
            TraceService.WriteLine("Connect::AddConverters");
            this.Controller.AddConverters();
        }

        /// <summary>
        /// Shows the stack over flow.
        /// </summary>
        internal void ShowStackOverFlow()
        {
            TraceService.WriteLine("Connect::StackOverFlow");
            this.Controller.ShowStackOverFlow();
        }

        /// <summary>
        /// Shows the Jabbr room.
        /// </summary>
        internal void ShowJabbrRoom()
        {
            TraceService.WriteLine("Connect::ShowJabbrRoom");
            this.Controller.ShowJabbrRoom();
        }

        /// <summary>
        /// Shows the GitHub.
        /// </summary>
        internal void ShowGitHub()
        {
            TraceService.WriteLine("Connect::ShowGitHub");
            this.Controller.ShowGitHub();
        }

        /// <summary>
        /// Shows the options.
        /// </summary>
        internal void ShowOptions()
        {
            TraceService.WriteLine("Connect::ShowOptions");
            this.Controller.ShowOptions();
        }

        /// <summary>
        /// Shows help.
        /// </summary>
        internal void ShowHelp()
        {
            TraceService.WriteLine("Connect::ShowHelp");
            MessageBox.Show(Version);
        }

        /// <summary>
        /// Shows about.
        /// </summary>
        internal void ShowAbout()
        {
            TraceService.WriteLine("Connect::ShowAbout");
            MessageBox.Show(Version);
        }
    }
}