// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ITestingServiceFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using System.Collections.Generic;

    using Scorchio.Infrastructure.Services.Testing.Interfaces;

    /// <summary>
    ///  Defines the ITestingServiceFactory type.
    /// </summary>
    public interface ITestingServiceFactory
    {
        /// <summary>
        /// Gets or sets the current frame work.
        /// </summary>
        string CurrentFrameWork { get; set; }

        /// <summary>
        /// Gets the frame works.
        /// </summary>
        IEnumerable<string> FrameWorks { get; }

        /// <summary>
        /// Gets the testing service.
        /// </summary>
        /// <returns>The testing service.</returns>
        ITestingService GetTestingService();
    }
}