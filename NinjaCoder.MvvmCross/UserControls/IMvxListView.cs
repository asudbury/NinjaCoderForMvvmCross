// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IMvxListView type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.UserControls
{
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the IMvxListView type.
    /// </summary>
    public interface IMvxListView
    {
        /// <summary>
        /// Gets the required templates.
        /// </summary>
        List<object> RequiredTemplates { get; }

        /// <summary>
        /// Adds the template.
        /// </summary>
        /// <param name="baseTemplateInfo">The base template info.</param>
        void AddTemplate(BaseTemplateInfo baseTemplateInfo);
    }
}