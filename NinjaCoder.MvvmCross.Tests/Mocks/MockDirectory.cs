// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MockDirectory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Mocks
{
    using System;
    using System.IO;
    using System.IO.Abstractions;
    using System.Security.AccessControl;

    /// <summary>
    ///  Defines the MockDirectory type.
    /// </summary>
    public class MockDirectory : DirectoryBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether [directory exists].
        /// Used by my Unit test to set the Exists method return value.
        /// </summary>
        public bool DirectoryExists { get; set; }

        /// <summary>
        /// Gets or sets the get files list.
        /// Used by my Unit test to set the GetFiles method return value.
        /// </summary>
        public string[] GetFilesList { get; set; }

        /// <summary>
        /// Creates the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override DirectoryInfoBase CreateDirectory(string path)
        {
            return null;
        }

        /// <summary>
        /// Creates the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="directorySecurity">The directory security.</param>
        /// <returns></returns>
        public override DirectoryInfoBase CreateDirectory(
            string path, 
            DirectorySecurity directorySecurity)
        {
            return null;
        }

        /// <summary>
        /// Deletes the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        public override void Delete(string path)
        {
        }

        /// <summary>
        /// Deletes the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="recursive">if set to <c>true</c> [recursive].</param>
        public override void Delete(
            string path, 
            bool recursive)
        {
        }

        /// <summary>
        /// Existses the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override bool Exists(string path)
        {
            return this.DirectoryExists;
        }

        /// <summary>
        /// Gets the access control.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override DirectorySecurity GetAccessControl(string path)
        {
            return null;
        }

        /// <summary>
        /// Gets the access control.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="includeSections">The include sections.</param>
        /// <returns></returns>
        public override DirectorySecurity GetAccessControl(
            string path, 
            AccessControlSections includeSections)
        {
            return null;
        }

        /// <summary>
        /// Gets the creation time.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override DateTime GetCreationTime(string path)
        {
            return new DateTime();
        }

        /// <summary>
        /// Gets the creation time UTC.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override DateTime GetCreationTimeUtc(string path)
        {
            return new DateTime();
        }

        /// <summary>
        /// Gets the current directory.
        /// </summary>
        /// <returns></returns>
        public override string GetCurrentDirectory()
        {
            return null;
        }

        /// <summary>
        /// Gets the directories.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override string[] GetDirectories(string path)
        {
            return new string[] { };
        }

        /// <summary>
        /// Gets the directories.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        public override string[] GetDirectories(string path, string searchPattern)
        {
            return new string[] { };
        }

        /// <summary>
        /// Gets the directories.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns></returns>
        public override string[] GetDirectories(string path, string searchPattern, SearchOption searchOption)
        {
            return new string[] { };
        }

        /// <summary>
        /// Gets the directory root.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override string GetDirectoryRoot(string path)
        {
            return null;
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override string[] GetFiles(string path)
        {
            return this.GetFilesList;
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        public override string[] GetFiles(
            string path, 
            string searchPattern)
        {
            return this.GetFilesList;
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns></returns>
        public override string[] GetFiles(
            string path, 
            string searchPattern, 
            SearchOption searchOption)
        {
            return this.GetFilesList;
        }

        /// <summary>
        /// Gets the file system entries.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override string[] GetFileSystemEntries(string path)
        {
            return new string[] { };
        }

        /// <summary>
        /// Gets the file system entries.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        public override string[] GetFileSystemEntries(
            string path, 
            string searchPattern)
        {
            return new string[] { };
        }

        /// <summary>
        /// Gets the last access time.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override DateTime GetLastAccessTime(string path)
        {
            return new DateTime();
        }

        /// <summary>
        /// Gets the last access time UTC.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override DateTime GetLastAccessTimeUtc(string path)
        {
            return new DateTime();
        }

        /// <summary>
        /// Gets the last write time.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override DateTime GetLastWriteTime(string path)
        {
            return new DateTime();
        }

        /// <summary>
        /// Gets the last write time UTC.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override DateTime GetLastWriteTimeUtc(string path)
        {
            return new DateTime();
        }

        /// <summary>
        /// Gets the logical drives.
        /// </summary>
        /// <returns></returns>
        public override string[] GetLogicalDrives()
        {
            return new string[] { };
        }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override DirectoryInfoBase GetParent(string path)
        {
            return null;
        }

        /// <summary>
        /// Moves the specified source dir name.
        /// </summary>
        /// <param name="sourceDirName">Name of the source dir.</param>
        /// <param name="destDirName">Name of the dest dir.</param>
        public override void Move(string sourceDirName, string destDirName)
        {
        }

        /// <summary>
        /// Sets the access control.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="directorySecurity">The directory security.</param>
        public override void SetAccessControl(
            string path, 
            DirectorySecurity directorySecurity)
        {
        }

        /// <summary>
        /// Sets the creation time.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="creationTime">The creation time.</param>
        public override void SetCreationTime(
            string path, 
            DateTime creationTime)
        {
        }

        /// <summary>
        /// Sets the creation time UTC.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="creationTimeUtc">The creation time UTC.</param>
        public override void SetCreationTimeUtc(
            string path, 
            DateTime creationTimeUtc)
        {
        }

        /// <summary>
        /// Sets the current directory.
        /// </summary>
        /// <param name="path">The path.</param>
        public override void SetCurrentDirectory(string path)
        {
        }

        /// <summary>
        /// Sets the last access time.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="lastAccessTime">The last access time.</param>
        public override void SetLastAccessTime(
            string path, 
            DateTime lastAccessTime)
        {
        }

        /// <summary>
        /// Sets the last access time UTC.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="lastAccessTimeUtc">The last access time UTC.</param>
        public override void SetLastAccessTimeUtc(
            string path, 
            DateTime lastAccessTimeUtc)
        {
        }

        /// <summary>
        /// Sets the last write time.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="lastWriteTime">The last write time.</param>
        public override void SetLastWriteTime(
            string path, 
            DateTime lastWriteTime)
        {
        }

        /// <summary>
        /// Sets the last write time UTC.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="lastWriteTimeUtc">The last write time UTC.</param>
        public override void SetLastWriteTimeUtc(
            string path, 
            DateTime lastWriteTimeUtc)
        {
        }
    }
}
