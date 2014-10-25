// --------------------------------- -----------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using Entities;
    using Scorchio.Infrastructure.Translators;
    using System.Collections.Generic;
    using System.IO;
    using System.IO.Abstractions;
    using System.Linq;

    /// <summary>
    /// Defines the PluginsTranslator type.
    /// </summary>
    internal class PluginsTranslator : ITranslator<IEnumerable<DirectoryInfoBase>, Plugins>
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
        /// <param name="from">From.</param>
        /// <returns></returns>
        public Plugins Translate(IEnumerable<DirectoryInfoBase> @from)
        {
            List<FileInfoBase> combinedFiles = new List<FileInfoBase>();

            foreach (DirectoryInfoBase directoryInfoBase in from)
            {
                IEnumerable<FileInfoBase> files = this.GetFiles(directoryInfoBase);
                
                combinedFiles.AddRange(files);
            }

            //// ensure we order the plugins by friendly name.
            IEnumerable<Plugin> items = combinedFiles.Select(
                                    fileInfo => this.translator.Translate(fileInfo))
                                    .OrderBy(p => p.FriendlyName);

            Plugins plugins = new Plugins { Items = items };

            return plugins;
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="directoryInfoBase">The directory info base.</param>
        /// <returns>The files.</returns>
        internal IEnumerable<FileInfoBase> GetFiles(DirectoryInfoBase directoryInfoBase)
        {
            return directoryInfoBase.GetFiles("*.*", SearchOption.TopDirectoryOnly);
        }
    }
}

