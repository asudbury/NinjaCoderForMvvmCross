// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ZipperService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System.Collections.ObjectModel;
    using System.IO;
    using System.IO.Abstractions;
    using System.IO.Compression;
    using System.Linq;

    using Interfaces;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    ///  Defines the ZipperService type.
    /// </summary>
    public class ZipperService : BaseService, IZipperService
    {
        /// <summary>
        /// The file system.
        /// </summary>
        private readonly IFileSystem fileSystem;

        /// <summary>
        /// The streamWriter.
        /// </summary>
        private StreamWriter streamWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="ZipperService" /> class.
        /// </summary>
        /// <param name="fileSystem">The file system.</param>
        public ZipperService(IFileSystem fileSystem)
        {
            //// init the tracing service first!
            TraceService.Initialize(
                false,
                true, //// log to console.
                false,
                string.Empty,
                false);

            TraceService.WriteLine("ZipperService::Constructor");

            this.fileSystem = fileSystem;
        }

        /// <summary>
        /// Updates the directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="updatesDirectory">The updates directory.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="createLogFile">if set to <c>true</c> [create log file].</param>
        public void UpdateDirectory(
            string directory, 
            string updatesDirectory,
            string folderName,
            bool createLogFile)
        {
            TraceService.WriteLine("ZipperService::UpdateDirectory");

            bool exists = this.fileSystem.Directory.Exists(directory);

            if (exists)
            {
                string[] files = this.fileSystem.Directory.GetFiles(directory, "*.zip");

                files
                    .ToList()
                    .ForEach(x => this.UpdateZip(x, updatesDirectory, folderName, createLogFile));
            }
        }

        /// <summary>
        /// Updates the zip.
        /// </summary>
        /// <param name="zipName">Name of the zip.</param>
        /// <param name="updatesDirectory">The updates directory.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="createLogFile">if set to <c>true</c> [create log file].</param>
        public void UpdateZip(
            string zipName, 
            string updatesDirectory,
            string folderName,
            bool createLogFile)
        {
            ////TraceService.WriteLine("ZipperService::UpdateZip");

            if (createLogFile)
            {
                this.streamWriter = new StreamWriter(zipName + ".log", true);
                TraceService.WriteLine("Start");
            }
             
            using (ZipArchive zipArchive = ZipFile.Open(zipName, ZipArchiveMode.Update))
            {
                TraceService.WriteLine("Opening Zip File");

                //// look for the lib directory
                ReadOnlyCollection<ZipArchiveEntry> entries = zipArchive.Entries;

                TraceService.WriteLine("Entries = " + entries.Count);

                for (int index = 0; index < entries.Count; index++)
                {
                    ZipArchiveEntry zipArchiveEntry = entries[index];
                    this.BuildZipFile(updatesDirectory, folderName, zipArchive, zipArchiveEntry);
                }
            }
                    
            if (this.streamWriter != null)
            {
                TraceService.WriteLine("End");
                this.streamWriter.Close();
            }
        }

        /// <summary>
        /// Builds the zip file.
        /// </summary>
        /// <param name="updatesDirectory">The updates directory.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="zipArchive">The zip archive.</param>
        /// <param name="zipArchiveEntry">The zip archive entry.</param>
        internal void BuildZipFile(
            string updatesDirectory, 
            string folderName, 
            ZipArchive zipArchive, 
            ZipArchiveEntry zipArchiveEntry)
        {
            ////TraceService.WriteLine("ZipperService::BuildZipFile");

            string fullName = zipArchiveEntry.FullName;

            ////TraceService.WriteLine("Processing " + fullName);

            if (fullName.ToLower().StartsWith(folderName.ToLower()) &&
                fullName.ToLower().EndsWith(".dll"))
            {
                //// we have found one of the assemblies
                TraceService.WriteLine("Found assembley " + fullName);

                //// first look to see if we have a replacement
                string newFilePath = updatesDirectory + @"\" + zipArchiveEntry.Name;

                bool exists = this.fileSystem.File.Exists(newFilePath);

                if (exists)
                {
                    this.UpdateFile(zipArchive, zipArchiveEntry, fullName, newFilePath);
                }
                else
                {
                    TraceService.WriteLine(newFilePath + " does not exist");
                }
            }
        }

        /// <summary>
        /// Updates the file.
        /// </summary>
        /// <param name="zipArchive">The zip archive.</param>
        /// <param name="zipArchiveEntry">The zip archive entry.</param>
        /// <param name="fullName">The full name.</param>
        /// <param name="newFilePath">The new file path.</param>
        internal void UpdateFile(
            ZipArchive zipArchive, 
            ZipArchiveEntry zipArchiveEntry, 
            string fullName, 
            string newFilePath)
        {
            TraceService.WriteLine("ZipperService::UpdateFile fullName=" + fullName);

            FileInfoBase fileInfoBase = this.fileSystem.FileInfo.FromFileName(fullName);
            FileInfoBase newFileInfoBase = this.fileSystem.FileInfo.FromFileName(newFilePath);

            if (newFileInfoBase.LastWriteTime > fileInfoBase.LastWriteTime)
            {
                //// delete the current one!
                zipArchiveEntry.Delete();

                //// and now add the new one!
                zipArchive.CreateEntryFromFile(newFilePath, fullName);

                TraceService.WriteLine(zipArchiveEntry.Name + " has been replaced");
            }
            else
            {
                TraceService.WriteLine(zipArchiveEntry.Name + " has not been replaced");
            }
        }
    }
}
