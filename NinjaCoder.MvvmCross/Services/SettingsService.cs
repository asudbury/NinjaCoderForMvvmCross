// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SettingsService type.
// </summary>
// ------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System;

    using Microsoft.Win32;

    using Interfaces;

    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the SettingsService type.
    /// </summary>
    public class SettingsService : ISettingsService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsService" /> class.
        /// </summary>
        public SettingsService()
        {
            TraceService.WriteLine("SettingsService::Constructor");
        }

        /// <summary>
        /// Gets or sets a value indicating whether [log to file].
        /// </summary>
        public bool LogToFile
        {
            get { return this.GetRegistryValue("LogToFile", "N") == "Y"; }
            set { this.SetRegistryValue("LogToFile", value ? "Y" : "N"); }
        }

        /// <summary>
        /// Gets a value indicating whether [display logo].
        /// </summary>
        public bool DisplayLogo
        {
            get { return this.GetRegistryValue("DisplayLogo", "Y") == "Y"; }
        }
        
        /// <summary>
        /// Gets or sets the log file path.
        /// </summary>
        public string LogFilePath 
        {
            get { return this.GetRegistryValue("LogFilePath", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\ninja-coder-for-mvvmcross.log"); }
            set { this.SetRegistryValue("LogFilePath", value); }
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
            get { return this.GetRegistryValue("Core Plugins Path", string.Empty); }
        }

        /// <summary>
        /// Gets the code snippets path.
        /// </summary>
        public string CodeSnippetsPath
        {
            get { return this.GetRegistryValue("Code Snippets Path", string.Empty); }
        }

        /// <summary>
        /// Gets the application version.
        /// </summary>
        public string ApplicationVersion 
        { 
            get { return this.GetRegistryValue("Version", "Unknown"); }
        }

        /// <summary>
        /// Gets the MvvmCross version.
        /// </summary>
        public string MvvmCrossVersion 
        {
            get { return this.GetRegistryValue("MvvmCross Version", "Unknown"); } 
        }

        /// <summary>
        /// Gets the registry key.
        /// </summary>
        /// <param name="writeable">if set to <c>true</c> [writeable].</param>
        /// <returns>
        /// The registry key.
        /// </returns>
        internal RegistryKey GetRegistryKey(bool writeable)
        {
            RegistryKey softwareKey = Registry.CurrentUser.OpenSubKey("Software");

            if (softwareKey != null)
            {
                RegistryKey scorchioKey = softwareKey.OpenSubKey("Scorchio Limited");

                if (scorchioKey != null)
                {
                    return scorchioKey.OpenSubKey("Ninja Coder for MvvmCross", writeable);
                }
            }

            return null;
        }

        /// <summary>
        /// Gets the registry value.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="defaultValue">The default value.</param>
        /// <returns>The value.</returns>
        internal string GetRegistryValue(
            string name, 
            string defaultValue)
        {
            RegistryKey registryKey = this.GetRegistryKey(false);

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
        /// <param name="name">The name.</param>
        /// <param name="value">The value.</param>
        internal void SetRegistryValue(
            string name,
            string value)
        {
             RegistryKey registryKey = this.GetRegistryKey(true);

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
