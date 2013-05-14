// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ConvertersPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Presenters
{
    using System.Collections.Generic;
    using NinjaCoder.MvvmCross.Views.Interfaces;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the ConvertersPresenter type.
    /// </summary>
    public class ConvertersPresenter
    {
        /// <summary>
        /// The view.
        /// </summary>
        private readonly IConvertersView view;

        /// <summary>
        /// The item template infos.
        /// </summary>
        private readonly List<ItemTemplateInfo> itemTemplateInfos;

        /// <summary>
        /// Initializes a new instance of the <see cref="ConvertersPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        public ConvertersPresenter(
            IConvertersView view,
            List<ItemTemplateInfo> itemTemplateInfos)
        {
            this.view = view;
            this.itemTemplateInfos = itemTemplateInfos;
        }


        /// <summary>
        /// Loads the item templates.
        /// </summary>
        public void LoadItemTemplates()
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
