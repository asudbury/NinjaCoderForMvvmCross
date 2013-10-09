// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IServicesService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Entities;

    /// <summary>
    ///  Defines the IServicesService type.
    /// </summary>
    public interface IServicesService
    {
        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        List<string> NugetCommands { get; }

        /// <summary>
        /// Adds the services.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="itemTemplateInfos">The item template infos.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="createUnitTests">if set to <c>true</c> [create unit tests].</param>
        /// <returns> The messages.</returns>
        List<string> AddServices(
                IVisualStudioService visualStudioService,
                IEnumerable<ItemTemplateInfo> itemTemplateInfos,
                string viewModelName,
                bool createUnitTests);
    }
}