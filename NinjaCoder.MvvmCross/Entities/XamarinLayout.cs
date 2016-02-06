// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the XamarinLayout type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Entities
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the XamarinLayout type.
    /// </summary>
    public enum XamarinLayout
    {
        /// <summary>
        /// The StackLayout.
        /// </summary>
        [Description("Stack Layout")]
        StackLayout,

        /// <summary>
        /// The AbsoluteLayout.
        /// </summary>
        [Description("Absolute Layout")]
        AbsoluteLayout,

        /// <summary>
        /// The RelativeLayout.
        /// </summary>
        [Description("Relative Layout")]
        RelativeLayout,

        /// <summary>
        /// The GridLayout.
        /// </summary>
        [Description("Grid Layout")]
        GridLayout,

        /// <summary>
        /// The ContentView.
        /// </summary>
        [Description("Content View")]
        ContentView,

        /// <summary>
        /// The ScrollView.
        /// </summary>
        [Description("Scroll View")]
        ScrollView,

        /// <summary>
        /// The Frame.
        /// </summary>
        [Description("Frame")]
        Frame
    }
}