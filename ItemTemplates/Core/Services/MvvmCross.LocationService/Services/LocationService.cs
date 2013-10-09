// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the LocationService type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.LocationService.Services
{
    using Cirrious.CrossCore;
    using Cirrious.MvvmCross.Plugins.Location;
    using Cirrious.MvvmCross.Plugins.Messenger;

    using MvvmCross.LocationService.Entities;

    /// <summary>
    /// Defines the LocationService type.
    /// </summary>
    public class LocationService : ILocationService
    {
        /// <summary>
        /// The watcher
        /// </summary>
        private readonly IMvxGeoLocationWatcher watcher;

        /// <summary>
        /// The messenger
        /// </summary>
        private readonly IMvxMessenger messenger;

        /// <summary>
        /// The lock object
        /// </summary>
        private readonly object lockObject = new object();

        /// <summary>
        /// The latest location
        /// </summary>
        private MvxGeoLocation latestLocation;

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationService"/> class.
        /// </summary>
        /// <param name="watcher">The watcher.</param>
        /// <param name="messenger">The messenger.</param>
        public LocationService(
            IMvxGeoLocationWatcher watcher, 
            IMvxMessenger messenger)
        {
            this.messenger = messenger;

            this.watcher = watcher;
            this.watcher.Start(new MvxGeoLocationOptions(), this.OnSuccess, this.OnError);
        }

        /// <summary>
        /// Tries the get latest location.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        /// <returns>true or false.</returns>
        public bool TryGetLatestLocation(
            out double latitude, 
            out double longitude)
        {
            lock (this.lockObject)
            {
                if (this.latestLocation == null)
                {
                    latitude = longitude = 0;
                    return false;
                }

                latitude = this.latestLocation.Coordinates.Latitude;
                longitude = this.latestLocation.Coordinates.Longitude;
                return true;
            }
        }

        /// <summary>
        /// Called when [success].
        /// </summary>
        /// <param name="location">The location.</param>
        private void OnSuccess(MvxGeoLocation location)
        {
            lock (this.lockObject)
            {
                this.latestLocation = location;
            }

            LocationMessage message = new LocationMessage(
                this, 
                location.Coordinates.Latitude, 
                location.Coordinates.Longitude);

            this.messenger.Publish(message);
        }

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="error">The error.</param>
        private void OnError(MvxLocationError error)
        {
            Mvx.Warning("Error seen during location {0}", error.Code);
        }
    }
}
