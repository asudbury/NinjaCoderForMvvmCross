// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ZipperService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using System.Collections.ObjectModel;
    using System.IO;
    using System.IO.Compression;

    using NinjaCoder.MvvmCross.Services.Interfaces;

    /// <summary>
    ///  Defines the ZipperService type.
    /// </summary>
    public class ZipperService : IZipperService
    {
        /// <summary>
        /// The streamWriter.
        /// </summary>
        private StreamWriter sw;

        /// <summary>
        /// Updates the directory.
        /// </summary>
        /// <param name="directory">The directory.</param>
        /// <param name="updatesDirectory">The updates directory.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="createLogFile">if set to <c>true</c> [create log file].</param>
        /// <param name="replaceFiles">if set to <c>true</c> [replace files].</param>
        public void UpdateDirectory(
            string directory, 
            string updatesDirectory,
            string folderName,
            bool createLogFile,
            bool replaceFiles)
        {
            bool exists = Directory.Exists(directory);

            if (exists)
            {
                string[] files = Directory.GetFiles(directory, "*.zip");

                foreach (string file in files)
                {
                    this.UpdateZip(file, updatesDirectory, folderName, createLogFile, replaceFiles);
                }
            }
        }

        /// <summary>
        /// Updates the zip.
        /// </summary>
        /// <param name="zipName">Name of the zip.</param>
        /// <param name="updatesDirectory">The updates directory.</param>
        /// <param name="folderName">Name of the folder.</param>
        /// <param name="createLogFile">if set to <c>true</c> [create log file].</param>
        /// <param name="replaceFiles">if set to <c>true</c> [replace files].</param>
        public void UpdateZip(
            string zipName, 
            string updatesDirectory,
            string folderName,
            bool createLogFile,
            bool replaceFiles)
        {
            if (createLogFile)
            {
                this.sw = new StreamWriter(zipName + ".log", true);
                this.WriteMessageToLogFile("Start");
            }
             
            using (ZipArchive zipArchive = ZipFile.Open(zipName, ZipArchiveMode.Update))
            {
                this.WriteMessageToLogFile("Opening Zip File");

                //// look for the lib directory
                ReadOnlyCollection<ZipArchiveEntry> entries = zipArchive.Entries;

                this.WriteMessageToLogFile("Entries = " + entries.Count);

                for (int i = 0; i < entries.Count; i++)
                {
                    ZipArchiveEntry zipArchiveEntry = entries[i];

                    string fullName = zipArchiveEntry.FullName;

                    this.WriteMessageToLogFile("Processing " + fullName);

                    if (fullName.ToLower().StartsWith(folderName.ToLower()) &&
                        fullName.ToLower().EndsWith(".dll"))
                    {
                        //// we have found one of the assemblies
                        this.WriteMessageToLogFile("Found assembley " + fullName);

                        //// first look to see if we have a replacement
                        string newFilePath = updatesDirectory + @"\" + zipArchiveEntry.Name;

                        bool exists = File.Exists(newFilePath);

                        if (exists)
                        {
                            FileInfo fileInfo = new FileInfo(fullName);
                            FileInfo newFileInfo = new FileInfo(newFilePath);

                            if (newFileInfo.LastWriteTime > fileInfo.LastWriteTime)
                            {
                                this.WriteMessageToLogFile(zipArchiveEntry.Name + " is marked to be replaced");

                                if (replaceFiles)
                                {
                                    //// delete the current one!
                                    zipArchiveEntry.Delete();

                                    //// and now add the new one!
                                    zipArchive.CreateEntryFromFile(newFilePath, fullName);

                                    this.WriteMessageToLogFile(zipArchiveEntry.Name + " has been replaced");
                                }
                            }
                            else
                            {
                                this.WriteMessageToLogFile(zipArchiveEntry.Name + " has not been replaced");
                            }
                        }
                        else
                        {
                            this.WriteMessageToLogFile(newFilePath + " does not exist");                            
                        }
                    }
                }
            }

            if (this.sw != null)
            {
                this.WriteMessageToLogFile("End");
                this.sw.Close();
            }
        }

        /// <summary>
        /// Writes the message to log file.
        /// </summary>
        /// <param name="message">The message.</param>
        internal void WriteMessageToLogFile(string message)
        {
            if (this.sw != null)
            {
                this.sw.WriteLine(message);
            }
        }
    }
}
