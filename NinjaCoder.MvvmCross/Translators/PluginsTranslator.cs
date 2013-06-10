// --------------------------------- -----------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using System.Collections.Generic;
    using System.IO;

    using NinjaCoder.MvvmCross.Entities;

    /// <summary>
    /// Defines the PluginsTranslator type.
    /// </summary>
    public class PluginsTranslator : ITranslator<string, Plugins>
    {
        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">From.</param>
        /// <returns>An instance of Plugins</returns>
        public Plugins Translate(string @from)
        {
            if (Directory.Exists(from))
            {
                Plugins plugins = new Plugins { Items = new List<Plugin>() };

                string[] files = Directory.GetFiles(from);
                
                foreach (string filePath in files)
                {
                    FileInfo fileInfo = new FileInfo(filePath);

                    string name = Path.GetFileNameWithoutExtension(fileInfo.Name);
                    
                    plugins.Items.Add(new Plugin
                    {
                        FriendlyName = name.Replace("Cirrious.MvvmCross.Plugins.", string.Empty),
                        FileName = fileInfo.Name,
                        Source = fileInfo.FullName
                    });
                }

                return plugins;
            }

            return null;
        }
    }
}

