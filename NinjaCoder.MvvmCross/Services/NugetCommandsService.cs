// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NugetCommandsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Interfaces;
    using NinjaCoder.MvvmCross.Constants;
    using NinjaCoder.MvvmCross.Entities;
    using Scorchio.Infrastructure.Constants;
    using System.Collections.Generic;
    using Scorchio.VisualStudio.Services;

    /// <summary>
    /// Defines the NugetCommandsService type.
    /// </summary>
    public class NugetCommandsService : INugetCommandsService
    {
        /// <summary>
        /// The nuget mvvm cross library.
        /// </summary>
        private const string NugetMvvmCrossPackage = "MvvmCross.HotTuna.MvvmCrossLibraries";

        /// <summary>
        /// The nuget scorchio MVVM cross mstest tests.
        /// </summary>
        private const string NugetScorchioMvvmCrossMsTestTests = "Scorchio.NinjaCoder.MvvmCross.MSTests.Tests";

        /// <summary>
        /// The nuget scorchio nunit tests.
        /// </summary>
        private const string NugetScorchioNUnitTests = "Scorchio.NinjaCoder.NUnit.Tests";

        /// <summary>
        /// The nuget scorchio mstest tests.
        /// </summary>
        private const string NugetScorchioMsTestTests = "Scorchio.NinjaCoder.MSTest.Tests";

        /// <summary>
        /// The nuget scorchio MVVM cross nunit tests.
        /// </summary>
        private const string NugetScorchioMvvmCrossNUnitTests = "Scorchio.NinjaCoder.MvvmCross.NUnit.Tests";

        /// <summary>
        /// The nuget scorchio no framework package
        /// </summary>
        private const string NugetScorchioNoFrameworkPackage = "Scorchio.NinjaCoder.NoFramework";

        /// <summary>
        /// The nuget scorchio no framework iOS classic package
        /// </summary>
        private const string NugetScorchioNoFrameworkiOSClassicPackage = "Scorchio.NinjaCoder.NoFramework.iOS.Classic";

        /// <summary>
        /// The nuget scorchio no framework iOS unified package
        /// </summary>
        private const string NugetScorchioNoFrameworkiOSUnifiedPackage = "Scorchio.NinjaCoder.NoFramework.iOS.Unified";
        
        /// <summary>
        /// The nuget scorchio xamarin forms core package.
        /// </summary>
        private const string NugetScorchioXamarinFormsCorePackage = "Scorchio.NinjaCoder.Xamarin.Forms.Core";

        /// <summary>
        /// The nuget scorchio xamarin forms package.
        /// </summary>
        private const string NugetScorchioXamarinFormsPackage = "Scorchio.NinjaCoder.Xamarin.Forms";

        /// <summary>
        /// The nuget scorchio xamarin formsi os classic package.
        /// </summary>
        private const string NugetScorchioXamarinFormsiOSClassicPackage = "Scorchio.NinjaCoder.Xamarin.Forms.iOS.Classic";

        /// <summary>
        /// The nuget scorchio xamarin formsi os unified package.
        /// </summary>
        private const string NugetScorchioXamarinFormsiOSUnifiedPackage = "Scorchio.NinjaCoder.Xamarin.Forms.iOS.Unified";
        
        /// <summary>
        /// The nuget scorchio MVVM crossi os classic package.
        /// </summary>
        private const string NugetScorchioMvvmCrossiOSClassicPackage = "Scorchio.NinjaCoder.MvvmCross.iOS.Classic";
        
        /// <summary>
        /// The nuget scorchio MVVM crossi os unified package.
        /// </summary>
        private const string NugetScorchioMvvmCrossiOSUnifiedPackage = "Scorchio.NinjaCoder.MvvmCross.iOS.Unified";

        /// <summary>
        /// The nuget scorchio MVVM cross WPF package.
        /// </summary>
        private const string NugetScorchioMvvmCrossWpfPackage = "Scorchio.NinjaCoder.MvvmCross.Wpf";

        /// <summary>
        /// The nuget moq package.
        /// </summary>
        private const string NugetMoqPackage = "Moq";

        /// <summary>
        /// The nuget rhino mocks package.
        /// </summary>
        private const string NugetRhinoMocksPackage = "RhinoMocks";

        /// <summary>
        /// The nuget n substitute package.
        /// </summary>
        private const string NugetNSubstitutePackage = "NSubstitute";

        /// <summary>
        /// The nuget xamarin forms package
        /// </summary>
        private const string NugetXamarinFormsPackage = "Xamarin.Forms";
        
        /// <summary>
        /// The nuget xamarin android package.
        /// </summary>
        private const string NugetXamarinAndroidPackage = "Xamarin.Android.Support.v4";

        /// <summary>
        /// The nuget xamarin forms WPF package.
        /// </summary>
        private const string NugetXamarinFormsWpfPackage = "Xamarin.Forms.Platform.WPF";

        /// <summary>
        /// The nuget scorchio xamarin forms WPF package.
        /// </summary>
        private const string NugetScorchioXamarinFormsWpfPackage = "Scorchio.NinjaCoder.Xamarin.Forms.Wpf";

        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;
  
        /// <summary>
        /// Initializes a new instance of the <see cref="NugetCommandsService" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public NugetCommandsService(ISettingsService settingsService)
        {
            TraceService.WriteLine("NugetCommandsService::Constructor");

            this.settingsService = settingsService;
        }

        /// <summary>
        /// Gets the test commands.
        /// </summary>
        public IEnumerable<string> GetTestCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetTestCommands");

            List<string> commands = new List<string>();

            //// add the mocking framework.
            switch (this.settingsService.MockingFramework)
            {
                case TestingConstants.RhinoMocks.Name:
                    commands.Add(Settings.NugetInstallPackage.Replace("%s", NugetRhinoMocksPackage));
                    break;

                case TestingConstants.NSubstitute.Name:
                    commands.Add(Settings.NugetInstallPackage.Replace("%s", NugetNSubstitutePackage));
                    break;

                default:
                    commands.Add(Settings.NugetInstallPackage.Replace("%s", NugetMoqPackage));
                    break;
            }

            switch (this.settingsService.FrameworkType)
            {
                case FrameworkType.MvvmCross:
                case FrameworkType.MvvmCrossAndXamarinForms:
                    break;

                case FrameworkType.XamarinForms:
                    break;
            }

            commands.Add(
                this.settingsService.TestingFramework == TestingConstants.NUnit.Name
                    ? Settings.NugetInstallPackage.Replace("%s", NugetScorchioNUnitTests)
                    : Settings.NugetInstallPackage.Replace("%s", NugetScorchioMsTestTests));

            return commands;
        }

        /// <summary>
        /// Gets the MVVM cross core cross commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossCoreCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetMvvmCrossCoreCommands");

            return new List<string> 
                {
                    this.GetMvvmCrossCommand(NugetMvvmCrossPackage)
                };
        }

        /// <summary>
        /// Gets the MVVM cross tests commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossTestsCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetMvvmCrossTestsCommands");

            string testingFrameworkNugetPackage = NugetScorchioMvvmCrossMsTestTests;

            if (this.settingsService.TestingFramework == TestingConstants.NUnit.Name)
            {
                testingFrameworkNugetPackage = NugetScorchioMvvmCrossNUnitTests;
            }

            List<string> commands = new List<string>
                                        {
                                            Settings.NugetInstallPackage.Replace("%s", testingFrameworkNugetPackage)
                                        };

            IEnumerable<string> testCommands = this.GetTestCommands();

            commands.AddRange(testCommands);

            return commands;
        }

        /// <summary>
        /// Gets the MVVM cross droid commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossDroidCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetMvvmCrossDroidCommands");

            return new List<string> 
                {
                    this.GetMvvmCrossCommand(NugetMvvmCrossPackage),
                    Settings.NugetInstallPackage.Replace("%s", NugetXamarinAndroidPackage)
                };
        }

        /// <summary>
        /// Gets the MVVM cross ios commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossiOSCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetMvvmCrossiOSCommands");

            if (this.settingsService.iOSApiVersion == "Classic")
            {
                return new List<string>
                           {
                                this.GetMvvmCrossCommand(NugetMvvmCrossPackage),
                               Settings.NugetInstallPackage.Replace("%s", NugetScorchioMvvmCrossiOSClassicPackage)
                           };
            }

            return new List<string>
                           {
                                this.GetMvvmCrossCommand(NugetMvvmCrossPackage),
                               Settings.NugetInstallPackage.Replace("%s", NugetScorchioMvvmCrossiOSUnifiedPackage)
                           };
        }

        /// <summary>
        /// Gets the MVVM cross windows phone commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossWindowsPhoneCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetMvvmCrossWindowsPhoneCommands");

            return new List<string> 
                {
                    this.GetMvvmCrossCommand(NugetMvvmCrossPackage)
                };
        }

        /// <summary>
        /// Gets the MVVM cross windows store commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossWindowsStoreCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetMvvmCrossWindowsStoreCommands");

            return new List<string> 
                {
                    this.GetMvvmCrossCommand(NugetMvvmCrossPackage)
                };
        }

        /// <summary>
        /// Gets the MVVM cross WPF commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossWpfCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetMvvmCrossWpfCommands");

            return new List<string> 
                {
                    this.GetMvvmCrossCommand(NugetMvvmCrossPackage),
                    Settings.NugetInstallPackageOverwriteFiles.Replace("%s", NugetScorchioMvvmCrossWpfPackage)
                };
        }

        /// <summary>
        /// Gets the xamarin forms commands.
        /// </summary>
        public IEnumerable<string> GetNoFrameworksCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetNoFrameworksCommands");

            return new List<string> 
                {
                    Settings.NugetInstallPackage.Replace("%s", NugetScorchioNoFrameworkPackage)
                };
        }

        /// <summary>
        /// Gets the no frameworksi os commands.
        /// </summary>
        public IEnumerable<string> GetNoFrameworksiOSCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetNoFrameworksiOSCommands");

            if (this.settingsService.iOSApiVersion == "Classic")
            {
                return new List<string>
                           {
                               Settings.NugetInstallPackage.Replace("%s", NugetScorchioNoFrameworkiOSClassicPackage)
                           };
            }

            return new List<string>
                           {
                               Settings.NugetInstallPackage.Replace("%s", NugetScorchioNoFrameworkiOSUnifiedPackage)
                           };
        }

        /// <summary>
        /// Gets the xamarin forms core commands.
        /// </summary>
        public IEnumerable<string> GetXamarinFormsCoreCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetXamarinFormsCoreCommands");

            return new List<string> 
                {
                    Settings.NugetInstallPackage.Replace("%s", NugetScorchioXamarinFormsCorePackage)
                };
        }

        /// <summary>
        /// Gets the xamarin forms commands.
        /// </summary>
        public IEnumerable<string> GetXamarinFormsCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetXamarinFormsCommands");

            return new List<string> 
                {
                    this.GetXamarinFormsCommand(NugetXamarinFormsPackage),
                    Settings.NugetInstallPackage.Replace("%s", NugetScorchioXamarinFormsPackage)
                };
        }

        /// <summary>
        /// Gets the xamarin formsi os commands.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetXamarinFormsiOSCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetXamarinFormsiOSCommands");

            if (this.settingsService.iOSApiVersion == "Classic")
            {
                return new List<string>
                           {
                               this.GetXamarinFormsCommand(NugetXamarinFormsPackage),
                               Settings.NugetInstallPackage.Replace("%s", NugetScorchioXamarinFormsiOSClassicPackage)
                           };
            }

            return new List<string>
                           {
                               this.GetXamarinFormsCommand(NugetXamarinFormsPackage),
                               Settings.NugetInstallPackage.Replace("%s", NugetScorchioXamarinFormsiOSUnifiedPackage)
                           };
        }

        /// <summary>
        /// Gets the xamarin forms WPF commands.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetXamarinFormsWpfCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetXamarinFormsWpfCommands");

            return new List<string> 
                {
                    this.GetXamarinFormsCommand(NugetXamarinFormsPackage),
                    Settings.NugetInstallPackageOverwriteFiles.Replace("%s", NugetScorchioXamarinFormsWpfPackage),
                    Settings.NugetInstallPackage.Replace("%s", NugetXamarinFormsWpfPackage)
                };
        }
        
        /// <summary>
        /// Gets the MVVM cross xamarin form droid commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossXamarinFormDroidCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetMvvmCrossXamarinFormDroidCommands");

            return new List<string> 
                {
                    this.GetMvvmCrossCommand(NugetMvvmCrossPackage),
                    Settings.NugetInstallPackage.Replace("%s", NugetScorchioXamarinFormsPackage)
                };
        }

        /// <summary>
        /// Gets the MVVM cross xamarin formsi os commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossXamarinFormsiOSCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetMvvmCrossXamarinFormsiOSCommands");

            return new List<string> 
                {
                    this.GetMvvmCrossCommand(NugetMvvmCrossPackage),
                    Settings.NugetInstallPackage.Replace("%s", NugetScorchioXamarinFormsPackage)
                };
        }

        /// <summary>
        /// Gets the MVVM cross xamarin forms windows phone commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossXamarinFormsWindowsPhoneCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetMvvmCrossXamarinFormsWindowsPhoneCommands");

            return new List<string> 
                {
                    this.GetMvvmCrossCommand(NugetMvvmCrossPackage),
                    Settings.NugetInstallPackage.Replace("%s", NugetScorchioXamarinFormsPackage),
                };
        }

        /// <summary>
        /// Gets the xamarin android commands.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetXamarinAndroidCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetXamarinAndroidCommands");

            return new List<string> 
                {
                    Settings.NugetInstallPackage.Replace("%s", NugetXamarinAndroidPackage),
                };
        }

        /// <summary>
        /// Gets the MVVM cross command.
        /// </summary>
        /// <returns></returns>
        internal string GetMvvmCrossCommand(string command)
        {
            TraceService.WriteLine("NugetCommandsService::GetMvvmCrossCommand");

            if (this.settingsService.UsePreReleaseMvvmCrossNugetPackages)
            {
                command += Settings.NugetIncludePreRelease;
            }

            return Settings.NugetInstallPackage.Replace("%s", command);
        }

        /// <summary>
        /// Gets the xamarin forms command.
        /// </summary>
        /// <returns></returns>
        internal string GetXamarinFormsCommand(string command)
        {
            TraceService.WriteLine("NugetCommandsService::GetXamarinFormsCommand");

            if (this.settingsService.UsePreReleaseXamarinFormsNugetPackages)
            {
                command += Settings.NugetIncludePreRelease;
            }

            return Settings.NugetInstallPackage.Replace("%s", command);
        }
    }
}
