// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CustomeRendererController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.ViewModels.AddCustomRenderers;
    using NinjaCoder.MvvmCross.ViewModels.Wizard;
    using NinjaCoder.MvvmCross.Views.Wizard;
    using Scorchio.Infrastructure.Services;
    using Scorchio.VisualStudio.Services;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the CustomeRendererController type.
    /// </summary>
    public class CustomRendererController : BaseController
    {
        /// <summary>
        /// The custom renderer factory.
        /// </summary>
        private readonly ICustomRendererFactory customRendererFactory;

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomRendererController"/> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="customRendererFactory">The custome renderer factory.</param>
        public CustomRendererController(
            IVisualStudioService visualStudioService, 
            ISettingsService settingsService, 
            IMessageBoxService messageBoxService, 
            IResolverService resolverService, 
            IReadMeService readMeService,
            ICustomRendererFactory customRendererFactory)
            : base(
                visualStudioService, 
                settingsService, 
                messageBoxService, 
                resolverService, 
                readMeService)
        {
            TraceService.WriteLine("CustomerRendererController::Constructor");
            this.customRendererFactory = customRendererFactory;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteHeader("CustomerRendererController::Run");

            FrameworkType frameworkType = this.VisualStudioService.GetFrameworkType();

            if (frameworkType == FrameworkType.XamarinForms ||
                frameworkType == FrameworkType.MvvmCrossAndXamarinForms)
            {
                this.customRendererFactory.RegisterWizardData();

                WizardFrameViewModel viewModel = this.ShowDialog<WizardFrameViewModel>(new WizardView());

                if (viewModel.Continue)
                {
                    CustomRendererViewModel customRendererViewModel = (CustomRendererViewModel)viewModel.GetWizardStepViewModel("CustomRendererViewModel").ViewModel;

                    this.Process(customRendererViewModel);
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
        /// <param name="customRendererViewModel">The custom renderer view model.</param>
        internal void Process(CustomRendererViewModel customRendererViewModel)
        {
            TraceService.WriteLine("CustomerRendererController::Process");

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            List<string> messages = new List<string>();

            //// show the readme.
            this.ShowReadMe("Add Xamarin Forms Custom Renderer", messages);
        }
    }
}