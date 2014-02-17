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
        /// Processes the code config.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="codeConfig">The code config.</param>
        /// <param name="extensionSource">The extension source.</param>
        /// <param name="extensionDestination">The extension destination.</param>
        void ProcessCodeConfig(
            IProjectService projectService, 
            CodeConfig codeConfig,
            string extensionSource, 
            string extensionDestination);

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
        /// Uses the local disk copy.
        /// </summary>
        /// <param name="codeConfig">The code config.</param>
        /// <returns>True or false.</returns>
        bool UseLocalDiskCopy(CodeConfig codeConfig);

        /// <summary>
        /// Nuget has been requested and is not supported.
        /// </summary>
        /// <param name="codeConfig">The code config.</param>
        /// <returns>True or false.</returns>
        bool NugetRequestedAndNotSupported(CodeConfig codeConfig);

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