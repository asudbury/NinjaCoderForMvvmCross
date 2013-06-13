// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ErrorService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoreTemplate.Services
{
    using System;

    using Cirrious.MvvmCross.Plugins.Messenger;

    using CoreTemplate.Entities;
    using CoreTemplate.EventArgs;

    /// <summary>
    /// Defines the ErrorService type.
    /// </summary>
    public class ErrorService : IErrorService, IErrorSource
    {
        /// <summary>
        /// The messenger
        /// </summary>
        private readonly IMvxMessenger messenger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorService"/> class.
        /// </summary>
        /// <param name="messenger">The messenger.</param>
        public ErrorService(IMvxMessenger messenger)
        {
            this.messenger = messenger;
        }

        /// <summary>
        /// Reports the error.
        /// </summary>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public void ReportError(string title, string message)
        {
            this.messenger.Publish(new ErrorMessage(this, title, message));
        }

        /// <summary>
        /// Occurs when error reported.
        /// </summary>
        public event EventHandler<ErrorEventArgs> ErrorReported;
    }
}
