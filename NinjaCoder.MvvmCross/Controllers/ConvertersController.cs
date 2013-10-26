// ---------------------- ----------------------------------------------------------------------------------------------
// <summary>
//    Defines the ConvertersController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Constants;
    using EnvDTE;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using Views.Interfaces;

    /// <summary>
    /// Defines the ConvertersController type.
    /// </summary>
    public class ConvertersController : BaseController
    {
        /// <summary>
        /// The converter service.
        /// </summary>
        private readonly IConvertersService converterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertersController" /> class.
        /// </summary>
        /// <param name="convertersService">The converters service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="formsService">The forms service.</param>
        public ConvertersController(
            IConvertersService convertersService,
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
            TraceService.WriteLine("ConfigurationController::Constructor");

            this.converterService = convertersService;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteHeader("ConvertersController::Run");

            if (this.VisualStudioService.IsMvvmCrossSolution)
            {
                string templatesPath = this.SettingsService.ConvertersTemplatesPath;

                List<ItemTemplateInfo> itemTemplateInfos = this.VisualStudioService.GetFolderTemplateInfos(templatesPath, "Converters");

                IItemTemplatesView view = this.FormsService.GetItemTemplatesForm(itemTemplateInfos, this.SettingsService);

                DialogResult result = this.DialogService.ShowDialog(view as Form);

                if (result == DialogResult.OK)
                {
                    this.Process(templatesPath, view.RequiredTemplates);
                }
            }
            else
            {
                this.ShowNotMvvmCrossSolutionMessage();
            }
        }

        /// <summary>
        /// Processes the specified templates path.
        /// </summary>
        /// <param name="templatesPath">The templates path.</param>
        /// <param name="templateInfos">The template infos.</param>
        internal void Process(string templatesPath, List<ItemTemplateInfo> templateInfos)
        {
            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            ProjectItemsEvents cSharpProjectItemsEvents = this.VisualStudioService.DTEService.GetCSharpProjectItemsEvents();
            cSharpProjectItemsEvents.ItemAdded += this.ProjectItemsEventsItemAdded;

            IEnumerable<string> messages = this.converterService.AddConverters(
                this.VisualStudioService.CoreProjectService, templatesPath, templateInfos);

            cSharpProjectItemsEvents.ItemAdded -= this.ProjectItemsEventsItemAdded;

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

            //// show the readme.
            this.ShowReadMe("Add Converters", messages);

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.ConvertersCompleted);
        }
    }
}
