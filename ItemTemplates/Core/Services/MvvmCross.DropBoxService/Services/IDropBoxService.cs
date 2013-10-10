// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IDropBoxService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.DropBoxService.Services
{
    using System;

    using Cirrious.MvvmCross.Plugins.Network.Rest;

    /// <summary>
    ///  Defines the IDropBoxService type.
    /// </summary>
    public interface IDropBoxService
    {
        /// <summary>
        /// Downloads a File from dropbox given the path
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path">The path of the file to download</param>
        /// <param name="root">The root.</param>
        /// <param name="successAction">The success action.</param>
        /// <param name="errorAction">The error action.</param>
        void GetFile<T>(
            string path,
            string root,
            Action<MvxDecodedRestResponse<T>> successAction, 
            Action<Exception> errorAction);
    }
}