// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ServicesPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Presenters
{
    using System.Collections.Generic;

    using Views.Interfaces;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the ServicesPresenter type.
    /// </summary>
    public class ServicesPresenter : BasePresenter
    {
        /// <summary>
        /// The view.
        /// </summary>
        private readonly IServicesView view;

        /// <summary>
        /// The item template infos.
        /// </summary>
        private readonly List<ItemTemplateInfo> itemTemplateInfos;

        /// <summary>
        /// Initializes a new instance of the <see cref="ServicesPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        public ServicesPresenter(
            IServicesView view,
            List<ItemTemplateInfo> itemTemplateInfos)
        {
            this.view = view;
            this.itemTemplateInfos = itemTemplateInfos;
        }

        /// <summary>
        /// Loads this instance.
        /// </summary>
        /// <param name="viewModelNames">The view model names.</param>
        public void Load(List<string> viewModelNames)
        {
            foreach (ItemTemplateInfo itemTemplateInfo in this.itemTemplateInfos)
            {
                this.view.AddTemplate(itemTemplateInfo);
            }

            foreach (string viewModelName in viewModelNames)
            {
                this.view.AddViewModel(viewModelName);
            }
        }
    }
}
