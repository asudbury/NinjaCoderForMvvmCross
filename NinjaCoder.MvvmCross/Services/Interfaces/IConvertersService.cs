// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IConvertersService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the IConvertersService type.
    /// </summary>
    public interface IConvertersService
    {
        /// <summary>
        /// Adds the converters.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="templatesPath">The templates path.</param>
        /// <param name="templateInfos">The template infos.</param>
        /// <returns>The messages.</returns>
        IEnumerable<string> AddConverters(
            IProjectService projectService, 
            string templatesPath,
            List<ItemTemplateInfo> templateInfos);
    }
}