// --------------------------------- -----------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.Linq;

    using Entities;

    using NinjaCoder.MvvmCross.Constants;

    /// <summary>
    /// Defines the PluginsTranslator type.
    /// </summary>
    public class PluginsTranslator : ITranslator<DirectoryInfoBase, Plugins>
    {
        /// <summary>
        /// The translator.
        /// </summary>
        private readonly ITranslator<FileInfoBase, Plugin> translator;

        /// <summary>
        /// Initializes a new instance of the <see cref="PluginsTranslator" /> class.
        /// </summary>
        /// <param name="translator">The translator.</param>
        public PluginsTranslator(ITranslator<FileInfoBase, Plugin> translator)
        {
            this.translator = translator;
        }

        /// <summary>
        /// Translates the specified from.
        /// </summary>
        /// <param name="from">From object.</param>
        /// <returns>An instance of Plugins</returns>
        public Plugins Translate(DirectoryInfoBase @from)
        {
            //// we don't want the UI plugins (and also the messenger plugin - that will already be in the projects)
            FileInfoBase[] files = from.GetFiles("*Plugin*")
                .Where(x => !x.Name.Contains(Settings.Droid) && 
                            !x.Name.Contains(Settings.Touch) &&
                            !x.Name.Contains(Settings.WindowsPhone) &&
                            !x.Name.Contains(Settings.WindowsStore) &&
                            !x.Name.Contains(Settings.Wpf) &&
                            !x.Name.Contains("Plugins.Messenger"))
                .ToArray();

            //// ensure we order the plugins by friendly name.
            List<Plugin> items = files.Select(fileInfo => this.translator.Translate(fileInfo))
                                    .OrderBy(p => p.FriendlyName).ToList();

            Plugins plugins = new Plugins { Items = items };

            return plugins;
        }
    }
}

