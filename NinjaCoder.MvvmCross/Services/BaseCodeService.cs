// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the BaseCodeService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using Constants;
    using EnvDTE;
    using Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Translators;

    /// <summary>
    ///  Defines the BaseCodeService type.
    /// </summary>
    public abstract class BaseCodeService : BaseService
    {
        /// <summary>
        /// The translator.
        /// </summary>
        protected readonly ITranslator<string, CodeConfig> Translator;

        /// <summary>
        /// The file system.
        /// </summary>
        protected readonly IFileSystem FileSystem;

        /// <summary>
        /// The settings service.
        /// </summary>
        protected readonly ISettingsService SettingsService;

        /// <summary>
        /// The snippet service.
        /// </summary>
        protected readonly ISnippetService SnippetService;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseCodeService"/> class.
        /// </summary>
        /// <param name="translator">The translator.</param>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="snippetService">The snippet service.</param>
        protected BaseCodeService(
            ITranslator<string, CodeConfig> translator,
            IFileSystem fileSystem, 
            ISettingsService settingsService,
            ISnippetService snippetService)
        {
            TraceService.WriteLine("BaseCodeService::Constructor");

            this.Translator = translator;
            this.FileSystem = fileSystem;
            this.SettingsService = settingsService;
            this.SnippetService = snippetService;

            this.NugetCommands = new List<string>();
        }

        /// <summary>
        /// Gets the nuget commands.
        /// </summary>
        public List<string> NugetCommands { get; private set; }

        /// <summary>
        /// Inits the nuget.
        /// </summary>
        /// <param name="message">The message.</param>
        public void InitNuget(string message)
        {
            TraceService.WriteLine("BaseCodeService::InitNuget");

            //// make sure we clear out any nuget commands before we start adding the plugins!
            this.NugetCommands.Clear();

            //// put at the top of the stack!
            if (this.SettingsService.UseNugetForPlugins)
            {
                this.Messages.Add(message);
                this.Messages.Add(NinjaMessages.PmConsole);
                this.Messages.Add(string.Empty);
            }
        }

        /// <summary>
        /// Updates the via nuget.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="codeConfig">The code config.</param>
        /// <returns>True if updated via nuget.</returns>
        internal bool UpdateViaNuget(
            IProjectService projectService,
            CodeConfig codeConfig)
        {
            TraceService.WriteLine("BaseCodeService::UpdateViaNuget");

            bool updated = false;

            if (codeConfig != null &&
                string.IsNullOrEmpty(codeConfig.NugetPackage) == false)
            {
                string command = Settings.NugetInstallPackage.Replace("%s", codeConfig.NugetPackage);

                //// need to add the project to the end of the command!
                command += string.Format(" {0}", projectService.Name);

                this.NugetCommands.Add(command);

                updated = true;
            }

            return updated;
        }

        /// <summary>
        /// Creates the unit tests.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="projectService">The project service.</param>
        /// <param name="codeSnippetsPath">The code snippets path.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <param name="usingStatement">The using statement.</param>
        /// <returns>The project item service.</returns>
        protected virtual IProjectItemService CreateUnitTests(
            IVisualStudioService visualStudioService,
            IProjectService projectService,
            string codeSnippetsPath,
            string viewModelName,
            string friendlyName,
            string usingStatement)
        {
            TraceService.WriteLine("BaseCodeService::CreateUnitTests viewModelName=" + viewModelName);

            CodeSnippet codeSnippet = this.SnippetService.GetUnitTestingSnippet(codeSnippetsPath);

            if (codeSnippet != null)
            {
                if (string.IsNullOrEmpty(usingStatement) == false)
                {
                    codeSnippet.UsingStatements.Add(usingStatement);
                }

                if (this.SettingsService.ReplaceVariablesInSnippets)
                {
                    this.ApplyGlobals(visualStudioService, codeSnippet);
                }

                string fileName = "Test" + viewModelName;

                //// are we going to assume that the TestViewModel source file already exists?
                IProjectItemService projectItemService = projectService.GetProjectItem(fileName);

                if (projectItemService != null)
                {
                    projectItemService.ImplementUnitTestingCodeSnippet(
                        codeSnippet,
                        viewModelName,
                        this.SettingsService.RemoveDefaultFileHeaders,
                        this.SettingsService.RemoveDefaultComments,
                        this.SettingsService.FormatFunctionParameters);

                    this.Messages.Add(friendlyName + " test code added to " + fileName + ".cs in project " + projectService.Name + ".");

                    return projectItemService;
                }
            }
            else
            {
                TraceService.WriteError("BaseCodeService::CreateUnitTests File Not Found=" + codeSnippetsPath);
            }

            return null;
        }

        /// <summary>
        /// Applies the global variables.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="codeSnippet">The code snippet.</param>
        protected void ApplyGlobals(
            IVisualStudioService visualStudioService,
            CodeSnippet codeSnippet)
        {
            TraceService.WriteLine("BaseCodeService::ApplyGlobals");

            Globals globals = visualStudioService.DTE2.Solution.Globals;

            foreach (string variable in (Array)globals.VariableNames)
            {
                string value = globals[variable];

                TraceService.WriteLine("BaseCodeService::ApplyGlobals variable=" + variable + " value=" + value);

                if (value != null)
                {
                    codeSnippet.AddReplacementVariable(variable, value);
                }
            }
        }

        /// <summary>
        /// Removes the globals.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        protected void RemoveGlobals(IVisualStudioService visualStudioService)
        {
            TraceService.WriteLine("BaseCodeService::RemoveGlobals");

            Globals globals = visualStudioService.DTE2.Solution.Globals;

            foreach (string variable in (Array)globals.VariableNames)
            {
                globals[variable] = null;
            }
        }
    }
}
