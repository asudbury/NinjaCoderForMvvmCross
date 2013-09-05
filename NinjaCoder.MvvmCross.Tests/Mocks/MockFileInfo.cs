// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MockFileInfo type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Mocks
{
    using System;
    using System.IO;
    using System.IO.Abstractions;
    using System.Security.AccessControl;

    /// <summary>
    ///  Defines the MockFileInfo type.
    /// </summary>
    public class MockFileInfo : FileInfoBase
    {
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
        /// Gets a value indicating whether this <see cref="MockFileInfo"/> is exists.
        /// </summary>
        public override bool Exists
        {
            get { return true; }
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
        /// Gets the directory.
        /// </summary>
        public override DirectoryInfoBase Directory
        {
            get { return null; }
        }

        /// <summary>
        /// Gets the name of the directory.
        /// </summary>
        public override string DirectoryName
        {
            get { return null; }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is read only.
        /// </summary>
        public override bool IsReadOnly
        {
            get { return false; }
            set { }
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        public override long Length
        {
            get { return 1; }
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
        /// Appends the text.
        /// </summary>
        /// <returns></returns>
        public override StreamWriter AppendText()
        {
            return null;
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="destFileName">Name of the dest file.</param>
        /// <returns></returns>
        public override FileInfoBase CopyTo(string destFileName)
        {
            return null;
        }

        /// <summary>
        /// Copies to.
        /// </summary>
        /// <param name="destFileName">Name of the dest file.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        /// <returns></returns>
        public override FileInfoBase CopyTo(
            string destFileName, 
            bool overwrite)
        {
            return null;
        }

        /// <summary>
        /// Creates this instance.
        /// </summary>
        /// <returns></returns>
        public override Stream Create()
        {
            return null;
        }

        /// <summary>
        /// Creates the text.
        /// </summary>
        /// <returns></returns>
        public override StreamWriter CreateText()
        {
            return null;
        }

        /// <summary>
        /// Decrypts this instance.
        /// </summary>
        public override void Decrypt()
        {
        }

        /// <summary>
        /// Encrypts this instance.
        /// </summary>
        public override void Encrypt()
        {
        }

        /// <summary>
        /// Gets the access control.
        /// </summary>
        /// <returns></returns>
        public override FileSecurity GetAccessControl()
        {
            return null;
        }

        /// <summary>
        /// Gets the access control.
        /// </summary>
        /// <param name="includeSections">The include sections.</param>
        /// <returns></returns>
        public override FileSecurity GetAccessControl(AccessControlSections includeSections)
        {
            return null;
        }

        /// <summary>
        /// Moves to.
        /// </summary>
        /// <param name="destFileName">Name of the dest file.</param>
        public override void MoveTo(string destFileName)
        {
        }

        /// <summary>
        /// Opens the specified mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <returns></returns>
        public override Stream Open(FileMode mode)
        {
            return null;
        }

        /// <summary>
        /// Opens the specified mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="access">The access.</param>
        /// <returns></returns>
        public override Stream Open(
            FileMode mode, 
            FileAccess access)
        {
            return null;
        }

        /// <summary>
        /// Opens the specified mode.
        /// </summary>
        /// <param name="mode">The mode.</param>
        /// <param name="access">The access.</param>
        /// <param name="share">The share.</param>
        /// <returns></returns>
        public override Stream Open(
            FileMode mode, 
            FileAccess access, 
            FileShare share)
        {
            return null;
        }

        /// <summary>
        /// Opens the read.
        /// </summary>
        /// <returns></returns>
        public override Stream OpenRead()
        {
            return null;
        }

        /// <summary>
        /// Opens the text.
        /// </summary>
        /// <returns></returns>
        public override StreamReader OpenText()
        {
            return null;
        }

        /// <summary>
        /// Opens the write.
        /// </summary>
        /// <returns></returns>
        public override Stream OpenWrite()
        {
            return null;
        }

        /// <summary>
        /// Replaces the specified destination file name.
        /// </summary>
        /// <param name="destinationFileName">Name of the destination file.</param>
        /// <param name="destinationBackupFileName">Name of the destination backup file.</param>
        /// <returns></returns>
        public override FileInfoBase Replace(
            string destinationFileName, 
            string destinationBackupFileName)
        {
            return null;
        }

        /// <summary>
        /// Replaces the specified destination file name.
        /// </summary>
        /// <param name="destinationFileName">Name of the destination file.</param>
        /// <param name="destinationBackupFileName">Name of the destination backup file.</param>
        /// <param name="ignoreMetadataErrors">if set to <c>true</c> [ignore metadata errors].</param>
        /// <returns></returns>
        public override FileInfoBase Replace(
            string destinationFileName, 
            string destinationBackupFileName, 
            bool ignoreMetadataErrors)
        {
            return null;
        }

        /// <summary>
        /// Sets the access control.
        /// </summary>
        /// <param name="fileSecurity">The file security.</param>
        public override void SetAccessControl(FileSecurity fileSecurity)
        {
        }
    }
}
