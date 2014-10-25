// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the RouteModifier type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Wpf.ViewModels.Wizard
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Defines the RouteModifier type.
    /// </summary>
    public class RouteModifier
    {
        /// <summary>
        /// Gets or sets the exclude view types.
        /// </summary>
        public List<Type> ExcludeViewTypes { get; set; }

        /// <summary>
        /// Gets or sets the include view types.
        /// </summary>
        public List<Type> IncludeViewTypes { get; set; }

        /// <summary>
        /// Adds to exclude views.
        /// </summary>
        /// <param name="viewTypes">The view types.</param>
        public void AddToExcludeViews(List<Type> viewTypes)
        {
            if (this.ExcludeViewTypes == null)
            {
                this.ExcludeViewTypes = viewTypes;
            }
            else
            {
                this.ExcludeViewTypes.AddRange(viewTypes);
            }
        }

        /// <summary>
        /// Adds to include views.
        /// </summary>
        /// <param name="viewTypes">The view types.</param>
        public void AddToIncludeViews(List<Type> viewTypes)
        {
            if (this.IncludeViewTypes == null)
            {
                this.IncludeViewTypes = viewTypes;
            }
            else
            {
                this.IncludeViewTypes.AddRange(viewTypes);
            }
        }
    }
}
