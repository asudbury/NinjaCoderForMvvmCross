// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IErrorService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoreTemplate.Services
{
    /// <summary>
    ///    Defines the IErrorService type.
    /// </summary>
    public interface IErrorService
    {
        /// <summary>
        /// Reports the error.
        /// </summary>
        /// <param name="error">The error.</param>
        void ReportError(string error);
    }
}
