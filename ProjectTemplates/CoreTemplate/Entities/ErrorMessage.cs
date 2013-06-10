// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ErrorMessage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CoreTemplate.Entities
{
    /// <summary>
    /// Defines the ErrorMessage type.
    /// </summary>
    public class ErrorMessage : Message
    {
        /// <summary>
        /// Gets the title.
        /// </summary>
        /// <value>
        /// The title.
        /// </value>
        public string Title { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="ErrorMessage" /> class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="title">The title.</param>
        /// <param name="message">The message.</param>
        public ErrorMessage(
            object sender,
            string title,
            string message)
            : base(sender)
        {
            this.Title = title;
            this.Message = message;
        }
    }
}
