// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the SettingsService type.
// </summary>
// ------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.TemplateWizards.Services
{
    using System;

    using Microsoft.Win32;

    /// <summary>
    ///  Defines the SettingsService type.
    /// </summary>
    public class SettingsService  
    {
        /// <summary>
        /// Gets a value indicating whether [display errors].
        /// </summary>
        public bool DisplayErrors
        {
            get { return this.GetRegistryValue("Tracing", "DisplayErrors", "N") == "Y"; }
        }

        /// <summary>
        /// Gets a value indicating whether [log to trace].
        /// </summary>
        public bool LogToTrace
        {
            get { return this.GetRegistryValue("Tracing", "LogToTrace", "N") == "Y"; }
        }

        /// <summary>
        /// Gets a value indicating whether [log to file].
        /// </summary>
        public bool LogToFile
        {
            get { return this.GetRegistryValue("Tracing", "LogToFile", "N") == "Y"; }
        }

        /// <summary>
        /// Gets the log file path.
        /// </summary>
        public string LogFilePath
        {
            get { return this.GetRegistryValue("Tracing", "LogFilePath", Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\ninja-coder-for-mvvmcross.log"); }
        }

        /// <summary>
        /// Gets a value indicating whether [remove default file headers].
        /// </summary>
        public bool RemoveDefaultFileHeaders
        {
            get { return this.GetRegistryValue("Coding Style", "RemoveDefaultFileHeaders", "N") == "Y"; }
        }

        /// <summary>
        /// Gets a value indicating whether [remove default comments].
        /// </summary>
        public bool RemoveDefaultComments
        {
            get { return this.GetRegistryValue("Coding Style", "RemoveDefaultComments", "N") == "Y"; }
        }
        
        /// <summary>
        /// Gets the active project.
        /// </summary>
        public string ActiveProject
        {
            get { return this.GetRegistryValue("Internals", "ActiveProject", string.Empty); }
        }

        /// <summary>
        /// Gets a value indicating whether [process wizard].
        /// </summary>
        public bool ProcessWizard
        {
            get { return this.GetRegistryValue("Internals", "ProcessWizard", "Y") == "Y"; }
        }

        /// <summary>
        /// Gets the type of the selected view.
        /// </summary>
        public string SelectedViewType
        {
            get { return this.GetRegistryValue("Internals", "SelectedViewType", "SampleData"); }
        }

        /// <summary>
        /// Gets the selected view prefix.
        /// </summary>
        public string SelectedViewPrefix
        {
            get { return this.GetRegistryValue("Internals", "SelectedViewPrefix", string.Empty); }
        }

        /// <summary>
        /// Gets the plugins to add.
        /// </summary>
        public string PluginsToAdd
        {
            get { return this.GetRegistryValue("Internals", "PluginsToAdd", string.Empty); }
        }

        /// <summary>
        /// Gets the xamarin forms views.
        /// </summary>
        public string XamarinFormsViews
        {
            get { return this.GetRegistryValue("Internals", "XamarinFormsViews", string.Empty); }
        }

        /// <summary>
        /// Gets a value indicating whether [bind context in xaml for xamarin forms].
        /// </summary>
        public bool BindContextInXamlForXamarinForms
        {
            get { return this.GetRegistryValue("Coding Style", "BindContextInXamlForXamarinForms", "Y") == "Y"; }
        }

        /// <summary>
        /// Gets or sets the API version.
        /// </summary>
        public string iOSApiVersion
        {
            get { return this.GetRegistryValue("Projects", "iOSApiVersion", "Classic"); }
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
    }
}
