// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MockDirectoryInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Tests.Mocks
{
    using System;
    using System.IO;
    using System.IO.Abstractions;
    using System.Security.AccessControl;

    /// <summary>
    ///  Defines the MockDirectoryInfo type.
    /// </summary>
    public class MockDirectoryInfo : DirectoryInfoBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether [directory exists].
        /// Used my Unit test to set the Exists method return value.
        /// </summary>
        public bool DirectoryExists { get; set; }
        
        /// <summary>
        /// Gets or sets the attributes.
        /// </summary>
        public override FileAttributes Attributes
        {
            get { return 0; }
            set { }
        }

        /// <summary>
        /// Gets or sets the creation time.
        /// </summary>
        public override DateTime CreationTime
        {
            get { return new DateTime(); }
            set { }
        }

        /// <summary>
        /// Gets or sets the creation time UTC.
        /// </summary>
        public override DateTime CreationTimeUtc
        {
            get { return new DateTime(); }
            set { }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="MockDirectoryInfo"/> is exists.
        /// </summary>
        public override bool Exists
        {
            get { return this.DirectoryExists; }
        }

        /// <summary>
        /// Gets the extension.
        /// </summary>
        public override string Extension
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the full name.
        /// </summary>
        public override string FullName
        {
            get { return null; }
        }

        /// <summary>
        /// Gets or sets the last access time.
        /// </summary>
        public override DateTime LastAccessTime
        {
            get { return new DateTime(); }
            set { }
        }

        /// <summary>
        /// Gets or sets the last access time UTC.
        /// </summary>
        public override DateTime LastAccessTimeUtc
        {
            get { return new DateTime(); }
            set { }
        }

        /// <summary>
        /// Gets or sets the last write time.
        /// </summary>
        public override DateTime LastWriteTime
        {
            get { return new DateTime(); }
            set { }
        }

        /// <summary>
        /// Gets or sets the last write time UTC.
        /// </summary>
        public override DateTime LastWriteTimeUtc
        {
            get { return new DateTime(); }
            set { }
        }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public override string Name
        { 
            get { return null; }
        }

        /// <summary>
        /// Gets the parent.
        /// </summary>
        public override DirectoryInfoBase Parent
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the root.
        /// </summary>
        public override DirectoryInfoBase Root
        {
            get { return null; }
        }

        /// <summary>
        /// Deletes this instance.
        /// </summary>
        public override void Delete()
        {
        }

        /// <summary>
        /// Refreshes this instance.
        /// </summary>
        public override void Refresh()
        {
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        public override void Create()
        {
        }

        /// <summary>
        /// Creates the specified directory security.
        /// </summary>
        /// <param name="directorySecurity">The directory security.</param>
        public override void Create(DirectorySecurity directorySecurity)
        {
        }

        /// <summary>
        /// Creates the subdirectory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override DirectoryInfoBase CreateSubdirectory(string path)
        {
            return null;
        }

        /// <summary>
        /// Creates the subdirectory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="directorySecurity">The directory security.</param>
        /// <returns></returns>
        public override DirectoryInfoBase CreateSubdirectory(
            string path, 
            DirectorySecurity directorySecurity)
        {
            return null;
        }

        /// <summary>
        /// Deletes the specified recursive.
        /// </summary>
        /// <param name="recursive">if set to <c>true</c> [recursive].</param>
        public override void Delete(bool recursive)
        {
        }

        /// <summary>
        /// Gets the access control.
        /// </summary>
        /// <returns></returns>
        public override DirectorySecurity GetAccessControl()
        {
            return null;
        }

        /// <summary>
        /// Gets the access control.
        /// </summary>
        /// <param name="includeSections">The include sections.</param>
        /// <returns></returns>
        public override DirectorySecurity GetAccessControl(AccessControlSections includeSections)
        {
            return null;
        }

        /// <summary>
        /// Gets the directories.
        /// </summary>
        /// <returns></returns>
        public override DirectoryInfoBase[] GetDirectories()
        {
            return new DirectoryInfoBase[] { };
        }

        /// <summary>
        /// Gets the directories.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        public override DirectoryInfoBase[] GetDirectories(string searchPattern)
        {
            return new DirectoryInfoBase[] { };
        }

        /// <summary>
        /// Gets the directories.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns></returns>
        public override DirectoryInfoBase[] GetDirectories(string searchPattern, SearchOption searchOption)
        {
            return new DirectoryInfoBase[] { };
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <returns></returns>
        public override FileInfoBase[] GetFiles()
        {
            return new FileInfoBase[] { };
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <returns></returns>
        public override FileInfoBase[] GetFiles(string searchPattern)
        {
            return new FileInfoBase[] { };
        }

        /// <summary>
        /// Gets the files.
        /// </summary>
        /// <param name="searchPattern">The search pattern.</param>
        /// <param name="searchOption">The search option.</param>
        /// <returns></returns>
        public override FileInfoBase[] GetFiles(string searchPattern, SearchOption searchOption)
        {
            return new FileInfoBase[] { };
        }

        /// <summary>
        /// Gets the file system infos.
        /// </summary>
        /// <returns></returns>
        public override FileSystemInfoBase[] GetFileSystemInfos()
        {
            return new FileSystemInfoBase[] { };
        }

        public override FileSystemInfoBase[] GetFileSystemInfos(string searchPattern)
        {
            return new FileSystemInfoBase[] { };
        }

        /// <summary>
        /// Moves to.
        /// </summary>
        /// <param name="destDirName">Name of the dest dir.</param>
        public override void MoveTo(string destDirName)
        {
        }

        /// <summary>
        /// Sets the access control.
        /// </summary>
        /// <param name="directorySecurity">The directory security.</param>
        public override void SetAccessControl(DirectorySecurity directorySecurity)
        {
        }
    }
}
