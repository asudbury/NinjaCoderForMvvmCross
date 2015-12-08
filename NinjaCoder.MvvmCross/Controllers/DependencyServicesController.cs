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
    using Scorchio.VisualStudio.Services.Interfaces;
    using Services.Interfaces;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
        /// Initializes a new instance of the <see cref="DependencyServicesController" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="resolverService">The resolver service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="dependencyServicesFactory">The dependency services factory.</param>
        public DependencyServicesController(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IResolverService resolverService,
            IReadMeService readMeService,
            IDependencyServicesFactory dependencyServicesFactory)
            : base(
            visualStudioService, 
            settingsService, 
            messageBoxService,
            resolverService,
            readMeService)
        {
            TraceService.WriteLine("DependencyServicesController::Constructor");
            this.dependencyServicesFactory = dependencyServicesFactory;
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
                List<string> messages = new List<string>();

                TraceService.WriteLine("DependencyServicesController::Process GetTextTemplates");

                IEnumerable<TextTemplateInfo> textTemplates = this.dependencyServicesFactory.GetTextTemplates(
                    dependencyServiceViewModel.RequestedName, 
                    dependencyServiceViewModel.MethodComment,
                    dependencyServiceViewModel.MethodReturnType,
                    dependencyServiceViewModel.MethodName,
                    dependencyServiceViewModel.Directory);

                ITextTransformationService textTransformationService = this.VisualStudioService.GetTextTransformationService();

                foreach (TextTemplateInfo textTemplateInfo in textTemplates)
                {
                    TraceService.WriteLine("DependencyServicesController::Process textTemplate=" + textTemplateInfo.FileName);

                    textTemplateInfo.TextOutput = textTransformationService.Transform(textTemplateInfo.TemplateName, textTemplateInfo.Tokens);

                    if (textTemplateInfo.TextOutput != string.Empty &&
                        textTransformationService.T4CallBack.ErrorMessages.Any() == false)
                    {
                        //// add file to the solution
                        IProjectService projectService = this.VisualStudioService.GetProjectServiceBySuffix(textTemplateInfo.ProjectSuffix);

                        if (projectService != null)
                        {
                            string message = projectService.AddTextTemplate(textTemplateInfo);
                            messages.Add(message);
                        }
                    }

                    else
                    {
                        foreach (string errorMessage in textTransformationService.T4CallBack.ErrorMessages)
                        {
                            messages.Add("T4 Error " + errorMessage);
                        }
                    }
                }

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
