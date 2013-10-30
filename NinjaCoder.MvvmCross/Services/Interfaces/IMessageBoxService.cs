// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IMessageBoxService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    /// <summary>
    ///  Defines the IMessageBoxService type.
    /// </summary>
    internal interface IMessageBoxService
    {
        /// <summary>
        /// Shows the specified text.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="caption">The caption.</param>
        void Show(string text, string caption);
    }
}