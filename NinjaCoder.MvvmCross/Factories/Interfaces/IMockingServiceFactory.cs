// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the IMockingServiceFactory type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace NinjaCoder.MvvmCross.Factories.Interfaces
{
    using System.Collections.Generic;

    using Scorchio.Infrastructure.Services.Testing;
    using Scorchio.Infrastructure.Services.Testing.Interfaces;

    /// <summary>
    ///  Defines the IMockingServiceFactory type.
    /// </summary>
    public interface IMockingServiceFactory
    {
        /// <summary>
        /// Gets or sets the get mocking frame work.
        /// </summary>
        string CurrentFrameWork { get; set; }

        /// <summary>
        /// Gets the mocking frame works.
        /// </summary>
        IEnumerable<string> FrameWorks { get;  }

        /// <summary>
        /// Gets the mocking service.
        /// </summary>
        /// <returns>The mocking service.</returns>
        IMockingService GetMockingService();
    }
}