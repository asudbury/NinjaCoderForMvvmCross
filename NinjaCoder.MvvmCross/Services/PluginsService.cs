// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using Constants;
    using Entities;
    using EnvDTE80;
    using Interfaces;
    using Scorchio.VisualStudio.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;
    using Scorchio.VisualStudio.Services.Interfaces;
    using Translators;

    /// <summary>
    /// Defines the PluginsService type.
    /// </summary>
    public class PluginsService : IPluginsService
    {
        /// <summary>
        /// The translator.
        /// </summary>
        private readonly ITranslator<string, CodeSnippet> translator;

        /// <summary>
        /// The file system.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// The messages
        /// </summary>
        private readonly List<string> messages;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsService" /> class.
        /// </summary>
        /// <param name="translator">The translator.</param>
        /// <param name="fileSystem">The file system.</param>
        public PluginsService(
            ITranslator<string, CodeSnippet> translator,
            IFileSystem fileSystem)
        {
            TraceService.WriteLine("PluginsService::Constructor");

            this.translator = translator;
            this.fileSystem = fileSystem;
            this.messages = new List<string>();
        }

        /// <summary>
        /// Adds the plugins.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="plugins">The plugins.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="codeSnippetsPath">The code snippets path.</param>
        /// <param name="createUnitTests">if set to <c>true</c> [create unit tests].</param>
        /// <returns>The messages.</returns>
        public IEnumerable<string> AddPlugins(
            IVisualStudioService visualStudioService,
            List<Plugin> plugins,
            string viewModelName,
            string codeSnippetsPath,
            bool createUnitTests)
        {
            TraceService.WriteLine("PluginsService::AddPlugins viewModelName=" + viewModelName);

            IProjectService coreProjectService = visualStudioService.CoreProjectService;

            this.AddProjectPlugins(coreProjectService, plugins, Settings.Core, Settings.Core);
            this.AddProjectPlugins(visualStudioService.DroidProjectService, plugins, Settings.Droid, Settings.Droid);
            this.AddProjectPlugins(visualStudioService.iOSProjectService, plugins, Settings.iOS, "Touch");
            this.AddProjectPlugins(visualStudioService.WindowsPhoneProjectService, plugins, Settings.WindowsPhone, Settings.WindowsPhone);
            this.AddProjectPlugins(visualStudioService.WindowsStoreProjectService, plugins, Settings.WindowsStore, Settings.WindowsStore);
            this.AddProjectPlugins(visualStudioService.WpfProjectService, plugins, Settings.Wpf, Settings.Wpf);

            if (string.IsNullOrEmpty(viewModelName) == false)
            {
                IProjectItemService testProjectItemService = null;
                IProjectItemService projectItemService = coreProjectService.GetProjectItem(viewModelName);

                if (projectItemService.ProjectItem != null)
                {
                    foreach (Plugin plugin in plugins)
                    {
                        testProjectItemService = this.CreateSnippet(
                            visualStudioService,
                            viewModelName,
                            codeSnippetsPath,
                            createUnitTests,
                            coreProjectService,
                            projectItemService,
                            plugin);
                    }
                    
                    //// move the using statements to inside the namespace.
                    projectItemService.ProjectItem.MoveUsingStatements();

                    //// we do this just once now instead of each plugin.
                    projectItemService.ProjectItem.SortAndRemoveUsingStatements();

                    //// also only do once for the unit test file.
                    if (createUnitTests && testProjectItemService != null)
                    {
                        //// move the using statements to inside the namespace.
                        testProjectItemService.ProjectItem.MoveUsingStatements();

                        testProjectItemService.ProjectItem.SortAndRemoveUsingStatements();
                    }
                }
            }

            return this.messages;
        }

        /// <summary>
        /// Adds the project plugins.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="plugins">The plugins.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="extensionName">Name of the extension.</param>
        public void AddProjectPlugins(
            IProjectService projectService,
            List<Plugin> plugins,
            string folderName,
            string extensionName)
        {
            TraceService.WriteLine("PluginsService::AddProjectPlugins folder=" + folderName);

            if (projectService.Project != null)
            {
                foreach (Plugin plugin in plugins)
                {
                    string message = string.Format("Ninja Coder is adding {0} plugin to {1} project.", plugin.FriendlyName, projectService.Name);
                    projectService.WriteStatusBarMessage(message);

                    this.AddPlugin(projectService, plugin, folderName, extensionName);
                    this.messages.Add(plugin.FriendlyName + " plugin added to project " + projectService.Name + ".");
                }
            }
        }

        /// <summary>
        /// Creates the snippet.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="codeSnippetsPath">The code snippets path.</param>
        /// <param name="createUnitTests">if set to <c>true</c> [create unit tests].</param>
        /// <param name="coreProjectService">The core project service.</param>
        /// <param name="projectItemService">The project item service.</param>
        /// <param name="plugin">The plugin.</param>
        internal IProjectItemService CreateSnippet(
            IVisualStudioService visualStudioService, 
            string viewModelName, 
            string codeSnippetsPath, 
            bool createUnitTests, 
            IProjectService coreProjectService, 
            IProjectItemService projectItemService, 
            Plugin plugin)
        {
            TraceService.WriteLine("PluginsService::CreateSnippet viewModelName=" + viewModelName);

            string snippetPath = string.Format(@"{0}\Plugins.{1}.xml", codeSnippetsPath, plugin.FriendlyName);
            IProjectItemService testProjectItemService = null;

            if (this.fileSystem.File.Exists(snippetPath))
            {
                FileInfoBase fileInfoBase = this.fileSystem.FileInfo.FromFileName(snippetPath);

                //// only do if the snippet contains some text :-)
                if (fileInfoBase.Length > 0)
                {
                    CodeSnippet codeSnippet = this.translator.Translate(snippetPath);

                    this.UpdateSnippet(plugin, codeSnippet);

                    projectItemService.ImplementCodeSnippet(codeSnippet);

                    this.messages.Add(plugin.FriendlyName + " plugin code added to " + viewModelName + ".cs in project " + coreProjectService.Name + ".");

                    //// do we need to implement any unit tests?
                    if (createUnitTests)
                    {
                        IProjectItemService itemService = this.CreateUnitTests(
                            visualStudioService.CoreTestsProjectService,
                            plugin,
                            codeSnippetsPath,
                            viewModelName);

                        //// if we actually create some units tests save the pointer to do
                        //// the sort and remove of the using statements later!
                        if (itemService != null)
                        {
                            testProjectItemService = itemService;
                        }
                    }
                }
            }

            return testProjectItemService;
        }

        /// <summary>
        /// Updates the snippet.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <param name="codeSnippet">The code snippet.</param>
        internal void UpdateSnippet(Plugin plugin, CodeSnippet codeSnippet)
        {
            //// add in the reference to the plugin - doing this way means we dont need it in the xml files

            string usingStatement = this.fileSystem.Path.GetFileNameWithoutExtension(plugin.FileName);
            codeSnippet.UsingStatements.Add(usingStatement);
        }

        /// <summary>
        /// Creates the unit tests.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="plugin">The plugin.</param>
        /// <param name="codeSnippetsPath">The code snippets path.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <returns>The project item service.</returns>
        internal IProjectItemService CreateUnitTests(
            IProjectService projectService,
            Plugin plugin,
            string codeSnippetsPath, 
            string viewModelName)
        {
            TraceService.WriteLine("PluginsService::CreateUnitTests viewModelName=" + viewModelName);
            
            string testSnippetPath = string.Format(@"{0}\Plugins.{1}.Tests.xml", codeSnippetsPath, plugin.FriendlyName);

            if (this.fileSystem.File.Exists(testSnippetPath))
            {
                FileInfoBase testFileInfoBase = this.fileSystem.FileInfo.FromFileName(testSnippetPath);

                if (testFileInfoBase.Length > 0)
                {
                    CodeSnippet codeSnippet = this.translator.Translate(testSnippetPath);

                    string fileName = "Test" + viewModelName;

                    //// are we going to assume that the TestViewModel source file already exists?
                    IProjectItemService projectItemService = projectService.GetProjectItem(fileName);

                    if (projectItemService != null)
                    {
                        //// add relevant plugin assemblies to test project!
                        this.AddPlugin(projectService, plugin, string.Empty, Settings.CoreTests);

                        this.UpdateTestSnippet(plugin, codeSnippet, projectItemService);

                        this.messages.Add(plugin.FriendlyName + " test code added to " + fileName + ".cs in project " + projectService.Name + ".");
                        
                        //// this really shouldn't be done - templates now have a mind of their own - please fix!!
                        DTE2 dte2 = projectService.Project.DTE as DTE2;

                        //// change the testable placeholders!
                        string instanceName = "this." + viewModelName.Substring(0, 1).ToLower() + viewModelName.Substring(1);
                        dte2.ReplaceText("this.TestableObject", instanceName, true);

                        return projectItemService;
                    }
                }
            }
            else
            {
                TraceService.WriteError("PluginsService::CreateUnitTests File Not Found=" + testSnippetPath);
            }

            return null;
        }

        /// <summary>
        /// Updates the test snippet.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <param name="codeSnippet">The code snippet.</param>
        /// <param name="projectItemService">The project item service.</param>
        internal void UpdateTestSnippet(
            Plugin plugin, 
            CodeSnippet codeSnippet, 
            IProjectItemService projectItemService)
        {
            //// add in the Moq using statements - doing this way means we dont need it in the xml files
            codeSnippet.UsingStatements.Add("Moq");

            //// another one to add so we dont need to put in the xml file!
            codeSnippet.UsingStatements.Add("Cirrious.CrossCore");

            //// add in the reference to the plugin - doing this way means we dont need it in the xml files

            string usingStatement = this.fileSystem.Path.GetFileNameWithoutExtension(plugin.FileName);
            codeSnippet.UsingStatements.Add(usingStatement);

            //// add in the init method here- doing this way means we dont need it in the xml files
            codeSnippet.TestInitMethod = "CreateTestableObject";

            projectItemService.ImplementCodeSnippet(codeSnippet);
        }

        /// <summary>
        /// Adds the plugin.
        /// </summary>)
        /// <param name="projectService">The project service.</param>
        /// <param name="plugin">The plugin.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="extensionName">Name of the extension.</param>
        internal void AddPlugin(
            IProjectService projectService,
            Plugin plugin,
            string folderName,
            string extensionName)
        {
            TraceService.WriteLine("PluginsService::AddPlugin " + plugin.FriendlyName);

            string projectPath = projectService.GetProjectPath();
            string source = plugin.Source;
            string destination = string.Format(@"{0}\Lib\{1}", projectPath, plugin.FileName);

            //// at this moment we only want ot do the core as this plugin might not be
            //// supported in the ui project.
            if (extensionName == Settings.Core)
            {
                projectService.AddReference("Lib", destination, source);
            }
            else if (extensionName == Settings.CoreTests)
            {
                projectService.AddReference("Lib", destination, source);
            }
            else
            {
                //// now if we are not the core project we need to add the platform specific assemblies
                //// and the bootstrap item templates!

                string extensionSource = this.GetExtensionSource(folderName, extensionName, source);

                string extensionDestination = this.GetExtensionDestination(folderName, extensionName, destination);

                if (this.fileSystem.File.Exists(extensionSource))
                {
                    //// if the plugin is supported add in the core library.

                    this.AddNonCorePlugin(
                        projectService,
                        plugin,
                        source,
                        destination,
                        extensionSource,
                        extensionDestination);
                }
            }
        }

        /// <summary>
        /// Adds the non core plugin.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="plugin">The plugin.</param>
        /// <param name="source">The source.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="extensionSource">The extension source.</param>
        /// <param name="extensionDestination">The extension destination.</param>
        internal void AddNonCorePlugin(
            IProjectService projectService,
            Plugin plugin,
            string source,
            string destination,
            string extensionSource,
            string extensionDestination)
        {
            TraceService.WriteLine("PluginsService::AddNonCorePlugin " + plugin.FriendlyName);

            //// only do if destination file doesn't exist
            if (this.fileSystem.File.Exists(destination) == false)
            {
                projectService.AddReference("Lib", destination, source);
            }

            //// only do if extensionDestination file doesn't exist
            if (this.fileSystem.File.Exists(extensionDestination) == false)
            {
                projectService.AddReference("Lib", extensionDestination, extensionSource);
                this.BuildSourceFile(projectService, extensionSource, extensionDestination, plugin.FriendlyName);
            }
        }

        /// <summary>
        /// Gets the extension destination.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="extensionName">Name of the extension.</param>
        /// <param name="destination">The destination.</param>
        /// <returns>The extension destination.</returns>
        internal string GetExtensionDestination(
            string folderName,
            string extensionName,
            string destination)
        {
            string extensionDestination = destination.Replace(@"\Core\", string.Format(@"\{0}\", folderName));
            extensionDestination = extensionDestination.Replace(".dll", string.Format(".{0}.dll", extensionName));
            return extensionDestination;
        }

        /// <summary>
        /// Gets the extension source.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="extensionName">Name of the extension.</param>
        /// <param name="source">The source.</param>
        /// <returns>The extension source.</returns>
        internal string GetExtensionSource(
            string folderName,
            string extensionName,
            string source)
        {
            string extensionSource = source.Replace(@"\Core\", string.Format(@"\{0}\", folderName));
            extensionSource = extensionSource.Replace(".dll", string.Format(".{0}.dll", extensionName));
            return extensionSource;
        }

        /// <summary>
        /// Builds the source file.
        /// </summary>
        /// <param name="projectService">The project service.</param>
        /// <param name="extensionSource">The extensionSource.</param>
        /// <param name="extensionDestination">The extension destination.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        internal void BuildSourceFile(
            IProjectService projectService,
            string extensionSource,
            string extensionDestination,
            string friendlyName)
        {
            TraceService.WriteLine("PluginsService::BuildSourceFile " + friendlyName);

            try
            {
                string message = string.Format("BuildSourceFile Project Name={0} friendlyName={1}", projectService.Name, friendlyName);

                TraceService.WriteLine(message);

                string sourceFile = friendlyName + "PluginBootstrap.cs";

                //// now we need to sort out the item template!
                projectService.AddToFolderFromTemplate("Bootstrap", "MvvmCross.Plugin.zip", sourceFile, false);

                this.messages.Add(string.Format(@"Bootstrap\{0} added to {1} project.", sourceFile, projectService.Name));

                IProjectItemService projectItemService = projectService.GetProjectItem(sourceFile);

                //// if we find the project item replace the text in it.
                
                if (projectItemService.ProjectItem != null)
                {
                    //// fix ups!

                    projectItemService.ReplaceText("All", friendlyName);
                    projectItemService.ReplaceText("CoreTemplates.Bootstrap", projectService.Name + ".Bootstrap");
                    projectItemService.ReplaceText("class Plugin", "class " + friendlyName + "PluginBootstrap");
                }
            }
            catch (Exception exception)
            {
                TraceService.WriteError("BuildSourceFile " + exception.Message);
            }
        }
    }
}
