// --------------------------------------------------------------------------------------------------------------------
// <summary>
//      Defines the T4CallBack type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Scorchio.VisualStudio.Services
{
    using Microsoft.VisualStudio.TextTemplating.VSHost;
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Defines the T4CallBack type.
    /// </summary>
    [Serializable]
    public class T4CallBack : ITextTemplatingCallback
    {
        /// <summary>
        /// Gets the error messages.
        /// </summary>
        public List<string> ErrorMessages { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="T4CallBack" /> class.
        /// </summary>
        public T4CallBack()
        {
            this.ErrorMessages = new List<string>();
        }

        /// <summary>
        /// Errors the callback.
        /// </summary>
        /// <param name="warning">if set to <c>true</c> [warning].</param>
        /// <param name="message">The message.</param>
        /// <param name="line">The line.</param>
        /// <param name="column">The column.</param>
        public void ErrorCallback(
            bool warning,
            string message,
            int line,
            int column)
        {
            string errorMessage = string.Format("{0} {1} {2} {3}", warning ? "Warning" : "Error ", line, column, message);

            this.ErrorMessages.Add(errorMessage);
        }

        /// <summary>
        /// Sets the file extension.
        /// </summary>
        /// <param name="extension">The extension.</param>
        public void SetFileExtension(string extension)
        {
        }

        /// <summary>
        /// Sets the output encoding.
        /// </summary>
        /// <param name="encoding">The encoding.</param>
        /// <param name="fromOutputDirective">if set to <c>true</c> [from output directive].</param>
        public void SetOutputEncoding(
            Encoding encoding, 
            bool fromOutputDirective)
        {
        }
    }
}