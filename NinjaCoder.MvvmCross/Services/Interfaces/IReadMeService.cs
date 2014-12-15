// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IReadMeService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the IReadMeService type.
    /// </summary>
    public interface IReadMeService
    {
        /// <summary>
        /// Adds the lines.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <param name="functionName">Name of the function.</param>
        /// <param name="lines">The lines.</param>
        void AddLines(
            string filePath, 
            string functionName, 
            IEnumerable<string> lines);

        /// <summary>
        /// Gets the seperator line.
        /// </summary>
        /// <returns></returns>
        string GetSeperatorLine();
    }
}
