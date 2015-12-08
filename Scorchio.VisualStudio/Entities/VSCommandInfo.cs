// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the VSCommandInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.VisualStudio.Entities
{
    using EnvDTE;
    using EnvDTE80;
    using Microsoft.VisualStudio.CommandBars;

    /// <summary>
    ///  Defines the VSCommand type.
    /// </summary>
    public class VSCommandInfo
    {
        /// <summary>
        /// default Office bitmap id for menu
        /// </summary>
        private int bitmapResourceId = 59;

        /// <summary>
        /// USe Office "location" for resources ?
        /// </summary>
        private bool useOfficeResources = true;

        /// <summary>
        /// The command status value
        /// </summary>
        private int commandStatusValue = 3;

        /// <summary>
        /// The command style flags
        /// </summary>
        private int commandStyleFlags = 3;

        /// <summary>
        /// The control type
        /// </summary>
        private vsCommandControlType controlType = vsCommandControlType.vsCommandControlTypeButton;

        /// <summary>
        ///  Delegate used to perform action on one command.
        /// </summary>
        public delegate void VSCommandDelegate();

        /// <summary>
        /// Gets or sets the owner.
        /// </summary>
        public object Owner { get; set; }

        /// <summary>
        /// Gets or sets the add in.
        /// </summary>
        public AddIn AddIn { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the button text.
        /// </summary>
        public string ButtonText { get; set; }

        /// <summary>
        /// Gets or sets the tooltip.
        /// </summary>
        public string Tooltip { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether to UseOfficeResources.
        /// </summary>
        public bool UseOfficeResources
        {
            get { return this.useOfficeResources; }
            set { this.useOfficeResources = value; }
        }

        /// <summary>
        /// Gets or sets the bitmap.
        /// </summary>
        public int BitmapResourceId
        {
            get { return this.bitmapResourceId; }
            set { this.bitmapResourceId = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [begin group].
        /// </summary>
        public bool BeginGroup { get; set; }

        /// <summary>
        /// Gets or sets the command status value.
        /// </summary>
        public int VsCommandStatusValue
        {
            get { return this.commandStatusValue; }
            set { this.commandStatusValue = value; }
        }

        /// <summary>
        /// Gets or sets the command style flags.
        /// </summary>
        public int CommandStyleFlags
        {
            get { return this.commandStyleFlags; }
            set { this.commandStyleFlags = value; }
        }

        /// <summary>
        /// Gets or sets the type of the control.
        /// </summary>
        public vsCommandControlType ControlType
        {
            get { return this.controlType; }
            set { this.controlType = value; }            
        }

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        public Command Command { get; set; }

        /// <summary>
        /// Gets or sets the parent command.
        /// </summary>
        public CommandBar ParentCommand { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public int Position { get; set; }

        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        public VSCommandDelegate Action { get; set; }
    }
}
