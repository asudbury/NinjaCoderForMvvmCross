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

    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    [PackageRegistration(UseManagedResourcesOnly = true)]
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.GuidNinjaCoderMvvmCrossVsPackagePkgString)]
    public sealed class NinjaCoder_MvvmCross_VSPackagePackage : Package
    {
        /// <summary>
        /// Gets or sets the vs instance.
        /// </summary>
        private VSInstance VsInstance { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NinjaCoder_MvvmCross_VSPackagePackage"/> class.
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
            
            OleMenuCommandService mcs = this.GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            if (mcs == null)
            {
                TraceService.WriteError("NinjaCoder_MvvmCross_VSPackagePackage::menuCommandService is null");
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

            menuCommandId = new CommandID(GuidList.GuidNinjaCoderMvvmCrossVsPackageCmdSet, (int)PkgCmdIdList.AddEffect);
            menuItem = new MenuCommand(this.OnAddEffect, menuCommandId);
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

            menuCommandId = new CommandID(GuidList.GuidNinjaCoderMvvmCrossVsPackageCmdSet, (int)PkgCmdIdList.ViewErrorLog);
            menuItem = new MenuCommand(this.OnViewErrorLogFile, menuCommandId);
            mcs.AddCommand(menuItem);

            menuCommandId = new CommandID(GuidList.GuidNinjaCoderMvvmCrossVsPackageCmdSet, (int)PkgCmdIdList.ClearErrorLog);
            menuItem = new MenuCommand(this.OnClearErrorLogFile, menuCommandId);
            mcs.AddCommand(menuItem);

            menuCommandId = new CommandID(GuidList.GuidNinjaCoderMvvmCrossVsPackageCmdSet, (int)PkgCmdIdList.XamarinFormsHomePage);
            menuItem = new MenuCommand(this.OnXamarinFormsHomePage, menuCommandId);
            mcs.AddCommand(menuItem);

            menuCommandId = new CommandID(GuidList.GuidNinjaCoderMvvmCrossVsPackageCmdSet, (int)PkgCmdIdList.MvvmCrossHomePage);
            menuItem = new MenuCommand(this.OnMvvmCrossHomePage, menuCommandId);
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
            NinjaController.RunCustomRendererController(this.VsInstance.ApplicationObject);
        }

        /// <summary>
        /// Called when [add effect].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnAddEffect(object sender, EventArgs e)
        {
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::OnAddEffect");
            NinjaController.RunFormsEffectsController(this.VsInstance.ApplicationObject);
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

        /// <summary>
        /// Called when [view error log file].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnViewErrorLogFile(object sender, EventArgs e)
        {
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::OnViewErrorLogFile");
            NinjaController.ViewErrorLogFile();
        }

        /// <summary>
        /// Called when [clear error log file].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnClearErrorLogFile(object sender, EventArgs e)
        {
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::OnClearErrorLogFile");
            NinjaController.ClearErrorLogFile();
        }

        /// <summary>
        /// Called when [MVVM cross home page].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnMvvmCrossHomePage(object sender, EventArgs e)
        {
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::OnMvvmCrossHomePage");
            NinjaController.MvvmCrossHomePage();
        }

        /// <summary>
        /// Called when [xamarin forms home page].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        private void OnXamarinFormsHomePage(object sender, EventArgs e)
        {
            TraceService.WriteLine("NinjaCoder_MvvmCross_VSPackagePackage::OnXamarinFormsHomePage");
            NinjaController.XamarinFormsHomePage();
        }
    }
}