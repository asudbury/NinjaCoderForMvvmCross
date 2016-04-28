// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IProjectTemplateTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators.Interfaces
{
    using Scorchio.VisualStudio.Entities;
    using System.Xml.Linq;

    /// <summary>
    /// Defines the IProjectTemplateTranslator type.
    /// </summary>
    public interface IProjectTemplateTranslator
    {
        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>A ProjectTemplatInfo.</returns>
        ProjectTemplateInfo Translate(XElement from);
    }
}
