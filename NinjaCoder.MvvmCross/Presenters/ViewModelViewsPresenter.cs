// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ViewModelViewsPresenter type.
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
    public class ViewModelViewsPresenter : BasePresenter
    {
        /// <summary>
        /// The view.
        /// </summary>
        private readonly IViewModelViewsView view;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelViewsPresenter" /> class.
        /// </summary>
        /// <param name="view">The view.</param>
        public ViewModelViewsPresenter(IViewModelViewsView view)
        {
            this.view = view;
        }

        /// <summary>
        /// Loads the item templates.
        /// </summary>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        public void Load(IEnumerable<ItemTemplateInfo> itemTemplateInfos)
        {
            foreach (ItemTemplateInfo itemTemplateInfo in itemTemplateInfos)
            {
                this.view.AddTemplate(itemTemplateInfo);
            }
        }

        /// <summary>
        /// Updates the item templates.
        /// </summary>
        /// <returns>A list of required views.</returns>
        public IEnumerable<ItemTemplateInfo> GetRequiredItemTemplates()
        {
            const string ViewModelSuffix = "ViewModel";

            string viewName = this.view.ViewModelName.Remove(this.view.ViewModelName.Length - ViewModelSuffix.Length) + "View.cs";

            List<ItemTemplateInfo> itemTemplateInfos = new List<ItemTemplateInfo>();

            //// first add the view model

            ItemTemplateInfo viewModelTemplateInfo = new ItemTemplateInfo
            {
                ProjectSuffix = ProjectSuffixes.Core,
                FolderName = "ViewModels",
                TemplateName = ItemTemplates.ViewModel,
                FileName = this.view.ViewModelName,
            };

            itemTemplateInfos.Add(viewModelTemplateInfo);

            foreach (ItemTemplateInfo itemTemplateInfo in this.view.RequiredTemplates)
            {
                itemTemplateInfo.FileName = viewName;

                itemTemplateInfos.Add(itemTemplateInfo);
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

                itemTemplateInfos.Add(viewModelTemplateInfo);
            }

            return itemTemplateInfos;
        }
    }
}
