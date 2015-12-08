// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Helper type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests
{
    using System.IO;

    /// <summary>
    ///  Defines the Helper type.
    /// </summary>
    public static class Helper
    {
        /// <summary>
        /// Gets the test data path.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The path of the test file.</returns>
        public static string GetTestDataPath(string fileName)
        {
            string testDirectory = @"C:\Scorchio\Projects\c#\NinjaCoderForMvvmCross\NinjaCoder.MvvmCross.Tests\TestData\";

            if (Directory.Exists(testDirectory) == false)
            {
                testDirectory = @"C:\Projects\c#\NinjaCoderForMvvmCross\NinjaCoder.MvvmCross.Tests\TestData\";
            }

            return string.Format("{0}{1}", testDirectory, fileName);
        }
    }
}
