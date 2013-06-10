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
    using Entities;
    using EnvDTE;
    using EnvDTE80;
    using Interfaces;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    /// Defines the PluginsService type.
    /// </summary>
    public class PluginsService : IPluginsService
    {
        /// <summary>
        /// The messages
        /// </summary>
        private List<string> messages;
 
        /// <summary>
        /// Adds the plugins.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="plugins">The plugins.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="codeSnippetsPath">The code snippets path.</param>
        /// <returns>The messages.</returns>
        public IEnumerable<string> AddPlugins(
            IVisualStudioService visualStudioService, 
            List<Plugin> plugins,
            string viewModelName,
            string codeSnippetsPath)
        {
            this.messages = new List<string>();

            Project coreProject = visualStudioService.CoreProject;

            this.AddProjectPlugins(coreProject, plugins, "Core", "Core");
            this.AddProjectPlugins(visualStudioService.DroidProject, plugins, "Droid", "Droid");
            this.AddProjectPlugins(visualStudioService.iOSProject, plugins, "iOS", "Touch");
            this.AddProjectPlugins(visualStudioService.WindowsPhoneProject, plugins, "WindowsPhone", "WindowsPhone");
            this.AddProjectPlugins(visualStudioService.WindowsStoreProject, plugins, "WindowsStore", "WindowsStore");
            this.AddProjectPlugins(visualStudioService.WpfProject, plugins, "Wpf", "Wpf");

            if (string.IsNullOrEmpty(viewModelName) == false)
            {
                ProjectItem projectItem = coreProject.GetProjectItem(viewModelName);

                if (projectItem != null)
                {
                    foreach (Plugin plugin in plugins)
                    {
                        this.GenerateSnippet(codeSnippetsPath, plugin, projectItem, coreProject);
                    }
                }
            }
            
            DTE2 dte2 = coreProject.DTE as DTE2;
            dte2.SaveAll();

            return this.messages;
        }

        /// <summary>
        /// Adds the project plugins.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="plugins">The plugins.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="extensionName">Name of the extension.</param>
        public void AddProjectPlugins(
            Project project,
            List<Plugin> plugins,
            string folderName,
            string extensionName)
        {
            if (project != null)
            {
                foreach (Plugin plugin in plugins)
                {
                    DTE2 dte2 = project.DTE as DTE2;

                    string message = string.Format("Ninja Coder is adding {0} plugin to {1} project.", plugin.FriendlyName, project.Name);
                    dte2.WriteStatusBarMessage(message);

                    this.AddPlugin(plugin, project, folderName, extensionName);
                }
            }
        }


        /// <summary>
        /// Adds the plugin.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <param name="project">The project.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="extensionName">Name of the extension.</param>
        internal void AddPlugin(
            Plugin plugin,
            Project project,
            string folderName,
            string extensionName)
        {
            string projectPath = project.GetProjectPath();
            string source = plugin.Source;
            string destination = string.Format(@"{0}\Lib\{1}", projectPath, plugin.FileName);

            //// at this moment we only want ot do the core as this plugin might not be
            //// supported in the ui project.
            if (extensionName == "Core")
            {
                project.AddReference("Lib", destination, source);
            }
            else
            {
                //// now if we are not the core project we need to add the platform specific assemblies
                //// and the bootstrap item templates!

                string extensionSource = this.GetExtensionSource(folderName, extensionName, source);

                string extensionDestination = this.GetExtensionDestination(folderName, extensionName, destination);

                if (File.Exists(extensionSource))
                {
                    //// if the plugin is supported add in the core library.

                    //// only do if destination file doesn't exist
                    if (File.Exists(destination) == false)
                    {
                        project.AddReference("Lib", destination, source);
                    }

                    //// only do if extensionDestination file doesn't exist
                    if (File.Exists(extensionDestination) == false)
                    {
                        project.AddReference("Lib", extensionDestination, extensionSource);
                        this.BuildSourceFile(project, extensionSource, extensionDestination, plugin.FriendlyName);
                    }
                }
            }
        }

        /// <summary>
        /// Generates the snippet.
        /// </summary>
        /// <param name="codeSnippetsPath">The code snippets path.</param>
        /// <param name="plugin">The plugin.</param>
        /// <param name="projectItem">The project item.</param>
        /// <param name="coreProject">The core project.</param>
        internal void GenerateSnippet(
            string codeSnippetsPath, 
            Plugin plugin, 
            ProjectItem projectItem, 
            Project coreProject)
        {
            string snippetPath = string.Format(@"{0}\Plugins.{1}.txt", codeSnippetsPath, plugin.FriendlyName);

            if (File.Exists(snippetPath))
            {
                FileInfo fileInfo = new FileInfo(snippetPath);

                //// only do if the snippet contains some text :-)
                if (fileInfo.Length > 0)
                {
                    this.ProduceSnippet(plugin, projectItem, coreProject, snippetPath);
                }
            }
        }

        /// <summary>
        /// Products the snippet.
        /// </summary>
        /// <param name="plugin">The plugin.</param>
        /// <param name="projectItem">The project item.</param>
        /// <param name="coreProject">The core project.</param>
        /// <param name="snippetPath">The snippet path.</param>
        internal void ProduceSnippet(
            Plugin plugin, 
            ProjectItem projectItem, 
            Project coreProject, 
            string snippetPath)
        {
            string assemblyName = Path.GetFileNameWithoutExtension(plugin.FileName);

            projectItem.AddUsingStatement("Cirrious.CrossCore");

            projectItem.AddUsingStatement(assemblyName);

            projectItem.InsertMethod(snippetPath, true, true);

            this.messages.Add(plugin.FriendlyName + " plugin code added to " + projectItem.Name + " in project " + coreProject.Name + ".");
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
        /// <param name="project">The project.</param>
        /// <param name="extensionSource">The extensionSource.</param>
        /// <param name="extensionDestination">The extension destination.</param>
        /// <param name="friendlyName">Name of the friendly.</param>
        internal void BuildSourceFile(
            Project project, 
            string extensionSource, 
            string extensionDestination, 
            string friendlyName)
        {
            try
            {
                const string PlaceHolderText = "All";

                string message = string.Format("BuildSourceFile Project Name={0} friendlyName={1}", project.Name, friendlyName);

                TraceService.WriteLine(message);

                string sourceFile = friendlyName + "PluginBootstrap.cs";

                //// now we need to sort out the item template!
                project.AddToFolderFromTemplate("Bootstrap", "MvvmCross.Plugin.zip", sourceFile, false);
                
                this.messages.Add(string.Format(@"Bootstrap\{0} added to {1} project.", sourceFile, project.Name));

                ProjectItem projectItem = project.GetProjectItem(sourceFile);

                //// if we find the project item replace the text in it.
                if (projectItem != null)
                {
                    projectItem.ReplaceText(PlaceHolderText, friendlyName);
                }
            }
            catch (Exception exception)
            {
                TraceService.WriteError("BuildSourceFile " + exception.Message);
            }
        }
    }
}
