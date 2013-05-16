// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CommandManager type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Scorchio.VisualStudio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Resources;
    using System.Runtime.InteropServices;

    using EnvDTE;
    using EnvDTE80;
    using Extensibility;

    using Microsoft.VisualStudio.CommandBars;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;

    using stdole;

    /// <summary>
    ///  Defines the ConnectBase type.
    /// </summary>
    [ComVisible(true)]
    [ClassInterface(ClassInterfaceType.None)]
    public class CommandManager : IDispatch, IDTExtensibility2, IDTCommandTarget
    {
        /// <summary>
        /// The command infos
        /// </summary>
        private readonly List<VSCommandInfo> commandInfos = new List<VSCommandInfo>();

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandManager" /> class.
        /// </summary>
        protected CommandManager()
        {
            TraceService.WriteLine("CommandManager::Constructor");  
        }

        /// <summary>
        /// Gets the visual studio instance.
        /// </summary>
        public VSInstance VSInstance { get; private set; }

        /// <summary>
        /// Gets the AddInInstance.
        /// </summary>
        public AddIn AddInInstance { get; private set; }

        /// <summary>
        /// Sets the resource assembly.
        /// </summary>
        public Assembly ResourceAssembly 
        { 
            set
            {
                this.ResourceManager = new ResourceManager(value.GetName().Name + ".Resources", value);                       
            }
        }

        /// <summary>
        /// Gets or sets the resource manager.
        /// </summary>
        public ResourceManager ResourceManager { get; set; }                   

        /// <summary>
        /// Called when [connection].
        /// </summary>
        /// <param name="application">The application.</param>
        /// <param name="connectMode">The connect mode.</param>
        /// <param name="addInInst">The add in inst.</param>
        /// <param name="custom">The custom.</param>
        public void OnConnection(
            object application, 
            ext_ConnectMode connectMode, 
            object addInInst, 
            ref Array custom)
        {
            this.VSInstance = new VSInstance((DTE2)application);
            this.AddInInstance = (AddIn)addInInst;
            
            TraceService.WriteLine("--------------------------------------------------------");
            TraceService.WriteLine("CommandManager::OnConnection ConnectMode=" + connectMode);
            TraceService.WriteLine("--------------------------------------------------------");
            
            switch (connectMode)
            {
                case ext_ConnectMode.ext_cm_UISetup:
                    //// Do nothing for this add-in with temporary user interface

                    //// Initialize for permanent user interface

                    this.UISetup();
                    this.Initialize();
                    break;

                case ext_ConnectMode.ext_cm_Startup:
                    //// The add-in was marked to load on startup
                    //// Do nothing at this point because the IDE may not be fully initialized
                    //// Visual Studio will call OnStartupComplete when fully initialized
                    
                    this.StartUp();
                    this.Initialize();
                    break;

                case ext_ConnectMode.ext_cm_AfterStartup:
                    //// The add-in was loaded by hand after startup using the Add-In Manager
                    //// Initialize it in the same way that when is loaded on startup
                    
                    this.AfterStartUp();
                    this.Initialize();
                    break;
            }
        }

        /// <summary>
        /// UIs the setup.
        /// </summary>
        public virtual void UISetup()
        {
            TraceService.WriteLine("CommandManager::UISetup");
        }
    
        /// <summary>
        /// Starts up.
        /// </summary>
        public virtual void StartUp()
        {
            TraceService.WriteLine("CommandManager::StartUp");
        }

        /// <summary>
        /// After the start up.
        /// </summary>
        public virtual void AfterStartUp()
        {
            TraceService.WriteLine("CommandManager::AfterStartUp");
        }

        /// <summary>
        /// Initalizes this instance.
        /// </summary>
        public virtual void Initialize()
        {
            TraceService.WriteLine("CommandManager::Initialize");
        }

        /// <summary>
        /// Adds the command bar.
        /// </summary>
        /// <param name="commandName">Name of the command.</param>
        /// <returns>The command bar.</returns>
        public CommandBar AddCommandBar(string commandName)
        {
            TraceService.WriteLine("CommandManager::AddCommandBar commandName=" + commandName);

            Commands2 commands = (Commands2)this.VSInstance.ApplicationObject.Commands;
            CommandBarPopup toolsMenuPopUp = this.VSInstance.ApplicationObject.GetToolsMenuPopUp();

            CommandBar commandBar = null;

            for (int i = 1; i <= toolsMenuPopUp.CommandBar.Controls.Count; i++)
            {
                if (toolsMenuPopUp.CommandBar.Controls[i].Caption == commandName)
                {
                    TraceService.WriteLine("CommandManager::AddCommandBar commandFound in collection");
                    CommandBarPopup commandBarPopup = (CommandBarPopup)toolsMenuPopUp.CommandBar.Controls[i];

                    commandBar = commandBarPopup.CommandBar;
                    break;
                }
            }

            if (commandBar == null)
            {
                TraceService.WriteLine("CommandManager::AddCommandBar creating command");
                return (CommandBar)commands.AddCommandBar(commandName, vsCommandBarType.vsCommandBarTypeMenu, toolsMenuPopUp.CommandBar);
            }

            return commandBar;
        }

        /// <summary>
        /// Add item to the menu.
        /// </summary>
        /// <param name="vsCommandInfo">The command info.</param>
        /// <returns>The command</returns>
        public void AddMenuItem(VSCommandInfo vsCommandInfo)
        {
            TraceService.WriteLine("CommandManager::AddMenuItem Name=" + vsCommandInfo.Name);

            //// This try/catch block can be duplicated if you wish to add multiple commands to be handled by your Add-in,
            //// just make sure you also update the QueryStatus/Exec method to include the new command names.
            try
            {
                Commands2 commands = (Commands2)this.VSInstance.ApplicationObject.Commands;
                object[] context = new object[] { };

                TraceService.WriteLine("CommandManager::AddMenuItem Adding command name=" + vsCommandInfo.Name);

                IEnumerable<Command> currentCommands = commands.Cast<Command>();

                foreach (Command command in currentCommands
                    .Where(command => command.Name == vsCommandInfo.Name))
                {
                    TraceService.WriteLine("CommandManager::AddMenuItem Deleting Command already in memory name=" + vsCommandInfo.Name);
                    command.Delete();
                    break;
                }
  
                vsCommandInfo.Command = commands.AddNamedCommand2(
                        vsCommandInfo.AddIn,
                        vsCommandInfo.Name,
                        vsCommandInfo.ButtonText,
                        vsCommandInfo.Tooltip,
                        vsCommandInfo.UseOfficeResources,
                        vsCommandInfo.BitmapResourceId,
                        ref context);

                vsCommandInfo.Command.AddControl(vsCommandInfo.ParentCommand, vsCommandInfo.Position);
                
                this.commandInfos.Add(vsCommandInfo);
            }
            catch (ArgumentException exception)
            {
                //// If we are here, then the exception is probably because a command with that name
                //// already exists. If so there is no need to recreate the command and we can 
                //// safely ignore the exception.
                
                TraceService.WriteError("CommandManager::AddMenuItem ArgumentException");
                TraceService.WriteLine("commandName=" + vsCommandInfo.Name);
                TraceService.WriteError("message=" + exception.Message);
                TraceService.WriteError("parameterName=" + exception.ParamName);
                TraceService.WriteError("stackTrace=" + exception.StackTrace);
            } 
        }

        /// <summary>
        /// Called when [disconnection].
        /// </summary>
        /// <param name="removeMode">The remove mode.</param>
        /// <param name="custom">The custom.</param>
        public void OnDisconnection(
            ext_DisconnectMode removeMode, 
            ref Array custom)
        {
            TraceService.WriteLine("CommandManager::OnDisconnection removeMode" + removeMode);

            if (removeMode == ext_DisconnectMode.ext_dm_HostShutdown ||
                removeMode == ext_DisconnectMode.ext_dm_UserClosed)
            {
                foreach (VSCommandInfo vsCommandInfo in this.commandInfos)
                {
                    if (vsCommandInfo.Command != null)
                    {
                        TraceService.WriteLine("CommandManager::OnDisconnection deleting command " + vsCommandInfo.Name);
                        vsCommandInfo.Command.Delete();
                    }
                }
            }
        }

        /// <summary>
        /// Called when [add ins update].
        /// </summary>
        /// <param name="custom">The custom.</param>
        public void OnAddInsUpdate(ref Array custom)
        {
            TraceService.WriteLine("CommandManager::OnAddInsUpdate");  
        }

        /// <summary>
        /// Called when [startup complete].
        /// </summary>
        /// <param name="custom">The custom.</param>
        public void OnStartupComplete(ref Array custom)
        {
            TraceService.WriteLine("CommandManager::OnStartupComplete");  
        }

        /// <summary>
        /// Called when [begin shutdown].
        /// </summary>
        /// <param name="custom">The custom.</param>
        public void OnBeginShutdown(ref Array custom)
        {
            TraceService.WriteLine("CommandManager::OnBeginShutdown");  
        }

        /// <summary>
        /// Queries the status.
        /// </summary>
        /// <param name="commandName">ProjectName of the command.</param>
        /// <param name="neededText">The needed text.</param>
        /// <param name="status">The status option.</param>
        /// <param name="commandText">The command text.</param>
        public void QueryStatus(
            string commandName, 
            vsCommandStatusTextWanted neededText, 
            ref vsCommandStatus status, 
            ref object commandText)
        {
            try
            {
                if (neededText != vsCommandStatusTextWanted.vsCommandStatusTextWantedNone)
                {
                    return;
                }

                if (this.commandInfos.Any(vsCommandInfo => vsCommandInfo.Command.Name == commandName))
                {
                    status = vsCommandStatus.vsCommandStatusSupported | vsCommandStatus.vsCommandStatusEnabled;
                }
            }
            catch (Exception exception)
            {
                TraceService.WriteError("CommandManager::QueryStatus exception=" + exception.Message);
            }
        }

        /// <summary>
        /// Execs the specified command name.
        /// </summary>
        /// <param name="commandName">ProjectName of the command.</param>
        /// <param name="executeOption">The execute option.</param>
        /// <param name="variantIn">The variant in.</param>
        /// <param name="variantOut">The variant out.</param>
        /// <param name="handled">if set to <c>true</c> [handled].</param>
        public void Exec(
            string commandName, 
            vsCommandExecOption executeOption, 
            ref object variantIn, 
            ref object variantOut, 
            ref bool handled)
        {
            TraceService.WriteLine("CommandManager::Exec commandName=" + commandName);  

            if (executeOption != vsCommandExecOption.vsCommandExecOptionDoDefault)
            {
                return;
            }

            foreach (VSCommandInfo vsCommandInfo in this.commandInfos
                .Where(vsCommandInfo => vsCommandInfo.Command.Name == commandName))
            {
                if (vsCommandInfo.Action != null)
                {
                    vsCommandInfo.Action();
                }

                handled = true;
                break;
            }
        }
    }
}
