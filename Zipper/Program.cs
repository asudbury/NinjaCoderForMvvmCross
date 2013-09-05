// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Zipper
{
    using System;
    using System.IO;
    using System.IO.Abstractions;
    using NinjaCoder.MvvmCross.Services.Interfaces;
    using TinyIoC;

    /// <summary>
    /// Defines the Program type.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Mains the specified args.
        /// </summary>
        /// <param name="args">The args.</param>
        public static void Main(string[] args)
        {
            if (args == null)
            {
                Console.WriteLine(@"args are null");
            }
            else
            {
                if (args.Length == 2)
                {
                    Initialize();

                    IZipperService zipperService = TinyIoCContainer.Current.Resolve<IZipperService>();

                    //// directory where the zip files are
                    string zipFile = args[0];

                    bool exists = File.Exists(zipFile);

                    if (exists)
                    {
                        //// the lib folder where the dlls will have been updated.
                        string updatesDirectory = args[1];

                        exists = Directory.Exists(updatesDirectory);

                        if (exists)
                        {
                            zipperService.UpdateZip(zipFile, updatesDirectory, "Lib", true);
                        }
                        else
                        {
                            Console.WriteLine(@"Directory " + updatesDirectory + @" does not exist");
                        }
                    }
                    else
                    {
                        Console.WriteLine(@"File " + zipFile + @" does not exist");
                    }
                }
                else
                {
                    Console.WriteLine(@"args are not valid");
                }
            }
        }

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        internal static void Initialize()
        {
            TinyIoCContainer container = TinyIoCContainer.Current;
            container.AutoRegister();
            container.Register<IFileSystem>(new FileSystem());
        }
    }
}
