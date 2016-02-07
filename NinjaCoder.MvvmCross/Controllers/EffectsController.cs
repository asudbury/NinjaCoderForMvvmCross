// --------------------------------------------------------------------------------------------------------------------
// <summary>
// 	Defines the EffectsController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Entities;
    using NinjaCoder.MvvmCross.Extensions;
    using NinjaCoder.MvvmCross.Factories.Interfaces;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.ViewModels.AddEffects;
    using NinjaCoder.MvvmCross.ViewModels.Wizard;
    using NinjaCoder.MvvmCross.Views.Wizard;
    using Scorchio.Infrastructure.Services;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using System;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the EffectsController type.
    /// </summary>
    public class EffectsController : BaseController
    {
        /// <summary>
        /// The effect factory.
        /// </summary>
        private readonly IEffectFactory effectFactory;

        /// <summary>
        /// The text templating service.
        /// </summary>
        private readonly ITextTemplatingService textTemplatingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="EffectsController" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="effectFactory">The effect factory.</param>
        /// <param name="textTemplatingService">The text templating service.</param>
        public EffectsController(
            IVisualStudioService visualStudioService, 
            ISettingsService settingsService, 
            IMessageBoxService messageBoxService, 
            IResolverService resolverService, 
            IReadMeService readMeService,
            IEffectFactory effectFactory, 
            ITextTemplatingService textTemplatingService)
            : base(
            visualStudioService, 
            settingsService, 
            messageBoxService, 
            resolverService, 
            readMeService)
        {
            TraceService.WriteLine("EffectsController::Constructor");

            this.effectFactory = effectFactory;
            this.textTemplatingService = textTemplatingService;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteHeader("EffectsController::Run");

            FrameworkType frameworkType = this.VisualStudioService.GetFrameworkType();

            if (frameworkType.IsXamarinFormsSolutionType())
            {
                this.effectFactory.RegisterWizardData();

                WizardFrameViewModel viewModel = this.ShowDialog<WizardFrameViewModel>(new WizardView());

                if (viewModel.Continue)
                {
                    EffectViewModel effectViewModel = (EffectViewModel)viewModel.GetWizardStepViewModel("EffectViewModel").ViewModel;

                    this.Process(effectViewModel);
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
        /// <param name="effectViewModel">The effect view model.</param>
        internal void Process(EffectViewModel effectViewModel)
        {
            TraceService.WriteLine("EffectsController::Process");

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            try
            {
                TraceService.WriteLine("EffectsController::Process GetTextTemplates");

                IEnumerable<TextTemplateInfo> textTemplates = this.effectFactory.GetTextTemplates(
                    effectViewModel.RequestedName,
                    effectViewModel.Directory);
                
                IEnumerable<string> messages = this.textTemplatingService.AddTextTemplates(
                    NinjaMessages.AddingEffect,
                    textTemplates);

                //// show the readme.
                this.ShowReadMe("Add Xamarin Forms Effect", messages);
            }

            catch (Exception exception)
            {
                TraceService.WriteError("Cannot create effect exception=" + exception.Message);
            }
        }
    }
}
