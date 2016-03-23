// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the ProjectSuffix type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Entities
{
    using System.ComponentModel;

    /// <summary>
    /// Defines the ProjectSuffix type.
    /// </summary>
    public enum XamarinFormsCompileOption
    {
        /// <summary>
        /// Compile,
        /// </summary>
        [Description("Compile")]
        Compile,

        /// <summary>
        /// Skip.
        /// </summary>
        [Description("Skip")]
        Skip,
    }
}