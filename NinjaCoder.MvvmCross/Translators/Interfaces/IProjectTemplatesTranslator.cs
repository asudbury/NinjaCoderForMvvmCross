// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IProjectTemplatesTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators.Interfaces
{
    using Scorchio.VisualStudio.Entities;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the IProjectTemplatesTranslator type.
    /// </summary>
    public interface IProjectTemplatesTranslator
    {
        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>A list of ProjectTemplateInfos</returns>
        IEnumerable<ProjectTemplateInfo> Translate(string from);
    }
}
