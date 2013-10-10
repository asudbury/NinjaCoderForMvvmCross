// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseWebRequestService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.WebRequestService.Services
{
    using System;
    using System.IO;
    using System.Net;

    /// <summary>
    /// Defines the BaseWebRequestService type.
    /// </summary>
    public abstract class BaseWebRequestService
    {
        /// <summary>
        /// Gets the error handler.
        /// </summary>
        public Action<Exception> Error { get; private set; }

        /// <summary>
        /// Executes this instance.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <param name="error">The error.</param>
        public void Execute(
            string url,
            Action<Exception> error)
        {
            try
            {
                this.Error = error;
                WebRequest request = WebRequest.Create(new Uri(url));
                request.BeginGetResponse(this.ReadCallback, request);
            }
            catch (Exception exception)
            {
                this.Error(exception);
            }
        }

        /// <summary>
        /// Reads the callback.
        /// </summary>
        /// <param name="asynchronousResult">The asynchronous result.</param>
        internal void ReadCallback(IAsyncResult asynchronousResult)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)asynchronousResult.AsyncState;
                HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asynchronousResult);

                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    string resultString = streamReader.ReadToEnd();
                    this.HandleResponse(resultString);
                }
            }
            catch (Exception exception)
            {
                this.Error(exception);
            }
        }

        /// <summary>
        /// Handles the response.
        /// </summary>
        /// <param name="response">The response.</param>
        internal virtual void HandleResponse(string response)
        {
        }
    }
}
