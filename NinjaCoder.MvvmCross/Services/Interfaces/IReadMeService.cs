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
        /// <param name="errorMessages">The error messages.</param>
        /// <param name="traceMessages">The trace messages.</param>
        void AddLines(
            string filePath, 
            string functionName, 
            IEnumerable<string> lines,
            IEnumerable<string> errorMessages,
            IEnumerable<string> traceMessages);

        /// <summary>
        /// Gets the header line.
        /// </summary>
        /// <param name="headerText">The header text.</param>
        /// <returns>
        /// The header line.
        /// </returns>
        string GetHeaderLine(string headerText);

        /// <summary>
        /// Gets the seperator line.
        /// </summary>
        /// <returns>The line seperator.</returns>
        string GetSeperatorLine();
    }
}
