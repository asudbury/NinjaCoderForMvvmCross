// --------------------------------------------------------------------------------------------------------------------
// <summary>
//    Defines the LocationMessage type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace MvvmCross.LocationService.Entities
{
    using Cirrious.MvvmCross.Plugins.Messenger;

    /// <summary>
    /// Defines the LocationMessage type.
    /// </summary>
    public class LocationMessage : MvxMessage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationMessage" /> class.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        public LocationMessage(
            object sender,
            double latitude,
            double longitude)
            : base(sender)
        {
            this.Latitude = latitude;
            this.Longitude = longitude;
        }

        /// <summary>
        /// Gets the latitude.
        /// </summary>
        public double Latitude { get; private set; }

        /// <summary>
        /// Gets the longitude.
        /// </summary>
        public double Longitude { get; private set; }
    }
}