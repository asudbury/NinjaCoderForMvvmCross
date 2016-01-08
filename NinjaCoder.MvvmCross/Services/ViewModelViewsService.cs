// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewModelViewsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Constants;
    using Factories.Interfaces;
    using Interfaces;
    using NinjaCoder.MvvmCross.Entities;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    ///  Defines the ViewModelViewsService type.
    /// </summary>
    internal class ViewModelViewsService : BaseService, IViewModelViewsService
    {
        /// <summary>
        /// The visual studio service.
        /// </summary>
        private readonly IVisualStudioService visualStudioService;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The view model and views factory.
        /// </summary>
        private readonly IViewModelAndViewsFactory viewModelAndViewsFactory;

        /// <summary>
        /// The file operation service.
        /// </summary>
        private readonly IFileOperationService fileOperationService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelViewsService" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="viewModelAndViewsFactory">The view model and views factory.</param>
        /// <param name="fileOperationService">The file operation service.</param>
        public ViewModelViewsService(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService,
            IViewModelAndViewsFactory viewModelAndViewsFactory,
            IFileOperationService fileOperationService)
        {
            TraceService.WriteLine("ViewModelViewsService::Constructor");

            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
            this.viewModelAndViewsFactory = viewModelAndViewsFactory;
            this.fileOperationService = fileOperationService;
        }

        /// <summary>
        /// Adds the view model and views.
        /// </summary>
        /// <param name="templateInfos">The template infos.</param>
        /// <param name="textTemplateInfos">The text template infos.</param>
        /// <returns>
        /// The messages.
        /// </returns>
        public IEnumerable<string> AddViewModelAndViews(
            IEnumerable<ItemTemplateInfo> templateInfos,
            IEnumerable<TextTemplateInfo> textTemplateInfos)
        {
            TraceService.WriteLine("ViewModelViewsService::AddViewModelAndViews");

            List<string> messages = new List<string>();

            List<TextTemplateInfo> textTemplates = textTemplateInfos.ToList();

            messages.AddRange(this.visualStudioService.SolutionService.AddItemTemplateToProjects(textTemplates));
            messages.AddRange(this.visualStudioService.SolutionService.AddItemTemplateToProjects(templateInfos));

            //// now add any post action commands

            foreach (TextTemplateInfo textTemplateInfo in textTemplates)
            {
                if (textTemplateInfo.FileOperations != null)
                {
                    foreach (FileOperation fileOperation in textTemplateInfo.FileOperations)
                    {
                        this.fileOperationService.ProcessCommand(fileOperation);
                    }
                }
            }
            
            TraceService.WriteLine("ViewModelViewsService::AddViewModelAndViews END");

            return messages;
        }

        /// <summary>
        /// Adds the view models and views.
        /// </summary>
        /// <param name="views">The views.</param>
        /// <returns></returns>
        public IEnumerable<string> AddViewModelsAndViews(IEnumerable<View> views)
        {
            TraceService.WriteLine("ViewModelViewsService::AddViewModelsAndViews");

            List<string> messages = new List<string>();

            this.visualStudioService.WriteStatusBarMessage(NinjaMessages.AddingViewModelAndViews);

            if (this.settingsService.FrameworkType == FrameworkType.MvvmCrossAndXamarinForms)
            {
                this.settingsService.BindXamlForXamarinForms = true;
                this.settingsService.BindContextInXamlForXamarinForms = false;
            }
            else
            {
                this.settingsService.BindXamlForXamarinForms = true;
                this.settingsService.BindContextInXamlForXamarinForms = true;
            }

            foreach (View view in views)
            {
                if (view.Existing == false)
                {
                    string viewModelName = view.Name + "ViewModel";

                    IEnumerable<ItemTemplateInfo> itemTemplateInfos = this.viewModelAndViewsFactory.GetRequiredViewModelAndViews(
                            view,
                            viewModelName,
                            this.viewModelAndViewsFactory.AllowedUIViews,
                            this.visualStudioService.CoreTestsProjectService != null);

                    IEnumerable<TextTemplateInfo> textTemplateInfos = this.viewModelAndViewsFactory.GetRequiredTextTemplates(
                            view,
                            viewModelName,
                            this.viewModelAndViewsFactory.AllowedUIViews,
                            this.visualStudioService.CoreTestsProjectService != null);

                    IEnumerable<string> viewModelMessages = this.AddViewModelAndViews(
                        itemTemplateInfos,
                        textTemplateInfos);

                    messages.AddRange(viewModelMessages);
                }
            }

            TraceService.WriteLine("ViewModelViewsService::AddViewModelsAndViews END");

            return messages;
        }
    }
}