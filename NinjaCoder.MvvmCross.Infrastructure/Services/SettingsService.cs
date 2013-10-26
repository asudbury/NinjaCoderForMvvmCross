// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SettingsService type.
// </summary>
// ------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Infrastructure.Services
{
    using System;
    using System.IO;

    using Microsoft.Win32;

    /// <summary>
    ///  Defines the SettingsService type.
    /// </summary>
    public class SettingsService : ISettingsService
    {
        /// <summary> 
        /// Gets a value indicating whether [display logo].
        /// </summary>
        public bool DisplayLogo
        {
            get { return this.GetRegistryValue(string.Empty, "DisplayLogo", "Y") == "Y"; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [log to trace].
        /// </summary>
        public bool LogToTrace
        {
            get { return this.GetRegistryValue(string.Empty, "LogToTrace", "N") == "Y"; }
            set { this.SetRegistryValue(string.Empty, "LogToTrace", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [log to file].
        /// </summary>
        public bool LogToFile
        {
            get { return this.GetRegistryValue(string.Empty, "LogToFile", "N") == "Y"; }
            set { this.SetRegistryValue(string.Empty, "LogToFile", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets the log file path.
        /// </summary>
        public string LogFilePath 
        {
            get { return this.GetRegistryValue(string.Empty, "LogFilePath", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\ninja-coder-for-mvvmcross.log"); }
            set { this.SetRegistryValue(string.Empty, "LogFilePath", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [include lib folder in projects].
        /// </summary>
        public bool IncludeLibFolderInProjects
        {
            get { return this.GetRegistryValue("Coding Style", "IncludeLibFolderInProjects", "N") == "Y"; }
            set { this.SetRegistryValue("Coding Style", "IncludeLibFolderInProjects", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget for project templates].
        /// </summary>
        public bool UseNugetForProjectTemplates
        {
            get { return this.GetRegistryValue("Nuget", "UseNugetForProjectTemplates", "N") == "Y"; }
            set { this.SetRegistryValue("Nuget", "UseNugetForProjectTemplates", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget for plugins].
        /// </summary>
        public bool UseNugetForPlugins
        {
            get { return this.GetRegistryValue("Nuget", "UseNugetForPlugins", "N") == "Y"; }
            set { this.SetRegistryValue("Nuget", "UseNugetForPlugins", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [use nuget for services].
        /// </summary>
        public bool UseNugetForServices
        {
            get { return this.GetRegistryValue("Nuget", "UseNugetForServices", "N") == "Y"; }
            set { this.SetRegistryValue("Nuget", "UseNugetForServices", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [suspend re sharper during build].
        /// </summary>
        public bool SuspendReSharperDuringBuild
        {
            get { return this.GetRegistryValue("Nuget", "SuspendReSharperDuringBuild", "N") == "Y"; }
            set { this.SetRegistryValue("Nuget", "SuspendReSharperDuringBuild", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [display errors].
        /// </summary>
        public bool DisplayErrors
        {
            get { return this.GetRegistryValue(string.Empty, "DisplayErrors", "N") == "Y"; }
            set { this.SetRegistryValue(string.Empty, "DisplayErrors", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [remove default file headers].
        /// </summary>
        public bool RemoveDefaultFileHeaders
        {
            get { return this.GetRegistryValue("Coding Style", "RemoveDefaultFileHeaders", "N") == "Y"; }
            set { this.SetRegistryValue("Coding Style", "RemoveDefaultFileHeaders", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [remove default comments].
        /// </summary>
        public bool RemoveDefaultComments
        {
            get { return this.GetRegistryValue("Coding Style", "RemoveDefaultComments", "N") == "Y"; }
            set { this.SetRegistryValue("Coding Style", "RemoveDefaultComments", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets the converters templates path.
        /// </summary>
        public string ConvertersTemplatesPath
        {
            get { return this.GetItemTemplatesPath() + @"\Converters"; }
        }
        
        /// <summary>
        /// Gets the services templates path.
        /// </summary>
        public string ServicesTemplatesPath
        {
            get { return this.GetItemTemplatesPath() + @"\Services"; }
        }

        /// <summary>
        /// Gets the code snippets path.
        /// </summary>
        public string CodeSnippetsPath
        {
            get { return this.InstalledDirectory + @"CodeSnippets\"; }
        }

        /// <summary>
        /// Gets the MVVM cross assemblies path.
        /// </summary>
        public string MvvmCrossAssembliesPath
        {
            get { return this.InstalledDirectory + @"MvvmCross\Assemblies\"; }
        }

        /// <summary>
        /// Gets the config path.
        /// </summary>
        public string ConfigPath
        {
            get { return this.InstalledDirectory + @"Config\"; }
        }

        /// <summary>
        /// Gets the application version.
        /// </summary>
        public string ApplicationVersion 
        { 
            get { return this.GetRegistryValue(string.Empty, "Version", "Unknown"); }
        }

        /// <summary>
        /// Gets the MvvmCross version.
        /// </summary>
        public string MvvmCrossVersion 
        {
            get { return this.GetRegistryValue(string.Empty, "MvvmCross Version", "Unknown"); } 
        }

        /// <summary>
        /// Gets the download nuget page.
        /// </summary>
        public string DownloadNugetPage
        {
            get { return "http://visualstudiogallery.msdn.microsoft.com/27077b70-9dad-4c64-adcf-c7cf6bc9970c"; }
        }

        /// <summary>
        /// Gets the unit testing assemblies.
        /// </summary>
        public string UnitTestingAssemblies
        {
            get { return this.GetRegistryValue(string.Empty, "UnitTestingAssemblies", "Moq,Cirrious.CrossCore"); } 
        }

        /// <summary>
        /// Gets the unit testing init method.
        /// </summary>
        public string UnitTestingInitMethod
        {
            get { return this.GetRegistryValue(string.Empty, "UnitTestingInitMethod", "CreateTestableObject"); } 
        }

        /// <summary>
        /// Gets the name of the base view model.
        /// </summary>
        public string BaseViewModelName
        {
            get { return this.GetRegistryValue(string.Empty, "BaseViewModelName", "BaseViewModel"); } 
        }

        /// <summary>
        /// Gets the view model navigation snippet file.
        /// </summary>
        public string ViewModelNavigationSnippetFile
        {
            get { return this.GetRegistryValue(string.Empty, "ViewModelNavigationSnippetFile", this.CodeSnippetsPath + @"\ViewModelNavigation.xml"); } 
        }

        /// <summary>
        /// Gets the snippets override directory.
        /// </summary>
        public string SnippetsOverrideDirectory
        {
            get { return this.GetRegistryValue(string.Empty, "SnippetsOverrideDirectory", string.Empty); } 
        }

        /// <summary>
        /// Gets the config override directory.
        /// </summary>
        public string ConfigsOverrideDirectory
        {
            get { return this.GetRegistryValue(string.Empty, "ConfigsOverrideDirectory", string.Empty); } 
        }

        /// <summary>
        /// Gets or sets a value indicating whether [format function parameters].
        /// </summary>
        public bool FormatFunctionParameters
        {
            get { return this.GetRegistryValue("Coding Style", "FormatFunctionParameters", "N") == "Y"; }
            set { this.SetRegistryValue("Coding Style", "FormatFunctionParameters", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets a value indicating whether [replace variables in snippets].
        /// </summary>
        public bool ReplaceVariablesInSnippets
        {
            get { return this.GetRegistryValue(string.Empty, "ReplaceVariablesInSnippets", "Y") == "Y"; }
        }

        /// <summary>
        /// Gets or sets the MVVM cross plugins wiki page.
        /// </summary>
        public string MvvmCrossPluginsWikiPage
        {
            get { return this.GetRegistryValue(string.Empty, "MvvmCrossPluginsWikiPage", "http://github.com/MvvmCross/MvvmCross/wiki/MvvmCross-plugins#existing-mvvmcross-plugins"); }
            set { this.SetRegistryValue(string.Empty, "MvvmCrossPluginsWikiPage", value); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [verbose nuget output].
        /// </summary>
        public bool VerboseNugetOutput
        {
            get { return this.GetRegistryValue(string.Empty, "VerboseNugetOutput", "N") == "Y"; }
            set { this.SetRegistryValue(string.Empty, "VerboseNugetOutput", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [debug nuget].
        /// </summary>
        public bool DebugNuget
        {
            get { return this.GetRegistryValue(string.Empty, "DebugNuget", "N") == "Y"; }
            set { this.SetRegistryValue(string.Empty, "DebugNuget", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [remove assemblies if nuget used].
        /// </summary>
        public bool RemoveAssembliesIfNugetUsed
        {
            get { return this.GetRegistryValue(string.Empty, "RemoveAssembliesIfNugetUsed", "Y") == "Y"; }
            set { this.SetRegistryValue(string.Empty, "RemoveAssembliesIfNugetUsed", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets the installed directory.
        /// </summary>
        public string InstalledDirectory
        {
            get { return this.GetRegistryValue(string.Empty, "InstalledDirectory", string.Empty); }
        }

        /// <summary>
        /// Gets the build date time.
        /// </summary>
        public string BuildDateTime
        {
            get
            {
                string path = this.InstalledDirectory + @"NinjaCoder.MvvmCross.dll";

                if (File.Exists(path))
                {
                    FileInfo fileInfo = new FileInfo(path);

                    return fileInfo.CreationTime.ToString("dd-MMM-yyyy HH:mm");
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [copy assemblies to lib folder].
        /// </summary>
        public bool CopyAssembliesToLibFolder
        {
            get { return this.GetRegistryValue("Coding Style", "CopyAssembliesToLibFolder", "N") == "Y"; }
            set { this.SetRegistryValue("Coding Style", "CopyAssembliesToLibFolder", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets a value indicating whether [process template wizards].
        /// </summary>
        public bool ProcessTemplateWizards
        {
            get { return this.GetRegistryValue(string.Empty, "ProcessTemplateWizards", "Y") == "Y"; }
        }

        /// <summary>
        /// Gets a value indicating whether [process nuget commands].
        /// </summary>
        public bool ProcessNugetCommands
        {
            get { return this.GetRegistryValue(string.Empty, "ProcessNugetCommands", "Y") == "Y"; }
        }

        /// <summary>
        /// Gets or sets the active project.
        /// </summary>
        public string ActiveProject
        {
            get { return this.GetRegistryValue("Internals", "ActiveProject", string.Empty); }
            set { this.SetRegistryValue("Internals", "ActiveProject", value); }
        }
        
        /// <summary>
        /// Gets or sets the plugins to add.
        /// </summary>
        public string PluginsToAdd
        {
            get { return this.GetRegistryValue("Internals", "PluginsToAdd", string.Empty); }
            set { this.SetRegistryValue("Internals", "PluginsToAdd", value); }
        }

        /// <summary>
        /// Gets the registry key.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="writeable">if set to <c>true</c> [writeable].</param>
        /// <returns>The registry key.</returns>
        internal RegistryKey GetRegistryKey(
            string subKey,
            bool writeable)
        {
            RegistryKey softwareKey = Registry.CurrentUser.OpenSubKey("Software");

            if (softwareKey != null)
            {
                RegistryKey scorchioKey = softwareKey.OpenSubKey("Scorchio Limited");

                if (scorchioKey != null)
                {
                    RegistryKey ninjaKey = scorchioKey.OpenSubKey("Ninja Coder for MvvmCross", writeable);

                    if (ninjaKey != null)
                    {
                        if (string.IsNullOrEmpty(subKey) == false)
                        {
                            RegistryKey registryKey = ninjaKey.OpenSubKey(subKey, writeable);

                            return registryKey ?? ninjaKey.CreateSubKey(subKey);
                        }
                    }

                    return ninjaKey;
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the registry value.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns> The value.</returns>
        internal string GetRegistryValue(
            string subKey,
            string name, 
            string defaultValue)
        {
            RegistryKey registryKey = this.GetRegistryKey(subKey, true);

            if (registryKey != null)
            {
                object obj = registryKey.GetValue(name);

                if (obj == null)
                {
                    return defaultValue;
                }

                return (string)obj;
            }

            return defaultValue;
        }

        /// <summary>
        /// Sets the value.
        /// </summary>
        /// <param name="subKey">The sub key.</param>
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        internal void SetRegistryValue(
            string subKey,
            string name,
            string value)
        {
             RegistryKey registryKey = this.GetRegistryKey(subKey, true);

             if (registryKey != null)
             {
                 registryKey.SetValue(name, value);
             }
        }

        /// <summary>
        /// Gets the item templates path.
        /// </summary>
        /// <returns>The Item templates path.</returns>
        internal string GetItemTemplatesPath()
        {
            return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) + @"\Microsoft Visual Studio 11.0\Common7\IDE\ItemTemplates\CSharp\MvvmCross";
        }
    }
}
