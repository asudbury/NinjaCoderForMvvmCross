// --------------------------------------------------------------------------------------------------------------------
// <summary>
// 	Defines the ITestingFrameworkFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    /// <summary>
    ///  Defines the ITestingFrameworkFactory type.
    /// </summary>
    public interface ITestingFrameworkFactory
    {
        /// <summary>
        /// Gets the testing class attribute.
        /// </summary>
        /// <returns></returns>
        string GetTestingClassAttribute();

        /// <summary>
        /// Gets the testing method attribute.
        /// </summary>
        /// <returns></returns>
        string GetTestingMethodAttribute();

        /// <summary>
        /// Gets the testing library.
        /// </summary>
        /// <returns></returns>
        string GetTestingLibrary();
    }
}
