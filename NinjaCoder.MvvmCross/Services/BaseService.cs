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
    internal abstract class BaseService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService" /> class.
        /// </summary>
        protected BaseService()
        {
            this.Messages = new List<string>();
        }

        /// <summary>
        /// Gets or sets the messages.
        /// </summary>
        public List<string> Messages { get; protected set; }
    }
}
