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
    internal interface ICodeConfigService
    {
        /// <summary>
        /// Processes the code config.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="extensionSource">The extension source.</param>
        /// <param name="extensionDestination">The extension destination.</param>
        /// <returns>The code config.</returns>
        CodeConfig ProcessCodeConfig(
            IProjectService projectService, 
            string friendlyName, 
            string extensionSource, 
            string extensionDestination);

        /// <summary>
        /// Gets the code config.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The code config.</returns>
        CodeConfig GetCodeConfigFromPath(string path);

        /// <summary>
        /// Gets the code config.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="useProjectExtension">if set to <c>true</c> [use project extension].</param>
        /// <returns>The code config.</returns>
        CodeConfig GetCodeConfig(
            IProjectService projectService, 
            string friendlyName, 
            bool useProjectExtension);

        /// <summary>
        /// Gets the ui code config.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <returns>The code config.</returns>
        CodeConfig GetUICodeConfig(
            IProjectService projectService, 
            string friendlyName);

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