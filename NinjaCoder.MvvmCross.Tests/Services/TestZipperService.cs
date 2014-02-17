// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestZipperService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Tests.Services
{
    using System;
    using System.IO;
    using System.IO.Abstractions;
    using System.IO.Compression;

    using Moq;
    using NinjaCoder.MvvmCross.Tests.Mocks;
    using NUnit.Framework;

    using Scorchio.Infrastructure.Services;

    /// <summary>
    ///  Defines the TestZipperService type.
    /// </summary>
    [TestFixture]
    public class TestZipperService
    {
        /// <summary>
        /// The zip path.
        /// </summary>
        private const string ZipPath = @"c:\temp\ninja.zip";

        /// <summary>
        /// The service.
        /// </summary>
        private ZipperService service;

        /// <summary>
        /// The mock file system.
        /// </summary>
        private Mock<IFileSystem> mockFileSystem;

        /// <summary>
        /// The mock directory
        /// </summary>
        private MockDirectory mockDirectory;

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        [TestFixtureSetUp]
        public void Initialize()
        {
            this.mockFileSystem = new Mock<IFileSystem>();
            this.mockDirectory = new MockDirectory { DirectoryExists = true };
            this.mockFileSystem.SetupGet(x => x.Directory).Returns(this.mockDirectory);
            this.service = new ZipperService(this.mockFileSystem.Object);

            this.BuildTestZipFile();
        }

        /// <summary>
        /// Tests the update directory.
        /// </summary>
        [Test]
        public void TestUpdateDirectory()
        {
            string[] filesList = new[] { "1", "2" };

            this.mockDirectory.GetFilesList = filesList;

            this.service.UpdateDirectory(
                "directory", 
                "updatesDirectory", 
                "folderName", 
                 true);
        }

        /// <summary>
        /// Tests the update zip.
        /// </summary>
        [Test]
        public void TestUpdateZip()
        {
            this.service.UpdateZip(
                ZipPath,
                "updatesDirectory",
                "folderName",
                true);
        }

        /// <summary>
        /// Tests the build zip file.
        /// </summary>
        [Test]
        public void TestBuildZipFile()
        {
            ZipArchive zipArchive = this.GetZipArchive();
            ZipArchiveEntry zipArchiveEntry = zipArchive.CreateEntry("folderName.dll");

            MockFile mockFile = new MockFile { FileExists = false };

            this.mockFileSystem.SetupGet(x => x.File).Returns(mockFile);
           
            this.service.BuildZipFile("updatesDirectory", "folderName", zipArchive, zipArchiveEntry);
        }

        /// <summary>
        /// Tests the update file.
        /// </summary>
        [Test]
        public void TestUpdateFile()
        {
            //// setup test file

            const string TestFile = @"c:\temp\test.txt";

            File.Create(TestFile);

            const string TestNewFile = @"c:\temp\newtest.txt";

            FileStream fileStream = File.Create(TestNewFile);
            fileStream.Close();

            ZipArchive zipArchive = this.GetZipArchive();
            ZipArchiveEntry zipArchiveEntry = zipArchive.CreateEntry("folderName.dll");

            Mock<IFileInfoFactory> mockFileInfoFactory = new Mock<IFileInfoFactory>();
            
            MockFileInfoBase mockFileInfoBase = new MockFileInfoBase
                                                    {
                                                        LastWriteTimeValue = DateTime.Now
                                                    };

            mockFileInfoFactory.Setup(x => x.FromFileName(TestFile)).Returns(mockFileInfoBase);

            MockFileInfoBase mockFileInfoBase2 = new MockFileInfoBase
                                                     {
                                                         LastWriteTimeValue = DateTime.Now.AddMinutes(1)
                                                     };

            mockFileInfoFactory.Setup(x => x.FromFileName(TestNewFile)).Returns(mockFileInfoBase2);

            this.mockFileSystem.SetupGet(x => x.FileInfo).Returns(mockFileInfoFactory.Object);

            this.service.UpdateFile(zipArchive, zipArchiveEntry, TestFile, TestNewFile);
 }

        /// <summary>
        /// Gets the zip archive.
        /// </summary>
        /// <returns>The zip archive.</returns>
        internal ZipArchive GetZipArchive()
        {
            FileStream zipToOpen = new FileStream(ZipPath, FileMode.Open);
            ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update);

            zipToOpen.Close();

            return archive;
        }

        /// <summary>
        /// Builds the test zip file.
        /// </summary>
        internal void BuildTestZipFile()
        {
            using (FileStream zipToOpen = new FileStream(ZipPath, FileMode.Create))
            {
                using (ZipArchive archive = new ZipArchive(zipToOpen, ZipArchiveMode.Update))
                {
                    ZipArchiveEntry readmeEntry = archive.CreateEntry("Readme.dll");
                    using (StreamWriter writer = new StreamWriter(readmeEntry.Open()))
                    {
                        writer.WriteLine("Information about this package.");
                        writer.WriteLine("========================");
                    }
                }
            }
        }
    }
}
