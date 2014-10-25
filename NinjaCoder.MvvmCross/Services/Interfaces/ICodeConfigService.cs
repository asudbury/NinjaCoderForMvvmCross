// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ICodeConfigService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services.Interfaces
{
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the ICodeConfigService type.
    /// </summary>
    public interface ICodeConfigService
    {
        /// <summary>
        /// Gets the nuget command.
        /// </summary>
        /// <param name="codeConfig">The code config.</param>
        /// <returns>The nuget command.</returns>
        string GetNugetCommand(CodeConfig codeConfig);

        /// <summary>
        /// Gets the project nuget command if we need one.
        /// </summary>
        /// <param name="codeConfig">The code config.</param>
        /// <param name="projectService">The project service.</param>
        /// <returns>The nuget command if applicable.</returns>
        string GetProjectNugetCommand(
            CodeConfig codeConfig, 
            IProjectService projectService);

        /// <summary>
        /// Gets the name of the bootstrap file.
        /// </summary>
        /// <param name="codeConfig">The code config.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <returns>The bootstrap file name.</returns>
        string GetBootstrapFileName(
            CodeConfig codeConfig, 
            string friendlyName);

        /// <summary>
        /// Applies the code dependencies.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="codeConfig">The code config.</param>
        /// <returns>The messages.</returns>
        IEnumerable<string> ApplyCodeDependencies(
            IVisualStudioService visualStudioService, 
            CodeConfig codeConfig);
    }
}