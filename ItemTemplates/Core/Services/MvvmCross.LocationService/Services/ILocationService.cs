// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the ILocationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.LocationService.Services
{
    /// <summary>
    /// Defines the ILocationService type.
    /// </summary>
    public interface ILocationService
    {
        /// <summary>
        /// Tries the get latest location.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns>
        /// true or false.
        /// </returns>
        bool TryGetLatestLocation(
            out double latitude, 
            out double longitude);
    }
}
