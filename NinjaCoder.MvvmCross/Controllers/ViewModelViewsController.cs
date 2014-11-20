// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewModelViewsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using Constants;
    using EnvDTE;
    using NinjaCoder.MvvmCross.Entities;
    using Scorchio.Infrastructure.Services;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System.Collections.Generic;
    using ViewModels;
    using Views;

    /// <summary>
    /// Defines the ViewModelViewsController type.
    /// </summary>
    internal class ViewModelViewsController : BaseController
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
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="readMeService">The read me service.</param>
        public ViewModelViewsController(
            IViewModelViewsService viewModelViewsService,
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IResolverService resolverService,
            IReadMeService readMeService)
            : base(
            visualStudioService,
            settingsService,
            messageBoxService,
            resolverService,
            readMeService)
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

            FrameworkType frameworkType = this.VisualStudioService.GetFrameworkType();

            if (frameworkType == FrameworkType.MvvmCross || 
                frameworkType == FrameworkType.XamarinForms ||
                frameworkType == FrameworkType.MvvmCrossAndXamarinForms)
            {
                ViewModelViewsViewModel viewModel = this.ShowDialog<ViewModelViewsViewModel>(new ViewModelViewsView());

                if (viewModel.Continue)
                {
                    this.Process(
                        viewModel.RequiredTemplates,
                        viewModel.ViewModelName,
                        viewModel.IncludeUnitTests,
                        viewModel.ViewModelInitiatedFrom,
                        viewModel.ViewModelToNavigateTo);
                }
            }
            else
            {
                this.ShowNotMvvmCrossOrXamarinFormsSolutionMessage();
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

            if (cSharpProjectItemsEvents != null)
            {
                cSharpProjectItemsEvents.ItemAdded += this.ProjectItemsEventsItemAdded;
            }

            IEnumerable<string> messages = this.viewModelViewsService.AddViewModelAndViews(
                this.VisualStudioService.CoreProjectService,
                templateInfos,
                viewModelName,
                addUnitTests,
                viewModelInitiateFrom,
                viewModelNavigateTo);

            if (cSharpProjectItemsEvents != null)
            {
                cSharpProjectItemsEvents.ItemAdded -= this.ProjectItemsEventsItemAdded;
            }

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

            //// show the readme.
            this.ShowReadMe("Add ViewModel and Views", messages);

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.ViewModelAndViewsCompleted);
        }
    }
}
