// ---------------------- ----------------------------------------------------------------------------------------------
// <summary>
//    Defines the ConvertersController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Constants;
    using EnvDTE;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.ViewModels;
    using NinjaCoder.MvvmCross.Views;

    using Scorchio.Infrastructure.Services;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;

    /// <summary>
    /// Defines the ConvertersController type.
    /// </summary>
    internal class ConvertersController : BaseController
    {
        /// <summary>
        /// The converter service.
        /// </summary>
        private readonly IConvertersService converterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertersController" /> class.
        /// </summary>
        /// <param name="configurationService">The configuration service.</param>
        /// <param name="convertersService">The converters service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="readMeService">The read me service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="messageBoxService">The message box service.</param>
        /// <param name="resolverService">The resolver service.</param>
        public ConvertersController(
            IConfigurationService configurationService,
            IConvertersService convertersService,
            IVisualStudioService visualStudioService,
            IReadMeService readMeService,
            ISettingsService settingsService,
            IMessageBoxService messageBoxService,
            IResolverService resolverService)
            : base(
            configurationService,
            visualStudioService, 
            readMeService, 
            settingsService, 
            messageBoxService,
            resolverService)
        {
            TraceService.WriteLine("ConvertersController::Constructor");

            this.converterService = convertersService;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            TraceService.WriteHeader("ConvertersController::Run2");

            if (this.VisualStudioService.IsMvvmCrossSolution)
            {
                ConvertersViewModel viewModel = this.ShowDialog<ConvertersViewModel>(new ConvertersView());

                if (viewModel.Continue)
                {
                    string templatesPath = this.SettingsService.ConvertersTemplatesPath;

                    this.Process(templatesPath, viewModel.GetRequiredConverters().ToList());
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
        internal void Process(
            string templatesPath, 
            List<ItemTemplateInfo> templateInfos)
        {
            TraceService.WriteLine("ConvertersController::Process");

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.NinjaIsRunning);

            ProjectItemsEvents cSharpProjectItemsEvents = this.VisualStudioService.DTEService.GetCSharpProjectItemsEvents();

            if (cSharpProjectItemsEvents != null)
            {
                cSharpProjectItemsEvents.ItemAdded += this.ProjectItemsEventsItemAdded;
            }

            IEnumerable<string> messages = this.converterService.AddConverters(
                    this.VisualStudioService.CoreProjectService,
                    templatesPath, 
                    templateInfos);

            if (cSharpProjectItemsEvents != null)
            {
                cSharpProjectItemsEvents.ItemAdded -= this.ProjectItemsEventsItemAdded;
            }

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.UpdatingFiles);

            //// show the readme.
            this.ShowReadMe("Add Converters", messages);

            this.VisualStudioService.WriteStatusBarMessage(NinjaMessages.ConvertersCompleted);
        }
    }
}
