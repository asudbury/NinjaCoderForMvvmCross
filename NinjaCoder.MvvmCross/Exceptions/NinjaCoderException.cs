// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NinjaCoderException type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Exceptions
{
    using System;

    /// <summary>
    ///  Defines the NinjaCoderException type.
    /// </summary>
    public class NinjaCoderException : Exception
    {
        /// <summary>
        /// Gets or sets the ninja message.
        /// </summary>
        public string NinjaMessage { get; set; }
    }
}
