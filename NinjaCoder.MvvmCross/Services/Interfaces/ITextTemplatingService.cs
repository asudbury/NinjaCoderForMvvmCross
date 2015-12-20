// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the ITextTemplatingService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using System.Collections.Generic;
    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the ITextTemplatingService type.
    /// </summary>
    public interface ITextTemplatingService
    {
        /// <summary>
        /// Adds the text templates.
        /// </summary>
        /// <param name="statusBarMessage">The status bar message.</param>
        /// <param name="textTemplates">The text templates.</param>
        /// <returns></returns>
        IEnumerable<string> AddTextTemplates(
            string statusBarMessage,
            IEnumerable<TextTemplateInfo> textTemplates);
    }
}
