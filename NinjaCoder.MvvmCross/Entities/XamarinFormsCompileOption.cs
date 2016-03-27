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
        /// The Compile option.
        /// </summary>
        [Description("Compile")]
        Compile,

        /// <summary>
        /// The Skip option.
        /// </summary>
        [Description("Skip")]
        Skip,
    }
}