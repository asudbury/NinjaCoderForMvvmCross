// --------------------------------- -----------------------------------------------------------------------------------
// <summary>
//    Defines the PluginsTranslator type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Translators
{
    using System;
    using System.Collections.Generic;
    using System.IO.Abstractions;
    using System.Linq;

    using Entities;

    using NinjaCoder.MvvmCross.Constants;

    using Scorchio.Infrastructure.Translators;

    /// <summary>
    /// Defines the PluginsTranslator type.
    /// </summary>
    internal class PluginsTranslator : ITranslator<Tuple<DirectoryInfoBase, DirectoryInfoBase>, Plugins>
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
        /// Translates the object.
        /// </summary>
        /// <param name="from">The object to translate from.</param>
        /// <returns>The translated object.</returns>
        public Plugins Translate(Tuple<DirectoryInfoBase, DirectoryInfoBase> @from)
        {
            IEnumerable<FileInfoBase> combinedFiles;

            IEnumerable<FileInfoBase> files1 = this.GetFiles(@from.Item1);

            if (@from.Item2 != null)
            {
                //// merge the files if we have some overrides.
                IEnumerable<FileInfoBase> files2 = this.GetFiles(@from.Item2);

                combinedFiles = this.GetCombinedList(files1, files2);
            }
            else
            {
                combinedFiles = files1;
            }

            //// ensure we order the plugins by friendly name.
            List<Plugin> items = combinedFiles.Select(fileInfo => this.translator.Translate(fileInfo))
                                    .OrderBy(p => p.FriendlyName).ToList();

            Plugins plugins = new Plugins { Items = items };

            return plugins;
        }

        /// <summary>
        /// Gets the combined list.
        /// </summary>
        /// <param name="files1">The files1.</param>
        /// <param name="files2">The files2.</param>
        /// <returns>The combined list.</returns>
        internal IEnumerable<FileInfoBase> GetCombinedList(
            IEnumerable<FileInfoBase> files1, 
            IEnumerable<FileInfoBase> files2)
        {
            Dictionary<string, FileInfoBase> dictionary1 = files1.ToDictionary(l1 => l1.Name);
            Dictionary<string, FileInfoBase> dictionary2 = files2.ToDictionary(l2 => l2.Name);

            ////get the full list of names.
            List<string> fileNames = dictionary1.Keys.Union(dictionary2.Keys).ToList();

            return fileNames.Select(name => this.GetFile(name, dictionary1, dictionary2));
        }

        /// <summary>
        /// Gets the file.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="dictionary1">The dictionary1.</param>
        /// <param name="dictionary2">The dictionary2.</param>
        /// <returns>The file.</returns>
        internal FileInfoBase GetFile(
            string name,
            Dictionary<string, FileInfoBase> dictionary1,
            Dictionary<string, FileInfoBase> dictionary2)
        {
            FileInfoBase file1 = dictionary1.ContainsKey(name) ? dictionary1[name] : null;
            FileInfoBase file2 = dictionary2.ContainsKey(name) ? dictionary2[name] : null;

            if (file1 != null &&
                file2 != null)
            {
                return file2;
            }

            return file2 ?? file1;
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="directoryInfoBase">The directory info base.</param>
        /// <returns>The files.</returns>
        internal IEnumerable<FileInfoBase> GetFiles(DirectoryInfoBase directoryInfoBase)
        {
            //// we don't want the UI plugins (and also the messenger plugin - that will already be in the projects)
            return directoryInfoBase.GetFiles("*Plugin*")
                .Where(x => !x.Name.Contains(Settings.Droid) &&
                            !x.Name.Contains(Settings.Touch) &&
                            !x.Name.Contains(Settings.WindowsPhone) &&
                            !x.Name.Contains(Settings.WindowsStore) &&
                            !x.Name.Contains(Settings.Wpf) &&
                            !x.Name.Contains(Settings.MessengePluginSuffix));
        }
    }
}

