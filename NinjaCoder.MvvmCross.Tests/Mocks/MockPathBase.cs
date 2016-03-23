// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MockPathBase type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Mocks
{
    using System.IO.Abstractions;

    /// <summary>
    ///  Defines the MockPathBase type.
    /// </summary>
    public class MockPathBase : PathBase
    {
        /// <summary>
        /// Gets the alt directory separator char.
        /// </summary>
        public override char AltDirectorySeparatorChar
        {
            get { return '\0'; }
        }

        /// <summary>
        /// Gets the directory separator char.
        /// </summary>
        public override char DirectorySeparatorChar
        {
            get { return '\0'; }
        }

        /// <summary>
        /// Gets the invalid path chars.
        /// </summary>
        public override char[] InvalidPathChars
        {
            get { return new char[] { }; }
        }

        /// <summary>
        /// Gets the path separator.
        /// </summary>
        public override char PathSeparator
        {
            get { return '\0'; }
        }

        /// <summary>
        /// Gets the volume separator char.
        /// </summary>
        public override char VolumeSeparatorChar
        {
            get { return '\0'; }
        }

        /// <summary>
        /// Changes the extension.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="extension">The extension.</param>
        /// <returns>The changed extension.</returns>
        public override string ChangeExtension(
            string path, 
            string extension)
        {
            return null;
        }

        /// <summary>
        /// Combines the specified path1.
        /// </summary>
        /// <param name="path1">The path1.</param>
        /// <param name="path2">The path2.</param>
        /// <returns>The combined path.</returns>
        public override string Combine(
            string path1, 
            string path2)
        {
            return null;
        }

        /// <summary>
        /// Gets the name of the directory.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The directory name.</returns>
        public override string GetDirectoryName(string path)
        {
            return null;
        }

        /// <summary>
        /// Gets the extension.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The extension.</returns>
        public override string GetExtension(string path)
        {
            return null;
        }

        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The file name.</returns>
        public override string GetFileName(string path)
        {
            return null;
        }

        /// <summary>
        /// Gets the file name without extension.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The file name without extension.</returns>
        public override string GetFileNameWithoutExtension(string path)
        {
            return null;
        }

        /// <summary>
        /// Gets the full path.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The full path.</returns>
        public override string GetFullPath(string path)
        {
            return null;
        }

        /// <summary>
        /// Gets the invalid file name chars.
        /// </summary>
        /// <returns>The invalid file name characters.</returns>
        public override char[] GetInvalidFileNameChars()
        {
            return new char[] { };
        }

        /// <summary>
        /// Gets the invalid path chars.
        /// </summary>
        /// <returns>The invalid path characters.</returns>
        public override char[] GetInvalidPathChars()
        {
            return new char[] { };
        }

        /// <summary>
        /// Gets the path root.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>The path root.</returns>
        public override string GetPathRoot(string path)
        {
            return null;
        }

        /// <summary>
        /// Gets the random name of the file.
        /// </summary>
        /// <returns>A randon file name.</returns>
        public override string GetRandomFileName()
        {
            return null;
        }

        /// <summary>
        /// Gets the name of the temp file.
        /// </summary>
        /// <returns>A temp file name.</returns>
        public override string GetTempFileName()
        {
            return null;
        }

        /// <summary>
        /// Gets the temp path.
        /// </summary>
        /// <returns>The temp path.</returns>
        public override string GetTempPath()
        {
            return null;
        }

        /// <summary>
        /// Determines whether the specified path has extension.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>True or false.</returns>
        public override bool HasExtension(string path)
        {
            return false;
        }

        /// <summary>
        /// Determines whether [is path rooted] [the specified path].
        /// </summary>
        /// <param name="path">The path.</param>
        /// <returns>True or false.</returns>
        public override bool IsPathRooted(string path)
        {
            return false;
        }
    }
}
