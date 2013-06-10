// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IViewModelOptionsView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Views.Interfaces
{
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the IViewModelOptionsView type.
    /// </summary>
    public interface IViewModelOptionsView
    {
        /// <summary>
        /// Gets or sets the name of the view model.
        /// </summary>
        string ViewModelName { get; set; }

        /// <summary>
        /// Gets the required templates.
        /// </summary>
        List<ItemTemplateInfo> RequiredTemplates { get; }

        /// <summary>
        /// Adds the template.
        /// </summary>
        /// <param name="itemTemplateInfo">The item template info.</param>
        void AddTemplate(ItemTemplateInfo itemTemplateInfo);
    }
}