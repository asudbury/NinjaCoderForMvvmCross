// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ApplicationController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using EnvDTE;

    using NinjaCoder.MvvmCross.Infrastructure.Services;

    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using Views;
    using Views.Interfaces;

    /// <summary>
    /// Defines the ApplicationController type.
    /// </summary>
    internal class ApplicationController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApplicationController" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="formsService">The forms service.</param>
        public ApplicationController(
            IVisualStudioService visualStudioService,
            IReadMeService readMeService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IDialogService dialogService,
            IFormsService formsService)
            : base(
            visualStudioService, 
            readMeService, 
            settingsService, 
            messageBoxService,
            dialogService,
            formsService)
        {
            TraceService.WriteLine("ApplicationController::Constructor");
        }

        /// <summary>
        /// Shows the options.
        /// </summary>
        public void ShowOptions()
        {
            TraceService.WriteLine("ApplicationController::ShowOptions");

            IOptionsView view = this.FormsService.GetOptionsForm(this.SettingsService);

            this.DialogService.ShowDialog(view as Form);

            //// in case any of the setting have changed to do with logging reset them!
            TraceService.Initialize(
                this.SettingsService.LogToTrace, 
                false,  //// log to console.
                this.SettingsService.LogToFile, 
                this.SettingsService.LogFilePath, 
                this.SettingsService.DisplayErrors);
        }
        
        /// <summary>
        /// Shows the about box.
        /// </summary>
        public void ShowAboutBox()
        {
            TraceService.WriteLine("ApplicationController::ShowAboutBox");

            AboutBoxForm aboutBoxForm = this.FormsService.GetAboutBoxForm();

            this.DialogService.ShowDialog(aboutBoxForm);
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
    }
}
