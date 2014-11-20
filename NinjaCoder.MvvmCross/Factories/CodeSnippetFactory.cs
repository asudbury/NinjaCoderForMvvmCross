// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the CodeSnippetFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using Entities;

    using Interfaces;
    using Scorchio.Infrastructure.Services.Testing.Interfaces;
    using Scorchio.Infrastructure.Translators;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System.IO.Abstractions;

    /// <summary>
    /// Defines the CodeSnippetFactory type.
    /// </summary>
    public class CodeSnippetFactory : ICodeSnippetFactory
    {
        /// <summary>
        /// The code snippet service.
        /// </summary>
        private readonly ICodeSnippetService codeSnippetService;

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// The translator.
        /// </summary>
        private readonly ITranslator<string, CodeSnippet> translator;

        /// <summary>
        /// The mocking service.
        /// </summary>
        private readonly IMockingService mockingService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CodeSnippetFactory" /> class.
        /// </summary>
        /// <param name="codeSnippetService">The code snippet service.</param>
        /// <param name="fileSystem">The file system.</param>
        /// <param name="settingsService">The settings service.</param>
        /// <param name="translator">The translator.</param>
        /// <param name="mockingServiceFactory">The mocking service factory.</param>
        public CodeSnippetFactory(
            ICodeSnippetService codeSnippetService,
            IFileSystem fileSystem,
            ISettingsService settingsService,
            ITranslator<string, CodeSnippet> translator,
            IMockingServiceFactory mockingServiceFactory)
        {
            TraceService.WriteLine("CodeSnippetFactory::Constructor");

            this.codeSnippetService = codeSnippetService;
            this.settingsService = settingsService;
            this.translator = translator;
            
            this.mockingService = mockingServiceFactory.GetMockingService();
        }

        /// <summary>
        /// Gets the code snippet service.
        /// </summary>
        /// <returns>
        /// The code snippet service.
        /// </returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public ICodeSnippetService GetCodeSnippetService()
        {
            TraceService.WriteLine("CodeSnippetFactory::GetCodeSnippetService");

            return this.codeSnippetService;
        }

        /// <summary>
        /// Gets the snippet.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The code snippet.</returns>
        public CodeSnippet GetSnippet(string path)
        {
            TraceService.WriteLine("CodeSnippetFactory::GetSnippet path=" + path);

            return this.translator.Translate(path);
        }

        /// <summary>
        /// Gets the plugin snippet.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <returns>
        /// The code snippet.
        /// </returns>
        public CodeSnippet GetPluginSnippet(Plugin plugin)
        {
            TraceService.WriteLine("CodeSnippetFactory::GetPluginSnippet plugin=" + plugin.FriendlyName);

            string fileName = string.Format(@"Plugins.{0}.xml", plugin.FriendlyName);

            return this.GetSnippet(
                this.settingsService.PluginsCodeSnippetsPath,
                fileName);
        }

        /// <summary>
        /// Gets the plugin test snippet.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <returns>The code snippet.</returns>
        public CodeSnippet GetPluginTestSnippet(Plugin plugin)
        {
            TraceService.WriteLine("CodeSnippetFactory::GetPluginTestSnippet pluign=" + plugin);
            
            string fileName = string.Format(@"Plugins.{0}.Tests.xml", plugin.FriendlyName);
            
            CodeSnippet codeSnippet = this.GetSnippet(
                this.settingsService.PluginsCodeSnippetsPath,
                fileName);

            this.BuildTestingSnippet(codeSnippet);

            return codeSnippet;
        }

        /// <summary>
        /// Gets the service snippet.
        /// </summary>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <returns>The code snippet.</returns>
        public CodeSnippet GetServiceSnippet(string friendlyName)
        {
            TraceService.WriteLine("CodeSnippetFactory::GetServiceSnippet fileName=" + friendlyName);

            string fileName = string.Format(@"Services.{0}.xml", friendlyName);

            return this.GetSnippet(
                this.settingsService.ServicesCodeSnippetsPath,
                fileName);
        }

        /// <summary>
        /// Gets the service test snippet.
        /// </summary>
        /// <param name="friendlyName">Name of the friendly.</param>
        /// <returns>The code snippet.</returns>
        public CodeSnippet GetServiceTestSnippet(string friendlyName)
        {
            TraceService.WriteLine("CodeSnippetFactory::GetServiceTestSnippet fileName=" + friendlyName);

            string fileName = string.Format(@"Services.{0}.Tests.xml", friendlyName);

            return this.GetSnippet(
                this.settingsService.CodeSnippetsPath,
                fileName);
        }

        /// <summary>
        /// Gets the snippet.
        /// </summary>
        /// <param name="coreDirectory">The core directory.</param>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>The code snippet.</returns>
        internal CodeSnippet GetSnippet(
            string coreDirectory,
            string fileName)
        {
            TraceService.WriteLine("CodeSnippetFactory::GetSnippet fileName=" + fileName);

            //// use the core if no user version of the snippet.
            string snippetPath = string.Format(
                "{0}{1}",
                coreDirectory,
                fileName);

            TraceService.WriteLine("path=" + snippetPath);

            return this.GetSnippet(snippetPath);
        }

        /// <summary>
        /// Builds the testing snippet.
        /// </summary>
        /// <param name="codeSnippet">The code snippet.</param>
        internal void BuildTestingSnippet(CodeSnippet codeSnippet)
        {
            TraceService.WriteLine("CodeSnippetFactory::BuildTestingSnippet");

            //// we grab thecurrious.core files and add them to the test project.
            //// doing this way means we don't need them in the xml files
            string assemblies = this.settingsService.UnitTestingAssemblies;

            if (string.IsNullOrEmpty(assemblies) == false)
            {
                string[] parts = assemblies.Split(',');

                foreach (string part in parts)
                {
                    codeSnippet.UsingStatements.Add(part);
                }
            }

            //// adding the specific mocking framework assembly reference.
            codeSnippet.UsingStatements.Add(this.mockingService.MockingAssemblyReference);

            //// add in the init method here- doing this way means we dont need it in the xml files
            codeSnippet.TestInitMethod = this.settingsService.UnitTestingInitMethod;
        }
    }
}
