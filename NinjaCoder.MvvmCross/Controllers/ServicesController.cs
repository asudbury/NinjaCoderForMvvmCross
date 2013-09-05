// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ServicesController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using Constants;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Views.Interfaces;

    /// <summary>
    /// Defines the ServicesController type.
    /// </summary>
    public class ServicesController : BaseController
    {
        /// <summary>
        /// The services service.
        /// </summary>
        private readonly IServicesService servicesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesController" /> class.
        /// </summary>
        /// <param name="servicesService">The services service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="formsService">The forms service.</param>
        public ServicesController(
            IServicesService servicesService,
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
            TraceService.WriteLine("ServicesController::Constructor");

            this.servicesService = servicesService;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteHeader("ServicesController::Run");

            if (this.VisualStudioService.IsMvvmCrossSolution)
            {
                string templatesPath = this.SettingsService.ServicesTemplatesPath;

                List<ItemTemplateInfo> itemTemplateInfos = this.VisualStudioService.GetFolderTemplateInfos(templatesPath, "Services");

                IEnumerable<string> viewModelNames = this.VisualStudioService.CoreProjectService.GetFolderItems("ViewModels", false);

                IServicesView view = this.FormsService.GetServicesForm(viewModelNames, itemTemplateInfos, this.SettingsService);

                DialogResult result = this.DialogService.ShowDialog(view as Form);
                
                if (result == DialogResult.OK)
                {
                    this.Process(view.RequiredTemplates, view.ImplementInViewModel, view.IncludeUnitTests);
                }
            }
            else
            {
                this.ShowNotMvvmCrossSolutionMessage();
            }
        }

        /// <summary>
        /// Processes the specified form.
        /// </summary>
        /// <param name="templateInfos">The template infos.</param>
        /// <param name="implementInViewModel">The implement in view model.</param>
        /// <param name="includeUnitTests">if set to <c>true</c> [include unit tests].</param>
        internal void Process(
            IEnumerable<ItemTemplateInfo> templateInfos,
            string implementInViewModel,
            bool includeUnitTests)
        {
            TraceService.WriteLine("ServicesController::Process");

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            IProjectService projectService = this.VisualStudioService.CoreProjectService;

            if (projectService != null)
            {
                List<string> messages = this.servicesService.AddServices(
                    this.VisualStudioService,
                    templateInfos,
                    implementInViewModel,
                    includeUnitTests);

                this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

                //// show the readme.
                this.ShowReadMe("Add Services", messages);

                this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.ServicesCompleted);
            }
            else
            {
                TraceService.WriteError("ServicesController::AddServices Cannot find Core project");
            }
        }
    }
}
