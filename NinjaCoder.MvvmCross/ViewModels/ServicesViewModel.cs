// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ServicesViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels
{
    using Scorchio.Infrastructure.Wpf.ViewModels;
    using Scorchio.VisualStudio.Entities;
    using Services.Interfaces;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    /// <summary>
    ///  Defines the ServicesViewModel type.
    /// </summary>
    public class ServicesViewModel : NinjaBaseViewModel
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The visual studio service.
        /// </summary>
        private readonly IVisualStudioService visualStudioService;

        /// <summary>
        /// The view model names.
        /// </summary>
        private IEnumerable<string> viewModelNames;

        /// <summary>
        /// The implement in view model
        /// </summary>
        private string implementInViewModel;

        /// <summary>
        /// The include unit tests.
        /// </summary>
        private bool includeUnitTests = true;

        /// <summary>
        /// The services.
        /// </summary>
        private readonly ObservableCollection<SelectableItemViewModel<ItemTemplateInfo>> services;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesViewModel" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="visualStudioService">The visual studio service.</param>
        public ServicesViewModel(
            ISettingsService settingsService,
            IVisualStudioService visualStudioService) 
            : base(settingsService)
        {
            this.settingsService = settingsService;
            this.visualStudioService = visualStudioService;

            string templatesPath = this.settingsService.ServicesTemplatesPath;

            this.services = new ObservableCollection<SelectableItemViewModel<ItemTemplateInfo>>();
            List<ItemTemplateInfo> itemTemplateInfos = this.visualStudioService.GetFolderTemplateInfos(templatesPath);
            
            itemTemplateInfos
                .ForEach(x => this.services.Add(new SelectableItemViewModel<ItemTemplateInfo>(x)));
        }

        /// <summary>
        /// Gets the view model names.
        /// </summary>
        public IEnumerable<string> ViewModelNames
        {
            get { return this.viewModelNames ?? (this.viewModelNames = this.visualStudioService.GetPublicViewModelNames()); }
        }

        /// <summary>
        /// Gets or sets the implement in view model.
        /// </summary>
        public string ImplementInViewModel
        {
            get { return this.implementInViewModel; }
            set { this.SetProperty(ref this.implementInViewModel, value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [include unit tests].
        /// </summary>
        public bool IncludeUnitTests
        {
            get { return this.includeUnitTests; }
            set { this.SetProperty(ref this.includeUnitTests, value); }
        }

        public ObservableCollection<SelectableItemViewModel<ItemTemplateInfo>> Services
        {
            get { return this.services; }
        }

        /// <summary>
        /// Gets the required services.
        /// </summary>
        /// <returns>The required services.</returns>
        public IEnumerable<ItemTemplateInfo> GetRequiredServices()
        {
            return this.services.ToList()
                .Where(x => x.IsSelected)
                .Select(x => x.Item).ToList();
        }
    }
}
