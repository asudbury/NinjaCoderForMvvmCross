// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MockDirectoryInfoFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Mocks
{
    using System.IO.Abstractions;

    /// <summary>
    ///  Defines the MockDirectoryInfoFactory type.
    /// </summary>
    public class MockDirectoryInfoFactory : IDirectoryInfoFactory
    {
        /// <summary>
        /// Froms the name of the directory.
        /// </summary>
        /// <param name="directoryName">Name of the directory.</param>
        /// <returns>The directory info.</returns>
        public DirectoryInfoBase FromDirectoryName(string directoryName)
        {
            return new MockDirectoryInfo();
        }
    }
}
