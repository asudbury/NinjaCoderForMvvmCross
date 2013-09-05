// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the BaseService type.
    /// </summary>
    public abstract class BaseService
    {
        /// <summary>
        /// The messages.
        /// </summary>
        private readonly List<string> messages;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService" /> class.
        /// </summary>
        protected BaseService()
        {
            this.messages = new List<string>();
        }

        /// <summary>
        /// Gets the messages.
        /// </summary>
        protected List<string> Messages
        {
            get { return this.messages; }
        }
    }
}
