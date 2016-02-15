// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the MvvmCrossSampleViewType type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Entities
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the MvvmCrossSampleViewType type.
    /// </summary>
    public enum MvvmCrossSampleViewType
    {
        /// <summary>
        /// The HardCoded
        /// </summary>
        [Description("HardCoded")]
        HardCoded,

        /// <summary>
        /// The Xib
        /// </summary>
        [Description("Xib")]
        Xib,

        /// <summary>
        /// The StoryBoard
        /// </summary>
        [Description("StoryBoard")]
        StoryBoard
    }
}