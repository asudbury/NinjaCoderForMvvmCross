// --------------------------------- -----------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using System.Collections.Generic;
    using System.IO.Abstractions;

    using Entities;

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
            Plugins plugins = new Plugins { Items = new List<Plugin>() };

            FileInfoBase[] files = from.GetFiles("*.*");
                
            foreach (FileInfoBase fileInfo in files)
            {
                plugins.Items.Add(this.translator.Translate(fileInfo));
            }

            return plugins;
        }
    }
}

