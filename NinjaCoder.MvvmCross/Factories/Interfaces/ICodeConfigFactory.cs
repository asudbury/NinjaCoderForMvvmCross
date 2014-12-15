// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ICodeConfigFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using Services.Interfaces;

    /// <summary>
    /// Defines the ICodeConfigFactory type.
    /// </summary>
    public interface ICodeConfigFactory
    {
        /// <summary>
        /// Gets the code config service.
        /// </summary>
        /// <returns>The code config service.</returns>
        ICodeConfigService GetCodeConfigService();
    }
}
