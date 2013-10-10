// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the DropBoxService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.DropBoxService.Services
{
    using System;

    using Cirrious.MvvmCross.Plugins.Network.Rest;

    /// <summary>
    ///  Defines the DropBoxService type.
    /// </summary>
    public class DropBoxService : IDropBoxService
    {
        /// <summary>
        /// The MVX json rest client.
        /// </summary>
        private readonly IMvxJsonRestClient mvxJsonRestClient;

        /// <summary>
        /// The version.
        /// </summary>
        private const string Version = "1.0.0";

        /// <summary>
        /// Initializes a new instance of the <see cref="DropBoxService" /> class.
        /// </summary>
        /// <param name="mvxJsonRestClient">The MVX Json rest client.</param>
        public DropBoxService(IMvxJsonRestClient mvxJsonRestClient)
        {
            this.mvxJsonRestClient = mvxJsonRestClient;
        }

        /// <summary>
        /// Downloads a File from dropbox given the path.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path.</param>
        /// <param name="root">The root.</param> 
        /// <param name="successAction">The success action.</param>
        /// <param name="errorAction">The error action.</param>
        public void GetFile<T>(
            string path, 
            string root, 
            Action<MvxDecodedRestResponse<T>> successAction, 
            Action<Exception> errorAction)
        {
            MvxRestRequest restRequest = new MvxRestRequest("adrian.com")
                                             {
                                                 Verb = "{version}/files/{root}{path}"
                                             };
            
            restRequest.Headers.Add("version", Version);
            restRequest.Headers.Add("path", path);
            restRequest.Headers.Add("root", root);

            this.mvxJsonRestClient.MakeRequestFor(restRequest, successAction, errorAction);
        }
    }
}
