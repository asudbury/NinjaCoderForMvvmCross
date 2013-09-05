// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewModelViewsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using Constants;
    using EnvDTE;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using Views.Interfaces;

    /// <summary>
    /// Defines the ViewModelViewsController type.
    /// </summary>
    public class ViewModelViewsController : BaseController
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelViewsController" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="dialogService">The dialog service.</param>
        /// <param name="formsService">The forms service.</param>
        public ViewModelViewsController(
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

                IViewModelViewsView view = this.FormsService.GetViewModelOptionsForm(templateInfos);

                DialogResult result = this.DialogService.ShowDialog(view as Form);

                if (result == DialogResult.OK)
                {
                    this.Process(view.Presenter.GetRequiredItemTemplates(), view.ViewModelName);
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
        internal void Process(
            IEnumerable<ItemTemplateInfo> templateInfos, 
            string viewModelName)
        {
            TraceService.WriteLine("ViewModelAndViewsController::Process");

            this.VisualStudioService.DTEService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            ProjectItemsEvents cSharpProjectItemsEvents = this.VisualStudioService.DTEService.GetCSharpProjectItemsEvents();
            cSharpProjectItemsEvents.ItemAdded += this.ProjectItemsEventsItemAdded;

            IEnumerable<string> messages = this.VisualStudioService.SolutionService.AddItemTemplateToProjects(templateInfos, true);

            //// we now need to amend code in the unit test file that references FirstViewModel to this ViewModel
            this.UpdateUnitTestFile(viewModelName);

            cSharpProjectItemsEvents.ItemAdded -= this.ProjectItemsEventsItemAdded;

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

            //// show the readme.
            this.ShowReadMe("Add ViewModel and Views", messages);

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.ViewModelAndViewsCompleted);
        }

        /// <summary>
        /// Updates the unit test file.
        /// </summary>
        /// <param name="viewModelName">Name of the view model.</param>
        internal void UpdateUnitTestFile(string viewModelName)
        {
            TraceService.WriteLine("ViewModelAndViewsController::UpdateUnitTestFile ViewModelName=" + viewModelName);

            IProjectService testProjectService = this.VisualStudioService.CoreTestsProjectService;

            if (testProjectService != null)
            {
                IProjectItemService projectItemService = testProjectService.GetProjectItem("Test" + viewModelName);

                if (projectItemService != null)
                {
                    projectItemService.ReplaceText("CoreTemplate.", "Core.");
                    projectItemService.ReplaceText("FirstViewModel", viewModelName);
                    projectItemService.ReplaceText("firstViewModel",  viewModelName.Substring(0, 1).ToLower() + viewModelName.Substring(1));
                }
            }
        }
    }
}
