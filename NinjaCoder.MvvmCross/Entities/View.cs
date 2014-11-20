// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the View type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Entities
{
    /// <summary>
    ///  Defines the View type.
    /// </summary>
    public class View
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the framework.
        /// </summary>
        public string Framework { get; set; }

        /// <summary>
        /// Gets or sets the type of the page.
        /// </summary>
        public string PageType { get; set; }

        /// <summary>
        /// Gets or sets the type of the layout.
        /// </summary>
        public string LayoutType { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="View"/> is existing.
        /// </summary>
        public bool Existing { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [allow deletion].
        /// </summary>
        public bool AllowDeletion { get; set; }
    }
}
