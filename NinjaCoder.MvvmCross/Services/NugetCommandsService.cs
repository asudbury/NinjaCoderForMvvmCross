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
    using System.Linq;

    using Scorchio.VisualStudio.Services;

    /// <summary>
    /// Defines the NugetCommandsService type.
    /// </summary>
    public class NugetCommandsService : INugetCommandsService
    {
        /// <summary>
        /// The mvvm cross library.
        /// </summary>
        private const string MvvmCrossPackage = "MvvmCross.HotTuna.MvvmCrossLibraries";

        /// <summary>
        /// The scorchio MVVM cross mstest tests.
        /// </summary>
        private const string ScorchioMvvmCrossMsTestTests = "Scorchio.NinjaCoder.MvvmCross.MSTests.Tests";

        /// <summary>
        /// The scorchio nunit tests.
        /// </summary>
        private const string ScorchioNUnitTests = "Scorchio.NinjaCoder.NUnit.Tests";

        /// <summary>
        /// The scorchio mstest tests.
        /// </summary>
        private const string ScorchioMsTestTests = "Scorchio.NinjaCoder.MSTest.Tests";

        /// <summary>
        /// The scorchio xunit tests.
        /// </summary>
        private const string ScorchioXUnitTests = "Scorchio.NinjaCoder.XUnit.Tests";

        /// <summary>
        /// The scorchio MVVM cross.
        /// </summary>
        private const string ScorchioMvvmCrossPackage = "Scorchio.NinjaCoder.MvvmCross";

        /// <summary>
        /// The scorchio MVVM cross nunit tests.
        /// </summary>
        private const string ScorchioMvvmCrossNUnitTests = "Scorchio.NinjaCoder.MvvmCross.NUnit.Tests";

        /// <summary>
        /// The scorchio no framework package
        /// </summary>
        private const string ScorchioNoFrameworkPackage = "Scorchio.NinjaCoder.NoFramework";

        /// <summary>
        /// The scorchio xamarin forms core package.
        /// </summary>
        private const string ScorchioXamarinFormsCorePackage = "Scorchio.NinjaCoder.Xamarin.Forms.Core";

        /// <summary>
        /// The scorchio xamarin forms package.
        /// </summary>
        private const string ScorchioXamarinFormsPackage = "Scorchio.NinjaCoder.Xamarin.Forms";

        /// <summary>
        /// The scorchio MVVM cross WPF package.
        /// </summary>
        private const string ScorchioMvvmCrossWpfPackage = "Scorchio.NinjaCoder.MvvmCross.Wpf";

        /// <summary>
        /// The moq package.
        /// </summary>
        private const string MoqPackage = "Moq";

        /// <summary>
        /// The rhino mocks package.
        /// </summary>
        private const string RhinoMocksPackage = "RhinoMocks";

        /// <summary>
        /// The nsubstitute package.
        /// </summary>
        private const string NSubstitutePackage = "NSubstitute";

        /// <summary>
        /// The xamarin forms package
        /// </summary>
        private const string XamarinFormsPackage = "Xamarin.Forms";
        
        /// <summary>
        /// The xamarin android package.
        /// </summary>
        private const string XamarinAndroidPackage = "Xamarin.Android.Support.v4";

        /// <summary>
        /// The xamarin forms WPF package.
        /// </summary>
        private const string XamarinFormsWpfPackage = "Xamarin.Forms.Platform.WPF";

        /// <summary>
        /// The scorchio xamarin forms WPF package.
        /// </summary>
        private const string ScorchioXamarinFormsWpfPackage = "Scorchio.NinjaCoder.Xamarin.Forms.Wpf";

        /// <summary>
        /// The MVVM cross xamrin forms.
        /// </summary>
        private const string ScorchioMvvmCrossXamrinForms = "Scorchio.NinjaCoder.MvvmCross.Xamarin.Forms";

        /// <summary>
        /// The MVVM cross xamrin forms core.
        /// </summary>
        private const string ScorchioMvvmCrossXamrinFormsCore = "Scorchio.NinjaCoder.MvvmCross.Xamarin.Forms.Core";

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
                    commands.Add(Settings.NugetInstallPackage.Replace("%s", RhinoMocksPackage));
                    break;

                case TestingConstants.NSubstitute.Name:
                    commands.Add(Settings.NugetInstallPackage.Replace("%s", NSubstitutePackage));
                    break;

                default:
                    commands.Add(Settings.NugetInstallPackage.Replace("%s", MoqPackage));
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

            string nugetPackageName;

            switch (this.settingsService.TestingFramework)
            {
                case TestingConstants.NUnit.Name:
                    nugetPackageName = ScorchioNUnitTests;
                    break;

                case TestingConstants.XUnit.Name:
                    nugetPackageName = ScorchioXUnitTests;
                    break;

                default:
                    nugetPackageName = ScorchioMsTestTests;
                    break;
            }

            commands.Add(this.GetNinjaCommand(nugetPackageName, false));

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
                    this.GetMvvmCrossCommand(MvvmCrossPackage)
                };
        }

        /// <summary>
        /// Gets the MVVM cross tests commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossTestsCommands()
        {
            TraceService.WriteLine("NugetCommandsService::GetMvvmCrossTestsCommands");

            string testingFrameworkNugetPackage = ScorchioMvvmCrossMsTestTests;

            if (this.settingsService.TestingFramework == TestingConstants.NUnit.Name)
            {
                testingFrameworkNugetPackage = ScorchioMvvmCrossNUnitTests;
            }

            List<string> commands = new List<string>
            {
                this.GetNinjaCommand(testingFrameworkNugetPackage, false)
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
            return new List<string> 
            {
                this.GetMvvmCrossCommand(MvvmCrossPackage),
                Settings.NugetInstallPackage.Replace("%s", XamarinAndroidPackage)
            };
        }

        /// <summary>
        /// Gets the MVVM cross ios commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossiOSCommands()
        {
            return new List<string>
            {
                this.GetMvvmCrossCommand(MvvmCrossPackage),
                this.GetNinjaCommand(ScorchioMvvmCrossPackage, false)
            };
        }

        /// <summary>
        /// Gets the MVVM cross windows phone commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossWindowsPhoneCommands()
        {
            return new List<string> 
            {
                this.GetMvvmCrossCommand(MvvmCrossPackage)
            };
        }

        /// <summary>
        /// Gets the MVVM cross windows store commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossWindowsStoreCommands()
        {
            return new List<string> 
            {
                this.GetMvvmCrossCommand(MvvmCrossPackage)
            };
        }

        /// <summary>
        /// Gets the MVVM cross WPF commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossWpfCommands()
        {
            return new List<string> 
            {
                this.GetMvvmCrossCommand(MvvmCrossPackage),
                this.GetNinjaCommand(ScorchioMvvmCrossWpfPackage, true)
            };
        }

        /// <summary>
        /// Gets the xamarin forms commands.
        /// </summary>
        public IEnumerable<string> GetNoFrameworksCommands()
        {
            return new List<string> 
            {
                this.GetNinjaCommand(ScorchioNoFrameworkPackage, false)
            };
        }

        /// <summary>
        /// Gets the no frameworksi os commands.
        /// </summary>
        public IEnumerable<string> GetNoFrameworksiOSCommands()
        {
            return new List<string>
            {
                this.GetNinjaCommand(ScorchioNoFrameworkPackage, false)
            };
        }

        /// <summary>
        /// Gets the xamarin forms core commands.
        /// </summary>
        public IEnumerable<string> GetXamarinFormsCoreCommands()
        {
            return new List<string> 
            {
                this.GetNinjaCommand(ScorchioXamarinFormsCorePackage, false)
            };
        }

        /// <summary>
        /// Gets the xamarin forms commands.
        /// </summary>
        public IEnumerable<string> GetXamarinFormsCommands()
        {
            return new List<string> 
            {
                this.GetXamarinFormsCommand(XamarinFormsPackage),
                this.GetNinjaCommand(ScorchioXamarinFormsPackage, false)
            };
        }

        /// <summary>
        /// Gets the xamarin formsi os commands.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetXamarinFormsiOSCommands()
        {
            return new List<string>
            {
                this.GetXamarinFormsCommand(XamarinFormsPackage),
                this.GetNinjaCommand(ScorchioXamarinFormsPackage, true)
            };
        }

        /// <summary>
        /// Gets the xamarin forms WPF commands.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetXamarinFormsWpfCommands()
        {
            return new List<string> 
            {
                this.GetXamarinFormsCommand(XamarinFormsPackage),

                this.GetNinjaCommand(ScorchioXamarinFormsWpfPackage, true),

                Settings.NugetInstallPackage.Replace("%s", XamarinFormsWpfPackage)
            };
        }

        /// <summary>
        /// Gets the MVVM cross xamarin forms core commands.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetMvvmCrossXamarinFormsCoreCommands()
        {
            List<string> commands = this.GetMvvmCrossCoreCommands().ToList();

            commands.Add(this.GetNinjaCommand(ScorchioMvvmCrossXamrinFormsCore, true));

            return commands;
        }

        /// <summary>
        /// Gets the MVVM cross xamarin forms commands.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetMvvmCrossXamarinFormsCommands()
        {
            List<string> commands = this.GetXamarinFormsCommands().ToList();

            commands.Add(this.GetNinjaCommand(ScorchioMvvmCrossXamrinForms, true));

            return commands;
        }

        /// <summary>
        /// Gets the MVVM cross xamarin form droid commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossXamarinFormDroidCommands()
        {
            List<string> commands = this.GetMvvmCrossDroidCommands().ToList();

            commands.AddRange(this.GetXamarinFormsCommands());

            commands.Add(this.GetNinjaCommand(ScorchioMvvmCrossXamrinForms, true));

            return commands;
        }

        /// <summary>
        /// Gets the MVVM cross xamarin formsi os commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossXamarinFormsiOSCommands()
        {
            List<string> commands = this.GetMvvmCrossiOSCommands().ToList();

            commands.AddRange(this.GetXamarinFormsiOSCommands());

            commands.Add(this.GetNinjaCommand(ScorchioMvvmCrossXamrinForms, true));

            return commands;
        }

        /// <summary>
        /// Gets the MVVM cross xamarin forms windows phone commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossXamarinFormsWindowsPhoneCommands()
        {
            List<string> commands = this.GetMvvmCrossWindowsPhoneCommands().ToList();

            commands.AddRange(this.GetXamarinFormsCommands());
            
            commands.Add(this.GetNinjaCommand(ScorchioMvvmCrossXamrinForms, true));

            return commands;
        }

        /// <summary>
        /// Gets the xamarin android commands.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<string> GetXamarinAndroidCommands()
        {
            return new List<string> 
            {
                Settings.NugetInstallPackage.Replace("%s", XamarinAndroidPackage),
            };
        }

        /// <summary>
        /// Gets the MVVM cross command.
        /// </summary>
        /// <returns></returns>
        internal string GetMvvmCrossCommand(string command)
        {
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
            if (this.settingsService.UsePreReleaseXamarinFormsNugetPackages)
            {
                command += Settings.NugetIncludePreRelease;
            }

            return Settings.NugetInstallPackage.Replace("%s", command);
        }

        /// <summary>
        /// Gets the ninja command.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="overWrite">if set to <c>true</c> [over write].</param>
        /// <returns></returns>
        internal string GetNinjaCommand(
            string command,
            bool overWrite)
        {
            if (this.settingsService.UsePreReleaseNinjaNugetPackages)
            {
                command += Settings.NugetIncludePreRelease;
            }

            return overWrite ? Settings.NugetInstallPackageOverwriteFiles.Replace("%s", command) : 
                               Settings.NugetInstallPackage.Replace("%s", command);
        }
    }
}
