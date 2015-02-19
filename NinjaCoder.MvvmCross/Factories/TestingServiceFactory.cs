// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the TestingServiceFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using Interfaces;

    using NinjaCoder.MvvmCross.Services.Interfaces;

    using Scorchio.Infrastructure.Constants;
    using Scorchio.Infrastructure.Services.Testing;
    using Scorchio.Infrastructure.Services.Testing.Interfaces;
    using Scorchio.VisualStudio.Services;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the TestingServiceFactory type.
    /// </summary>
    public class TestingServiceFactory : ITestingServiceFactory
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestingServiceFactory" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public TestingServiceFactory(ISettingsService settingsService)
        {
            TraceService.WriteLine("TestingServiceFactory::Constructor");

            this.settingsService = settingsService;
        }

        /// <summary>
        /// Gets or sets the current frame work.
        /// </summary>
        public string CurrentFrameWork
        {
            get { return this.settingsService.TestingFramework; }
            set { this.settingsService.TestingFramework = value; }
        }


        /// <summary>
        /// Gets the frame works.
        /// </summary>
        public IEnumerable<string> FrameWorks
        {
            get
            {
                return new List<string>
                       {
                           TestingConstants.NUnit.Name, 
                           TestingConstants.MsTest.Name,
                           TestingConstants.XUnit.Name
                       };
            }
        }

        /// <summary>
        /// Gets the testing service.
        /// </summary>
        /// <returns>The testing service.</returns>
        public ITestingService GetTestingService()
        {
            TraceService.WriteLine("TestingServiceFactory::GetTestingService");

            if (this.settingsService.TestingFramework == TestingConstants.MsTest.Name)
            {
                return new MsTestTestingService();
            }

            return new NUnitTestingService();
        }
    }
}
