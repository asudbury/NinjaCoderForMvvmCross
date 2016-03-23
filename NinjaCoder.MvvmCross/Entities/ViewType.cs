// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the ViewType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Entities
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the ViewType type.
    /// </summary>
    public enum ViewType
    {
        /// <summary>
        /// The Blank
        /// </summary>
        [Description("Blank")]
        Blank,

        /// <summary>
        /// The SampleData
        /// </summary>
        [Description("SampleData")]
        SampleData,

        /// <summary>
        /// The Web
        /// </summary>
        [Description("Web")]
        Web
    }
}