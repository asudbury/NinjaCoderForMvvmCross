// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ItemTemplatesPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Presenters
{
    using Scorchio.VisualStudio.Entities;
    using Services.Interfaces;
    using System.Collections.Generic;
    using Views.Interfaces;

    /// <summary>
    ///  Defines the ItemTemplatesPresenter type.
    /// </summary>
    public class ItemTemplatesPresenter : BasePresenter
    {
        /// <summary>
        /// The view.
        /// </summary>
        private readonly IItemTemplatesView view;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTemplatesPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="settingsService">The settings service.</param>
        public ItemTemplatesPresenter(
            IItemTemplatesView view,
            ISettingsService settingsService)
        {
            this.view = view;
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Loads the item templates.
        /// </summary>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        public void Load(IEnumerable<ItemTemplateInfo> itemTemplateInfos)
        {
            this.view.DisplayLogo = this.settingsService.DisplayLogo;

            foreach (ItemTemplateInfo itemTemplateInfo in itemTemplateInfos)
            {
                this.view.AddTemplate(itemTemplateInfo);
            }
        }

        /// <summary>
        /// Updates the item templates.
        /// </summary>
        /// <returns>A list of required views.</returns>
        public List<ItemTemplateInfo> GetRequiredItemTemplates()
        {
            return this.view.RequiredTemplates;
        }
    }
}
