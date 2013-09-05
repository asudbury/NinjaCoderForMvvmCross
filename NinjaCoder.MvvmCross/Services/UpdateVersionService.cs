// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the UpdateVersionService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System;
    using System.IO;
    using System.IO.Abstractions;
    using System.Text;

    using Interfaces;

    /// <summary>
    ///  Defines the UpdateVersionService type.
    /// </summary>
    public class UpdateVersionService : BaseService, IUpdateVersionService
    {
        /// <summary>
        /// The file system.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateVersionService"/> class.
        /// </summary>
        public UpdateVersionService()
            : this(new FileSystem())
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateVersionService"/> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public UpdateVersionService(IFileSystem fileSystem)
        {
            this.fileSystem = fileSystem;
        }

        /// <summary>
        /// Runs the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <param name="versionNumber">The version number.</param>
        public void Run(
            string fileName,
            string versionNumber)
        {
            if (!this.fileSystem.File.Exists(fileName))
            {
                Console.WriteLine(@"Error: Cannot find file " + fileName);
                return;
            }

            StreamReader reader = new StreamReader(fileName);
            StreamWriter writer = new StreamWriter(fileName + ".new");
            
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                line = this.ProcessLine(line, versionNumber);
                writer.WriteLine(line);
            }

            reader.Close();
            writer.Close();

            this.fileSystem.File.Delete(fileName);
            this.fileSystem.File.Move(fileName + ".new", fileName);
        }

        /// <summary>
        /// Processes the line.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="versionNumber">The version number.</param>
        /// <returns>
        /// The processed line.
        /// </returns>
        internal string ProcessLine(
            string line,
            string versionNumber)
        {
            line = this.ProcessLinePart(line, "[assembly: AssemblyVersion(\"", versionNumber);
            line = this.ProcessLinePart(line, "[assembly: AssemblyFileVersion(\"", versionNumber);
            return line;
        }

        /// <summary>
        /// Processes the line part.
        /// </summary>
        /// <param name="line">The line.</param>
        /// <param name="part">The part.</param>
        /// <param name="versionNumber">The version number.</param>
        /// <returns>
        /// The processed line part.
        /// </returns>
        internal string ProcessLinePart(
            string line, 
            string part,
            string versionNumber)
        {
            int startPosition = line.IndexOf(part, StringComparison.Ordinal);

            if (startPosition >= 0)
            {
                startPosition += part.Length;
                int endPosition = line.IndexOf('"', startPosition);

                StringBuilder stringBuilder = new StringBuilder(line);
                stringBuilder.Remove(startPosition, endPosition - startPosition);
                stringBuilder.Insert(startPosition, versionNumber);
                line = stringBuilder.ToString();
            }

            return line;
        }
    }
}
