// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IMessageBoxService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Scorchio.Infrastructure.Services
{
    /// <summary>
    ///  Defines the IMessageBoxService type.
    /// </summary>
    public interface IMessageBoxService
    {
        /// <summary>
        /// Shows the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        void Show(
            string text, 
            string caption);

    }
}