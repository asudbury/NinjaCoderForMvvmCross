// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the DependencyServicesController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using Constants;
    using Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.ViewModels.AddDependencyServices;
    using NinjaCoder.MvvmCross.ViewModels.Wizard;
    using NinjaCoder.MvvmCross.Views.Wizard;
    using Scorchio.Infrastructure.Services;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the DependencyServicesController type.
    /// </summary>
    public class DependencyServicesController : BaseController
    {
        /// <summary>
        /// The dependency services factory.
        /// </summary>
        private readonly IDependencyServicesFactory dependencyServicesFactory;
        
        /// <summary>
        /// The text templating service.
        /// </summary>
        private readonly ITextTemplatingService textTemplatingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DependencyServicesController" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="dependencyServicesFactory">The dependency services factory.</param>
        /// <param name="textTemplatingService">The text templating service.</param>
        public DependencyServicesController(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IResolverService resolverService,
            IReadMeService readMeService,
            IDependencyServicesFactory dependencyServicesFactory,
            ITextTemplatingService textTemplatingService)
            : base(
            visualStudioService, 
            settingsService, 
            messageBoxService,
            resolverService,
            readMeService)
        {
            TraceService.WriteLine("DependencyServicesController::Constructor");
            this.dependencyServicesFactory = dependencyServicesFactory;
            this.textTemplatingService = textTemplatingService;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteHeader("DependencyServicesController::Run");

            FrameworkType frameworkType = this.VisualStudioService.GetFrameworkType();

            if (frameworkType == FrameworkType.XamarinForms || 
                frameworkType == FrameworkType.MvvmCrossAndXamarinForms)
            {
                this.dependencyServicesFactory.RegisterWizardData();

                WizardFrameViewModel viewModel = this.ShowDialog<WizardFrameViewModel>(new WizardView());

                if (viewModel.Continue)
                {
                    DependencyServiceViewModel dependencyServiceViewModel = (DependencyServiceViewModel)viewModel.GetWizardStepViewModel("DependencyServiceViewModel").ViewModel;

                    this.Process(dependencyServiceViewModel);
                }
            }

            else
            {
                this.ShowNotXamarinFormsSolutionMessage();
            }
        }

        /// <summary>
        /// Processes the specified form.
        /// </summary>
        /// <param name="dependencyServiceViewModel">The dependency service view model.</param>
        internal void Process(DependencyServiceViewModel dependencyServiceViewModel)
        {
            TraceService.WriteLine("DependencyServicesController::Process");

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            try
            {
                TraceService.WriteLine("DependencyServicesController::Process GetTextTemplates");

                IEnumerable<TextTemplateInfo> textTemplates = this.dependencyServicesFactory.GetTextTemplates(
                    dependencyServiceViewModel.RequestedName, 
                    dependencyServiceViewModel.MethodComment,
                    dependencyServiceViewModel.MethodReturnType,
                    dependencyServiceViewModel.MethodName,
                    dependencyServiceViewModel.Directory);

                IEnumerable<string> messages = this.textTemplatingService.AddTextTemplates(
                    NinjaMessages.AddingDependencyService,
                    textTemplates);

                //// show the readme.
                this.ShowReadMe("Add Xamarin Forms Dependency Service", messages);
            }

            catch (Exception exception)
            {
                TraceService.WriteError("Cannot create dependency service exception=" + exception.Message);
            }
        }
    }
}
