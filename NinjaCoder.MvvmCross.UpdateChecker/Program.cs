// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the Program type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.UpdateChecker
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    using NinjaCoder.MvvmCross.Infrastructure.Services;
    using NinjaCoder.MvvmCross.UpdateChecker.VisualStudioGalleryService;

    using Scorchio.VisualStudio.Services;

    /// <summary>
    /// Defines the Program type.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Mains the specified args.
        /// </summary>
        static void Main()
        {
            TraceService.WriteHeader("NinjaCoder.MvvmCross.UpdateChecker::Main");

            CheckForUpdate();
        }

        /// <summary>
        /// Checks for update.
        /// </summary>
        private static void CheckForUpdate()
        {
            TraceService.WriteLine("NinjaCoder.MvvmCross.UpdateChecker::CheckForUpdate");

            try
            {
                VsIdeServiceClient client = new VsIdeServiceClient();

                SettingsService settingsService = new SettingsService();

                string[] keys = new string[1];
                keys[0] = settingsService.GalleryId;

                Dictionary<string, string> requestContext = new Dictionary<string, string>()
                                                            {
                                                                { "LCID", "1033" },
                                                                {"SearchSource", "ExtensionManagerUpdate"},
                                                            };

                string[] output = client.GetCurrentVersionsForVsixList(keys, requestContext);

                if (output.Length > 0)
                {
                    string version = output[0];

                    TraceService.WriteLine("NinjaCoder.MvvmCross.UpdateChecker::CheckForUpdate version=" + version);

                    settingsService.LatestVersionOnGallery = version;
                    settingsService.LastCheckedForUpdateDateTime = DateTime.Now.ToString(CultureInfo.InvariantCulture);
                }
            }
            catch (Exception exception)
            {
                TraceService.WriteError("NinjaCoder.MvvmCross.UpdateChecker::CheckForUpdate Error=" + exception.Message);
            }
        }
    }
}
