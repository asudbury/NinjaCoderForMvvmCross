// --------------------------------------------------------------------------------------------------------------------
// <summary>
//  Defines the TestingFrameworkFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories
{
    using Interfaces;
    using Scorchio.Infrastructure.Constants;
    using Services.Interfaces;

    /// <summary>
    ///  Defines the TestingFrameworkFactory type.
    /// </summary>
    public class TestingFrameworkFactory : ITestingFrameworkFactory
    {
        /// <summary>
        /// The settings service.
        /// </summary>
        private readonly ISettingsService settingsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="TestingFrameworkFactory"/> class.
        /// </summary>
        /// <param name="settingsService">The settings service.</param>
        public TestingFrameworkFactory(ISettingsService settingsService)
        {
            this.settingsService = settingsService;
        }

        /// <summary>
        /// Gets the testing class attribute.
        /// </summary>
        /// <returns>The testing class attribute.</returns>
        public string GetTestingClassAttribute()
        {
            switch (this.settingsService.TestingFramework)
            {
                case TestingConstants.MsTest.Name:
                    return TestingConstants.MsTest.ClassAttribute;

                case TestingConstants.XUnit.Name:
                    return TestingConstants.XUnit.ClassAttribute;

                default:
                    return TestingConstants.NUnit.ClassAttribute;
            }
        }

        /// <summary>
        /// Gets the testing method attribute.
        /// </summary>
        /// <returns>The name of the testing attribute to use.</returns>
        public string GetTestingMethodAttribute()
        {
            switch (this.settingsService.TestingFramework)
            {
                case TestingConstants.MsTest.Name:
                    return TestingConstants.MsTest.MethodAttribute;

                case TestingConstants.XUnit.Name:
                    return TestingConstants.XUnit.MethodAttribute;

                default:
                    return TestingConstants.NUnit.MethodAttribute;
            }
        }
        
        /// <summary>
        /// Gets the testing library.
        /// </summary>
        /// <returns>The name of the testing library.</returns>
        public string GetTestingLibrary()
        {
            switch (this.settingsService.TestingFramework)
            {
                case TestingConstants.MsTest.Name:
                    return TestingConstants.MsTest.Library;

                case TestingConstants.XUnit.Name:
                    return TestingConstants.XUnit.Library;

                default:
                    return TestingConstants.NUnit.Library;
            }
        }
    }
}
