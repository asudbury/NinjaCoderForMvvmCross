// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the  NinjaCoder_MvvmCross_VSPackagePackage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace ScorchioLimited.NinjaCoder_MvvmCross_VSPackage
{
    using EnvDTE80;
    using Microsoft.VisualStudio.Shell;
    using NinjaCoder.MvvmCross.Controllers;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using System;
    using System.ComponentModel.Design;
    using System.IO;
    using System.Runtime.InteropServices;

    using NinjaCoder.MvvmCross.Services;

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
    [Guid(GuidList.GuidNinjaCoderMvvmCrossVsPackagePkgString)]
    public sealed class NinjaCoder_MvvmCross_VSPackagePackage : Package
    {
        /// <summary>
        /// Gets or sets the vs instance.
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
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::Initialize START");

            base.Initialize();

            DTE2 dte2 = this.GetService(typeof(Microsoft.VisualStudio.Shell.Interop.SDTE)) as DTE2;

            if (dte2 == null)
            {
                TraceService.WriteError("DTE2 is null from this.GetService(typeof(DTE2))");
            }

            this.VsInstance = new VSInstance(dte2);

            string assemblyPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

            TraceService.WriteLine("assembly path=" + assemblyPath);

            NinjaController.SetWorkingDirectory(Path.GetDirectoryName(assemblyPath));
            
            SettingsService settingsService = new SettingsService();
            NinjaController.UseSimpleTextTemplatingEngine(settingsService.UseSimpleTextTemplatingEngine);

            OleMenuCommandService mcs = this.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            if (mcs == null)
            {
                return;
            }

            CommandID menuCommandId = new CommandID(GuidList.GuidNinjaCoderMvvmCrossVsPackageCmdSet, (int)PkgCmdIdList.AddProjects);
            MenuCommand menuItem = new MenuCommand(this.OnAddProjects, menuCommandId);
            mcs.AddCommand(menuItem);

            menuCommandId = new CommandID(GuidList.GuidNinjaCoderMvvmCrossVsPackageCmdSet, (int)PkgCmdIdList.AddViewModelsAndViews);
            menuItem = new MenuCommand(this.OnAddViewModelAndViews, menuCommandId);
            mcs.AddCommand(menuItem);

            menuCommandId = new CommandID(GuidList.GuidNinjaCoderMvvmCrossVsPackageCmdSet, (int)PkgCmdIdList.AddMvvmCrossPlugins);
            menuItem = new MenuCommand(this.OnAddMvvmCrossPlugins, menuCommandId);
            mcs.AddCommand(menuItem);

            menuCommandId = new CommandID(GuidList.GuidNinjaCoderMvvmCrossVsPackageCmdSet, (int)PkgCmdIdList.AddNugetPackages);
            menuItem = new MenuCommand(this.OnAddNugetPackages, menuCommandId);
            mcs.AddCommand(menuItem);

            menuCommandId = new CommandID(GuidList.GuidNinjaCoderMvvmCrossVsPackageCmdSet, (int)PkgCmdIdList.AddDependencyService);
            menuItem = new MenuCommand(this.OnAddDependencyService, menuCommandId);
            mcs.AddCommand(menuItem);

            menuCommandId = new CommandID(GuidList.GuidNinjaCoderMvvmCrossVsPackageCmdSet, (int)PkgCmdIdList.AddCustomRenderer);
            menuItem = new MenuCommand(this.OnAddCustomerRenderer, menuCommandId);
            mcs.AddCommand(menuItem);

            menuCommandId = new CommandID(GuidList.GuidNinjaCoderMvvmCrossVsPackageCmdSet, (int)PkgCmdIdList.Options);
            menuItem = new MenuCommand(this.OnOptions, menuCommandId);
            mcs.AddCommand(menuItem);

            menuCommandId = new CommandID(GuidList.GuidNinjaCoderMvvmCrossVsPackageCmdSet, (int)PkgCmdIdList.ViewLogFile);
            menuItem = new MenuCommand(this.OnViewLogFile, menuCommandId);
            mcs.AddCommand(menuItem);
            
            menuCommandId = new CommandID(GuidList.GuidNinjaCoderMvvmCrossVsPackageCmdSet, (int)PkgCmdIdList.ClearLogFile);
            menuItem = new MenuCommand(this.OnClearLogFile, menuCommandId);
            mcs.AddCommand(menuItem);

            menuCommandId = new CommandID(GuidList.GuidNinjaCoderMvvmCrossVsPackageCmdSet, (int)PkgCmdIdList.About);
            menuItem = new MenuCommand(this.OnAbout, menuCommandId);
            mcs.AddCommand(menuItem);

            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::Initialize END");
        }

        /// <summary>
        /// Called when [add projects].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnAddProjects(object sender, EventArgs e)
        {
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::OnAddProjects");
            NinjaController.RunProjectsController(this.VsInstance.ApplicationObject);
        }

        /// <summary>
        /// Called when [add view model and views].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnAddViewModelAndViews(object sender, EventArgs e)
        {
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::OnAddViewModelAndViews");
            NinjaController.RunViewModelViewsController(this.VsInstance.ApplicationObject);
        }

        /// <summary>
        /// Called when [add MVVM cross plugins].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnAddMvvmCrossPlugins(object sender, EventArgs e)
        {
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::OnAddMvvmCrossPlugins");
            NinjaController.RunPluginsController(this.VsInstance.ApplicationObject);
        }

        /// <summary>
        /// Called when [add nuget packages].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnAddNugetPackages(object sender, EventArgs e)
        {
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::OnAddNugetPackages");
            NinjaController.RunNugetPackagesController(this.VsInstance.ApplicationObject);
        }

        /// <summary>
        /// Called when [add dependency service].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnAddDependencyService(object sender, EventArgs e)
        {
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::OnAddDependencyService");
            NinjaController.RunDependencyServicesController(this.VsInstance.ApplicationObject);
        }

        /// <summary>
        /// Called when [add customer renderer].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnAddCustomerRenderer(object sender, EventArgs e)
        {
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::OnAddCustomerRenderer");
            NinjaController.RunCustomerRendererController(this.VsInstance.ApplicationObject);
        }

        /// <summary>
        /// Called when [options].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnOptions(object sender, EventArgs e)
        {
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::OnOptions");
            NinjaController.ShowOptions(this.VsInstance.ApplicationObject);
        }

        /// <summary>
        /// Called when [about].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnAbout(object sender, EventArgs e)
        {
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::OnAbout");
            NinjaController.ShowAboutBox(this.VsInstance.ApplicationObject);
        }

        /// <summary>
        /// Called when [view log file].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnViewLogFile(object sender, EventArgs e)
        {
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::OnViewLogFile");
            NinjaController.ViewLogFile();
        }

        /// <summary>
        /// Called when [clear log file].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnClearLogFile(object sender, EventArgs e)
        {
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::OnClearLogFile");
            NinjaController.ClearLogFile();
        }
    }
}