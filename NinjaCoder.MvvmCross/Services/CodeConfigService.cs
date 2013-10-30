// --------------------------------------------------------------------------------------------------------------------
//    Defines the CodeConfigService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System.Collections.Generic;
    using System.IO.Abstractions;

    using EnvDTE;

    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using NinjaCoder.MvvmCross.Translators;

    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;

    /// <summary>
    ///  Defines the CodeConfigService type.
    /// </summary>
    internal class CodeConfigService : ICodeConfigService
    {
        /// <summary>
        /// The file system.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The translator.
        /// </summary>
        private readonly ITranslator<string, CodeConfig> translator;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeConfigService" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="translator">The translator.</param>
        public CodeConfigService(
            IFileSystem fileSystem,
            ISettingsService settingsService,
            ITranslator<string, CodeConfig> translator)
        {
            this.fileSystem = fileSystem;
            this.settingsService = settingsService;
            this.translator = translator;
        }

        /// <summary>
        /// Processes the code config.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="extensionSource">The extension source.</param>
        /// <param name="extensionDestination">The extension destination.</param>
        /// <returns>The code config.</returns>
        public CodeConfig ProcessCodeConfig(
            IProjectService projectService,
            string friendlyName,
            string extensionSource,
            string extensionDestination)
        {
            TraceService.WriteLine("CodeConfigService::ProcessCodeConfig " + friendlyName);

            //// grab the code config if it exists and process it.
            CodeConfig codeConfig = this.GetUICodeConfig(projectService, friendlyName);

            if (codeConfig != null)
            {
                //// process it if we are using local disk copy.
                if (this.UseLocalDiskCopy(codeConfig))
                {
                    string sourceDirectory = this.fileSystem.Path.GetDirectoryName(extensionSource);
                    string destinationDirectory = this.fileSystem.Path.GetDirectoryName(extensionDestination);

                    if (codeConfig.References != null)
                    {
                        codeConfig.References.ForEach(
                            x => projectService.AddReference(
                                "Lib",
                                destinationDirectory + @"\" + x,
                                sourceDirectory + @"\" + x,
                                this.settingsService.IncludeLibFolderInProjects,
                                this.settingsService.CopyAssembliesToLibFolder));
                    }
                }
            }

            return codeConfig;
        }

        /// <summary>
        /// Gets the code config from path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The code config.</returns>
        public CodeConfig GetCodeConfigFromPath(string path)
        {
            return this.translator.Translate(path);
        }

        /// <summary>
        /// Gets the config.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="useProjectExtension">if set to <c>true</c> [use project extension].</param>
        /// <returns>The code config.</returns>
        public CodeConfig GetCodeConfig(
            IProjectService projectService,
            string friendlyName,
            bool useProjectExtension)
        {
            TraceService.WriteLine("CodeConfigService::GetConfig friendlyName=" + friendlyName);

            string projectExtension = string.Empty;

            //// look for ui config files!
            if (useProjectExtension &&
                projectService.Name.ToLower().Contains("core") == false)
            {
                projectExtension = "." + projectService.Name.Split('.')[1];
            }

            string path = string.Format(
                    @"{0}\Plugins\Config.Plugin.{1}{2}.xml",
                    this.settingsService.ConfigPath,
                    friendlyName,
                    projectExtension);

            TraceService.WriteLine("path=" + path);

            return this.translator.Translate(path);
        }

        /// <summary>
        /// Gets the UI code config.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <returns>The code config.</returns>
        public CodeConfig GetUICodeConfig(
            IProjectService projectService, 
            string friendlyName)
        {
            TraceService.WriteLine("CodeConfigService::GetUICodeConfig friendlyName=" + friendlyName);

            return this.GetCodeConfig(projectService, friendlyName, true);
        }

        /// <summary>
        /// Gets the nuget command.
        /// </summary>
        /// <param name="codeConfig">The code config.</param>
        /// <returns>The nuget command.</returns>
        public string GetNugetCommand(CodeConfig codeConfig)
        {
            TraceService.WriteLine("CodeConfigService::GetNugetCommand");

            if (codeConfig != null &&
                string.IsNullOrEmpty(codeConfig.NugetPackage) == false)
            {
                return codeConfig.NugetPackage;
            }

            return string.Empty;
        }

        /// <summary>
        /// Gets the project nuget command if we need one.
        /// </summary>
        /// <param name="codeConfig">The code config.</param>
        /// <param name="projectService">The project service.</param>
        /// <returns>The nuget command if applicable.</returns>
        public string GetProjectNugetCommand(
            CodeConfig codeConfig,
            IProjectService projectService)
        {
            TraceService.WriteLine("CodeConfigService::GetProjectNugetCommand");

            string command = string.Empty;

            string nugetCommand = this.GetNugetCommand(codeConfig);

            if (string.IsNullOrEmpty(nugetCommand) == false)
            {
                command = Settings.NugetInstallPackage.Replace("%s", nugetCommand);

                //// need to add the project to the end of the command!
                command += string.Format(" {0}", projectService.Name);

                TraceService.WriteLine("Command=" + command);
            }

            return command;
        }

        /// <summary>
        /// Uses the local disk copy.
        /// </summary>
        /// <param name="codeConfig">The code config.</param>
        /// <returns>True or false.</returns>
        public bool UseLocalDiskCopy(CodeConfig codeConfig)
        {
            return this.settingsService.UseNugetForPlugins == false ||
                   string.IsNullOrEmpty(this.GetNugetCommand(codeConfig));
        }

        /// <summary>
        /// Nuget has been requested and is not supported.
        /// </summary>
        /// <param name="codeConfig">The code config.</param>
        /// <returns>True or false.</returns>
        public bool NugetRequestedAndNotSupported(CodeConfig codeConfig)
        {
            return this.settingsService.UseNugetForPlugins && 
                   string.IsNullOrEmpty(this.GetNugetCommand(codeConfig));
        }

        /// <summary>
        /// Gets the name of the bootstrap file.
        /// </summary>
        /// <param name="codeConfig">The code config.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <returns>The bootstrap file name.</returns>
        public string GetBootstrapFileName(
            CodeConfig codeConfig, 
            string friendlyName)
        {
            TraceService.WriteLine("CodeConfigService::GetBootstrapFileName name=" + friendlyName);

            string bootstrapFileName = friendlyName + "PluginBootstrap.cs";

            //// the bootstrap file name can be overridden in the code config file.
            if (codeConfig != null &&
                string.IsNullOrEmpty(codeConfig.BootstrapFileNameOverride) == false)
            {
                bootstrapFileName = codeConfig.BootstrapFileNameOverride;

                TraceService.WriteLine("bootstrapFile has been overidden filename=" + bootstrapFileName);
            }

            return bootstrapFileName;
        }

        /// <summary>
        /// Applies the code dependencies.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="codeConfig">The code config.</param>
        /// <returns>The messages.</returns>
        public IEnumerable<string> ApplyCodeDependencies(
            IVisualStudioService visualStudioService, CodeConfig codeConfig)
        {
            TraceService.WriteLine("ServicesService::ApplyCodeDependencies");

            List<string> messages = new List<string>();

            //// apply any code dependencies
            if (codeConfig.CodeDependencies != null)
            {
                foreach (CodeSnippet codeSnippet in codeConfig.CodeDependencies)
                {
                    IEnumerable<string> snippetMessages = this.ApplyCodeSnippet(visualStudioService, codeSnippet);

                    messages.AddRange(snippetMessages);
                }
            }

            return messages;
        }

        /// <summary>
        /// Applies the code snippet.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="codeSnippet">The code snippet.</param>
        /// <returns>The messages.</returns>
        internal IEnumerable<string> ApplyCodeSnippet(
            IVisualStudioService visualStudioService, 
            CodeSnippet codeSnippet)
        {
            List<string> messages = new List<string>();

            //// find the project
            IProjectService projectService = visualStudioService.GetProjectServiceBySuffix(codeSnippet.Project);

            if (projectService != null)
            {
                //// find the class
                IProjectItemService projectItemService = projectService.GetProjectItem(codeSnippet.Class + ".cs");

                if (projectItemService != null)
                {
                    //// find the method.
                    CodeFunction codeFunction = projectItemService.GetFirstClass().GetFunction(codeSnippet.Method);

                    if (codeFunction != null)
                    {
                        string code = codeFunction.GetCode();

                        if (code.Contains(codeSnippet.Code.Trim()) == false)
                        {
                            codeFunction.InsertCode(codeSnippet.Code, true);

                            string message = string.Format(
                                "Code added to project {0} class {1} method {2}.",
                                projectService.Name,
                                codeSnippet.Class,
                                codeSnippet.Method);

                            messages.Add(message);
                        }
                    }
                }
            }

            return messages;
        }
    }
}
