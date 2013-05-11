// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ErrorMessage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoreTemplate.Entities
{
    /// <summary>
    /// efines the ErrorMessage type.
    /// </summary>
    public class ErrorMessage : Message
    {
        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessage"/> class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="message">The message.</param>
        public ErrorMessage(object sender, string message)
            : base(sender)
        {
            this.Message = message;
        }
    }
}
