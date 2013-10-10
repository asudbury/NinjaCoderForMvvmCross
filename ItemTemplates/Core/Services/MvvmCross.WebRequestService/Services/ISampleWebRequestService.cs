// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ISampleWebRequestService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.WebRequestService.Services
{
    using System;
    using System.Collections.Generic;
    using Entities;

    /// <summary>
    ///  Defines the ISampleWebRequestService type.
    /// </summary>
    public interface ISampleWebRequestService
    {
        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <param name="success">The success.</param>
        /// <param name="error">The error.</param>
        void Execute(
            Action<IEnumerable<WebRequestSampleData>> success,
            Action<Exception> error);
    }
}