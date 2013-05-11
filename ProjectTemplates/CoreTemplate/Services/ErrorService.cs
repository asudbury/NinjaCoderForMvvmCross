// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ErrorService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoreTemplate.Services
{
    using Cirrious.MvvmCross.Plugins.Messenger;

    using CoreTemplate.Entities;

    /// <summary>
    /// Defines the ErrorService type.
    /// </summary>
    public class ErrorService : IErrorService
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
        /// <param name="error">The error.</param>
        public void ReportError(string error)
        {
            this.messenger.Publish(new ErrorMessage(this, error));
        }
    }
}
