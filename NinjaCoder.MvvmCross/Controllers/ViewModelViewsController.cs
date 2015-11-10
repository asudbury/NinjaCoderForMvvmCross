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
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.ViewModels.AddViews;
    using NinjaCoder.MvvmCross.ViewModels.Wizard;
    using NinjaCoder.MvvmCross.Views.Wizard;

    using Scorchio.Infrastructure.Services;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System.Collections.Generic;

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
        /// The view model and views factory.
        /// </summary>
        private readonly IViewModelAndViewsFactory viewModelAndViewsFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelViewsController" /> class.
        /// </summary>
        /// <param name="viewModelViewsService">The view model views service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="viewModelAndViewsFactory">The view model and views factory.</param>
        public ViewModelViewsController(
            IViewModelViewsService viewModelViewsService,
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IResolverService resolverService,
            IReadMeService readMeService,
            IViewModelAndViewsFactory viewModelAndViewsFactory)
            : base(
            visualStudioService,
            settingsService,
            messageBoxService,
            resolverService,
            readMeService)
        {
            TraceService.WriteLine("ViewModelAndViewsController::Constructor");

            this.viewModelViewsService = viewModelViewsService;
            this.viewModelAndViewsFactory = viewModelAndViewsFactory;
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
                this.viewModelAndViewsFactory.RegisterWizardData();

                WizardFrameViewModel viewModel = this.ShowDialog<WizardFrameViewModel>(new WizardView());

                if (viewModel.Continue)
                {
                    ViewsViewModel viewsViewModel = (ViewsViewModel)viewModel.GetWizardStepViewModel("ViewsViewModel").ViewModel;

                    this.Process(viewsViewModel.Views);
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
        /// <param name="views">The views.</param>
        internal void Process(IEnumerable<View> views)
        {
            TraceService.WriteLine("ViewModelAndViewsController::Process");

            this.VisualStudioService.DTEService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            ProjectItemsEvents projectItemsEvents = this.VisualStudioService.DTEService.GetCSharpProjectItemsEvents();

            if (projectItemsEvents != null)
            {
                projectItemsEvents.ItemAdded += this.ProjectItemsEventsItemAdded;
            }

            IEnumerable<string> messages = this.viewModelViewsService.AddViewModelsAndViews(views);

            if (projectItemsEvents != null)
            {
                projectItemsEvents.ItemAdded -= this.ProjectItemsEventsItemAdded;
            }

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

            //// show the readme.
            this.ShowReadMe("Add ViewModel and Views", messages);

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.ViewModelAndViewsCompleted);
        }
    }
}
