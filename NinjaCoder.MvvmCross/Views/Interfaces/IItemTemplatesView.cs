// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IItemTemplatesView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Views.Interfaces
{
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the IItemTemplatesView type.
    /// </summary>
    public interface IItemTemplatesView
    {
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