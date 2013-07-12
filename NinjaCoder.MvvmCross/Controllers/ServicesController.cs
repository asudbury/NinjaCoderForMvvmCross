// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ServicesController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System.Collections.Generic;
    using System.IO.Abstractions;

    using EnvDTE;

    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.Translators;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Services;
    using Views;

    /// <summary>
    /// Defines the ServicesController type.
    /// </summary>
    public class ServicesController : BaseController
    {
        /// <summary>
        /// The services service.
        /// </summary>
        private readonly IServicesService servicesService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesController"/> class.
        /// </summary>
        public ServicesController()
            : this(new ServicesService(new CodeConfigTranslator(), new FileSystem(), new SettingsService(), new SnippetService(new FileSystem(), new CodeSnippetTranslator())))
        {
            TraceService.WriteLine("ServicesController::Constructor");
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesController" /> class.
        /// </summary>
        /// <param name="servicesService">The services service.</param>
        public ServicesController(IServicesService servicesService)
            : base(new VisualStudioService(), new ReadMeService(), new SettingsService())
        {
            TraceService.WriteLine("ServicesController::Constructor");

            this.servicesService = servicesService;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            this.AddTraceHeader("ServicesController", "Run");

            if (this.VisualStudioService.IsMvvmCrossSolution)
            {
                string templatesPath = this.SettingsService.ServicesTemplatesPath;

                List<ItemTemplateInfo> itemTemplateInfos = this.VisualStudioService.GetFolderTemplateInfos(templatesPath, "Services");

                ServicesForm form = new ServicesForm(this.GetViewModelNames(), itemTemplateInfos, this.SettingsService.DisplayLogo);

                form.ShowDialog();
                
                if (form.Continue)
                {
                    this.WriteStatusBarMessage("Ninja Coder is running....");

                    Project project = this.VisualStudioService.CoreProject;

                    if (project != null)
                    {
                        List<string> messages = this.servicesService.AddServices(
                            this.VisualStudioService,
                            form.RequiredTemplates,
                            form.ImplementInViewModel,
                            form.IncludeUnitTests);

                        //// show the readme.
                        this.ShowReadMe("Add Services", messages);

                        this.WriteStatusBarMessage("Ninja Coder has completed the adding of the services.");
                    }
                    else
                    {
                        TraceService.WriteError("ServicesController::AddServices Cannot find Core project");
                    }
                }
            }
            else
            {
                this.ShowNotMvvmCrossSolutionMessage();
            }
        }
    }
}
