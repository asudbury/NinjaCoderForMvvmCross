// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SampleWebRequestService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.WebRequestService.Services
{
    using System;
    using System.Collections.Generic;
    using Entities;

    using MvvmCross.WebRequestService.Translators;

    /// <summary>
    /// Defines the SampleWebRequestService type.
    /// </summary>
    public class SampleWebRequestService : BaseWebRequestService, ISampleWebRequestService
    {
        /// <summary>
        /// The translator.
        /// </summary>
        private readonly ISampleWebRequestTranslator translator;

        /// <summary>
        /// Initializes a new instance of the <see cref="SampleWebRequestService" /> class.
        /// </summary>
        /// <param name="translator">The translator.</param>
        public SampleWebRequestService(ISampleWebRequestTranslator translator)
        {
            this.translator = translator;
        }

        /// <summary>
        /// Gets the success handler.
        /// </summary>
        public Action<IEnumerable<WebRequestSampleData>> Success { get; private set; }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <param name="success">The success.</param>
        /// <param name="error">The error.</param>
        public void Execute(
            Action<IEnumerable<WebRequestSampleData>> success, 
            Action<Exception> error)
        {
            this.Success = success;

            //// TODO : change the url.

            this.Execute("http://test.com", error);
        }

        /// <summary>
        /// Handles the un formatted response.
        /// </summary>
        /// <param name="response">The response.</param>
        internal override void HandleResponse(string response)
        {
            this.Success(this.translator.Translate(response));
        }
    }
}
