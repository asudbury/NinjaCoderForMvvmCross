// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ApplicationController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using Constants;
    using EnvDTE;
    using MahApps.Metro;
    using Scorchio.Infrastructure.Entities;
    using Scorchio.Infrastructure.Services;
    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Windows;
    using ViewModels;
    using ViewModels.Options;
    using Views;

    /// <summary>
    /// Defines the ApplicationController type.
    /// </summary>
    internal class ApplicationController : BaseController
    {
        /// <summary>
        /// The application service.
        /// </summary>
        private readonly IApplicationService applicationService;

        /// <summary>
        /// The translator.
        /// </summary>
        private readonly ITranslator<IList<Accent>, IEnumerable<AccentColor>> translator;

        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationController" /> class.
        /// </summary>
        /// <param name="applicationService">The application service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="translator">The translator.</param>
        /// <param name="readMeService">The read me service.</param>
        public ApplicationController(
            IApplicationService applicationService,
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IResolverService resolverService,
            ITranslator<IList<Accent>, IEnumerable<AccentColor>> translator,
            IReadMeService readMeService)
            : base(
            visualStudioService,
            settingsService,
            messageBoxService,
            resolverService,
            readMeService)
        {
            TraceService.WriteLine("ApplicationController::Constructor");

            this.applicationService = applicationService;
            this.translator = translator;
        }

        /// <summary>
        /// Shows the options.
        /// </summary>
        public void ShowOptions()
        {
            TraceService.WriteLine("ApplicationController::ShowOptions");

            OptionsView view = new OptionsView();

            ResourceDictionary resourceDictionary = this.GetLanguageDictionary();
            
            OptionsViewModel viewModel = this.ResolverService.Resolve<OptionsViewModel>();
            viewModel.LanguageDictionary = resourceDictionary;

            view.DataContext = viewModel;

            view.ShowDialog();

            //// in case any of the setting have changed to do with logging reset them!
            TraceService.Initialize(
                this.SettingsService.LogToTrace, 
                false,  //// log to console.
                this.SettingsService.LogToFile, 
                this.SettingsService.LogFilePath, 
                this.SettingsService.DisplayErrors,
                this.SettingsService.ErrorFilePath);
        }

        /// <summary>
        /// Shows the about box.
        /// </summary>
        public void ShowAboutBox()
        {
            TraceService.WriteLine("ApplicationController::ShowAboutBox");
            this.ShowDialog<AboutViewModel>(new AboutView());
        }

        /// <summary>
        /// Gets the projects.
        /// </summary>
        /// <returns>The projects.</returns>
        public IEnumerable<Project> GetProjects()
        {
            TraceService.WriteLine("ApplicationController::GetProjects");
            return this.VisualStudioService.SolutionService.GetProjects();
        }

        /// <summary>
        /// Views the log file.
        /// </summary>
        public void ViewLogFile()
        {
            TraceService.WriteLine("ApplicationController::ViewLogFile");
            this.applicationService.ViewLogFile();
        }

        /// <summary>
        /// Views the error log file.
        /// </summary>
        public void ViewErrorLogFile()
        {
            TraceService.WriteLine("ApplicationController::ViewLogFile");
            this.applicationService.ViewErrorLogFile();
        }

        /// <summary>
        /// Clears the log file.
        /// </summary>
        public void ClearLogFile()
        {
            TraceService.WriteLine("ApplicationController::ClearLogFile");
            this.applicationService.ClearLogFile();

            this.MessageBoxService.Show(
                "Log File has been cleared.", 
                Settings.ApplicationName,
                false,
                Theme.Light,
                this.SettingsService.ThemeColor);
        }

        /// <summary>
        /// Clears the error log file.
        /// </summary>
        public void ClearErrorLogFile()
        {
            TraceService.WriteLine("ApplicationController::ClearErrorLogFile");
            this.applicationService.ClearErrorLogFile();

            this.MessageBoxService.Show(
                "Error Log File has been cleared.",
                Settings.ApplicationName,
                false,
                Theme.Light,
                this.SettingsService.ThemeColor);
        }

        /// <summary>
        /// Sets the working directory.
        /// </summary>
        /// <param name="path">The path.</param>
        public void SetWorkingDirectory(string path)
        {
            TraceService.WriteLine("ApplicationController::SetWorkingDirectory " + path);
            this.applicationService.SetWorkingDirectory(path);
        }

        /// <summary>
        /// MVVMs the cross home page.
        /// </summary>
        public void MvvmCrossHomePage()
        {
            TraceService.WriteLine("ApplicationController::MvvmCrossHomePage");
            this.applicationService.ShowMvvmCrossHomePage();
        }

        /// <summary>
        /// Xamarins the forms home page.
        /// </summary>
        public void XamarinFormsHomePage()
        {
            TraceService.WriteLine("ApplicationController::XamarinFormsHomePage");
            this.applicationService.ShowXamarinFormsHomePage();
        }
    }
}
