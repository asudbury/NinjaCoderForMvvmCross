// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the MockingServiceFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using Interfaces;
    using Scorchio.Infrastructure.Constants;
    using Scorchio.Infrastructure.Services.Testing;
    using Scorchio.Infrastructure.Services.Testing.Interfaces;
    using Scorchio.VisualStudio.Services;
    using Services.Interfaces;
    using System.Collections.Generic;

    /// <summary>
    ///  Defines the MockingServiceFactory type.
    /// </summary>
    public class MockingServiceFactory : IMockingServiceFactory
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="MockingServiceFactory" /> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public MockingServiceFactory(ISettingsService settingsService)
        {
            TraceService.WriteLine("MockingServiceFactory::Constructor");

            this.settingsService = settingsService;
        }

        /// <summary>
        /// Gets or sets the get mocking frame work.
        /// </summary>
        public string CurrentFrameWork
        {
            get { return this.settingsService.MockingFramework; }
            set { this.settingsService.MockingFramework = value; }
        }

        /// <summary>
        /// Gets the mocking frame works.
        /// </summary>
        public IEnumerable<string> FrameWorks
        {
            get
            {
                return new List<string>
                {
                    TestingConstants.Moq.Name,
                    TestingConstants.RhinoMocks.Name,
                    TestingConstants.NSubstitute.Name
                };
            }
        }

        /// <summary>
        /// Gets the mocking service.
        /// </summary>
        /// <returns>The mocking service.</returns>
        public IMockingService GetMockingService()
        {
            TraceService.WriteLine("MockingServiceFactory::GetMockingService");

            switch (this.settingsService.MockingFramework)
            {
                case TestingConstants.RhinoMocks.Name:
                    return new RhinoMocksMockingService();

                case TestingConstants.NSubstitute.Name:
                    return new NSubstituteMockingService();
            }

            return new MoqMockingService();
        }
    }
}
