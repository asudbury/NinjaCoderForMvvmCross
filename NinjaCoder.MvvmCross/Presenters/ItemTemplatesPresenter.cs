// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ItemTemplatesPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Presenters
{
    using System.Collections.Generic;
    using Views.Interfaces;

    using Scorchio.VisualStudio.Entities;

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
        /// The item template infos.
        /// </summary>
        private readonly List<ItemTemplateInfo> itemTemplateInfos;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTemplatesPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        public ItemTemplatesPresenter(
            IItemTemplatesView view,
            List<ItemTemplateInfo> itemTemplateInfos)
        {
            this.view = view;
            this.itemTemplateInfos = itemTemplateInfos;
        }
        
        /// <summary>
        /// Loads the item templates.
        /// </summary>
        public void Load()
        {
            foreach (ItemTemplateInfo itemTemplateInfo in this.itemTemplateInfos)
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
