// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewModelOptionsPresenter type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Presenters
{
    using System.Collections.Generic;
    using Constants;
    using Scorchio.VisualStudio.Entities;
    using Views.Interfaces;

    /// <summary>
    ///  Defines the ViewModelOptionsPresenter type.
    /// </summary>
    public class ViewModelOptionsPresenter : BasePresenter
    {
        /// <summary>
        /// The view.
        /// </summary>
        private readonly IViewModelOptionsView view;

        /// <summary>
        /// The item template infos.
        /// </summary>
        private readonly List<ItemTemplateInfo> itemTemplateInfos;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelOptionsPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        public ViewModelOptionsPresenter(
             IViewModelOptionsView view,
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
            const string ViewModelSuffix = "ViewModel";

            string viewName = this.view.ViewModelName.Remove(this.view.ViewModelName.Length - ViewModelSuffix.Length) + "View.cs";

            this.itemTemplateInfos.Clear();

            //// first add the view model

            ItemTemplateInfo viewModelTemplateInfo = new ItemTemplateInfo
            {
                ProjectSuffix = ProjectSuffixes.Core,
                FolderName = "ViewModels",
                TemplateName = ItemTemplates.ViewModel,
                FileName = this.view.ViewModelName,
            };

            this.itemTemplateInfos.Add(viewModelTemplateInfo);

            foreach (ItemTemplateInfo itemTemplateInfo in this.view.RequiredTemplates)
            {
                itemTemplateInfo.FileName = viewName;

                this.itemTemplateInfos.Add(itemTemplateInfo);
            }

            //// do we require a Test ViewModel?

            if (this.view.IncludeUnitTests)
            {
                viewModelTemplateInfo = new ItemTemplateInfo
                {
                    ProjectSuffix = ProjectSuffixes.CoreTests,
                    FolderName = "ViewModels",
                    TemplateName = ItemTemplates.TestViewModel,
                    FileName = "Test" + this.view.ViewModelName,
                };

                this.itemTemplateInfos.Add(viewModelTemplateInfo);
            }

            return this.itemTemplateInfos;
        }
    }
}
