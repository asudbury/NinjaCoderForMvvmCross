// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the MvvmCrossProjectTemplate type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Entities
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the MvvmCrossProjectTemplate type.
    /// </summary>
    public enum MvvmCrossProjectTemplate
    {
        /// <summary>
        /// The core
        /// </summary>
        [Description("MvvmCross.Core.zip")]
        Core,

        /// <summary>
        /// The droid
        /// </summary>
        [Description("MvvmCross.Droid.zip")]
        Droid,

        /// <summary>
        /// The windows phone
        /// </summary>
        [Description("MvvmCross.WindowsPhone.zip")]
        WindowsPhone,
    }
}