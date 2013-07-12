// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ItemTemplatesPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Presenters
{
    using System.Collections.Generic;
    using Scorchio.VisualStudio.Entities;
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
        /// The item template infos.
        /// </summary>
        private readonly List<ItemTemplateInfo> itemTemplateInfos;

        /// <summary>
        /// The display logo
        /// </summary>
        private readonly bool displayLogo;

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemTemplatesPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        /// <param name="displayLogo">if set to <c>true</c> [display logo].</param>
        public ItemTemplatesPresenter(
            IItemTemplatesView view,
            List<ItemTemplateInfo> itemTemplateInfos,
            bool displayLogo)
        {
            this.view = view;
            this.itemTemplateInfos = itemTemplateInfos;
            this.displayLogo = displayLogo;
        }
        
        /// <summary>
        /// Loads the item templates.
        /// </summary>
        public void Load()
        {
            this.view.DisplayLogo = this.displayLogo;

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
