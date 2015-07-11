// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the  NinjaCoder_MvvmCross_VSPackagePackage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ScorchioLimited.NinjaCoder_MvvmCross_VSPackage
{
    using EnvDTE80;
    using Microsoft.VisualStudio.Shell;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using System;
    using System.ComponentModel.Design;
    using System.Runtime.InteropServices;

    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the information needed to show this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidNinjaCoder_MvvmCross_VSPackagePkgString)]
    public sealed class NinjaCoder_MvvmCross_VSPackagePackage : Package
    {
        /// <summary>
        /// Gets the visual studio instance.
        /// </summary>
        private VSInstance VsInstance { get; set; }

        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public NinjaCoder_MvvmCross_VSPackagePackage()
        {
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::Constructor");
        }

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initialization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::Initialize");

            base.Initialize();

            DTE2 dte2 = (DTE2)this.GetService(typeof(DTE2));

            this.VsInstance = new VSInstance(dte2);

            // Add our command handlers for menu (commands must exist in the .vsct file)
            OleMenuCommandService mcs = this.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            if (mcs == null)
            {
                return;
            }

            // Create the command for the menu item.
            CommandID menuCommandId = new CommandID(GuidList.guidNinjaCoder_MvvmCross_VSPackageCmdSet, (int)PkgCmdIDList.NinjaCoderForMvvmCrossCommand);
            MenuCommand menuItem = new MenuCommand(this.MenuItemCallback, menuCommandId);
            mcs.AddCommand(menuItem);
        }

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void MenuItemCallback(object sender, EventArgs e)
        {
            ////NinjaController.RunProjectsController(this.VsInstance.ApplicationObject);
        }
    }
}
