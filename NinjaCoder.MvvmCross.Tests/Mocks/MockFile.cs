// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MockFile type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace NinjaCoder.MvvmCross.Tests.Mocks
{
    using System;
    using System.IO;
    using System.IO.Abstractions;
    using System.Security.AccessControl;
    using System.Text;

    /// <summary>
    ///  Defines the MockFile type.
    /// </summary>
    public class MockFile : FileBase
    {
        /// <summary>
        /// Gets or sets a value indicating whether [file exists].
        /// Used my Unit test to set the Exists method return value.
        /// </summary>
        public bool FileExists { get; set; }

        /// <summary>
        /// Appends all text.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The contents.</param>
        public override void AppendAllText(
            string path, 
            string contents)
        {
        }

        /// <summary>
        /// Appends all text.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="encoding">The encoding.</param>
        public override void AppendAllText(
            string path, 
            string contents, 
            Encoding encoding)
        {
        }

        /// <summary>
        /// Appends the text.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override StreamWriter AppendText(string path)
        {
            return null;
        }

        /// <summary>
        /// Copies the specified source file name.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destFileName">Name of the dest file.</param>
        public override void Copy(
            string sourceFileName, 
            string destFileName)
        {
        }

        /// <summary>
        /// Copies the specified source file name.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destFileName">Name of the dest file.</param>
        /// <param name="overwrite">if set to <c>true</c> [overwrite].</param>
        public override void Copy(
            string sourceFileName, 
            string destFileName, 
            bool overwrite)
        {
        }

        /// <summary>
        /// Creates the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override Stream Create(string path)
        {
            return null;
        }

        /// <summary>
        /// Creates the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <returns></returns>
        public override Stream Create(
            string path, 
            int bufferSize)
        {
            return null;
        }

        /// <summary>
        /// Creates the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="options">The options.</param>
        /// <returns></returns>
        public override Stream Create(
            string path, 
            int bufferSize, 
            FileOptions options)
        {
            return null;
        }

        /// <summary>
        /// Creates the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="bufferSize">Size of the buffer.</param>
        /// <param name="options">The options.</param>
        /// <param name="fileSecurity">The file security.</param>
        /// <returns></returns>
        public override Stream Create(
            string path, 
            int bufferSize, 
            FileOptions options, 
            FileSecurity fileSecurity)
        {
            return null;
        }

        /// <summary>
        /// Creates the text.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override StreamWriter CreateText(string path)
        {
            return null;
        }

        /// <summary>
        /// Decrypts the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        public override void Decrypt(string path)
        {
        }

        /// <summary>
        /// Deletes the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        public override void Delete(string path)
        {
        }

        /// <summary>
        /// Encrypts the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        public override void Encrypt(string path)
        {
        }

        /// <summary>
        /// Exists the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override bool Exists(string path)
        {
            return this.FileExists;
        }

        /// <summary>
        /// Gets the access control.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override FileSecurity GetAccessControl(string path)
        {
            return null;
        }

        /// <summary>
        /// Gets the access control.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="includeSections">The include sections.</param>
        /// <returns></returns>
        public override FileSecurity GetAccessControl(
            string path, 
            AccessControlSections includeSections)
        {
            return null;
        }

        /// <summary>
        /// Gets the attributes.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override FileAttributes GetAttributes(string path)
        {
            return 0;
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
        /// Moves the specified source file name.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destFileName">Name of the dest file.</param>
        public override void Move(string sourceFileName, string destFileName)
        {
        }

        /// <summary>
        /// Opens the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="mode">The mode.</param>
        /// <returns></returns>
        public override Stream Open(
            string path, 
            FileMode mode)
        {
            return null;
        }

        /// <summary>
        /// Opens the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="access">The access.</param>
        /// <returns></returns>
        public override Stream Open(
            string path, 
            FileMode mode, 
            FileAccess access)
        {
            return null;
        }

        /// <summary>
        /// Opens the specified path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="access">The access.</param>
        /// <param name="share">The share.</param>
        /// <returns></returns>
        public override Stream Open(
            string path, 
            FileMode mode, 
            FileAccess access, 
            FileShare share)
        {
            return null;
        }

        /// <summary>
        /// Opens the read.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override Stream OpenRead(string path)
        {
            return null;
        }

        /// <summary>
        /// Opens the text.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override StreamReader OpenText(string path)
        {
            return null;
        }

        /// <summary>
        /// Opens the write.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override Stream OpenWrite(string path)
        {
            return null;
        }

        /// <summary>
        /// Reads all bytes.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override byte[] ReadAllBytes(string path)
        {
            return new byte[] { };
        }

        /// <summary>
        /// Reads all lines.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override string[] ReadAllLines(string path)
        {
            return new string[] { };
        }

        /// <summary>
        /// Reads all lines.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public override string[] ReadAllLines(
            string path, 
            Encoding encoding)
        {
            return new string[] { };
        }

        /// <summary>
        /// Reads all text.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns></returns>
        public override string ReadAllText(string path)
        {
            return null;
        }

        /// <summary>
        /// Reads all text.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="encoding">The encoding.</param>
        /// <returns></returns>
        public override string ReadAllText(
            string path, 
            Encoding encoding)
        {
            return null;
        }

        /// <summary>
        /// Replaces the specified source file name.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destinationFileName">Name of the destination file.</param>
        /// <param name="destinationBackupFileName">Name of the destination backup file.</param>
        public override void Replace(
            string sourceFileName, 
            string destinationFileName, 
            string destinationBackupFileName)
        {
        }

        /// <summary>
        /// Replaces the specified source file name.
        /// </summary>
        /// <param name="sourceFileName">Name of the source file.</param>
        /// <param name="destinationFileName">Name of the destination file.</param>
        /// <param name="destinationBackupFileName">Name of the destination backup file.</param>
        /// <param name="ignoreMetadataErrors">if set to <c>true</c> [ignore metadata errors].</param>
        public override void Replace(
            string sourceFileName, 
            string destinationFileName, 
            string destinationBackupFileName, 
            bool ignoreMetadataErrors)
        {
        }

        /// <summary>
        /// Sets the access control.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileSecurity">The file security.</param>
        public override void SetAccessControl(
            string path, 
            FileSecurity fileSecurity)
        {
        }

        /// <summary>
        /// Sets the attributes.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="fileAttributes">The file attributes.</param>
        public override void SetAttributes(
            string path, 
            FileAttributes fileAttributes)
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

        /// <summary>
        /// Writes all bytes.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="bytes">The bytes.</param>
        public override void WriteAllBytes(
            string path, 
            byte[] bytes)
        {
        }

        /// <summary>
        /// Writes all lines.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The contents.</param>
        public override void WriteAllLines(
            string path, 
            string[] contents)
        {
        }

        /// <summary>
        /// Writes all lines.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="encoding">The encoding.</param>
        public override void WriteAllLines(
            string path, 
            string[] 
            contents, 
            Encoding encoding)
        {
        }

        /// <summary>
        /// Writes all text.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The contents.</param>
        public override void WriteAllText(
            string path, 
            string contents)
        {
        }

        /// <summary>
        /// Writes all text.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="contents">The contents.</param>
        /// <param name="encoding">The encoding.</param>
        public override void WriteAllText(
            string path, 
            string contents, 
            Encoding encoding)
        {
        }
    }
}
