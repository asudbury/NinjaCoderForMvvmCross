// --------------------------------- -----------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using System.Collections.Generic;
    using System.IO;
    using Entities;

    using NinjaCoder.MvvmCross.Constants;

    /// <summary>
    /// Defines the PluginsTranslator type.
    /// </summary>
    public class PluginsTranslator : ITranslator<string, Plugins>
    {
        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">From object.</param>
        /// <returns>An instance of Plugins</returns>
        public Plugins Translate(string @from)
        {
            if (Directory.Exists(from))
            {
                Plugins plugins = new Plugins { Items = new List<Plugin>() };

                string[] files = Directory.GetFiles(from);
                
                foreach (string filePath in files)
                {
                    plugins.Items.Add(this.GetPlugin(filePath));
                }

                return plugins;
            }

            return null;
        }

        /// <summary>
        /// Gets the plugin.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        /// <returns>The plugin.</returns>
        public Plugin GetPlugin(string filePath)
        {
            FileInfo fileInfo = new FileInfo(filePath);

            string name = Path.GetFileNameWithoutExtension(fileInfo.Name);

            if (string.IsNullOrEmpty(name) == false)
            {
                return new Plugin
                {
                    FriendlyName = name.Replace(Settings.PluginsAssemblyPrefix, string.Empty),
                    FileName = fileInfo.Name,
                    Source = fileInfo.FullName
                };
            }

            return null;
        }
    }
}

