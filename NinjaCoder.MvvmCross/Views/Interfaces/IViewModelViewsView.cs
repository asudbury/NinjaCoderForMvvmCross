// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IViewModelViewsView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Views.Interfaces
{
    using System.Collections.Generic;

    using Presenters;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the IViewModelOptionsView type.
    /// </summary>
    public interface IViewModelViewsView
    {
        /// <summary>
        /// Gets or sets the presenter.
        /// </summary>
        ViewModelViewsPresenter Presenter { get; set; }

        /// <summary>
        /// Sets a value indicating whether [display logo].
        /// </summary>
        bool DisplayLogo { set; }

        /// <summary>
        /// Gets or sets the name of the view model.
        /// </summary>
        string ViewModelName { get; set; }

        /// <summary>
        /// Gets or sets the view model initiated from.
        /// </summary>
        string ViewModelInitiatedFrom { get; set; }

        /// <summary>
        /// Gets or sets the view model to navigate to.
        /// </summary>
        string ViewModelToNavigateTo { get; set; }

        /// <summary>
        /// Gets the required templates.
        /// </summary>
        List<ItemTemplateInfo> RequiredTemplates { get; }

        /// <summary>
        /// Gets a value indicating whether [include unit tests].
        /// </summary>
        bool IncludeUnitTests { get; }

        /// <summary>
        /// Sets a value indicating whether [show view model navigation options].
        /// </summary>
        bool ShowViewModelNavigationOptions { set; }

        /// <summary>
        /// Adds the template.
        /// </summary>
        /// <param name="itemTemplateInfo">The item template info.</param>
        void AddTemplate(ItemTemplateInfo itemTemplateInfo);

        /// <summary>
        /// Adds the viewModel.
        /// </summary>
        /// <param name="viewModelName">Name of the view model.</param>
        void AddViewModel(string viewModelName);
    }
}