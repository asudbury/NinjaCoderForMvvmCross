// --------------------------------------------------------------------------------------------------------------------
// <summary>
//      Defines the T4Cache type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Scorchio.VisualStudio.Services
{
    using System.Collections.Generic;

    /// <summary>
    /// Defines the T4Cache type.
    /// </summary>
    public class T4Cache
    {
        /// <summary>
        /// The files.
        /// </summary>
        private readonly Dictionary<string, string> files;

        /// <summary>
        /// Initializes a new instance of the <see cref="T4Cache" /> class.
        /// </summary>
        public T4Cache()
        {
            this.files = new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        public Dictionary<string, string> Files
        {
            get { return this.files; }
        }
    }
}