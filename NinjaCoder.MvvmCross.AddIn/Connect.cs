// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Connect type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

using System;

namespace NinjaCoder.MvvmCross.AddIn
{
    using System.Windows.Forms;

    using Microsoft.VisualStudio.CommandBars;

    using Controllers;

    using Scorchio.VisualStudio;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///    Defines the Connect type.
    /// </summary>
    public class Connect : CommandManager
    {
        private string ApplicationVersion
        {
            get 
            { 
                Version version = typeof(MvvmCrossController).Assembly.GetName().Version;
 
                return "Version " + version.Major + "." + version.Minor + "." + version.Revision;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Connect"/> class.
        /// </summary>
        public Connect()
        {
            TraceService.WriteLine("Connect::Constructor Version=" + this.ApplicationVersion);  
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
            this.Controller.Initialize();
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
                Name = "NinjaCoderforMvvmCrossAddPlugins",
                ButtonText = "Add Plugins",
                Tooltip = "Ninja Coder for MvvmCross Add Plugins",
                Action = this.AddPlugins,
                ParentCommand = commandBar,
                Position = 3,
                BitmapResourceId = 450,
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
                Position = 4,
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
                Position = 5,
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
                Position = 6,
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
                Position = 7,
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
                    DTE2 = this.VSInstance.ApplicationObject
                };            
            }
        }

        /// <summary>
        /// Builds Projects.
        /// </summary>
        internal void BuildProjects()
        {
            TraceService.WriteLine("Connect::BuildProjects Version=" + this.ApplicationVersion);  
            this.Controller.BuildProjects();
        }

        /// <summary>
        /// Adds the view model and views.
        /// </summary>
        internal void AddViewModelAndViews()
        {
            TraceService.WriteLine("Connect::AddViewModelAndViews Version=" + this.ApplicationVersion);  
            this.Controller.AddViewModelAndViews();
        }

        /// <summary>
        /// Add the plugins.
        /// </summary>
        internal void AddPlugins()
        {
            TraceService.WriteLine("Connect::AddPlugins Version=" + this.ApplicationVersion); 
            this.Controller.AddPlugins();   
        }

        /// <summary>
        /// Adds the converters.
        /// </summary>
        internal void AddConverters()
        {
            TraceService.WriteLine("Connect::AddConverters Version=" + this.ApplicationVersion); 
            this.Controller.AddConverters();
        }

        /// <summary>
        /// Shows the options.
        /// </summary>
        internal void ShowOptions()
        {
            TraceService.WriteLine("Connect::ShowOptions Version=" + this.ApplicationVersion); 
            this.Controller.ShowOptions();
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
            MessageBox.Show(this.ApplicationVersion);
        }
    }
}