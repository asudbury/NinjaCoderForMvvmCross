// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewModelViewsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using Constants;
    using EnvDTE;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using Views.Interfaces;

    /// <summary>
    /// Defines the ViewModelViewsController type.
    /// </summary>
    public class ViewModelViewsController : BaseController
    {
        /// <summary>
        /// The view model views service.
        /// </summary>
        private readonly IViewModelViewsService viewModelViewsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelViewsController" /> class.
        /// </summary>
        /// <param name="viewModelViewsService">The view model views service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="formsService">The forms service.</param>
        public ViewModelViewsController(
            IViewModelViewsService viewModelViewsService,
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
            TraceService.WriteLine("ViewModelAndViewsController::Constructor");

            this.viewModelViewsService = viewModelViewsService;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteHeader("ViewModelAndViewsController::Run");

            if (this.VisualStudioService.IsMvvmCrossSolution)
            {
                List<ItemTemplateInfo> templateInfos = this.VisualStudioService.AllowedItemTemplates;

                IEnumerable<string> viewModelNames = this.VisualStudioService.CoreProjectService.GetFolderItems("ViewModels", false);
                
                //// we don't want the base view model - probably a better way of doing this!

                IEnumerable<string> exceptViewModelName = new List<string> { this.SettingsService.BaseViewModelName };

                IViewModelViewsView view = this.FormsService.GetViewModelViewsForm(
                    this.SettingsService,
                    templateInfos, 
                    viewModelNames.Except(exceptViewModelName));

                DialogResult result = this.DialogService.ShowDialog(view as Form);

                if (result == DialogResult.OK)
                {
                    this.Process(
                        view.Presenter.GetRequiredItemTemplates(), 
                        view.ViewModelName,
                        view.IncludeUnitTests,
                        view.ViewModelInitiatedFrom,
                        view.ViewModelToNavigateTo);
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
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="addUnitTests">if set to <c>true</c> [add unit tests].</param>
        /// <param name="viewModelInitiateFrom">The view model initiate from.</param>
        /// <param name="viewModelNavigateTo">The view model navigate to.</param>
        internal void Process(
            IEnumerable<ItemTemplateInfo> templateInfos, 
            string viewModelName,
            bool addUnitTests,
            string viewModelInitiateFrom,
            string viewModelNavigateTo)
        {
            TraceService.WriteLine("ViewModelAndViewsController::Process");

            this.VisualStudioService.DTEService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            ProjectItemsEvents cSharpProjectItemsEvents = this.VisualStudioService.DTEService.GetCSharpProjectItemsEvents();
            cSharpProjectItemsEvents.ItemAdded += this.ProjectItemsEventsItemAdded;

            IEnumerable<string> messages = this.viewModelViewsService.AddViewModelAndViews(
                this.VisualStudioService.CoreProjectService,
                this.VisualStudioService,
                templateInfos,
                viewModelName,
                addUnitTests,
                viewModelInitiateFrom,
                viewModelNavigateTo);

            cSharpProjectItemsEvents.ItemAdded -= this.ProjectItemsEventsItemAdded;

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

            //// show the readme.
            this.ShowReadMe("Add ViewModel and Views", messages);

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.ViewModelAndViewsCompleted);
        }
    }
}
