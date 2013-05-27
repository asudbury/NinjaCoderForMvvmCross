// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System;

    using EnvDTE80;

    using Interfaces;
    using EnvDTE;
    using System.Collections.Generic;
    using System.IO;
    using NinjaCoder.MvvmCross.Entities;
    using Scorchio.VisualStudio.Extensions;
    using Scorchio.VisualStudio.Services;

    using VSLangProj;

    /// <summary>
    /// Defines the PluginsService type.
    /// </summary>
    public class PluginsService : IPluginsService
    {
        /// <summary>
        /// Adds the plugins.
        /// </summary>
        /// <param name="visualStudioService">The visual studio service.</param>
        /// <param name="plugins">The plugins.</param>
        /// <param name="viewModelName">Name of the view model.</param>
        /// <param name="codeSnippetsPath">The code snippets path.</param>
        public void AddPlugins(
            IVisualStudioService visualStudioService, 
            List<Plugin> plugins,
            string viewModelName,
            string codeSnippetsPath)
        {
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
                        string snippetPath = string.Format(@"{0}\Plugins.{1}.txt", codeSnippetsPath, plugin.FriendlyName);

                        if (File.Exists(snippetPath))
                        {
                            FileInfo fileInfo = new FileInfo(snippetPath);

                            //// only do if the snippet contains some text :-)
                            if (fileInfo.Length > 0)
                            {
                                string assemblyName = Path.GetFileNameWithoutExtension(plugin.FileName);

                                projectItem.AddUsingStatement("Cirrious.CrossCore");

                                projectItem.AddUsingStatement(assemblyName);

                                projectItem.InsertMethod(snippetPath);

                                //// tidy up the using statements.
                                projectItem.Save();
                                projectItem.MoveUsingStatements();
                                projectItem.Save();
                                projectItem.SortAndRemoveUsingStatements();
                                projectItem.Save();

                                if (projectItem.Document != null)
                                {
                                    projectItem.Document.ActiveWindow.Close();
                                }
                            }
                        }
                    }
                }
            }
            
            DTE2 dte2 = coreProject.DTE as DTE2;
            dte2.SaveAll();
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
                this.AddReference(project, destination, source);
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

                    //// only do if destination file doesnt exist
                    if (File.Exists(destination) == false)
                    {
                        this.AddReference(project, destination, source);
                    }

                    //// only do if extensionDestination file doesnt exist
                    if (File.Exists(extensionDestination) == false)
                    {
                        this.AddReference(project, extensionDestination, extensionSource);
                        this.BuildSourceFile(project, extensionSource, extensionDestination, plugin.FriendlyName);
                    }
                }
            }
        }

        /// <summary>
        /// Gets the extension destination.
        /// </summary>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="extensionName">Name of the extension.</param>
        /// <param name="destination">The destination.</param>
        /// <returns></returns>
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
        /// <returns></returns>
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
        /// Adds the reference.
        /// </summary>
        /// <param name="project">The project.</param>
        /// <param name="destination">The destination.</param>
        /// <param name="source">The source.</param>
        internal void AddReference(
            Project project, 
            string destination, 
            string source)
        {
            //// only do if destination file doesnt exist
            if (File.Exists(destination) == false)
            {
                File.Copy(source, destination, true);
                project.AddToFolderFromFile("Lib", destination);

                //// now add a reference to the file
                VSProject vsProject = project.Object as VSProject;

                if (vsProject != null)
                {
                    vsProject.References.Add(destination);
                }
            }
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
                const string PlaceHolderText = "#PlaceHolder#";

                string message = string.Format("BuildSourceFile Project Name={0} friendlyName={1}", project.Name, friendlyName);

                TraceService.WriteLine(message);

                string sourceFile = friendlyName + "PluginBootstrap.cs";

                //// now we need to sort out the item template!
                project.AddToFolderFromTemplate("Bootstrap", "MvvmCross.Plugin.zip", sourceFile);

                ProjectItem projectItem = project.GetProjectItem(sourceFile);

                //// if we find the project item replace the text in it else use the find/replace window.
                if (projectItem != null)
                {
                    TextSelection textSelection = projectItem.DTE.ActiveDocument.Selection;
                    textSelection.SelectAll();
                    textSelection.ReplacePattern(PlaceHolderText, friendlyName);
                    projectItem.Save();
                }
            }
            catch (Exception exception)
            {
                TraceService.WriteError("BuildSourceFile " + exception.Message);
            }
        }
    }
}
