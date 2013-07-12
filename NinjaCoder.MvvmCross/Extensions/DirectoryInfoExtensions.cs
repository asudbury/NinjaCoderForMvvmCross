// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the DirectoryInfoExtensions type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Extensions
{
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    ///  Defines the DirectoryInfoExtensions type.
    /// </summary>
    public static class DirectoryInfoExtensions
    {
        /// <summary>
        /// Gets the override files.
        /// </summary>
        /// <param name="instance">The instance.</param>
        /// <param name="overrideDirectory">The override directory.</param>
        /// <returns>The overridden file list.</returns>
        public static List<FileInfo> GetOverrideFiles(
            this DirectoryInfo instance,
            DirectoryInfo overrideDirectory)
        {
            List<FileInfo> fileList = instance.GetFiles().ToList();
            List<FileInfo> overrideFileList = overrideDirectory.GetFiles().ToList();

            List<FileInfo> unionList = new List<FileInfo>();

            for (int i = 0; i < fileList.Count; i++)
            {
                FileInfo fileInfo = fileList[i];

                FileInfo overrideFileInfo = overrideFileList.Find(x => x.Name == fileInfo.Name);

                unionList.Add(overrideFileInfo ?? fileInfo);
            }

            return unionList.Union(overrideFileList).ToList();
        }
    }
}
