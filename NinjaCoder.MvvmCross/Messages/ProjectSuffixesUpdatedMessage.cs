// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ProjectSuffixesUpdatedMessage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Messages
{
    using TinyMessenger;

    /// <summary>
    /// Deifnes the ProjectSuffixesUpdatedMessage type.
    /// </summary>
    public class ProjectSuffixesUpdatedMessage : ITinyMessage
    {
        /// <summary>
        /// Gets the sender of the message, or null if not supported by the message implementation.
        /// </summary>
        public object Sender { get; }
    }
}
