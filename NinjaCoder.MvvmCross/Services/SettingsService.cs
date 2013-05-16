// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SettingsService type.
// </summary>
// ------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Services
{
    using System;

    using Microsoft.Win32;

    /// <summary>
    ///  Defines the SettingsService type.
    /// </summary>
    public class SettingsService : ISettingsService
    {
        /// <summary>
        /// Registry Key.
        /// </summary>
        private const string RegistryKey = @"SOFTWARE\Scorchio Limited\Ninja Coder for MvvmCross";

        /// <summary>
        /// Gets or sets a value indicating whether [log to file].
        /// </summary>
        public bool LogToFile
        {
            get { return this.GetRegistryValue("LogToFile", "N") == "N"; }
            set { this.SetRegistryValue("LogFile", value ? "Y" : "N"); }
        }
        
        /// <summary>
        /// Gets or sets the log file path.
        /// </summary>
        public string LogFilePath 
        {
            get { return Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\ninja-coder-for-mvvmcross.log"; } 
        }

        /// <summary>
        /// Gets the converters templates path.
        /// </summary>
        public string ConvertersTemplatesPath
        {
            get { return  this.GetItemTemplatesPath() + @"\Converters"; }
        }

        /// <summary>
        /// Gets the plugins templates path.
        /// </summary>
        public string PluginsTemplatesPath
        {
            get { return this.GetItemTemplatesPath() + @"\Plugins"; }
        }


        /// <summary>
        /// Gets the value key.
        /// </summary>
        /// <param name="keyName">Name of the key.</param>
        /// <returns></returns>
        internal string GetValueKey(string keyName)
        {
            return string.Format("{0}\\{1}", RegistryKey, keyName);
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
            return (string)Registry.CurrentUser.GetValue(this.GetValueKey(name), defaultValue);
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
            Registry.CurrentUser.SetValue(this.GetValueKey(name), value);
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
