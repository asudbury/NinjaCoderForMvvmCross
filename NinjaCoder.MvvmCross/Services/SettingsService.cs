// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SettingsService type.
// </summary>
// ------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System;
    using Interfaces;
    using Microsoft.Win32;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the SettingsService type.
    /// </summary>
    public class SettingsService : BaseService, ISettingsService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsService" /> class.
        /// </summary>
        public SettingsService()
        {
            TraceService.WriteLine("SettingsService::Constructor");
        }

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
        /// Gets the core plugins path.
        /// </summary>
        public string CorePluginsPath 
        { 
            get { return this.GetRegistryValue("Plugins", "Core Path", string.Empty); }
        }

        /// <summary>
        /// Gets the code snippets path.
        /// </summary>
        public string CodeSnippetsPath
        {
            get { return this.GetRegistryValue(string.Empty, "Code Snippets Path", string.Empty); }
        }

        /// <summary>
        /// Gets the config path.
        /// </summary>
        public string ConfigPath
        {
            get { return this.GetRegistryValue(string.Empty, "Config Path", string.Empty); } 
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
