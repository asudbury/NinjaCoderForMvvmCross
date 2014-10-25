// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the NugetCommandsService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Services
{
    using Interfaces;
    using Scorchio.Infrastructure.Constants;
    using System.Collections.Generic;

    using Scorchio.VisualStudio.Services;

    /// <summary>
    /// Defines the NugetCommandsService type.
    /// </summary>
    public class NugetCommandsService : INugetCommandsService
    {
        /// <summary>
        /// The nuget unit tests package.
        /// </summary>
        private const string NugetUnitTestsPackage = "MvvmCross.HotTuna.Tests";

        /// <summary>
        /// The nuget mvvm cross library.
        /// </summary>
        private const string NugetMvvmCrossPackage = "MvvmCross.HotTuna.MvvmCrossLibraries";

        /// <summary>
        /// The nuget xamarin forms package.
        /// </summary>
        private const string NugetXamarinFormsPackage = "Xamarin.Forms";

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
        /// The nuget nunit package.
        /// </summary>
        private const string NugetNUnitPackage = "NUnit";
        
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="NugetCommandsService"/> class.
        /// </summary>
        public NugetCommandsService(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        /// <summary>
        /// The nuget install package.
        /// </summary>
        public const string NugetInstallPackage = "Install-Package %s -FileConflictAction ignore -ProjectName";

        /// <summary>
        /// Gets the MVVM cross core cross commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossCoreCommands()
        {
            return new List<string> 
                {
                    NugetInstallPackage.Replace("%s", NugetMvvmCrossPackage)
                };
        }

        /// <summary>
        /// Gets the core tests commands.
        /// </summary>
        public IEnumerable<string> GetCoreTestsCommands()
        {
            List<string> commands = new List<string>
                                        {
                                            NugetInstallPackage.Replace("%s", NugetUnitTestsPackage),
                                            NugetInstallPackage.Replace("%s", NugetMvvmCrossPackage)
                                        };

            //// add the mocking framework.
            switch (this.settingsService.MockingFramework)
            {
                case TestingConstants.RhinoMocks.Name:
                    commands.Add(NugetInstallPackage.Replace("%s", NugetRhinoMocksPackage));
                    break;

                case TestingConstants.NSubstitute.Name:
                    commands.Add(NugetInstallPackage.Replace("%s", NugetNSubstitutePackage));
                    break;

                default:
                    commands.Add(NugetInstallPackage.Replace("%s", NugetMoqPackage));
                    break;
            }

            if (this.settingsService.TestingFramework == TestingConstants.NUnit.Name)
            {
                commands.Add(NugetInstallPackage.Replace("%s", NugetNUnitPackage));
            }

            TraceService.WriteLine("GetCoreTestsCommands=" + commands);

            return commands;
        }

        /// <summary>
        /// Gets the MVVM cross droid commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossDroidCommands()
        {
            return new List<string> 
                {
                    NugetInstallPackage.Replace("%s", NugetMvvmCrossPackage)
                };
        }

        /// <summary>
        /// Gets the MVVM cross ios commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossiOSCommands()
        {
            return new List<string> 
                {
                    NugetInstallPackage.Replace("%s", NugetMvvmCrossPackage)
                };
        }

        /// <summary>
        /// Gets the MVVM cross windows phone commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossWindowsPhoneCommands()
        {
            return new List<string> 
                {
                    NugetInstallPackage.Replace("%s", NugetMvvmCrossPackage)
                };
        }

        /// <summary>
        /// Gets the MVVM cross windows store commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossWindowsStoreCommands()
        {
            return new List<string> 
                {
                    NugetInstallPackage.Replace("%s", NugetMvvmCrossPackage)
                };
        }

        /// <summary>
        /// Gets the MVVM cross WPF commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossWpfCommands()
        {
            return new List<string> 
                {
                    NugetInstallPackage.Replace("%s", NugetMvvmCrossPackage)
                };
        }

        /// <summary>
        /// Gets the xamarin forms commands.
        /// </summary>
        public IEnumerable<string> GetXamarinFormsCommands()
        {
            return new List<string> 
                {
                    NugetInstallPackage.Replace("%s", NugetXamarinFormsPackage)
                };
        }

        /// <summary>
        /// Gets the MVVM cross xamarin form droid commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossXamarinFormDroidCommands()
        {
            return new List<string> 
                {
                    NugetInstallPackage.Replace("%s", NugetMvvmCrossPackage),
                    NugetInstallPackage.Replace("%s", NugetXamarinFormsPackage)
                };
        }

        /// <summary>
        /// Gets the MVVM cross xamarin formsi os commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossXamarinFormsiOSCommands()
        {
            return new List<string> 
                {
                    NugetInstallPackage.Replace("%s", NugetMvvmCrossPackage),
                    NugetInstallPackage.Replace("%s", NugetXamarinFormsPackage)
                };
        }

        /// <summary>
        /// Gets the MVVM cross xamarin forms windows phone commands.
        /// </summary>
        public IEnumerable<string> GetMvvmCrossXamarinFormsWindowsPhoneCommands()
        {
            return new List<string> 
                {
                    NugetInstallPackage.Replace("%s", NugetMvvmCrossPackage),
                    NugetInstallPackage.Replace("%s", NugetXamarinFormsPackage),
                };
        }
    }
}
