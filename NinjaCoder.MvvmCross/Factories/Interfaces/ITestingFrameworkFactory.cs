// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the ITestingFrameworkFactory type.
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
        /// <returns>The Testing class attribute.</returns>
        string GetTestingClassAttribute();

        /// <summary>
        /// Gets the testing method attribute.
        /// </summary>
        /// <returns>The Testing method attribute.</returns>
        string GetTestingMethodAttribute();

        /// <summary>
        /// Gets the testing library.
        /// </summary>
        /// <returns>The Testing class library.</returns>
        string GetTestingLibrary();
    }
}
