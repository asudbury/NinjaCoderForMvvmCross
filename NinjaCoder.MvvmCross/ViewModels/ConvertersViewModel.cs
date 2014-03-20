// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ConvertersViewModel type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.ViewModels
{
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;

    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.Infrastructure.Wpf.ViewModels;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the ConvertersViewModel type.
    /// </summary>
    internal class ConvertersViewModel : BaseViewModel
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
        /// The converters.
        /// </summary>
        private ObservableCollection<SelectableItemViewModel<ItemTemplateInfo>> converters;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertersViewModel" /> class.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="settingsService">The settings service.</param>
        public ConvertersViewModel(
            IVisualStudioService visualStudioService,
            ISettingsService settingsService)
            : base(settingsService)
        {
            this.visualStudioService = visualStudioService;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Gets the converters.
        /// </summary>
        public ObservableCollection<SelectableItemViewModel<ItemTemplateInfo>> Converters
        {
            get
            {
                if (this.converters == null)
                {
                    this.converters = new ObservableCollection<SelectableItemViewModel<ItemTemplateInfo>>();

                    string templatesPath = this.settingsService.ConvertersTemplatesPath;

                    IEnumerable<ItemTemplateInfo> itemTemplateInfos = this.visualStudioService.GetAllowedConverters(templatesPath);

                    foreach (SelectableItemViewModel<ItemTemplateInfo> viewModel in itemTemplateInfos
                        .Select(itemTemplateInfo => new SelectableItemViewModel<ItemTemplateInfo>(itemTemplateInfo, itemTemplateInfo.PreSelected)))
                    {
                        this.converters.Add(viewModel);
                    }
                }

                return this.converters;
            }
        }

        /// <summary>
        /// Gets the required converters.
        /// </summary>
        /// <returns>The list of converters required.</returns>
        public IEnumerable<ItemTemplateInfo> GetRequiredConverters()
        {
            return this.converters
                .Where(viewModel => viewModel.IsSelected)
                .Select(viewModel => viewModel.Item).ToList();
        }
    }
}
