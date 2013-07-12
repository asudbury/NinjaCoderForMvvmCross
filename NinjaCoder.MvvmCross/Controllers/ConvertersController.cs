// ---------------------- ----------------------------------------------------------------------------------------------
// <summary>
//    Defines the ConvertersController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Controllers
{
    using System.Collections.Generic;
    using Scorchio.VisualStudio.Entities;
    using Services;
    using Services.Interfaces;
    using Views;

    /// <summary>
    /// Defines the ConvertersController type.
    /// </summary>
    public class ConvertersController : BaseController
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The converter service.
        /// </summary>
        private readonly IConvertersService converterService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertersController"/> class.
        /// </summary>
        public ConvertersController()
            : this(new SettingsService(), new ConvertersService())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertersController" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="convertersService">The converters service.</param>
        public ConvertersController(
            ISettingsService settingsService,
            IConvertersService convertersService)
            : base(new VisualStudioService(), new ReadMeService(), new SettingsService())
        {
            this.settingsService = settingsService;
            this.converterService = convertersService;
        }

        /// <summary>
        /// Runs this instance.
        /// </summary>
        public void Run()
        {
            this.AddTraceHeader("ConvertersController", "Run");

            if (this.VisualStudioService.IsMvvmCrossSolution)
            {
                string templatesPath = this.settingsService.ConvertersTemplatesPath;

                List<ItemTemplateInfo> itemTemplateInfos = this.VisualStudioService.GetFolderTemplateInfos(templatesPath, "Converters");

                ItemTemplatesForm form = new ItemTemplatesForm(itemTemplateInfos, this.SettingsService.DisplayLogo);

                form.ShowDialog();

                if (form.Continue)
                {
                    this.WriteStatusBarMessage("Ninja Coder is running....");

                    IEnumerable<string> messages = this.converterService.AddConverters(
                                                        this.VisualStudioService.CoreProjectService, 
                                                        templatesPath, 
                                                        form.RequiredTemplates);

                    //// show the readme.
                    this.ShowReadMe("Add Converters", messages);

                    this.WriteStatusBarMessage("Ninja Coder has completed the adding of the converters.");
                }
            }
            else
            {
                this.ShowNotMvvmCrossSolutionMessage();
            }
        }
    }
}
